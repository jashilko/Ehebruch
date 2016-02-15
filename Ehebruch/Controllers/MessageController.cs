using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ehebruch.Models.Account;
using Ehebruch.Models;
using System.Data;
using System.Data.Entity;


namespace Ehebruch.Controllers
{
    public class MessageController : Controller
    {
        private EhebruchContex db = new EhebruchContex();
        //
        // GET: /Message/

        public ActionResult Index()
        {
            // ищем пользователя. 
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            // Ищем его профиль. 
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginID == user.id).FirstOrDefault();
            // Выбираем список его сообщений. 
            ViewBag.Rec = userprofile;
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(string Mes)
        {
            /*
            if (!String.IsNullOrEmpty(Mes))
            {
                Message mes = new Message();
                mes.
            }
            ViewBag.Mes = Mes;
            */
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginID == user.id).FirstOrDefault();
            ViewBag.Rec = userprofile;
            return View("Index");
        }

        [HttpGet]
        public ActionResult Im()
        {
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginID == user.id).FirstOrDefault();
            //UserProfile Recuser = db.UserProfiles.Find(id);
            List<Message> MesList = new List<Message>();
            List<Dialog> DList = new List<Dialog>();

           DateTime MaxMes = db.Messages.Where(m => (m.SenderId == userprofile.id)).Max(m => m.CreatedTime);
            // Выбираем всех, с кем общался. 
            var bb = db.Messages.Where(m => m.SenderId == userprofile.id).GroupBy(m => m.RecipientId);
                
            foreach (var c in bb)
            {
                //MaxMes = db.Messages.Where(m => (m.SenderId == userprofile.id && m.RecipientId == c.Key)).Max(m => m.CreatedTime);
                // У каждого с кем общался находим время последнего сообщения. 
                MaxMes = c.Max(m => m.CreatedTime);

                // Выбираем последнее сообщение. 
                var ddd = c.Where(m => (m.SenderId == userprofile.id && m.RecipientId == c.Key && m.CreatedTime == MaxMes)).
                    Select(m => new Dialog{IdPerson = m.RecipientId, TextMessage = m.TextMessage });

                foreach (Dialog m in ddd)
                    DList.Add(m);
            }
            

            //ViewBag.Rec = userprofile;
            return View(DList.ToList());
        }

        

    }
}
