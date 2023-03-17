using ExcelDataReader;
using PEMS.BusinessLayer.InspectionModel;
using PEMS.Repository.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEMS.WEBUI.Controllers
{
    public class InspectionController : Controller
    {
        private IInspectionService inspectionService;
        public InspectionController()
        {
            inspectionService = new InspectionService();
        }
        // GET: Inspection
        public ActionResult InspectionSearchList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetInspectionData()
        {
            InspectionViewModel model = new InspectionViewModel();
            List<InspectionViewModel> inspectionList = inspectionService.GetInspectionList(model);
            return Json(new { data = inspectionList }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ImportInspectionData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportInspectionData(HttpPostedFileBase file, InspectionViewModel model)
        {
            DataTable dt = new DataTable();
            string userId = (Session["UserID"]).ToString();
            int userID = Int32.Parse(userId);
            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/ExcelFiles/InspectionFilesUpload/";
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", ""));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                dt = ReadData(finalPath);
                int fileId = inspectionService.UploadInspectionFileInfo(file.FileName, model.StateId, model.CityId);
                if (fileId > 0)
                {
                    int a = inspectionService.UploadInspectionFromExcel(dt, userId, fileId);
                }
            }
            return RedirectToAction("InspectionSearchList");
        }

        public ActionResult GetInspectionUploadedFile()
        {
            var surveyFiles = inspectionService.GetInspectionUploadedFiles("");
            return Json(new { data = surveyFiles }, JsonRequestBehavior.AllowGet);
        }
        private DataTable ReadData(string finalPath)
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

    }
}