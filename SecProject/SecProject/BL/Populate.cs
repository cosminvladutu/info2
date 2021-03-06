﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace SecProject.BL
{
    public class Populate
    {
        private static StardogConnector myContext;

        public void InitializeContext(StardogConnector c)
        {
            myContext = c;
        }

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
            var queryUrl = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?url WHERE { ?ind t:Has_URL ?url. ?ind rdf:type t:" + s.SubCategoryName + " } ") as SparqlResultSet;
            if (queryUrl != null)
                foreach (var res in queryUrl.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p!=null)
                    {
                        p.Url = res["url"].ToString();
                    }
                }
            var queryBuyUrl = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?url WHERE { ?ind t:Has_buy_url ?url. ?ind rdf:type t:" + s.SubCategoryName + " } ") as SparqlResultSet;
            if (queryBuyUrl != null)
                foreach (var res in queryBuyUrl.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        p.BuyUrl = res["url"].ToString();
                    }
                }
            var queryColours = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?c WHERE { ?ind t:Has_colour ?c. ?ind rdf:type t:" + s.SubCategoryName + " } ") as SparqlResultSet;
            if (queryColours != null)
                foreach (var res in queryColours.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        if (p.Colour==null)
                        {
                            p.Colour=new List<string>();
                        }
                        p.Colour.Add(res["c"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
            var querySeason = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?s WHERE { ?ind t:Is_from_season ?s. ?ind rdf:type t:" + s.SubCategoryName + " } ") as SparqlResultSet;
            if (querySeason != null)
                foreach (var res in querySeason.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        if (p.Season==null)
                        {
                            p.Season=new List<string>();
                        }
                        p.Season.Add(res["s"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
            var queryBrand = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?s WHERE { ?ind t:Is_from ?s. ?ind rdf:type t:" + s.SubCategoryName + " } ") as SparqlResultSet;
            if (queryBrand != null)
                foreach (var res in queryBrand.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        if (p.Brand==null)
                        {
                            p.Brand=new List<string>();
                        }
                        p.Brand.Add(res["s"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
            var queryStyle = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?s WHERE { ?ind t:Has_style ?s. ?ind rdf:type t:" + s.SubCategoryName + " } ") as SparqlResultSet;
            if (queryStyle != null)
                foreach (var res in queryStyle.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        if (p.Style==null)
                        {
                            p.Style=new List<string>();
                        }
                        p.Style.Add(res["s"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
            var queryGender = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?ind ?s WHERE { ?ind t:For_gender ?s. ?ind rdf:type t:" + s.SubCategoryName + " } ") as SparqlResultSet;
            if (queryGender != null)
                foreach (var res in queryGender.Results)
                {
                    var p = s.Products.FirstOrDefault(f => f.URI.ToLower() == res["ind"].ToString().ToLower());
                    if (p != null)
                    {
                        if (p.Gender==null)
                        {
                            p.Gender=new List<string>();
                        }
                        p.Gender.Add(res["s"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length));
                    }
                }
        }



        public Dictionary<string, string> PopulateAllBrands()
        {
            var dict = new Dictionary<string, string>();
                var queryUrl = myContext.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?y WHERE { ?x rdfs:subClassOf* t:Brand .  ?y rdf:type ?x .  }") as SparqlResultSet;
            if (queryUrl == null) return dict;
            foreach (var aux in queryUrl.Results.Select(res => res["y"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length)))
            {
                dict.Add(aux,aux);
            }
            return dict;
        }

        public Dictionary<string, string> PopulateAllColours()
        {
            var dict = new Dictionary<string, string>();
            var queryUrl = myContext.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?y WHERE { ?x rdfs:subClassOf* t:Colour .  ?y rdf:type ?x .  }") as SparqlResultSet;
            if (queryUrl == null) return dict;
            foreach (var aux in queryUrl.Results.Select(res => res["y"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length)).Where(aux => aux.ToUpper() != "FEMALE"))
            {
                dict.Add(aux, aux);
            }
            return dict;
        }

        public Dictionary<string, string> PopulateAllGenders()
        {
            var dict = new Dictionary<string, string>();
            var queryUrl = myContext.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?y WHERE { ?x rdfs:subClassOf* t:Gender .  ?y rdf:type ?x .  }") as SparqlResultSet;
            if (queryUrl == null) return dict;
            foreach (var aux in queryUrl.Results.Select(res => res["y"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length)))
            {
                dict.Add(aux, aux);
            }
            return dict;
        }

        public Dictionary<string, string> PopulateAllSeasons()
        {
            var dict = new Dictionary<string, string>();
            var queryUrl = myContext.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?y WHERE { ?x rdfs:subClassOf* t:Season .  ?y rdf:type ?x .  }") as SparqlResultSet;
            if (queryUrl == null) return dict;
            foreach (var aux in queryUrl.Results.Select(res => res["y"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length)))
            {
                dict.Add(aux, aux);
            }
            return dict;
        }

        public Dictionary<string, string> PopulateAllStyles()
        {
            var dict = new Dictionary<string, string>();
            var queryUrl = myContext.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#> SELECT ?y WHERE { ?x rdfs:subClassOf* t:Style .  ?y rdf:type ?x .  }") as SparqlResultSet;
            if (queryUrl == null) return dict;
            foreach (var aux in queryUrl.Results.Select(res => res["y"].ToString().Substring("http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#".Length)))
            {
                dict.Add(aux, aux);
            }
            return dict;
        }
    }
}