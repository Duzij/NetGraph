using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetGraph
{
    public class FlagedLink
    {
        public string URL { get; set; }
        public string Domain => TextUtils.GetDomain(URL);

        public bool IsRelaviteURL => IsRelativeURL();

        public bool IsSameDomain => (URL.Contains(TextUtils.GetDomain(ParentURL)));

        private bool IsRelativeURL()
        {
            //todo process all kinds of child URls and generate valid links
            if (URL[0] == '/' || URL[0] == '.' || (URL.StartsWith("..")))
                return true;
           
            else
                return false;
        }

        public string ParentURL { get; set; }
        public List<string> ChildLinks { get; set; } = new List<string>();
    }
}