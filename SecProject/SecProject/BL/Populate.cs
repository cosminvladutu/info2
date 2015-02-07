using System.Collections.Generic;
using System.Linq;
using Common;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace SecProject.BL
{
    public class Populate
    {
        public List<ProductType> PopulateProducts(StardogConnector context)
        {
            var factory = new Factory();
            var productTypeList = new List<ProductType>();

            var resultsQuery =
                context.Query(
                    "PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#>SELECT ?s WHERE {?s rdfs:subClassOf <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#Type> .}")
                    as SparqlResultSet;

            if (resultsQuery != null)
                resultsQuery.Results.ForEach(item =>
                {
                    var link = item["s"];
                    var productResult = context.Query(
                        "PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#>SELECT ?s WHERE {?s rdfs:subClassOf <" +
                        link.ToString() + "> .}") as SparqlResultSet;
                    if (productResult != null && productResult.Results.Count > 2)
                    {
                        var product = factory.CreateProductTypes(productResult.Results, link);
                        if (product != null) productTypeList.Add(product);
                    }
                });

            foreach (var productType in productTypeList)
            {
                foreach (var subCategory in productType.SubCategories)
                {
                    var query = context.Query("SELECT * WHERE { ?subject rdfs:subClassOf <" + subCategory.URI + "> . ?instance a ?subject . }") as SparqlResultSet;
                    subCategory.Products = new List<ProductInstance>();
                    if (query != null)
                        foreach (var res in query.Results)
                        {
                            if (res["instance"].ToString().ToLower() != subCategory.URI.ToLower())
                                subCategory.Products.Add(new ProductInstance
                                {
                                    URI = res["instance"].ToString(),
                                    Name = res["instance"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length)
                                });
                        }
                }
            }
            return productTypeList;
        }

        public void PopulateProductsInfoBySubcategory(StardogConnector context, SubCategory s)
        {
            var queryUrl = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?url WHERE { ?ind t:Has_URL ?url. ?ind rdf:type " + s.URI + " } ") as SparqlResultSet;
            if (queryUrl != null)
                foreach (var res in queryUrl.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p!=null)
                    {
                        p.Url = res["url"].ToString();
                    }
                }
            var queryColours = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?c WHERE { ?ind t:Has_colour ?c. ?ind rdf:type " + s.URI + " } ") as SparqlResultSet;
            if (queryColours != null)
                foreach (var res in queryColours.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        p.Colour.Add(res["c"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
            var querySeason = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?s WHERE { ?ind t:Is_from_season ?s. ?ind rdf:type " + s.URI + " } ") as SparqlResultSet;
            if (querySeason != null)
                foreach (var res in querySeason.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        p.Season.Add(res["s"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
            var queryBrand = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?s WHERE { ?ind t:Is_from ?s. ?ind rdf:type " + s.URI + " } ") as SparqlResultSet;
            if (queryBrand != null)
                foreach (var res in queryBrand.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        p.Brand.Add(res["s"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
            var queryStyle = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?s WHERE { ?ind t:Has_style ?s. ?ind rdf:type " + s.URI + " } ") as SparqlResultSet;
            if (queryStyle != null)
                foreach (var res in queryStyle.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        p.Style.Add(res["s"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
            var queryGender = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?s WHERE { ?ind t:For_gender ?s. ?ind rdf:type " + s.URI + " } ") as SparqlResultSet;
            if (queryGender != null)
                foreach (var res in queryGender.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        p.Gender.Add(res["s"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
        }


    }
}