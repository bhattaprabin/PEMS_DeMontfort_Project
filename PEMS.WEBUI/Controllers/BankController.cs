using PEMS.Repository;
using PEMS.Repository.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEMS.WEBUI.Controllers
{
    public class BankController : Controller
    {
        private IBankService bankService;
        public BankController()
        {
            bankService = new BankService();
        }
        // GET: Bank
        public ActionResult Index()
        {
            return View();
        }
    }
}