using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetGraph
{
    public class FlagedLink
    {
        public string URL { get; set; }
        public string Domain => TextUtils.GetDomain(URL);
        public bool SameAsParentDomain => ChildDetection();

        private bool ChildDetection()
        {
            if (URL[0] == '/' || URL[0] == '.' || (URL.StartsWith("..")))
                return true;
            else if (URL.Contains(TextUtils.GetDomain(ParentURL)))
                return true;
            else
                return false;
        }

        public string ParentURL { get; set; }
        public List<FlagedLink> ChildLinks { get; set; } = new List<FlagedLink>();
    }
}