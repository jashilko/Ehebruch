using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ehebruch.Models.Account;
using Ehebruch.Models;
using Ehebruch.Providers;


namespace Ehebuch.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegLogin()
        {
            return View();
        }
        
        public ActionResult ContexName()
        {
            ViewBag.ContexName = "Костя";
            return PartialView();
        }
        
        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Profile");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser membershipUser = ((EheMembershipProvider)Membership.Provider).CreateUser(model.Email, model.Password, model.Nic);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Nic, false);
                    return RedirectToAction("Create", "Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
            }
            return View("Register");
        }

        /*
        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (EhebruchContex _db = new EhebruchContex())
            {
                try
                {
                    UserLogin user = (from u in _db.UserLogins
                                 where u.nic == login && u.pass == password
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch (Exception ex)
                {
                    isValid = false;
                    string exe = ex.InnerException.Message;
                }
            }
            return isValid;
        }
         */ 
    }
}