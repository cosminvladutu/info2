using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SecProject.DAL
{
    public class SecDbContext : DbContext
    {
        public DbSet<UserProductLinkTables> UserProductLink { get; set; }
        public DbSet<UserProfile> UserProfiles{ get; set; }

        public SecDbContext()
            : base("name=DefaultConnection")
        {
           Database.SetInitializer<SecDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProductLinkTables>().ToTable("UserProductLinkTables");
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile");
        }
    }
}