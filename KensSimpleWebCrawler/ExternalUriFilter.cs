using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainParser.Library;

namespace KensSimpleWebCrawler
{
    public class ExternalUriFilter : IUriFilter
    {
        private DomainName _rootDomainName;
        public ExternalUriFilter(Uri root)
        {
 
            DomainName.TryParse(root.Authority, out _rootDomainName);
   
        }



        public List<Uri> Filter(IEnumerable<Uri> input)
        {
            var result = input.Where(i => IsExternalUrl(i))
                .ToList();
            return result;
        }

        private bool IsExternalUrl(Uri value)
        {
            DomainName outDomain;
            DomainName.TryParse(value.Authority, out outDomain);
            if ((outDomain.Domain + outDomain.TLD) == (_rootDomainName.Domain + outDomain.TLD))
            { return true; }
            else
            { return false; }

        }
    }
}
