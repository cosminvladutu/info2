using System.Collections.Generic;
using System.Linq;

namespace SecProject.DAL
{
    public class DALContext
    {
        public UserProfile GetUserProfile(string username)
        {
            using (var dbContext = new SecDbContext())
            {
                return dbContext.UserProfiles.SingleOrDefault(f => f.UserName.ToUpper() == username.ToUpper());
            }
        }

        public void AddToWardrobe(string productName, int userId)
        {
            using (var dbContext = new SecDbContext())
            {
                dbContext.UserProductLink.Add(new UserProductLinkTables { ProductName = productName, UserId = userId });
                dbContext.SaveChanges();
            }
        }

        public List<UserProductLinkTables> GetProductsByUserId(int userId)
        {
            using (var dbContext = new SecDbContext())
            {
                return dbContext.UserProductLink.Where(d => d.UserId == userId).ToList();
            }
        }
    }

}