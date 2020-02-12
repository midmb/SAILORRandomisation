using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAILORRandomisation.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Postal; 


namespace SAILORRandomisation.Controllers
{
        [Authorize]
    public class randsailorController : Controller
    {
        private SAILOREntities db = new SAILOREntities();

        //
        // GET: /randsailor/

        //public ActionResult Index()
        //{
        //    return View(db.SAILORs.ToList());
        //}
                                
            
            public ActionResult Index(string id)
        {
            string searchString = id;
            var rands = (from m in db.SAILORs
                         where (m.ParticipantInitials == null)
                         select m).Take(1);
            if (!String.IsNullOrEmpty(searchString))
            {
                rands = rands.Where(s => s.ParticipantInitials.Contains(searchString));
            }
            return View(rands);
        }

        //
        // GET: /randsailor/Details/5

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
        // GET: /randsailor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /randsailor/Create

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

        
        public ActionResult AddNewPAtient(int id = 0)
        {
            SAILOR rand = db.SAILORs.Find(id);
            if (rand == null)
            {
                return HttpNotFound();
            }
            return View(rand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewPAtient
           ([Bind(Include ="RandID, ScreeningID, RandomisedBy,SiteID,SITE, ParticipantInitials, Age, Gender, Incl,Excl, ApproveMedical,ConsentDate, RandAllocation, RandMethod")] SAILOR rand)
        {
            if (ModelState.IsValid)
            {
                //Random Number
                rand.RandDate = DateTime.Today;
                rand.RandTime = DateTime.Now;
                 // Test test=new Test();
    rand.RandMethod=rand.RandMethod;
   // rand.SiteID = rand.SiteID;

    db.Entry(rand).State = EntityState.Modified;
              //  db.SaveChanges();
                try
                {
                    // return db.SaveChanges();


                    db.SaveChanges();
                }
                
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
                               
                return RedirectToAction("Result", rand);
            }

            return View(rand);
        }
                            
        // GET: /Model/Result/5
        public ActionResult Result(int RandID = 0)
        {
            SAILOR rand = db.SAILORs.Find(RandID);
            if (rand == null)
            {
                return HttpNotFound();
            }
            return View(rand);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Result([Bind(Include = "RandAllocation")] SAILOR rand)
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





        //public ActionResult Send([Bind(Include = "RandAllocation")] SAILOR rand)
        //{
        //    //SAILOR rand = db.SAILORs.Find(RandID);
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
        //    public SAILOR SAILOR { get; set; }
        //    //public string RandAllocation { get; set; }
        //    //public int RandID{ get; set; }
        //}


        public ActionResult Send(Example form)
        {
            dynamic email = new Email("Example");

            email.EmailAddress = form.EmailAddress;
            email.Name = form.Name;
            //email.RandID = form.RandID;
            //email.Message = form.Message;
            email.Send();

            return RedirectToAction("Index");

        }



        //public class Example
        //{
        //    public string EmailAddress { get; set; }
        //    public string Name { get; set; }
        //    public string Message { get; set; }
        //}











        
        //
        // GET: /randsailor/Edit/5
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
        // POST: /randsailor/Edit/5

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


        

        ////
        //// GET: /randsailor/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    SAILOR sailor = db.SAILORs.Find(id);
        //    if (sailor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sailor);
        //}






        //
        // POST: /randsailor/Delete/5

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