using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace SecProject.BL
{
    public class Base
    {
        public ISecService SecService { get; set; }
        public StardogConnector context;

        public Base()
        {
            Initialise();
            SecService = new SecService(context);
            var list = new SparqlResultSet();
          //var  resultsQuery = context.Query("SELECT DISTINCT ?s WHERE { ?s ?p ?o }") as SparqlResultSet;
          //  resultsQuery.Results.ForEach(item =>
          //  {
          //      var link = item["s"];
          //      SparqlResultSet productResult = new SparqlResultSet();
          //      productResult = context.Query("select ?p ?o WHERE { ?s ?p ?o FILTER (str(?s) = \"" + link.ToString() + "\" ) }") as SparqlResultSet;
          //      //Product product = CreateProduct(productResult.Results);
          //      //products.Add(product);
          //      list = productResult;
          //  });
            var resultsQuery = context.Query("PREFIX t: <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#>SELECT ?subject ?object	WHERE { ?subject t:Has_colour ?object }") as SparqlResultSet;
            resultsQuery.Results.ForEach(item =>
            {
                var link = item["s"];
                SparqlResultSet productResult = new SparqlResultSet();
                productResult = context.Query("select ?p ?o WHERE { ?s ?p ?o FILTER (str(?s) = \"" + link.ToString() + "\" ) }") as SparqlResultSet;
                //Product product = CreateProduct(productResult.Results);
                //products.Add(product);
                list = productResult;
            });
        }

        public void Initialise()
        {
            Dictionary<string, string> configs = AppHelper.ReadConfigs();
            StardogReasoningMode reasoning = StardogReasoningMode.QL;
            Enum.TryParse(configs[AppHelper.StardogReasoningMode], out reasoning);
            context = new StardogConnector(configs[AppHelper.Server], configs[AppHelper.DbName], reasoning,
                                                configs[AppHelper.Username], configs[AppHelper.Password]);
        }


    }
}