using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers {
    public class HomeController : Controller {
        private static VisualizationEntities db = new VisualizationEntities();
        public ActionResult Index() {

            return View(db.Websites.Take(0));
        }

        public PartialViewResult GetResults(object data) {
            return PartialView("Results");
        }

        
        public ActionResult Search(string tagsQuery, string keywordsQuery, string sourcesQuery) {

            List<string> hashtags = new List<string>();
            List<string> sources = new List<string>();
            List<string> keywords = new List<string>();
            if (tagsQuery != null) {
                hashtags = tagsQuery.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()).Select(i => i.ToUpper()).ToList();
            }

            if (sourcesQuery != null) {
                sources = sourcesQuery.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()).ToList();
            }

            if (keywordsQuery != null) {
                keywords = keywordsQuery.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()).Select(i => i.ToUpper()).ToList();
            }


            IQueryable<Website> finalResult = db.Websites;


            if (sources.Count() != 0) {
                var sourcesQueryResult = db.Websites.Where(i => sources.Any(j => i.Url.Contains(j)));
                finalResult = finalResult.Where(i => sourcesQueryResult.Contains(i));
            }

            if (hashtags.Count() != 0) {
                var tweetIDs = db.TweetHashtags.Where(i => hashtags.Contains(i.Hashtag.Tag)).Select(i => i.TweetID);
                var websiteIDs = db.TweetWebsites.Where(i => tweetIDs.Contains(i.TweetID)).Select(i => i.WebsiteID);
                var hashtagqueryresult = db.Websites.Where(i => websiteIDs.Contains(i.ID));
                finalResult = finalResult.Where(i => hashtagqueryresult.Contains(i));
            }

            if (keywords.Count() > 0) {
                var websiteIDs = db.WebsiteKeywords.Where(i => keywords.Contains(i.Keyword.Keyword1)).Select(i => i.WebsiteID);
                var keywordQueryResult = db.Websites.Where(i => websiteIDs.Contains(i.ID));
                finalResult = finalResult.Where(i => keywordQueryResult.Contains(i));
            }

            var finalResult2 = finalResult.Where(i => i.Title != "");

            Dictionary<string, Website> unique = new Dictionary<string, Website>();
            HashSet<string> seenTitles = new HashSet<string>();
            foreach(var r in finalResult2){
                int hitCount = db.TweetWebsites.Where(i => i.Website.Title == r.Title).Count();
                if(unique.ContainsKey(r.Title)){
                    unique[r.Title].HitCount += hitCount;
                } else{
                    r.HitCount = hitCount;
                    r.DisplayUrl = sanitize(r.Url);
                    unique.Add(r.Title, r);
                }    
            }

            return View("Index", unique.OrderByDescending(i =>i.Value.HitCount).Select(i =>i.Value).ToList());
            //return View("Index", finalResult2.ToList().Take(500);
        }

        private string removePrefix(string inputString, string prefix) {
            int prefix1 = inputString.IndexOf(prefix);
            if (prefix1 != -1) {
                return string.Concat(inputString.Skip(prefix1 + prefix.Count()));
            }
            return inputString;
        }

        private string removeSuffix(string inputString, char suffix) {
            int suffix1 = inputString.IndexOf(suffix);
            if (suffix1 != -1) {
                return string.Concat(inputString.Take(suffix1));
            }
            return inputString;
        }

        private string sanitize(string url) {
#if DEBUG
            string initialVal = url;
#endif
            url = removePrefix(url, "http://");
            url = removePrefix(url, "https://");
            url = removePrefix(url, "www.");
            url = removeSuffix(url, '?');
            url = removeSuffix(url, '#');
            var result = url.TrimEnd('/').Trim();
            return result;
        }


        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
