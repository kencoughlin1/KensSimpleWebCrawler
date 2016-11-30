using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KensSimpleWebCrawler
{
    public class ExcludeRootUriFilter : IUriFilter
    {
        private Uri _root;
        public ExcludeRootUriFilter(Uri root)
        {
            _root = root;
        }

        public List<Uri> Filter(IEnumerable<Uri> input)
        {

            var result = input.Where(i => URLS.RemoveWWWfromUrl(i.AbsoluteUri) != URLS.RemoveWWWfromUrl(_root.AbsoluteUri)).ToList();
            return result;

        }
    }
}
