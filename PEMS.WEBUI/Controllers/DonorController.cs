using PEMS.BusinessLayer.DonorModel;
using PEMS.Repository.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEMS.WEBUI.Controllers
{
    public class DonorController : Controller
    {
        private IDonorService donorService;

        // GET: Donor
        public DonorController()
        {
            donorService = new DonorService();
        }
        public ActionResult SearchDonorList()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetDonorData()
        {
            List<DonorViewModel> surveyList = donorService.GetDonorList("");
            return Json(new { data = surveyList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddDonor()
        {
            DonorViewModel model = new DonorViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddDonor(DonorViewModel model)
        {
            int result = donorService.RegisterDonor(model);
            return View();
        }
    }
}