using PEMS.Repository.CommonService;
using PEMS.WEBUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEMS.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        private ICommonService commonService;
        public HomeController()
        {
            commonService = new CommonService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveEmailsForAlert(string EmailId)
        {
            int result = commonService.SaveEmailsForAlert(EmailId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Dashboard()
        {
            var masterModel = new HomeIndexViewModel();
            var pieChartData = GetBarChartData();
            masterModel.barChartViewModel = pieChartData;
            return View(masterModel);
        }
        private BarChartViewModel GetBarChartData()
        {
            var barChartData = new BarChartViewModel();
            var labels = new List<string>();
            labels.Add("Male");
            labels.Add("Female");
            labels.Add("Other");

            var datasets = new List<BarChartChildViewModel>();

            var childModel = new BarChartChildViewModel();
            childModel.label = "Survey Data Gender Wise";
            childModel.backgroundColor.Add("blue");
            childModel.backgroundColor.Add("purple");
            childModel.backgroundColor.Add("green");
            childModel.borderColor = @"rgba(255,99,132,1)";
            childModel.borderWidth = 2;
            childModel.hoverBackgroundColor = @"rgba(255,99,132,0.4)";
            childModel.hoverBorderColor = @"rgba(255,99,132,1)";

            var dataList = new List<int>();

            foreach(var label in labels)
            {
                //fill the data
                if (label == "Male")
                {
                    dataList.Add(65);
                }
                if (label == "Female")
                {
                    dataList.Add(55);
                }
                if (label == "Other")
                {
                    dataList.Add(2);
                }
            }
            childModel.data = dataList;
            datasets.Add(childModel);

            barChartData.labels = labels;
            barChartData.datasets = datasets;
            return barChartData; ;
        }
    }
}