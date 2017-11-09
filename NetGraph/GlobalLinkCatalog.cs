using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGraph
{
    public class GlobalLinkCatalog : DbContext
    {
        public DbSet<FlagedLink> Links { get; set; }

        public DbSet<string> Domains { get; set; }

    }
}
