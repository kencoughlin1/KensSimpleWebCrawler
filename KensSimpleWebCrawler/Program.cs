using System;
using System.Collections.Generic;
using System.Linq;
using NSoup;
using NSoup.Nodes;
using System.Configuration;
using log4net;
using System.Collections.Specialized;

namespace KensSimpleWebCrawler
{
    class Program
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int Timeout;
        static void Main(string[] args)
        {

            log.Debug("Entered Program Main");
            SiteMapWriter stmapperwriter;
            var URL = string.Empty;
            try
            {
                //set default timeout for url retrievel
                Timeout = 30000;



                //need to take in a single url
                if (ArgsChecker.ValidateConsoleArguments(args))
                {
                    URL = ArgsChecker.ExtractURLFromConsoleArguments(args);
                    if (!string.IsNullOrEmpty(URL))
                    {
                        //create stream writer
                        stmapperwriter = new SiteMapWriter();

                        //showing links to other pages under the same domain
                        var parsedData = ParseURL(URL, stmapperwriter);

                        var uri = new Uri(URL);
                        //instantiate  filters
                        var excludeRoot = new ExcludeRootUriFilter(uri);//filter to stop reparsing root
                        var externalFilter = new ExternalUriFilter(uri);//filter to stop external links being parsed
                        var alreadyVisited = new AlreadyVisitedUriFilter();//filter to not parse already parsed urls

                        //Filter urls to parse based on the above 
                        IEnumerable<Uri> filteredResult = Filter(parsedData.AnchorList, excludeRoot, externalFilter, alreadyVisited);

                        //visit these pages
                        CrawlSubDomains(filteredResult, stmapperwriter, excludeRoot, externalFilter, alreadyVisited);

                        //dispose of stream witer
                        stmapperwriter.Dispose();
                    }
                    else
                    {
                        Console.Write("URL is empty or in an incorrect format");
                        Program.log.Error("URL is empty or in an incorrect format");
                    }
                }
                else
                {
                    Console.Write("No arguments supplied, please pass in a URL to web crawl");
                    Program.log.Error("No arguments supplied, please pass in a URL to web crawl");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Main : " + ex.Message);
            }
        }



        private static HtmlData ParseURL(string URL, SiteMapWriter stmapper)
        {
            //parse this page out put a map with 
            HtmlData parsedData = new HtmlData();
            try
            {
                Document document = GetDocumentFromUrl(URL);

                // Parsing ...
                parsedData.Clear();
                parsedData.Url = document.BaseUri;
                parsedData.RawHtml = document.Html();
                parsedData.Title = document.Title;
                foreach (Element meta in document.Select("meta")) parsedData.MetadataList.Add(meta.OuterHtml());
                foreach (Element image in document.Select("img")) parsedData.ImageList.Add(image.Attr("src"));
                foreach (Element anchor in document.Select("a"))
                {
                    if (anchor.BaseUri.TrimEnd('/') != anchor.Attr("href"))
                    {
                        if (URLS.IsValidUri(anchor.Attr("href")))
                        {
                            parsedData.AnchorList.Add(new Uri(anchor.Attr("href")));
                        }
                        else
                        {
                            log.Info("A href was invalid : " + anchor.Attr("href"));
                        }
                    }
                    else
                    {
                        parsedData.AnchorList.Add(new Uri(anchor.BaseUri));
                    }
                }
                //links to static content such as images
                //and to external URLs (but do not visit them)
                stmapper.WritePageStatic(parsedData);
            }
            catch (Exception ex)
            {
                log.Error("There was a problem retriving URL : " + URL);
                log.Error(ex.Message);
            }
            return parsedData;
        }
       private static Document GetDocumentFromUrl(string URL)
        {
            // Connecting & Fetching ...
            IConnection connection = NSoupClient.Connect(URL);
            connection.Timeout(Timeout);
            Document document = connection.Get();
            return document;
        }


        private static void CrawlSubDomains( IEnumerable<Uri> Result, SiteMapWriter stmapper, params IUriFilter[] filters)
        {
            foreach (var uri in Result)
            {
                //parse urls
                var subarsedData = ParseURL(uri.AbsoluteUri, stmapper);
                //Filter urls
                IEnumerable<Uri> filteredResult = Filter(subarsedData.AnchorList,  filters);
                CrawlSubDomains(filteredResult, stmapper, filters);
            }
        }
 
        private static List<Uri> Filter(IEnumerable<Uri> uris, params IUriFilter[] filters)
        {
            var filtered = uris.ToList();
            foreach (var filter in filters.ToList())
            {
                filtered = filter.Filter(filtered);
            }
            return filtered;
        }
    }
}
