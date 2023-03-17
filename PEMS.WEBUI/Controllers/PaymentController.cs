using ExcelDataReader;
using PEMS.BusinessLayer.PaymentModel;
using PEMS.Repository.Payment;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEMS.WEBUI.Controllers
{
    public class PaymentController : Controller
    {
        private IPaymentService paymentService;
        public PaymentController()
        {
            paymentService = new PaymentService();
        }
        // GET: Payment
        public ActionResult PaymentSearchList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetPaymentData()
        {
            PaymentViewModel model = new PaymentViewModel();
            List<PaymentViewModel> paymentList = paymentService.GetPaymentList(model);
            return Json(new { data = paymentList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImportPaymentData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportPaymentData(HttpPostedFileBase file, PaymentViewModel model)
        {
            DataTable dt = new DataTable();
            string userId = (Session["UserID"]).ToString();
            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/ExcelFiles/PaymentFilesUpload/";
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", ""));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                dt = ReadData(finalPath);
                int fileId = paymentService.UploadPaymentFileInfo(file.FileName, model.StateId, model.CityId);
                if (fileId > 0)
                {
                    int a = paymentService.UploadPaymentFromExcel(dt, userId, fileId);
                }
            }
            return RedirectToAction("PaymentSearchList");
        }

        public ActionResult GetPaymentUploadedFile()
        {
            var paymentFiles = paymentService.GetPaymentUploadedFiles("");
            List<PaymentFilesViewModel> objPaymentFiles = new List<PaymentFilesViewModel>();
            if (paymentFiles.Count > 0)
            {
                foreach (var item in paymentFiles)
                {
                    var surveyFile = new PaymentFilesViewModel()
                    {
                        FileId = item.FileId,
                        CityId = 0,
                        FileName = item.FileName
                    };
                    objPaymentFiles.Add(surveyFile);
                }
            }
            return Json(new { data = objPaymentFiles }, JsonRequestBehavior.AllowGet);
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