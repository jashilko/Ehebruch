using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ehebruch.Models.Account;
using Ehebruch.Models;
using Ehebruch.Providers;
using System.Net.Mail;
using System.Data;


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

        // Выслать подтверждение почты.
        private void SendMail(String Email)
        {
            try
            {
                // Подтверждение регистрации по e-mail.
                // наш email с заголовком письма
                MailAddress from = new MailAddress("ehebruchconfirm@gmail.com", "Confirm registratuion EHEBRUCH");
                // кому отправляем
                MailAddress to = new MailAddress(Email);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = "Email confirmation";
                // текст письма - включаем в него ссылку
                m.Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                                "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                    Url.Action("ConfirmEmail", "Account", new { Email = Email }, Request.Url.Scheme));
                m.IsBodyHtml = true;
                // адрес smtp-сервера, с которого мы и будем отправлять письмо
                SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                // логин и пароль
                smtp.Credentials = new System.Net.NetworkCredential("ehebruchconfirm@gmail.com", "Kostya00");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            catch
            {
                ;
            }
        }
        
        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {

                    EhebruchContex db = new EhebruchContex();
                    UserLogin user = db.UserLogins.Where(m => m.email == model.Email).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.confirm)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);

                            UserProfile userprofile = db.UserProfiles.Where(p => p.UserLoginId == user.Id).FirstOrDefault();
                            if (userprofile != null)
                            {
                                Session["nic"] = user.nic;
                                Session["pid"] = userprofile.Id;
                                Session["uid"] = user.Id;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(String.Empty, "Не подтверждена почта, вам на Email выслано повторное подтверждение!");
                            SendMail(user.email);
                            return View(model);
                        }

                    }
                    
                    // Установка сессии: 
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
                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    SendMail(model.Email);
                    //return RedirectToAction("Confirm", "Account", new { Email = model.Email });
                    return RedirectToAction("Create", "Profile");
                }
                else
                {
                    ModelState.AddModelError("Email", "Ошибка при регистрации: Данный EMail уже зарегистрирован.");
                }
            }
            return View("Register");
        }

        [AllowAnonymous]
        public string Confirm(string Email)
        {
            
            
            return "На почтовый адрес " + Email + " Вам высланы дальнейшие" +
                    "инструкции по завершению регистрации";
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmail(string Token, string Email)
        {
            using (EhebruchContex db = new EhebruchContex())
            {
                UserLogin user = db.UserLogins.Where(u => u.email == Email).FirstOrDefault();
                if (user != null)
                {
                    user.confirm = true;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = "" });
                }
            }
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