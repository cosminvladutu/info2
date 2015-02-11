using System.Collections.Generic;
using Common;

namespace SecProject.Models
{
    public class ProductViewModel
    {
        public List<ProductType> ProductTypes { get; set; }
        public ProductOntologyFilterModelViewModel ProductsAndFilters { get; set; }

        public void PopulateProductType(List<ProductType> pt)
        {
            ProductTypes=new List<ProductType>();
            ProductTypes = pt;
            ProductsAndFilters=new ProductOntologyFilterModelViewModel();
            ProductsAndFilters.PopulateModel(pt,"","","","","");
            ProductsAndFilters.PopulateProductListFull(pt, null, "", "", "", "", "");
        }

        public void PopulateWardrobe(List<ProductType> pt)
        {
            ProductTypes = new List<ProductType>();
            ProductTypes = pt;
            ProductsAndFilters = new ProductOntologyFilterModelViewModel();
            ProductsAndFilters.PopulateModel(pt, "", "", "", "", "");
     //       ProductsAndFilters.PopulateProductWardrobe(pt, null, "", "", "", "", "");
            ProductsAndFilters.PopulateProductListFull(pt, null, "", "", "", "", "");
        }
    }
}