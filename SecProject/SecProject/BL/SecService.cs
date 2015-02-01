using SecProject.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace SecProject.BL
{
    public class SecService : ISecService
    {
        private const string accountKey = "uAMtBqnLSpeUC3FC5R9ycQ8opkKdy6QXgMtvgrYqHc8";
        private string rootUrl = "https://api.datamarket.azure.com/Bing/Search";
        SparqlResultSet resultsQuery = new SparqlResultSet();
        SparqlResult singleResult = new SparqlResult();
        private BingSearchContainer bingContainer;
        public StardogConnector Context;

        public SecService(StardogConnector _context)
        {
            bingContainer = new BingSearchContainer(new Uri(rootUrl));
            Context = _context;
        }

        public List<DAL.NewsResult> GetNews()
        {
            List<NewsResult> news = new List<NewsResult>();
            // This is the query expression.

            string query = "fashion";

            string market = "en-us";

            // Get news for science and technology.

            string newsCat = "rt_ScienceAndTechnology";

            // Configure bingContainer to use your credentials.

            bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);

            var newsQuery = bingContainer.News(query, null, market, null, null, null, null, newsCat, null);

            newsQuery = newsQuery.AddQueryOption("$top", 10);

            var newsResults = newsQuery.Execute();

            foreach (var result in newsResults)
            {

                news.Add(result);

            }
            return news;
        }

        public Dictionary<string, string> ReadConfigs()
        {
            Dictionary<string, string> configs = new Dictionary<string, string>();
            try
            {
                configs.Add(AppHelper.Server, ConfigurationManager.AppSettings[AppHelper.Server]);
                configs.Add(AppHelper.DbName, ConfigurationManager.AppSettings[AppHelper.DbName]);
                configs.Add(AppHelper.Password, ConfigurationManager.AppSettings[AppHelper.Password]);
                configs.Add(AppHelper.StardogReasoningMode, ConfigurationManager.AppSettings[AppHelper.StardogReasoningMode]);
                configs.Add(AppHelper.Username, ConfigurationManager.AppSettings[AppHelper.Username]);
            }
            catch (Exception ex)
            {
                
               
            }


            return configs;
        }

        internal object Test()
        {

            resultsQuery = Context.Query("SELECT DISTINCT ?s WHERE { ?s ?p ?o } LIMIT 10") as SparqlResultSet;
                resultsQuery.Results.ForEach(item =>
                {
                    var link = item["s"];
                    SparqlResultSet productResult = new SparqlResultSet();
                    //productResult = Context.Query("select ?p ?o WHERE { ?s ?p ?o FILTER (str(?s) = \"" + link.ToString() + "\" ) }") as SparqlResultSet;
                    //Product product = CreateProduct(productResult.Results);
                    //products.Add(product);

                });
                return null;
        }
    }
}