using System;
using System.Collections.Generic;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace SecProject.BL
{
    public class Base
    {
        public ISecService SecService { get; set; }
        public StardogConnector Context;
        public Populate pop = new Populate();
        public Base()
        {
            Initialise();
            SecService = new SecService(Context);

            var typeList = pop.PopulateProducts(Context);

            
            //var resultsQuery =
            //    Context.Query("SELECT * WHERE { ?subject rdfs:subClassOf <http://www.semanticweb.org/bobo/ontologies/2015/0/Adriana#Dresses> . ?instance a ?subject . }")
            //        as SparqlResultSet;

            //var ttt = resultsQuery.Results;

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