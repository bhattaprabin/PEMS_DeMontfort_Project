using PEMS.BusinessLayer.InspectionModel;
using PEMS.BusinessLayer.PaymentModel;
using PEMS.BusinessLayer.ReportModel;
using PEMS.BusinessLayer.SurveyModel;
using PEMS.Repository.CommonService;
using PEMS.Repository.Inspection;
using PEMS.Repository.Payment;
using PEMS.Repository.Report;
using PEMS.Repository.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEMS.WEBUI.Controllers
{
    public class ReportController : Controller
    {
        private IReportService reportService;
        private ISurveyService surveyService;
        private IPaymentService paymentService;
        private IInspectionService inspectionService;
        private ICommonService commonService;
        public ReportController()
        {
            reportService = new ReportService();
            surveyService = new SurveyService();
            paymentService = new PaymentService();
            inspectionService = new InspectionService();
            commonService = new CommonService();
        }
        // GET: Report
        public ActionResult GetReportName(string id)
        {
            ReportViewModel reportModel = new ReportViewModel();
            reportModel.DetailReportPage = "_" + id + "DetailReport";
            reportModel.SummaryReportPage = "_" + id + "SummaryReport";
            reportModel.ParentPageName = id + " Report";
            reportModel.ReportFrom = id;
            reportModel.DetailReportTitle = id + " Detail Report";
            reportModel.SummaryReportTitle = id + " Summary Report";
            if (id == "Payment")
            {
                reportModel.ListItems = commonService.GetBanksinDropDown();
            }
            return View(reportModel);
        }
        [HttpPost]
        public ActionResult GetReportPage(ReportViewModel model)
        {
            if (model.ReportCategory == "Detail")
            {
                return View(model.DetailReportPage, model);
            }
            else
            {
                return View(model.SummaryReportPage, model);
            }

        }

        #region Survey Detail and Summary Report    
        [HttpGet]
        public ActionResult GetSurveyDetailData(string StateId, string CityId, string OwnerId, string OwnerFullName)
        {
            SurveyViewModel model = new SurveyViewModel();
            model.StateId = !string.IsNullOrEmpty(StateId) ? Int32.Parse(StateId) : 0;
            model.CityId = !string.IsNullOrEmpty(CityId) ? Int32.Parse(CityId) : 0;
            model.OwnerId = !string.IsNullOrEmpty(OwnerId) ? Int32.Parse(OwnerId) : 0;
            model.OwnerFullName = !string.IsNullOrEmpty(OwnerFullName) ? OwnerFullName : "";
            List<SurveyViewModel> surveyList = surveyService.GetSurveyList(model);
            return Json(new { data = surveyList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSurveySummaryData(string StateId, string CityId)
        {
            SurveyViewModel model = new SurveyViewModel();
            List<SurveySummaryReportViewModel> reportModel = new List<SurveySummaryReportViewModel>();
            model.StateId = !string.IsNullOrEmpty(StateId) ? Int32.Parse(StateId) : 0;
            model.CityId = !string.IsNullOrEmpty(CityId) ? Int32.Parse(CityId) : 0;
            reportModel = surveyService.GetSurveySummaryReport(model);
            return Json(new { data = reportModel }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Payment Detail and Summary Report    
        [HttpGet]
        public ActionResult GetPaymentDetailData(string StateId, string CityId, string OwnerId, string OwnerFullName, string BenId, string BankCode)
        {
            PaymentViewModel model = new PaymentViewModel();
            model.StateId = !string.IsNullOrEmpty(StateId) ? Int32.Parse(StateId) : 0;
            model.CityId = !string.IsNullOrEmpty(CityId) ? Int32.Parse(CityId) : 0;
            model.OwnerId = !string.IsNullOrEmpty(OwnerId) ? Int32.Parse(OwnerId) : 0;
            model.FullName = !string.IsNullOrEmpty(OwnerFullName) ? OwnerFullName : "";
            model.BankCode = !string.IsNullOrEmpty(BankCode) ? BankCode : "";
            model.BenId = !string.IsNullOrEmpty(BenId) ? BenId : "";

            List<PaymentViewModel> paymentList = paymentService.GetPaymentList(model);
            return Json(new { data = paymentList }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPaymentSummaryData(string StateId, string CityId)
        {
            PaymentViewModel model = new PaymentViewModel();
            model.StateId = !string.IsNullOrEmpty(StateId) ? Int32.Parse(StateId) : 0;
            model.CityId = !string.IsNullOrEmpty(CityId) ? Int32.Parse(CityId) : 0;
            List<PaymentSummaryViewModel> paymentSummaryList = paymentService.GetPaymentSummaryReport(model);
            return Json(new { data = paymentSummaryList }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Inspection Detail and Summary Report    
        [HttpGet]
        public ActionResult GetInspectionDetailData(string StateId, string CityId, string InspLevel, string BenId)
        {
            InspectionViewModel model = new InspectionViewModel();
            model.StateId = !string.IsNullOrEmpty(StateId) ? Int32.Parse(StateId) : 0;
            model.CityId = !string.IsNullOrEmpty(CityId) ? Int32.Parse(CityId) : 0;
            model.InspectionLevel = !string.IsNullOrEmpty(InspLevel) ? Int32.Parse(InspLevel) : 0;
            model.BenId = !string.IsNullOrEmpty(BenId) ? BenId : "";

            List<InspectionViewModel> inspectionList = inspectionService.GetInspectionList(model);
            return Json(new { data = inspectionList }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetInspectionSummaryData(string StateId, string CityId)
        {
            InspectionViewModel model = new InspectionViewModel();
            model.StateId = !string.IsNullOrEmpty(StateId) ? Int32.Parse(StateId) : 0;
            model.CityId = !string.IsNullOrEmpty(CityId) ? Int32.Parse(CityId) : 0;
            List<InspectionSummaryViewModel> inspectionSummaryList = inspectionService.GetInspectionSummaryReport(model);
            return Json(new { data = inspectionSummaryList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}