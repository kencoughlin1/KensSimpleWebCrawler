using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KensSimpleWebCrawler
{
    public class HtmlData
    {

            public string Url { get; set; }

            public string RawHtml { get; set; }

            public string Title { get; set; }

            public List<string> MetadataList { get; set; }

            public List<Uri> AnchorList { get; set; }

            public List<string> ImageList { get; set; }



            public HtmlData()
            {
                MetadataList = new List<string>();
                AnchorList = new List<Uri>();
                ImageList = new List<string>();
            }

            public void Clear()
            {
                Url = null;
                RawHtml = null;
                Title = null;
                MetadataList.Clear();
                AnchorList.Clear();
                ImageList.Clear();
            }
        }
    }
