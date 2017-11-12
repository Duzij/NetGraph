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
                    return AddCommonSlashIfNecessary(a.Substring(0, index2));
                }
                else
                {
                    return AddCommonSlashIfNecessary(a);
                }
               
            }
            else
            {
                return "-not-avalible-";
            }

            string AddCommonSlashIfNecessary(string link) {
                if (link[link.Length - 1] == '/')
                    return link;
                else
                    return link + "/";
            }
        }


        public static string CreateChildURL(string domain, string child)
        {

            if (child.StartsWith("/"))
            {
                return "http://" + domain + child.Substring(1, child.Length -1);
            }
            else if (child.StartsWith("./"))
            {
                return "http://" + domain.PadRight(1) + child.Substring(1, child.Length - 1);
            }
            else
            {
                return "http://" + domain + child;
            }
        }
    }
}