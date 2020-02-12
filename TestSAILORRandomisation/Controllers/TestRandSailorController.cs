using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using TestSAILORRandomisation.Models;

namespace TestSAILORRandomisation.Controllers
{
    [Authorize]
    public class TestRandSailorController : Controller
    {
        
        private SAILOREntities db = new SAILOREntities();


        public ActionResult Index(string id)
        {
            string searchString = id;
            var rands = (from m in db.SAILORTests
                         where (m.ParticipantInitials == null)
                         select m).Take(1);
            if (!String.IsNullOrEmpty(searchString))
            {
                rands = rands.Where(s => s.ParticipantInitials.Contains(searchString));
            }
            return View(rands);
        }

        //
        // GET: /randSAILORTest/Details/5

        public ActionResult Details(int id = 0)
        {
            SAILORTest SAILORTest = db.SAILORTests.Find(id);
            if (SAILORTest == null)
            {
                return HttpNotFound();
            }
            return View(SAILORTest);
        }

        //
        // GET: /randSAILORTest/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /randSAILORTest/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SAILORTest SAILORTest)
        {
            if (ModelState.IsValid)
            {
                db.SAILORTests.Add(SAILORTest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(SAILORTest);
        }


        public ActionResult AddNewPAtient(int id = 0)
        {
            SAILORTest rand = db.SAILORTests.Find(id);
            if (rand == null)
            {
                return HttpNotFound();
            }
            return View(rand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewPAtient
           ([Bind(Include = "RandID, ScreeningID, RandomisedBy,SiteID,SITE, ParticipantInitials, Age, Gender, Incl,Excl, ApproveMedical,ConsentDate, RandAllocation, RandMethod")] SAILORTest rand)
        {
            
            if (ModelState.IsValid)
            {
                //Random Number
                rand.RandDate = DateTime.Today;
                rand.RandTime = DateTime.Now;
                // Test test=new Test();
                rand.RandMethod = rand.RandMethod;
                //rand.SiteID = rand.SiteID;
                
               //rand.ConsentDate < DateTime.Now;
                
                db.Entry(rand).State = EntityState.Modified;
                //db.SaveChanges();
                

                try
                {
                    // return db.SaveChanges();


                    db.SaveChanges();
                }

                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        //foreach (var validationError in validationErrors.ValidationErrors)
                        //{
                        //    Trace.TraceInformation("Property: {0} Error: {1}",
                        //                            validationError.PropertyName,
                        //                            validationError.ErrorMessage);
                        //}
                    }
                }

                return RedirectToAction("Result", rand);
            }

            return View(rand);
        
        }


        public JsonResult AddNewPatientPost(Models.SAILORTest ConsentDate)
        {
            return Json(new { Result = "OK" });
        }



        // GET: /Model/Result/5
        public ActionResult Result(int RandID = 0)
        {
            SAILORTest rand = db.SAILORTests.Find(RandID);
            if (rand == null)
            {
                return HttpNotFound();
            }
            return View(rand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Result([Bind(Include = "RandAllocation")] SAILORTest rand)
        {
            if (ModelState.IsValid)
            {
                rand.RandDate = DateTime.UtcNow;
                db.Entry(rand).State = EntityState.Modified;
                db.SaveChanges();

                //email.EmailAddress = "m.barbu@swansea.ac.uk";
                //email.Name = "Mihaela";
                //email.RandAllocation = rand.RandAllocation;
                //email.RandID = rand.RandID;
                //email.Send();


            }
            return View(rand);
        }





        //public ActionResult Send([Bind(Include = "RandAllocation")] SAILORTest rand)
        //{
        //    //SAILORTest rand = db.SAILORTests.Find(RandID);
        //    //var Example = new Example(rand);
        //    EmailAddress = "m.barbu@swansea.ac.uk";
        //    Name = "Mihaela";
        //    rand.RandAllocation = rand.RandAllocation;
        //    rand.RandID = rand.RandID;
        //    rand.Send();
        //    return RedirectToAction("Index");
        //}


        //public class Example
        //{
        //    public string EmailAddress { get; set; }
        //    public string Name { get; set; }
        //    public SAILORTest SAILORTest { get; set; }
        //    //public string RandAllocation { get; set; }
        //    //public int RandID{ get; set; }
        //}


        //public ActionResult Send(Example form)
        //{
        //    dynamic email = new Email("Example");

        //    email.EmailAddress = form.EmailAddress;
        //    email.Name = form.Name;
        //    //email.RandID = form.RandID;
        //    //email.Message = form.Message;
        //    email.Send();

        //    return RedirectToAction("Index");

        //}
        
        //public class Example
        //{
        //    public string EmailAddress { get; set; }
        //    public string Name { get; set; }
        //    public string Message { get; set; }
        //}
        
        //
        // GET: /randSAILORTest/Edit/5
        public ActionResult Edit(int id = 0)
        {
            SAILORTest SAILORTest = db.SAILORTests.Find(id);
            if (SAILORTest == null)
            {
                return HttpNotFound();
            }
            return View(SAILORTest);
        }

        //
        // POST: /randSAILORTest/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SAILORTest SAILORTest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(SAILORTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(SAILORTest);
        }
        
        ////
        //// GET: /randSAILORTest/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    SAILORTest SAILORTest = db.SAILORTests.Find(id);
        //    if (SAILORTest == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(SAILORTest);
        //}

       //
        // POST: /randSAILORTest/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SAILORTest SAILORTest = db.SAILORTests.Find(id);
            db.SAILORTests.Remove(SAILORTest);
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