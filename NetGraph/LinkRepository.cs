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
            return GlobalLinkCatalog.Links.Where(a => a.URL == linkURL).FirstOrDefault();
            //var link = GlobalLinkCatalog.Links.Where(a => a.URL == linkURL).FirstOrDefault();
            //    if (link != null)
            //        return link;
            //    else
            //        throw new KeyNotFoundException("Link was not found");
        }

        public List<string> Search(string text)
        {
            return GlobalLinkCatalog.Links.Where(a => a.URL.Contains(text)).Select(b => b.URL).ToList();
        }

        public void AddLink(FlagedLink link)
        {
            var domain = link.Domain;
            if (!GlobalLinkCatalog.Domains.Contains(domain))
            {
                GlobalLinkCatalog.Domains.Add(domain);
            }

            if (!GlobalLinkCatalog.Links.Contains(link))
                GlobalLinkCatalog.Links.Add(link);
        }

    }
}
