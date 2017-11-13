using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetGraph
{
    public class LinkRepository
    {
        public FlagedLink GetLink(string linkURL)
        {
            return GlobalLinkCatalog.Links.Where(a => a.URL == linkURL).FirstOrDefault();
        }

        public List<string> Search(string text)
        {
            return GlobalLinkCatalog.Links.Where(a => a.URL.Contains(text)).Select(b => b.URL).Distinct().ToList();
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

        public List<string> GetAllLinksByDomain(string domain)
        {
            return GlobalLinkCatalog.Links.Where(a => a.Domain == domain).Select(b => b.URL).ToList();
        }

        public void FlushCatalog()
        {
            GlobalLinkCatalog.Links = new List<FlagedLink>();
            GlobalLinkCatalog.Domains = new List<string>();
        }

        public List<string> GetAllDomains()
        {
            return GlobalLinkCatalog.Domains;
        }

        internal void UpdateCode(FlagedLink flagedLink, HttpStatusCode code)
        {
            GlobalLinkCatalog.Links.Find(a => a.URL == flagedLink.URL).Code = code;
        }
    }
}
