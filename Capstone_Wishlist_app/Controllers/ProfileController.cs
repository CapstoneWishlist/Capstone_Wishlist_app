using Capstone_Wishlist_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Capstone_Wishlist_app.DAL;
using System.Net;


namespace Capstone_Wishlist_app.Controllers
{
    public class ProfileController : Controller
    {
        private Entities db = new Entities();
        //
        // GET: /Profile/
        public ActionResult Index(int? id)
        {
                var model = from r in db.Children
                            orderby r.FirstName
                            where r.FamilyId == id
                 
                            select r;


                return View(model);
        }

        //signup



        public ActionResult ChildSignup(int? id)
        {
             return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int fid)
        {
            Child child = db.Children.Find(id);
            db.Children.Remove(child);
            db.SaveChanges();
            db.Database.ExecuteSqlCommand("Update Family set ChildAmount = ChildAmount - 1  where Id = {0}", fid);
            return RedirectToAction("Index", new { id = fid });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChildSignup(Child Child)
        {
            if (ModelState.IsValid)
            {
                using (Entities usermanagement = new Entities())
                {
                    string id= this.RouteData.Values["id"].ToString();
                    int fid = Convert.ToInt32(id, 16);
                    Child.FamilyId = fid;
                    usermanagement.Children.Add(Child);
                    usermanagement.SaveChanges();
                    ModelState.Clear();
                    Child = null;
                    Entities1 db = new Entities1();
                    db.Database.ExecuteSqlCommand("Update Family set ChildAmount = ChildAmount + 1  where Id = {0}", fid);
                    ViewBag.Message = "Successfully Registration Done";
                }

            }
            return View(Child);
        }

	}
}