using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ehebruch.Models;
using Ehebruch.Models.Account;


namespace Ehebruch.Controllers
{
    public class ProfileController : Controller
    {
        private EhebruchContex db = new EhebruchContex();

        // GET: /Profile
        [Authorize]
        public ActionResult Index()
        {
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Where(p => p.UserID == user.id).FirstOrDefault();
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /Profile/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // GET: /Profile/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Profile/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(UserProfile request, HttpPostedFileBase error)
        {
            if (ModelState.IsValid)
            {
                if (request != null)
                {
                    UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
                    if (user != null)
                    {
                        request.UserID = user.id;
                        request.LastUpdateDate = DateTime.Now;
                        if (error != null)
                        {
                            DateTime current = DateTime.Now;
                            // Получаем расширение
                            string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                            // сохраняем файл по определенному пути на сервере
                            string path = current.ToString("dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                            error.SaveAs(Server.MapPath("~/Files/" + path));
                            request.AvatarPath = path;
                        }
                    }
                    
                }
                db.UserProfiles.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(request);
        }

        //
        // GET: /Profile/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        //
        // GET: /Profile/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult Upload()
        {
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                }
            }
            return Json("файл загружен");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}