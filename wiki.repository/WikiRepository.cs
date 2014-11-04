using System;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Wiki.Repository.Models;

namespace Wiki.Repository
{
    public class WikiRepository
    {
        private const string wikiApiUrl = "http://en.wikipedia.org/w/api.php?action=query&generator=random&prop=extracts|info&format=json";

        /// <summary>
        /// retrieves a random wikipedia article
        /// </summary>
        /// <returns>random article</returns>
        public WikiArticle GetRandomArticle()
        {
            WikiArticle article = null;
            string wikiData = GetWikiData(wikiApiUrl);
            dynamic wikiObject = JsonConvert.DeserializeObject(wikiData);

            foreach(var item in wikiObject.query.pages)
            {
                // this will only occur once;  need a better method
                article = new JavaScriptSerializer().Deserialize<WikiArticle>(item.Value.ToString());
            }
            
            return article;
        }


        /// <summary>
        /// retrieve json data from web service endpoint
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetWikiData(string url)
        {
            string json = string.Empty;

            using (var client = new WebClient())
            {
                json = client.DownloadString(url);
            }

            return json;
        }
 

    }
}
