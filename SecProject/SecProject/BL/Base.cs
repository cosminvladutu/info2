using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace SecProject.BL
{
    public class Base
    {
        public ISecService SecService { get; set; }
        public StardogConnector Context;
        public Populate pop = new Populate();
        public List<ProductType> ProductTypes;
        public Base()
        {
            Initialise();
            SecService = new SecService(Context);
            pop.InitializeContext(Context);
            ProductTypes = pop.PopulateProducts(Context);
            foreach (var categ in ProductTypes)
            {
                foreach (var sub in categ.SubCategories)
                {
                    pop.PopulateProductsInfoBySubcategory(Context, sub);
                }
            }
            
            //var resultsQuery =
            //    Context.Query("SELECT * WHERE { ?subject rdfs:subClassOf <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#Dresses> . ?instance a ?subject . }")
            //        as SparqlResultSet;

            //var ttt = resultsQuery.Results;

        }

        public List<ProductType> ReturnProductTypes()
        {
            if (ProductTypes!=null && ProductTypes.SingleOrDefault(f=>f.CategoryName.ToUpper()=="TYPE")!=null)
            {
                var t = ProductTypes.SingleOrDefault(f => f.CategoryName.ToUpper() == "TYPE");
                ProductTypes.Remove(t);
            }
            return ProductTypes;
        }

        public void Initialise()
        {
            Dictionary<string, string> configs = AppHelper.ReadConfigs();
            StardogReasoningMode reasoning = StardogReasoningMode.QL;
            Enum.TryParse(configs[AppHelper.StardogReasoningMode], out reasoning);
            Context = new StardogConnector(configs[AppHelper.Server], configs[AppHelper.DbName], reasoning,
                                                configs[AppHelper.Username], configs[AppHelper.Password]);
        }


    }
}