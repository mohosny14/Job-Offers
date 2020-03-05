using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Jop_Offers_Website.Controllers
{
    [Authorize(Roles = "Admins")]
    public class RolesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext(); // to open connection with this model

        // GET: Roles
        public ActionResult Index()
        {
            var list = db.Roles.ToList();
           
            return View(list);
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // get error
            }
            var role = db.Roles.Find(id); // else
            if(role == null)
            {
                return HttpNotFound(); // error 44
            }
            // else again
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            
                if (ModelState.IsValid)
                {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(role);
            
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // get error
            }
            var role = db.Roles.Find(id); // else
            if (role == null)
            {
                return HttpNotFound(); // error 44
            }
            // else again
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,Name")]IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // get error
            }
            var role = db.Roles.Find(id); // else
            if (role == null)
            {
                return HttpNotFound(); // error 44
            }
            // else again
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add delete logic here
                var myrole = db.Roles.Find(role.Id);
                db.Roles.Remove(myrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
            
        }
    }
}
