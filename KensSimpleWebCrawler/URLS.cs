using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KensSimpleWebCrawler
{
    public class URLS
    {
        

        public static string RemoveWWWfromUrl(string domainUrl)
        {
            var domain = new Uri(domainUrl).AbsoluteUri.Replace("/www.", "/");

            return domain;
        }


        public static  bool IsValidUri(string uri)
        {

            Uri uriResult;
            bool result = Uri.TryCreate(uri, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps) ;

            return result;
        }


    }
}
