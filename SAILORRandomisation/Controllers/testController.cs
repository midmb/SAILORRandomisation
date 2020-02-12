using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAILORRandomisation.Models;

namespace SAILORRandomisation.Controllers
{

     [Authorize]

    public class testController : Controller
    {
        private SAILOREntities db = new SAILOREntities();

        //
        // GET: /test/

        public ActionResult Index()
        {
            return View(db.SAILORs.ToList());
        }

        //
        // GET: /test/Details/5

        public ActionResult Details(int id = 0)
        {
            SAILOR sailor = db.SAILORs.Find(id);
            if (sailor == null)
            {
                return HttpNotFound();
            }
            return View(sailor);
        }

        //
        // GET: /test/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /test/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SAILOR sailor)
        {
            if (ModelState.IsValid)
            {
                db.SAILORs.Add(sailor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sailor);
        }

        //
        // GET: /test/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SAILOR sailor = db.SAILORs.Find(id);
            if (sailor == null)
            {
                return HttpNotFound();
            }
            return View(sailor);
        }

        //
        // POST: /test/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SAILOR sailor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sailor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sailor);
        }

        //
        // GET: /test/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SAILOR sailor = db.SAILORs.Find(id);
            if (sailor == null)
            {
                return HttpNotFound();
            }
            return View(sailor);
        }

        //
        // POST: /test/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SAILOR sailor = db.SAILORs.Find(id);
            db.SAILORs.Remove(sailor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}