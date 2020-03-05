using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jop_Offers_Website.Models;
using WebApplication1.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Jop_Offers_Website.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); // db which map into database

        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.category);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // get error  
            }
            Job job = db.Jobs.Find(id); // else 
            if (job == null)
            {
                return HttpNotFound(); // error 404
            } // else again
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName); // لتخزين مسار الصورة فى مجلد ع السيرفر
                upload.SaveAs(path);  // لتخزين مسار الصورة فى مجلد ع السيرفر
                job.jobImage = upload.FileName;
                job.UserID = User.Identity.GetUserId();
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryID);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryID);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                    
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), job.jobImage);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName); // لتخزين مسار الصورة فى مجلد ع السيرفر
                    upload.SaveAs(path);  // لتخزين مسار الصورة فى مجلد ع السيرفر
                    job.jobImage = upload.FileName;
                }
                
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "CategoryName", job.CategoryID);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
