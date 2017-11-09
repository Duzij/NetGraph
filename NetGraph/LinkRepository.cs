using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGraph
{
    public class LinkRepository
    {
        public FlagedLink GetLink(string linkURL)
        {
            using (var c = new GlobalLinkCatalog())
            {
                var link = c.Links.Find(linkURL);
                if (link != null)
                    return link;
                else
                    throw new KeyNotFoundException("Link was not found");
            }
        }

        public void AddLink(FlagedLink link)
        {
            using (var c = new GlobalLinkCatalog())
            {
                var domain = link.Domain;
                if (!GetAllDomains().Contains(domain))
                {
                    c.Domains.Add(domain);
                }

                if (!GetAllLinks().Contains(link))
                    c.Links.Add(link);
            }
        }

        public List<FlagedLink> GetAllLinks()
        {
            using (var c = new GlobalLinkCatalog())
            {
                return c.Links.ToList();
            }
        }

        public List<string> GetAllDomains()
        {
            using (var c = new GlobalLinkCatalog())
            {
                return c.Domains.ToList();
            }
        }

    }
}
