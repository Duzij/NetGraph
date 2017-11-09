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
            //todo process all kinds of child URls and generate valid links
            if (URL[0] == '/' || URL[0] == '.' || (URL.StartsWith("..")))
                return true;
            else if (URL.Contains(TextUtils.GetDomain(ParentURL)))
                return true;
            else
                return false;
        }

        public static bool operator ==(FlagedLink link1, FlagedLink link2)
        {
            return (link1.URL == link2.URL && link2.ParentURL == link1.ParentURL);
        }

        public static bool operator !=(FlagedLink link1, FlagedLink link2)
        {
            return (link1.URL != link2.URL && link2.ParentURL != link1.ParentURL);
        }

        public string ParentURL { get; set; }
        public List<FlagedLink> ChildLinks { get; set; } = new List<FlagedLink>();
    }
}