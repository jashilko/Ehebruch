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

        //
        
        // GET: /Profile
        [Authorize]
        public ActionResult Index()
        {
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginID == user.id).FirstOrDefault();
            var fotos = db.UserFotoes.Where(f => f.UserProfileid == userprofile.id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            userprofile.Fotoes = fotos;
            //ViewBag.Foto = fotos;
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
            userprofile.UserLogin = db.UserLogins.Find(userprofile.UserLoginID);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            var fotos = db.UserFotoes.Where(f => f.UserProfileid == userprofile.id) //Получаем фото текущего профиля. 
                                        .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            userprofile.Fotoes = fotos;
            return View("Index", userprofile);
        }

        //
        // GET: /Profile/Create

        [Authorize]
        public ActionResult Create()
        {
            var list = new List<KeyValuePair<string, int>>(){
            new KeyValuePair<string, int>("Флирт и переписка", 1),
            new KeyValuePair<string, int>("Встречи и свидания", 2),
            new KeyValuePair<string, int>("Быстрый секс", 4),
            new KeyValuePair<string, int>("Длительные отношения", 8),
            };

            ViewBag.wishe = list;
            

            // зхаполняем список 
            List<int> height = new List<int>();
            for (int i = 150; i <= 250; i++ )
            {
                height.Add(i);
            }
            ViewBag.height = height;

                return View();
        }



        //
        // POST: /Profile/Create

        

        [HttpPost]
        [Authorize]
        public ActionResult Create(UserProfile request, HttpPostedFileBase error, short[] selectedwishes)
        {
            if (ModelState.IsValid)
            {
                if (request != null)
                {
                    UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
                    if (user != null)
                    {
                        short wish = 0;
                        foreach (short i in selectedwishes)
                        {
                            wish += i;
                        }

                        request.UserLoginID = user.id;
                        request.LastUpdateDate = DateTime.Now;
                        request.wish = wish;
                        
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

                        db.UserProfiles.Add(request);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
            }
            var list = new List<KeyValuePair<string, int>>(){
            new KeyValuePair<string, int>("Флирт и переписка", 1),
            new KeyValuePair<string, int>("Встречи и свидания", 2),
            new KeyValuePair<string, int>("Быстрый секс", 4),
            new KeyValuePair<string, int>("Длительные отношения", 8),
            };

            ViewBag.wishe = list;


            // зхаполняем список 
            List<int> height = new List<int>();
            for (int i = 150; i <= 250; i++)
            {
                height.Add(i);
            }
            ViewBag.height = height;

            return View(request);
        }

        //
        // GET: /Profile/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);

            var list = new List<KeyValuePair<string, short>>(){
            new KeyValuePair<string, short>("Флирт и переписка", 1),
            new KeyValuePair<string, short>("Встречи и свидания", 2),
            new KeyValuePair<string, short>("Быстрый секс", 4),
            new KeyValuePair<string, short>("Длительные отношения", 8),
            };

            ViewBag.wishe = list;

            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile request, HttpPostedFileBase error, short[] selectedwishes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                if (request != null)
                {
                    UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
                    short wish = 0;
                    foreach (short i in selectedwishes)
                    {
                        wish += i;
                    }
                    request.LastUpdateDate = DateTime.Now;
                    request.wish = wish;
                    request.UserLoginID = user.id;

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
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(request);
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

        // Получаем список фото для частичного представления. 
        public ActionResult FotoShow()
        {
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Where(p => p.UserLoginID == user.id).FirstOrDefault();
            var fotos = db.UserFotoes.Where(f => f.UserProfileid == userprofile.id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию
            return PartialView("FotoShow", fotos.ToList());
        }

        [HttpPost]
        public ActionResult DeleteFoto(int fotoid)
        {
            UserFoto foto = db.UserFotoes.Find(fotoid);
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            if (fotoid != null && user != null)
            {
                db.UserFotoes.Remove(foto);
                db.SaveChanges();
            }
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginID == user.id).FirstOrDefault();
            var fotos = db.UserFotoes.Where(f => f.UserProfileid == userprofile.id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            userprofile.Fotoes = fotos;
            return View("Index", userprofile);
        }


        [HttpGet]
        public ActionResult UploadFoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFoto(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddFoto()
        {
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginID == user.id).FirstOrDefault();
            var fotos = db.UserFotoes.Where(f => f.UserProfileid == userprofile.id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            userprofile.Fotoes = fotos;
            //ViewBag.Foto = fotos;
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }


        [HttpPost]
        public ActionResult AddFoto(HttpPostedFileBase error, String descript)
        {
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginID == user.id).FirstOrDefault();
            if (error != null)
            {
                UserFoto userfoto = new UserFoto();
                DateTime current = DateTime.Now;
                // Получаем расширение
                string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                // сохраняем файл по определенному пути на сервере
                string path = current.ToString("dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                error.SaveAs(Server.MapPath("~/Files/" + path));
                
                userfoto.Path = path;
                userfoto.UploadDate = current;
                userfoto.Descript = descript;
                userfoto.UserProfileid = userprofile.id;
                db.UserFotoes.Add(userfoto);
                db.SaveChanges();
            }

            var fotos = db.UserFotoes.Where(f => f.UserProfileid == userprofile.id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            userprofile.Fotoes = fotos;
             
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}