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
        private SelectList FSelectCountry;
        private SelectList FSelectRegion;
        private SelectList FSelectCity;
        private SelectList FFigure;
        private SelectList FSmoking;
        private SelectList FAlcohol;
        private SelectList FWhatpart;

        // Получаем список стран. 
        private SelectList getSelectCountryList(int selectedIndex = 3159)
        {
            if (FSelectCountry == null)
                FSelectCountry = new SelectList(db.Countries.OrderByDescending(c => c.Priority), "Id", "name", db.Countries.Where(c => c.Id == selectedIndex));
            return FSelectCountry;
        }

        private SelectList getSelectRegionList(int CountryId = 3159, int selectedReg = 0)
        {
            if (FSelectRegion == null)
                FSelectRegion = new SelectList(db.Regions.Where(r => r.CountryId == CountryId).OrderByDescending(c => c.Priority).ThenBy(c => c.Name),
                    "Id", "name", db.Regions.Where(r => r.Id == selectedReg));
            return FSelectRegion;
        }

        private SelectList getSelectCityList(int RegionId = 4312, int selectedCity = 0)
        {
            if (FSelectCity == null)
                FSelectCity = new SelectList(db.Cities.Where(s => (s.RegionId == RegionId)).OrderByDescending(c => c.Priority).ThenBy(c => c.Name),
                "Id", "name", db.Cities.Where(c => c.Id == selectedCity));
            return FSelectCity;
        }

        private SelectList getSelectFigureList(int? Fig = 0)
        {
            if (FFigure == null)
            {
                if (Fig == null) Fig = 0;
                var figList = db.Figures;
                figList.Add(new Figure { Id = 0, Fig = "Выберите фигуру" });
                FFigure = new SelectList(figList, "Id", "Fig", figList.Where(f => f.Id == Fig));
            }
            return FFigure;
        }

        private SelectList getSmokingList(int? Smo = 0)
        {
            if (FSmoking == null)
            {
                if (Smo == null) Smo = 0;
                var smoList = db.Smokings;
                smoList.Add(new Smoking { Id = 0, Smoke = "Курение" });
                FSmoking = new SelectList(smoList, "Id", "Smoke", smoList.Where(s => s.Id == Smo).FirstOrDefault());
            }
            
            return FSmoking;
        }

        private SelectList getAlcoholList(int? Alc = 0)
        {
            if (FAlcohol == null)
            {
                if (Alc == null) Alc = 0;
                var alcoList = db.Alcohols;
                alcoList.Add(new Alcohol { Id = 0, Alco = "Алкоголь" });
                FAlcohol = new SelectList(alcoList, "Id", "Alco", alcoList.Where(a => a.Id == Alc).FirstOrDefault());
            }
            return FAlcohol;
        }

        // Возвращаем Чп со списком регионов.
        public ActionResult GetRegion(int id)
        {
            var reglist = db.Regions.Where(c => c.CountryId == id).OrderByDescending(c => c.Priority).ThenBy(c => c.Name);
            return PartialView(reglist.ToList());
        }

        // Возвращаем Чп со списком городов.
        public ActionResult GetCity(int id)
        {
            return PartialView(db.Cities.Where(c => c.RegionId == id).OrderByDescending(c => c.Priority).ThenBy(c => c.Name).ToList());
        }

        // Список языков.
        public List<KeyValuePair<string, int>> getLangList()
        {
            List<KeyValuePair<string, int>> res = new List<KeyValuePair<string, int>>();
            foreach (Language l in db.Languages)
            {
               res.Add(new KeyValuePair<string, int>(l.Lang, l.Id));
            }
            return res;
        }

        // Список, что привлекает.
        public List<KeyValuePair<string, int>> getExcitementList()
        {
            List<KeyValuePair<string, int>> res = new List<KeyValuePair<string, int>>();
            foreach (Excitement e in db.Excitements)
            {
                res.Add(new KeyValuePair<string, int>(e.Excit, e.Id));
            }
            return res;
        }

        // Что в партнере
        public List<KeyValuePair<string, int>> getWhatpartnerList(Boolean? man = null)
        {
            List<KeyValuePair<string, int>> res = new List<KeyValuePair<string, int>>();
            foreach (Whatpartner w in db.Whatpartners.Where(w => ((w.Smb == null)|| (w.Smb == man))))
            {
                res.Add(new KeyValuePair<string, int>(w.Whatpart, w.Id));
            }
            return res;
        }

        
        // GET: /Profile
        [Authorize]
        public ActionResult Index(int Id = 0)
        {
            db.UserProfiles.Load();
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            //UserProfile userprofile = db.UserProfiles.Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            UserProfile userprofile;
            if (Id == 0)
                userprofile = db.UserProfiles.Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            else
                userprofile = db.UserProfiles.Find(Id);
            if (userprofile == null)
                return Redirect("/Profile/Create");
            var fotos = db.UserFotoes.Where(f => f.UserProfileId == userprofile.Id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            if (fotos != null && fotos.Count() > 0)
                userprofile.Fotoes = fotos;

            var list = new List<KeyValuePair<string, short>>(){
            new KeyValuePair<string, short>("Флирт и переписка", 1),
            new KeyValuePair<string, short>("Встречи и свидания", 2),
            new KeyValuePair<string, short>("Быстрый секс", 4),
            new KeyValuePair<string, short>("Длительные отношения", 8),
            };

            ViewBag.wishe = list;
            ViewBag.Country = getSelectCountryList((userprofile.CountryId != null) ? (int)userprofile.CountryId : 0);
            ViewBag.Region = getSelectRegionList((userprofile.CountryId != null) ? (int)userprofile.CountryId : 0,
                (userprofile.RegionId != null) ? (int)userprofile.RegionId : 0);
            ViewBag.City = getSelectCityList((userprofile.RegionId != null) ? (int)userprofile.RegionId : 0,
                (userprofile.CityId != null) ? (int)userprofile.CityId : 0);



            ViewBag.Figure = getSelectFigureList((userprofile.FigureId != null) ? (int)userprofile.FigureId : 0);
            ViewBag.Smoking = getSmokingList(userprofile.SmokingId);
            ViewBag.Alcohol = getAlcoholList(userprofile.AlcoholId);
            ViewBag.LangList = getLangList();
            ViewBag.ExcitList = getExcitementList();
            ViewBag.Whatpart = getWhatpartnerList(userprofile.sex);


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
            var list = new List<KeyValuePair<string, short>>(){
            new KeyValuePair<string, short>("Флирт и переписка", 1),
            new KeyValuePair<string, short>("Встречи и свидания", 2),
            new KeyValuePair<string, short>("Быстрый секс", 4),
            new KeyValuePair<string, short>("Длительные отношения", 8),
            };

            ViewBag.wishe = list;
            
            
            // Города и Страны
            ViewBag.Country = getSelectCountryList();
            ViewBag.Region = getSelectRegionList();
            ViewBag.City = getSelectCityList();

            ViewBag.Figure = getSelectFigureList();
            ViewBag.Smoking = getSmokingList();
            ViewBag.Alcohol = getAlcoholList();
            ViewBag.LangList = getLangList();
            ViewBag.ExcitList = getExcitementList();
            ViewBag.Whatpart = getWhatpartnerList();

            // заполняем список 
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
                        if (selectedwishes != null)
                            foreach (short i in selectedwishes)
                                wish += i;

                        request.UserLoginId = user.Id;
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

        public ActionResult Edit()
        {
            db.UserProfiles.Load();
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            if (userprofile == null)
                return Redirect("/Profile/Create");

            var list = new List<KeyValuePair<string, short>>(){
            new KeyValuePair<string, short>("Флирт и переписка", 1),
            new KeyValuePair<string, short>("Встречи и свидания", 2),
            new KeyValuePair<string, short>("Быстрый секс", 4),
            new KeyValuePair<string, short>("Длительные отношения", 8),
            };

            ViewBag.wishe = list;
            ViewBag.Country = getSelectCountryList((userprofile.CountryId != null) ? (int)userprofile.CountryId : 0);
            ViewBag.Region = getSelectRegionList((userprofile.CountryId != null) ? (int)userprofile.CountryId : 0, 
                (userprofile.RegionId != null) ? (int)userprofile.RegionId : 0);
            ViewBag.City = getSelectCityList((userprofile.RegionId != null) ? (int)userprofile.RegionId : 0, 
                (userprofile.CityId != null) ? (int)userprofile.CityId : 0);



            ViewBag.Figure = getSelectFigureList((userprofile.FigureId != null) ? (int)userprofile.FigureId : 0);
            ViewBag.Smoking = getSmokingList(userprofile.SmokingId);
            ViewBag.Alcohol = getAlcoholList(userprofile.AlcoholId);
            ViewBag.LangList = getLangList();
            ViewBag.ExcitList = getExcitementList();
            ViewBag.Whatpart = getWhatpartnerList(userprofile.sex);
            

            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection formcollection,  UserProfile request, HttpPostedFileBase error, short[] selectedwishes, int[] selectedlang,
            int[] selectedexcit, int[] selectedwhatpart)
        {
            if (ModelState.IsValid)
            {
                if (request != null)
                {
                    db.UserProfiles.Load();
                    UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
                    //UserProfile userprofile = db.UserProfiles.Find(Id);
                    UserProfile userprofile = db.UserProfiles.Where(p => p.UserLoginId == user.Id).FirstOrDefault();

                    if (TryUpdateModel(userprofile))
                    {
                        short wish = 0;
                        foreach (short i in selectedwishes)
                        {
                            wish += i;
                        }

                        userprofile.LastUpdateDate = DateTime.Now;
                        userprofile.wish = wish;
                        userprofile.UserLoginId = user.Id;
                        userprofile.dateOfBirthday = null;

                        if (error != null)
                        {
                            DateTime current = DateTime.Now;
                            // Получаем расширение
                            string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                            // сохраняем файл по определенному пути на сервере
                            string path = current.ToString("dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                            error.SaveAs(Server.MapPath("~/Files/" + path));
                            userprofile.AvatarPath = path;
                        }



                        foreach (Language l in db.Languages)
                        {
                            if ((selectedlang != null) && (userprofile.Languages.Contains(l)) && (!selectedlang.Contains(l.Id)))
                                userprofile.Languages.Remove(l);
                            else if ((selectedlang != null) && (!userprofile.Languages.Contains(l)) && (selectedlang.Contains(l.Id)))
                                userprofile.Languages.Add(l);

                        }

                        foreach (Excitement e in db.Excitements)
                        {
                            if ((selectedexcit != null) && (userprofile.Excitements.Contains(e)) && (!selectedexcit.Contains(e.Id)))
                                userprofile.Excitements.Remove(e);
                            else if ((selectedexcit != null) && (!userprofile.Excitements.Contains(e)) && (selectedexcit.Contains(e.Id)))
                                userprofile.Excitements.Add(e);
                        }

                        foreach (Whatpartner e in db.Whatpartners)
                        {
                            if ((selectedwhatpart != null) && (userprofile.Whatpartners.Contains(e)) && (!selectedwhatpart.Contains(e.Id)))
                                userprofile.Whatpartners.Remove(e);
                            else if ((selectedwhatpart != null) && (!userprofile.Whatpartners.Contains(e)) && (selectedwhatpart.Contains(e.Id)))
                                userprofile.Whatpartners.Add(e);
                        }


                    }
                    db.Entry(userprofile).State = EntityState.Modified;    
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


       // Получаем список фото для частичного представления. 
        public ActionResult FotoShow()
        {
            db.UserProfiles.Load();
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            var fotos = db.UserFotoes.Where(f => f.UserProfileId == userprofile.Id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию
            return PartialView("FotoShow", userprofile);
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
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            var fotos = db.UserFotoes.Where(f => f.UserProfileId == userprofile.Id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            userprofile.Fotoes = fotos;
            return RedirectToAction("AddFoto");
        }


        [HttpGet]
        public ActionResult AddFoto()
        {
            UserLogin user = db.UserLogins.Where(m => m.nic == HttpContext.User.Identity.Name).FirstOrDefault();
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginId == user.Id).FirstOrDefault();
            var fotos = db.UserFotoes.Where(f => f.UserProfileId == userprofile.Id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            userprofile.Fotoes = fotos;
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
            UserProfile userprofile = db.UserProfiles.Include(p => p.UserLogin).Where(p => p.UserLoginId == user.Id).FirstOrDefault();
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
                userfoto.UserProfileId = userprofile.Id;
                db.UserFotoes.Add(userfoto);
                db.SaveChanges();
            }

            var fotos = db.UserFotoes.Where(f => f.UserProfileId == userprofile.Id) //Получаем фото текущего профиля. 
                            .OrderByDescending(f => f.UploadDate); // упорядочиваем по дате по убыванию            
            userprofile.Fotoes = fotos;
             
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("AddFoto");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}