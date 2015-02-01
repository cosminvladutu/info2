using System.Collections.Generic;
using Common;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace SecProject.BL
{
    public class Populate
    {
        public List<ProductType> PopulateProducts(StardogConnector Context)
        {
            var factory = new Factory();
            var productTypeList = new List<ProductType>();

            var resultsQuery =
                Context.Query(
                    "PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#>SELECT ?s WHERE {?s rdfs:subClassOf <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#Type> .}")
                    as SparqlResultSet;

            resultsQuery.Results.ForEach(item =>
            {
                var link = item["s"];
                SparqlResultSet productResult = new SparqlResultSet();
                productResult =
                    Context.Query(
                        "PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#>SELECT ?s WHERE {?s rdfs:subClassOf <" +
                        link.ToString() + "> .}") as SparqlResultSet;
                if (productResult.Results.Count>2)
                {
                    ProductType product = factory.CreateProductTypes(productResult.Results, link);
                    productTypeList.Add(product);    
                }
                
            });

            foreach (var productType in productTypeList)
            {
                foreach (var subCategory in productType.SubCategories)
                {
                var query = Context.Query("SELECT * WHERE { ?subject rdfs:subClassOf <"+subCategory.URI+"> . ?instance a ?subject . }")as SparqlResultSet;
                    subCategory.Products=new List<ProductInstance>();
                foreach (var res in query.Results)
                {
                   if(res["instance"].ToString().ToLower()!=subCategory.URI.ToLower())
                    subCategory.Products.Add(new ProductInstance
                    {
                        URI = res["instance"].ToString(),
                        Name = res["instance"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length)
                    });
                } }
            }
          

          //  var ttt = query.Results;



            return productTypeList;
        }
    }
}