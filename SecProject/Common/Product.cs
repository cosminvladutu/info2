using System.Collections.Generic;

namespace Common
{
    public class ProductType
    {
        public string CategoryName { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public string URI { get; set; }
    }

    public class SubCategory
    {
        public string SubCategoryName { get; set; }
        //public List<Product> Products { get; set; }
        public string URI{ get; set; }

        public List<ProductInstance> Products { get; set; }
    }

    public class ProductInstance
    {
        public string Name { get; set; }
        public string URI { get; set; }
        public List<string> Colour { get; set; }
        public string Url { get; set; }
        public List<string> Brand { get; set; }
        public List<string> Gender { get; set; }
        public List<string> Season { get; set; }
        public List<string> Style { get; set; }
    }

}
