using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGraph
{
    public static class GlobalLinkCatalog
    {
        public static List<FlagedLink> Links { get; set; } = new List<FlagedLink>();

        public static List<string> Domains { get; set; } = new List<string>();
    }
}
