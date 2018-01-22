using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using jobaffair.Models;

namespace jobaffair.Controllers
{
    public class mozfeensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: mozfeens
        public ActionResult Index()
        {
            return View(db.mozfeens.ToList());
        }

        // GET: mozfeens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mozfeens mozfeens = db.mozfeens.Find(id);
            if (mozfeens == null)
            {
                return HttpNotFound();
            }
            return View(mozfeens);
        }

        // GET: mozfeens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mozfeens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Mozf,Desc")] mozfeens mozfeens)
        {
            if (ModelState.IsValid)
            {
                db.mozfeens.Add(mozfeens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mozfeens);
        }

        // GET: mozfeens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mozfeens mozfeens = db.mozfeens.Find(id);
            if (mozfeens == null)
            {
                return HttpNotFound();
            }
            return View(mozfeens);
        }

        // POST: mozfeens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Mozf,Desc")] mozfeens mozfeens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mozfeens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mozfeens);
        }

        // GET: mozfeens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mozfeens mozfeens = db.mozfeens.Find(id);
            if (mozfeens == null)
            {
                return HttpNotFound();
            }
            return View(mozfeens);
        }

        // POST: mozfeens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mozfeens mozfeens = db.mozfeens.Find(id);
            db.mozfeens.Remove(mozfeens);
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
