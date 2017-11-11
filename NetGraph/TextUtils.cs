using System;
using System.Text.RegularExpressions;

namespace NetGraph
{
    public static class TextUtils
    {
        public static string GetDomain(string URL)
        {
            Regex r = new Regex(@".(:\/\/).");
            if (r.IsMatch(URL))
            {
                int index = URL.IndexOf("://") + 3;
                string a = URL.Substring(index, URL.Length - index);

                Regex r2 = new Regex(@"./.");
                if (r2.IsMatch(a))
                {
                    int index2 = a.IndexOf("/");
                    return a.Substring(0, index2);
                }
                else
                {
                    return a;
                }
               
            }
            else
            {
                return "-not-avalible-";
            }
        }

        public static string CreateChildURL(string domain, string child)
        {

            if (child.StartsWith("/"))
            {
                return domain + child.Substring(1, child.Length -1);
            }
            else if (child.StartsWith("./"))
            {
                return domain.PadRight(1) + child.Substring(1, child.Length - 1);
            }
            else
            {
                return domain + child;
            }
        }
    }
}