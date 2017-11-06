using System.Collections.Generic;

namespace NetGraph
{
    public class FlagedLink
    {
        public string URL { get; set; }
        public string Domain => TextUtils.GetDomain(URL);
        public bool IsInParentDomain => URL[0] == '#' || URL[0] == '/' ? true : URL.Contains(ParentURL);
        public string ParentURL { get; set; }
        public List<FlagedLink> ChildLinks { get; set; } = new List<FlagedLink>();
    }
}