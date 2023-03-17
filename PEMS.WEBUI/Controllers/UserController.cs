using PEMS.Repository.User;
using PEMS.WEBUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEMS.WEBUI.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController()
        {
            userService = new UserService();
        }
        // GET: User
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser1()
        {
            if(ModelState.IsValid)
            {

            }
            return View();
        }
    }
}