using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Capstone_Wishlist_app.DAL;
using Capstone_Wishlist_app.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Capstone_Wishlist_app.Controllers
{
    public class FamilyController : Controller
    {

        private Entities1 db = new Entities1();
        //
        // GET: /Family/
        public ActionResult Index()
        {
            return View(db.Families.ToList());
        }

        //
        // GET: /Family/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        //
        // GET: /Family/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Family/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Family family)
        {
            if (ModelState.IsValid)
            {
                using (Entities1 db = new Entities1())
                {

                    db.Families.Add(family);
                    try
                    {
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
                    ModelState.Clear();
                    family = null;
                    ViewBag.Message = "Successfully Registration Done";

                }


            }
            return View(family);
        }

        //
        // GET: /Family/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        //
        // POST: /Family/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address1,Address2,City,Country,Zipcode,Phone,Email")] Family family)
        {
            if (ModelState.IsValid)
            {
                db.Entry(family).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(family);
        }

        //
        // GET: /Family/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Family family = db.Families.Find(id);
            if (family == null)
            {
                return HttpNotFound();
            }
            return View(family);
        }

        //
        // POST: /Family/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Family family = db.Families.Find(id);
            db.Families.Remove(family);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public ActionResult Login()
        {
            return View();

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Family family)
        {

            // this action is for handle post (login)
            if(!ModelState.IsValid) //this is check validity
            {
               using(Entities1 dc = new Entities1())
               {
                   var v = dc.Families.Where(a => a.Email.Equals(family.Email)
                       && a.Password.Equals(family.Password)).FirstOrDefault();

                   Session["DBemail"] = family.Email.ToString();
                   Session["DBpassword"] = family.Password.ToString();


                   if(v!= null)
                   {
                       //Session["LogedName"] = v.Name.ToString();
                       return RedirectToAction("Index", new { controller ="Profile", id = v.Id });
                   }

               }
            }
            Session["Errormsg"] = "Invalid Email or Password";
            return View(family);
        }


        




    }
}
