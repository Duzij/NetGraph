using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetGraph
{
    public class LinkParser
    {
        /// <summary>
        /// Algoritmus prace parseru
        /// 0.Pridam jako prvni found link vlastne ten muj prvni
        /// 1.Dostanu URL jako prvni URL listu FoundLinks.
        /// 1.5 pridam tenhle link do VisitedLinks a vymazu z FoundLinks
        /// 2.Nactu dom a vytahnu vsechny linky.
        /// 3.Pro kazdy link predtim, nez ho pridam, tak se musim ujistit, ze uz neni prebytecny a ze je validni
        /// 4.Kdyz je link validni, tak ho pridam do databaze.
        /// 5.Kdyz uz jsem projel vsechny linky, tak jedu znova
        /// </summary>

        public LinkRepository linkRepository { get; set; } = new LinkRepository();
        public List<string> VisitedURLs { get; set; } = new List<string>();
        public List<string> FoundURLs { get; set; } = new List<string>();

        public Form1 Form { get; set; }
        public bool ProcessPaused { get; set; }

        public LinkParser(Form1 form)
        {
            linkRepository.AddLink(new FlagedLink { URL = form.StartURL, ParentURL = "" });
            FoundURLs.Add(form.StartURL);
            Form = form;
        }

        public async Task Analyze()
        {
            var StartLink = FoundURLs[0];
            var StartFlaggedLink = linkRepository.GetLink(StartLink);

            //avoiding recursive links
            if (!VisitedURLs.Contains(StartLink))
            {
                var links = new List<HtmlNode>();
                links = await GetAllLinksFromWebsite(StartLink, links);
                if (links?.Any() ?? false)
                {
                    //tady jsme si jisti, ze jsme web navstivili
                    var savedLinks = GlobalLinkCatalog.Links;
                    var savedDomains = GlobalLinkCatalog.Domains;
                    VisitedURLs.Add(StartLink);
                    FoundURLs.Remove(StartLink);
                    FoundURLs.AddRange(links.Sele));
                    foreach (HtmlNode link in links)
                    {
                        if (!ProcessPaused)
                        {
                            if (Form.MaxNumPages != 0 && Form.MaxNumDomain != 0)
                            {
                                if (savedLinks.Count < Form.MaxNumPages && savedDomains.Count < Form.MaxNumDomain)
                                {
                                    AddLink(StartFlaggedLink, link);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else if (Form.MaxNumPages != 0)
                            {
                                if (savedLinks.Count < Form.MaxNumPages)
                                {
                                    AddLink(StartFlaggedLink, link);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else if (Form.MaxNumDomain != 0)
                            {
                                if (GlobalLinkCatalog.Domains.Count < Form.MaxNumDomain)
                                {
                                    AddLink(StartFlaggedLink, link);
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("Error");
                }
            }
            await Analyze();
        }

        private static async Task<List<HtmlNode>> GetAllLinksFromWebsite(string StartLink, List<HtmlNode> links)
        {
            using (WebClient client = new WebClient())
            {
                string html = await client.DownloadStringTaskAsync(new Uri(StartLink));
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                links = doc.DocumentNode.SelectNodes("//a[@href]").ToList();
            }
            return links;
        }

        private void AddLink(FlagedLink parent, HtmlNode link)
        {
            var URL = link.GetAttributeValue("href", string.Empty);
            if (IsInvalidURL(URL))
            {
                return;
            }
            else
            {
                if (URL.StartsWith("/"))
                    URL = parent.Domain + URL;
                var child = new FlagedLink { URL = URL, ParentURL = parent.URL };
                parent.ChildLinks.Add(child.URL);
                linkRepository.AddLink(child);

                Form.setPagesText(GlobalLinkCatalog.Links.Count.ToString());
                Form.setDomainsText(GlobalLinkCatalog.Domains.Count.ToString());
            }
        }

        private bool IsInvalidURL(string URL)
        {
            return URL.StartsWith("#") || URL == "#" || URL == "" || URL == "/" || GlobalLinkCatalog.Links.Where(a => a.URL == URL || a.URL.Replace("https", "http") == URL).Count() > 0;
        }
    }
}
