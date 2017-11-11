using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGraph
{
    public class Connection
    {
        public string ParentPage { get; set; }
        public string ChildPage { get; set; }
        public Connection(string parentPage, string childPage)
        {
            ParentPage = parentPage;
            ChildPage = childPage;
        }
    }
}
