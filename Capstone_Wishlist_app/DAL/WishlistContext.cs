using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Capstone_Wishlist_app.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Capstone_Wishlist_app.DAL
{
    public class WishlistContext : DbContext
    {

        public WishlistContext() : base("WishlistContext") { }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Capstone_Wishlist_app.Models.Child> Children { get; set; }

        public System.Data.Entity.DbSet<Capstone_Wishlist_app.Models.WishList> WishLists { get; set; }

        public System.Data.Entity.DbSet<Capstone_Wishlist_app.Models.Family> Families { get; set; }
    }
}