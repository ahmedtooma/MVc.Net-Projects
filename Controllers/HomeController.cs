using jobaffair.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace jobaffair.Controllers
{

    
    public class HomeController : Controller
    {


       private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job == null)
            {

                return HttpNotFound();

            }else
            {
                Session["JobId"] = JobId;
                return View(job);
            }

            
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];
            var check = db.ApplyForJobs.Where(a => a.UserId == UserId && a.JobId == JobId).ToList();
            if (check.Count < 1)
            {

                var job = new ApplyForJob();

                job.UserId = UserId;
                job.JobId = JobId;


                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "تمت بنجاح";
            }else
            {
                ViewBag.Result = "تقدمت مسبقا";

            }
          

           

            return View();
        }


        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = from app in db.ApplyForJobs join
                       job in db.Jobs
                       on app.JobId equals job.ID

                       where job.UserID == UserId
                       select app
                       
                       ;

            var grouped = from j in jobs
                          group j by j.job.JobTitle
                           into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
   
        };




            return View(grouped.ToList());
        }


        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(jobs.ToList());
        }

        public ActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Search(string SearchName)
        {

            var job = db.Jobs.Where(a => a.JobTitle.Contains(SearchName) || a.JobContent.Contains(SearchName) || a.Category.CategoryType.Contains(SearchName) || a.Category.CategoryDesc.Contains(SearchName)).ToList();
            return View(job);
        }





        [Authorize]
        public ActionResult DetailsOfJobs(int id)

        {


            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }



        [Authorize]
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)

            {
                return HttpNotFound();

            }
            return View(job);
        }


        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {

            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();

            }

            return View(job);
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {


            try
            {
                var myjob = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(myjob);
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");

            }
            catch
            {
                return View(job);
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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
            var logininfo = new NetworkCredential("ahmedtooma50@gmail.com", "0132528350");       
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("ahmedtooma50@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string ClientMessage = "اسم المرسل :" + contact.Name + "<br>"+
                                    "البريد الالكترونى" + contact.Email  + "<br>" +
                                    "الرسالة" + "<b>"+contact.MessageBody+"</b>";
            mail.Body = ClientMessage;




            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
          
            
            smtp.EnableSsl = true;
            smtp.Credentials = logininfo;

            smtp.Send(mail);    

            return RedirectToAction("Index");
        }
    }
}