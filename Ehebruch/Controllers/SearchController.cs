using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ehebruch.Models;
using System.Data.Entity;

namespace Ehebruch.Controllers
{
    public class SearchController : Controller
    {
        private EhebruchContex db = new EhebruchContex();
        private SelectList FSelectCountry;
        private SelectList FSelectRegion;
        private SelectList FSelectCity;
        

        // Получаем список стран. 
        private SelectList getSelectCountryList(int selectedIndex = 3159)
        {
            if (FSelectCountry == null)
                FSelectCountry = new SelectList(db.Countries.OrderByDescending(c => c.Priority), "Id", "name", db.Countries.Where(c => c.Id == selectedIndex));
            return FSelectCountry;
        }

        private SelectList getSelectRegionList(int CountryId = 0, int selectedReg = 0)
        {
            if (FSelectRegion == null)
                FSelectRegion = new SelectList(db.Regions.Where(r => r.CountryId == CountryId).OrderByDescending(c => c.Priority).ThenBy(c => c.Name), 
                    "Id", "name", db.Regions.Where(r => r.Id == selectedReg));
            return FSelectRegion;
        }

        private SelectList getSelectCityList(int RegionId = 0, int selectedCity = 0)
        {
            if (FSelectCity == null)
                FSelectCity = new SelectList(db.Cities.Where(s => (s.RegionId == RegionId)).OrderByDescending(c => c.Priority).ThenBy(c => c.Name),
                "Id", "name", db.Cities.Where(c => c.Id == selectedCity));
            return FSelectCity;
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

        

        public ActionResult Index(SearchProfiles request, short[] wishe)
        {
            var list = new List<KeyValuePair<string, int>>(){
            new KeyValuePair<string, int>("Флирт и переписка", 1),
            new KeyValuePair<string, int>("Встречи и свидания", 2),
            new KeyValuePair<string, int>("Быстрый секс", 4),
            new KeyValuePair<string, int>("Длительные отношения", 8),
            };

            ViewBag.Country = getSelectCountryList();
            ViewBag.Region = getSelectRegionList();
            ViewBag.City = getSelectCityList();


            ViewBag.wishe = list;

            short wishes = 0;

            if (wishe != null)
            {
                   foreach (short w in wishe)
                {
                    wishes += w;
                }
            }

            if (!string.IsNullOrEmpty(request.SearchButton)) 
            {
                var result = db.UserProfiles.
                    Include(p => p.UserLogin).Include(p => p.City).
                    Where(p => (p.height >= request.heightFrom || request.heightFrom == null) && // Рост от
                        (p.height <= request.heightTo || request.heightTo == null) && // Рост до 
                        (p.Age >= request.AgeFrom || request.weightFrom == null) && // Возраст от
                        (p.Age <= request.AgeTo || request.weightTo == null) && // Возраст до
                        (p.CountryId == request.CountryId || (request.CountryId ??0) == 0) && // Страна.
                        (p.RegionId == request.RegionId || (request.RegionId ?? 0) == 0) && // Регион
                        (p.CityId == request.CityId || (request.CityId ?? 0) == 0) && // Город. 
                        (p.sex == request.SearchSex) && // Ищем пол
                        (((p.wish & wishes) > 0) && wishes != 0) // Желания. 
                        ).ToList();

                request.Profiles = result;
                request.wish = wishes;
            }
            return View(request);
        }
    }
}
