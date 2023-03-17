using ExcelDataReader;
using PEMS.Repository.Survey;
using PEMS.BusinessLayer.SurveyModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEMS.WEBUI.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        private ISurveyService iSurveyService;
        public SurveyController()
        {
            iSurveyService = new SurveyService();
        }
    
        // GET: Survey
        public ActionResult SurveySearchList()
        {
            SurveyViewModel model = new SurveyViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult SurveySearchList(SurveyViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult GetSurveyData(string StateId, string CityId, string OwnerId, string OwnerFullName )
        {
            SurveyViewModel model = new SurveyViewModel();
            model.StateId = !string.IsNullOrEmpty(StateId) ? Int32.Parse(StateId) : 0;
            model.CityId = !string.IsNullOrEmpty(CityId) ? Int32.Parse(CityId) : 0;
            model.OwnerId = !string.IsNullOrEmpty(OwnerId) ? Int32.Parse(OwnerId) : 0;
            model.OwnerFullName = !string.IsNullOrEmpty(OwnerFullName) ? OwnerFullName : "";
            List<SurveyViewModel> surveyList = iSurveyService.GetSurveyList(model);
            return Json(new { data = surveyList }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ImportSurveyData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportSurveyData(HttpPostedFileBase file, SurveyViewModel model)
        {
            DataTable dt = new DataTable();
            string userId = (Session["UserID"]).ToString();
            int userID = Int32.Parse(userId);
            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/ExcelFiles/SurveyFilesUpload/";
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", ""));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                dt = ReadData(finalPath);
                int fileId = iSurveyService.UploadExcelFileInfo(file.FileName, model.StateId, model.CityId, userID);
                if (fileId > 0)
                {
                    int a = iSurveyService.UploadSurveyFromExcel(dt, userId, fileId);
                }
            }
            return RedirectToAction("SurveySearchList");
        }

        public ActionResult GetSurveyUploadedFile()
        {
            var surveyFiles = iSurveyService.GetSurveyUploadedFiles("");         
            return Json(new { data = surveyFiles }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SurveyEvaluation(string id2)
        {
            SurveyViewModel objSurvey = iSurveyService.GetSurveyByID(Int32.Parse(id2));
            return View(objSurvey);
        }
        [HttpPost]
        public ActionResult SurveyEvaluation(PEMS.BusinessLayer.SurveyModel.SurveyViewModel objModel)
        {
            int result = iSurveyService.UpdateSurveyEvaluation(objModel);
            if (result > 0)
                objModel.ReturnMessage = "Evaluated Successfully";
            return View(objModel);
        }
        private DataTable ReadData( string finalPath)
        {
            var filePath = finalPath;// System.Web.HttpContext.Current.Server.MapPath("~/ExcelFiles/SurveyUploadSample.xlsx");
            var dataTable = new DataTable();
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelReader;
                if (Path.GetExtension(filePath).ToUpper() == ".XLS")
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                var result = excelReader.AsDataSet();
                excelReader = ExcelReaderFactory.CreateReader(stream);

                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                var dataSet = excelReader.AsDataSet(conf);
                dataTable = dataSet.Tables[0];
                //dataTable.Rows.RemoveAt(0);
            }
            return dataTable;
        }
        public ActionResult EditSurveyData(string id2, string noise)
        {
            SurveyViewModel objSurvey = iSurveyService.GetSurveyByID(Int32.Parse(id2));
            return View(objSurvey);
        }
    }
}