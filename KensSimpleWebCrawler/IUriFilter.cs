using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KensSimpleWebCrawler
{
    public interface IUriFilter
    {
            List<Uri> Filter(IEnumerable<Uri> input);
    }
}
