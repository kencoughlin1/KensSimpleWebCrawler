using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KensSimpleWebCrawler
{
    public class SiteMapWriter:IDisposable
    {

        StreamWriter sw;
        public SiteMapWriter()
        {
            try
            {
                sw = new StreamWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\sitemap.txt");
            }
            catch (Exception ex)
            {
                Program.log.Error("Error opening streamwriter");
                Program.log.Error(ex.Message);
            }
        }

        public void Dispose()
        {
            sw.Close();
            sw.Dispose();
        }

        public void WritePageStatic(HtmlData pageData)
        {

            try
            {
                //Write to a file
                sw.WriteLine("Current Page : " + pageData.Title + "  URL :" + pageData.Url);
                foreach (var line in pageData.AnchorList)
                {
                    sw.WriteLine(line);
                }

                foreach (var line in pageData.ImageList)
                {
                    sw.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Program.log.Error("Error wrting page statistic");
                Program.log.Error(ex.Message);
            }

        }


    }
}
