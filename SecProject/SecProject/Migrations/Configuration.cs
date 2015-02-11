
using System.Web.Security;
using WebMatrix.WebData;

namespace SecProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SecProject.DAL.SecDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SecProject.DAL.SecDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UseProfile", "UserId", "UserName", autoCreateTables: true);
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var roles = (SimpleRoleProvider) Roles.Provider;
            var membership = (SimpleMembershipProvider) Membership.Provider;
            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("cosmin", false) == null)
            {
                membership.CreateUserAndAccount("cosmin", "password");
            }
            //if (!roles.GetRolesForUser("cosmin").Contains("Admin"))
            //{
            //    roles.AddUsersToRoles(new[] { "cosmin" }, new[] { "admin" });
            //}

        }
    }
}
