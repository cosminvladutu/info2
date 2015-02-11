using System.Collections.Generic;
using System.Linq;

namespace SecProject.DAL
{
    public class DALContext
    {
        public UserProfile GetUserId(string username)
        {
            using (var dbContext = new SecDbContext())
            {
                return dbContext.UserProfiles.SingleOrDefault(f => f.UserName.ToUpper() == username.ToUpper());
            }
        }

        public void AddToWordrobe(string productName, int userId)
        {
            using (var dbContext = new SecDbContext())
            {
                dbContext.UserProductLink.Add(new UserProductLinkTable { ProductName = productName, UserId = userId });
            }
        }

        public List<UserProductLinkTable> GetProductsByUserId(int userId)
        {
            using (var dbContext = new SecDbContext())
            {
                return dbContext.UserProductLink.Where(d => d.UserId == userId).ToList();
            }
        }
    }

}