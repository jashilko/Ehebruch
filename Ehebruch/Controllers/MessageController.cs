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
        public ActionResult Index(int id, Message request)
        {
            if (!string.IsNullOrEmpty(request.TextMessage))
            {
                request.id = 0;
                request.CreatedTime = DateTime.Now;
                db.Messages.Add(request);
                db.SaveChanges();
            }
            // ищем пользователя. 
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            // Ищем его профиль. 
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            // С кем ведем беседу. 
            UserProfile userwith = db.UserProfiles.Include(u => u.UserLogin).Where(u => u.Id == id).FirstOrDefault();
            // Выбираем список сообщений. 
            var dl = db.Messages.Where(m => (m.RecipientId == userwith.Id && m.SenderId == userprofile.Id) || (m.RecipientId == userprofile.Id || m.SenderId == userwith.Id)).OrderBy(m => m.CreatedTime)
                .Select(m => new {id = m.id, TextMessage = m.TextMessage, CreatedTime = m.CreatedTime, RecipientId = m.RecipientId, SenderId = m.SenderId});

            List<Message> MList = new List<Message>();
            foreach (var m in dl)
            {
                Message mm = new Message();
                mm.CreatedTime = m.CreatedTime;
                mm.RecipientId = m.RecipientId;
                mm.SenderId = m.SenderId;
                mm.TextMessage = m.TextMessage;
                MList.Add(mm);
            }


            ViewBag.Rec = userprofile;
            ViewBag.userwith = userwith;
            return View(MList.ToList());
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
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            ViewBag.Rec = userprofile;
            return View("Index");
        }

        [HttpGet]
        public ActionResult Im(int id = 0)
        {
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            //UserProfile Recuser = db.UserProfiles.Find(id);
            List<Message> MesList = new List<Message>();
            List<Dialog> DList = new List<Dialog>();

            if (id == 0)
            {
                DateTime MaxMes = db.Messages.Where(m => (m.SenderId == userprofile.Id)).Max(m => m.CreatedTime);
                // Выбираем всех, с кем общался. 
                var bb = db.Messages.Where(m => m.SenderId == userprofile.Id).GroupBy(m => m.RecipientId);

                foreach (var c in bb)
                {
                    //MaxMes = db.Messages.Where(m => (m.SenderId == userprofile.id && m.RecipientId == c.Key)).Max(m => m.CreatedTime);
                    // У каждого с кем общался находим время последнего сообщения. 
                    MaxMes = c.Max(m => m.CreatedTime);

                    // Выбираем последнее сообщение. 
                    var ddd = c.Where(m => (m.SenderId == userprofile.Id && m.RecipientId == c.Key && m.CreatedTime == MaxMes)).
                        Select(m => new Dialog { IdPerson = m.RecipientId, TextMessage = m.TextMessage });

                    foreach (Dialog m in ddd)
                    {
                        m.AvatarPath = db.UserProfiles.Find(m.IdPerson).AvatarPath;
                        m.nic = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.Id == m.IdPerson).Select(p => p.UserLogin.nic).FirstOrDefault();
                        DList.Add(m);
                    }
                }
                return View(DList.ToList());
            }
            else
            {
                ViewBag.Rec = userprofile;
                return Index(id, null);
            }
            
        }

    }
}
