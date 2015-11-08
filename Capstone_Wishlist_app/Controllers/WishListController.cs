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

namespace Capstone_Wishlist_app.Controllers
{
    public class WishListController : Controller
    {
        private WishListEntities db = new WishListEntities();
        //
        // GET: /WishList/
        public ActionResult Index()
        {
            return View(db.WishLists.ToList());
        }

  /*      public ActionResult Index()
        {
            string familyid = User.Identity.GetUserId().ToString();
            var model = from r in db.Children
                        orderby r.FirstName
                        where r.FamilyId == familyid
                        select r;
            return View(model.ToList());
        }

*/



        public ActionResult CreateWishList()
        {
            return View();
        }


        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishlist = db.WishLists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishlist = db.WishLists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            WishList wishlist = db.WishLists.Find(id);
            db.WishLists.Remove(wishlist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
	}
}