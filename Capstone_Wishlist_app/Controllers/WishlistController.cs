﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using System.Threading.Tasks;
using Capstone_Wishlist_app.Models;
using Capstone_Wishlist_app.DAL;
using Capstone_Wishlist_app.Services;

namespace Capstone_Wishlist_app.Controllers {
    public class WishlistController : Controller {
        private static string AmazonAccessKey {
            get {
                return ConfigurationManager.AppSettings["AWSAccessKeyId"];
            }
        }

        private static string AmazonAssociateTag {
            get {
                return ConfigurationManager.AppSettings["AWSAssociatesId"];
            }
        }

        private WishlistContext _context;
        private IRetailer _retailer;

        public WishlistController()
            : base() {
            _context = new WishlistContext();
            _retailer = new AmazonRetailer(AmazonAssociateTag, AmazonAccessKey, "AWSECommerceServicePort");
        }

        public ActionResult Index(int id) {
            return View();
        }

        public ActionResult FindGifts(int id) {
            return View(new FindGiftsViewModel { WishlistId = id });
        }

        [HttpGet]
        public async Task<ActionResult> SearchItems(int id, ItemCategory category, string keywords) {
            var existingItemIds = await (
                from wi in _context.WishlistItems
                where wi.WishlistId == id
                select wi.ItemId).ToListAsync();
            var items = await _retailer.FindItemsAsync(category, keywords);
            var viewModel = new FindGiftsResultsViewModel {
                WishlistId = id,
                Results = items.ToList(),
                ExistingItemIds = existingItemIds
            };

            return PartialView("_SearchResults", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(int id, string itemId) {
            var isItemOnWishlist = await (
                from wi in _context.WishlistItems
                where wi.WishlistId == id && wi.ItemId == itemId
                select wi
                ).AnyAsync();

            if (isItemOnWishlist) {
                return Json(new { IsOnWishlist = true });
            }

            var wishItem = new WishlistItem {
                WishlistId = id,
                ItemId = itemId,
                Status = WishlistItemStatus.Unapproved,
            };

            _context.WishlistItems.Add(wishItem);
            await _context.SaveChangesAsync();

            return Json(new { IsOnWishlist = true });
        }
    }
}