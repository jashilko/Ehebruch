using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ehebruch.Models;

namespace Ehebruch.Controllers
{
    public class UserController : Controller
    {
        private EhebruchContex db = new EhebruchContex();    

        [HttpGet]
        [Authorize(Roles = "Администраторы")]
        public ActionResult Index()
        {
            var users = db.UserLogins.Include(u => u.Role).ToList();

            List<Role> roles = db.Roles.ToList();
            roles.Insert(0, new Role { Id = 0, Name = "Все" });
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View(users);
        }

        [HttpPost]
        [Authorize(Roles = "Администраторы")]
        public ActionResult Index(int role)
        {
            IEnumerable<UserLogin> allUsers = null;
            if (role == 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                allUsers = from user in db.UserLogins.Include(u => u.Role)
                           where user.RoleId == role
                           select user;
            }
            List<Role> roles = db.Roles.ToList();
            roles.Insert(0, new Role { Id = 0, Name = "Все" });
            ViewBag.Roles = ViewBag.Roles = new SelectList(roles, "Id", "Name"); ;

            return View(allUsers.ToList());

        }


        [HttpGet]
        [Authorize(Roles = "Администраторы")]
        public ActionResult Edit(int id)
        {
            UserLogin user = db.UserLogins.Find(id);
            SelectList roles = new SelectList(db.Roles, "id", "Name", user.RoleId);
            ViewBag.Roles = roles;

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles="Администраторы")]
        public ActionResult Edit (UserLogin user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SelectList profils = new SelectList(db.UserProfiles, "id", "sity");
            ViewBag.Profils = profils;
            SelectList roles = new SelectList(db.Roles, "id", "Name");
            ViewBag.Roles = roles;
             
            return View(user);
        }

        [Authorize(Roles = "Администраторы")]
        public ActionResult Delete(int id)
        {
            UserLogin user = db.UserLogins.Find(id);
            db.UserLogins.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Администраторы")]
        public ActionResult Create()
        {
            SelectList roles = new SelectList(db.Roles, "id", "Name");
            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администраторы")]
        public ActionResult Create(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                db.UserLogins.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SelectList roles = new SelectList(db.Roles, "id", "Name");
            ViewBag.Roles = roles;
            return View(user);

        }

    }

}
