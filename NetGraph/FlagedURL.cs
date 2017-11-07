using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetGraph
{
    public class FlagedLink
    {
        public string URL { get; set; }
        public string Domain => TextUtils.GetDomain(URL);
        public bool IsInParentDomain => URL[0] == '#' || URL[0] == '/' ? true : CheckForChildWithSSL();

        private bool CheckForChildWithSSL()
        {
            Regex r = new Regex("^https?://");
            if (URL.Contains(ParentURL))
                { return true; }
            else if (r.IsMatch(URL))
            {
                return URL.Replace("http", "https").Contains(ParentURL);
            }
            return false;
        }

        public string ParentURL { get; set; }
        public List<FlagedLink> ChildLinks { get; set; } = new List<FlagedLink>();
    }
}