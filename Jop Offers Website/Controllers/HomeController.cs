using Jop_Offers_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); // to invoke all models in this controller and using them in views

        // the index action:  represent the home page
        public ActionResult Index()
        {

           // var list = db.Categories.ToList();
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int jobId)
        {
            var job = db.Jobs.Find(jobId);
            if(job == null)
            {
                return HttpNotFound();
            }
            Session["JobID"] = jobId;
            return View(job);
        }

        [Authorize] // to prevent user to use this method if he not registerd or login
        public ActionResult Apply()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Apply(string Message)
        {
            // البيانات ال عايز ابعتها لما يتقدم للوظيفة
            var UserId = User.Identity.GetUserId();
            var jobId = (int)Session["JobID"];

            // علشان اتأكد انه مينفعش نفس اليوزر يتقدم لنفس الوظيفة اكتر من مره
            var check = db.ApplyForJobs.Where(a => a.JobId == jobId && a.UserId == UserId).ToList();

            if(check.Count < 1)
            {
                var job = new ApplyForJob();
                // add values of parameters to properties of ApplForJob Model
                // ApplyForJob properties = parameters
                job.UserId = UserId;
                job.JobId = jobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;

                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.result = "تمت الاضافة بنجاح";

            }
            else
            {
                ViewBag.result = "خطأ! لقد تقدمت لنفس الوظيفة من قبل";
            }


            return View();
        }

        // اعرض الوظائف اللى تقدملها يوزر معين
        [Authorize]
        public ActionResult GetjobByUser()
        {
            var userID = User.Identity.GetUserId();
            // entityFrameWork طريقة
            var jobs = db.ApplyForJobs.Where(a => a.UserId == userID);
            return View(jobs.ToList());
        }

        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);

        }

        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            // طريقة linq
            var jobs = from app in db.ApplyForJobs
                       join Job in db.Jobs // join 2 tables
                       on app.JobId equals Job.Id
                       where UserID == Job.user.Id
                       select app;
            #region groubing_in_Linq //___> الاسم
            // groubing in linq
            var grouped = from j in jobs
                          group j by j.objJob.JobName
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              items = gr
                          };
            #endregion

            return View(grouped.ToList());
        }

        public ActionResult Edit(int id)
        {

            var job = db.ApplyForJobs.Find(id); // else
            if (job == null)
            {
                return HttpNotFound(); // error 44
            }
            // else again
            return View(job);
        }

        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetjobByUser");

            }
            return View(job);
        }

        public ActionResult Delete(int id)
        {

            var job = db.ApplyForJobs.Find(id); // else
            if (job == null)
            {
                return HttpNotFound(); // error 44
            }
            // else again
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add delete logic here
                var myjob = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(myjob);
                db.SaveChanges();
                return RedirectToAction("GetjobByUser");
            }
            return View(job);

        }

        public ActionResult About()
        {
           // ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();

            // هنا بكتب الايميل الخاص بيا اللى هيتبعت عليه رسايل المستخدمين
            var loginInfo = new NetworkCredential("mohosny14@gmail.com","mo..hamedAli14");
            mail.From = new MailAddress(contact.E_mail); // مين المرسل
            mail.To.Add(new MailAddress("mohosny14@gmail.com")); // الرسالة رايحه لمين ويمكن اضيف اكتر من ايميل تتبعتله
            mail.Subject = contact.Subject; // باقى تفاصيل الايميل

            mail.IsBodyHtml = true; // html علشان يكون الايميل ع شكل
            string body = "اسم المرسل : " + contact.name + "<br>" +
                            "بريد المرسل : " + contact.E_mail + "<br>" +
                            "عنوان الرسالة : " + contact.Subject + "<br>" +
                             " نص الرساله : <b>" + contact.message +"</b>";

            mail.Body = body; // باقى تفاصيل الايميل

            // البرتوكول الخاص برسايل الميل
            var smtpClient = new SmtpClient("smtp.gmail.com", 587); // 587 this is Gmail Port


            smtpClient.EnableSsl = true; // يسمح بالوضع الامن gmail ال

            //smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobName.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.category.CategoryName.Contains(searchName)
            || a.category.CatehoryDescription.Contains(searchName)).ToList();


            return View(result);
        }


    }
}