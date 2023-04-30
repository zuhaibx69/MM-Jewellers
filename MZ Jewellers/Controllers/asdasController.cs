using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MZ_Jewellers.Models;

namespace MZ_Jewellers.Controllers
{
    public class asdasController : Controller
    {
        private JewelleryManagementSystemEntities db = new JewelleryManagementSystemEntities();

        // GET: asdas
        public ActionResult Index()
        {
            return View(db.Jewellers.ToList());
        }

        // GET: asdas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jeweller jeweller = db.Jewellers.Find(id);
            if (jeweller == null)
            {
                return HttpNotFound();
            }
            return View(jeweller);
        }

        // GET: asdas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: asdas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "jeweller_id,jeweller_name,jeweller_password")] Jeweller jeweller)
        {
            if (ModelState.IsValid)
            {
                db.Jewellers.Add(jeweller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jeweller);
        }

        // GET: asdas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jeweller jeweller = db.Jewellers.Find(id);
            if (jeweller == null)
            {
                return HttpNotFound();
            }
            return View(jeweller);
        }

        // POST: asdas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jeweller_id,jeweller_name,jeweller_password")] Jeweller jeweller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jeweller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jeweller);
        }

        // GET: asdas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jeweller jeweller = db.Jewellers.Find(id);
            if (jeweller == null)
            {
                return HttpNotFound();
            }
            return View(jeweller);
        }

        // POST: asdas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Jeweller jeweller = db.Jewellers.Find(id);
            db.Jewellers.Remove(jeweller);
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
