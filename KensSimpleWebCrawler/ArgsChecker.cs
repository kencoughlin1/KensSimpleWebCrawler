using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KensSimpleWebCrawler
{
    public class ArgsChecker
    {
        public static bool ValidateConsoleArguments(string[] args)
        {
            if (args != null)
            {
                if (args.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


        public static string ExtractURLFromConsoleArguments(string[] args)
        {
            var URL = string.Empty;
            try
            {
                foreach (var arg in args)
                {
                    if (!string.IsNullOrEmpty(arg))
                    {
                        if (ValidateURL(arg))
                        {
                            URL = arg;
                        }
                        else
                        {
                            Console.Write("The supplied URL is not valid");
                            Program.log.Error("The supplied URL is not valid");
                        }
                    }
                    else
                    {
                        Console.Write("No URL supplied in arguments");
                        Program.log.Error("Empty string or null in arguments so no URL in passed in");
                    }
                }
            }
            catch (Exception ex)
            {
                Program.log.Error("Error Extracting URL from arguments");
                Program.log.Error(ex.Message);
            }
            return URL;
        }

        public static bool ValidateURL(string URL)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(URL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }
    }
}
