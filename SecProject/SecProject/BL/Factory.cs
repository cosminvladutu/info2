using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using VDS.RDF;
using VDS.RDF.Query;

namespace SecProject.BL
{
    public class Factory
    {
        public ProductType CreateProductTypes(List<SparqlResult> results, INode link)
        {
            var productType = new ProductType();
            if (!link.ToString().Contains("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#") && link.ToString() != "http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#Type") return null;
            productType.URI = link.ToString();
            productType.CategoryName = link.ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length);
            productType.SubCategories = new List<SubCategory>();

            foreach (var result in results.Where(result => result.FirstOrDefault().Value.ToString().Contains("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#")))
            {
                if (String.Equals(result.FirstOrDefault().Value.ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length),
                    productType.CategoryName, StringComparison.CurrentCultureIgnoreCase)) continue;
                productType.SubCategories.Add(new SubCategory
                {
                    SubCategoryName = result.FirstOrDefault().Value.ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length),
                    URI = result.FirstOrDefault().Value.ToString()
                });
            }

            return productType;
        }
    }
}