using System;

namespace NetGraph
{
    public static class TextUtils
    {
        public static string GetDomain(string URL)
        {
            try
            {
                int index = URL.IndexOf("://") + 3;
                string a = URL.Substring(index, URL.Length - index);
                int index2 = a.IndexOf("/");
                return a.Substring(0, index2);
            }
            catch (Exception ex)
            {
                return "-not-avalible-";
            }
        }

    }
}