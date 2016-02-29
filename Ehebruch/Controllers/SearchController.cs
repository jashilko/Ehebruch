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
        //
        // GET: /Search/

        public ActionResult Index(SearchProfiles request, short[] wishe)
        {
            var list = new List<KeyValuePair<string, int>>(){
            new KeyValuePair<string, int>("Флирт и переписка", 1),
            new KeyValuePair<string, int>("Встречи и свидания", 2),
            new KeyValuePair<string, int>("Быстрый секс", 4),
            new KeyValuePair<string, int>("Длительные отношения", 8),
            };

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
                    Include(p => p.UserLogin).
                    Where(p => (p.height >= request.heightFrom || request.heightFrom == null) && // Рост от
                        (p.height <= request.heightTo || request.heightTo == null) && // Рост до 
                        (p.weight >= request.weightFrom || request.weightFrom == null) && // Вес от
                        (p.weight <= request.weightTo || request.weightTo == null) && // Вес до
                        (p.sity == request.Sity || String.IsNullOrEmpty(request.Sity)) && // Город
                        (p.sex == request.SearchSex) && // Ищем пол
                        (p.wish == (p.wish & wishes))
                        ).ToList();

                request.Profiles = result;
                request.wish = wishes;
            }
            return View(request);
        }
    }
}
