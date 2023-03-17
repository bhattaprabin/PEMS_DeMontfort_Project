using PEMS.BusinessEntities;
using PEMS.BusinessLayer.UserModel;
using PEMS.Repository.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PEMS.WEBUI.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService;
        public AccountController()
        {
            accountService = new AccountService();
        }
        // GET: Account
        public ActionResult RegisterUser()
        {
            ViewBag.DisplayHeader = "Register User";
            UserViewModel model = new UserViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult RegisterUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                int u = accountService.RegisterUser(model);
                string b = CommonVariables.SuperAdminRole;
                if (u > 0)
                {
                    model.ReturnMessage = u == 1 ? "Success" : "Failed!";
                    ModelState.Clear();
                }
            }
            return View(model);
        }
        public ActionResult Login()
        {
            LogInViewModel model = new LogInViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LogInViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = accountService.GetUser(model.UserName, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    Session["UserName"] = model.UserName.ToString();
                    Session["UserID"] = user.UserId;
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return Redirect("~/Home/Dashboard");
                    }
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["UserName"] = null;
            return Redirect("Login");
        }
        public ActionResult UserList()
        {
            return View();
        }

        //[Authorize(Roles = "Super Admin")] 
        public ActionResult GetUserList()
        {
            var userList =  accountService.GetUserList();
            return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("bhattaprabin1992@gmail.com", "Prabin");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
                    var password = "Pr@bin1$#";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }
    }
}