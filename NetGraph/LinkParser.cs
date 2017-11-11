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
        /// 3.Pro kazdy link predtim, nez ho pridam, tak se musim ujistit, ze uz jsem ho ješně nenavštívil a ze je validni
        /// 4.Kdyz je link validni, tak ho pridam do databaze.
        /// 5.Kdyz uz jsem projel vsechny linky, tak jedu, dokud není FoundLink kolekce prázdná
        /// </summary>

        public LinkRepository linkRepository { get; set; } = new LinkRepository();
        public List<string> VisitedURLs { get; set; } = new List<string>();
        public List<string> FoundURLs { get; set; } = new List<string>();

        public List<Connection> Connections { get; set; } = new List<Connection>();

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

            if (!PageVisited(StartLink) && !InvalidURL(StartLink))
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
                    foreach (HtmlNode link in links)
                    {
                        var URL = link.GetAttributeValue("href", string.Empty);
                        FoundURLs.Add(URL);

                        //avoiding recursive links
                        if (!PageVisited(URL) && !InvalidURL(StartLink))
                        {
                            if (!ProcessPaused)
                            {
                                if (Form.MaxNumPages != 0 && Form.MaxNumDomain != 0)
                                {
                                    if (savedLinks.Count < Form.MaxNumPages && savedDomains.Count < Form.MaxNumDomain)
                                    {
                                        AddLink(StartFlaggedLink, URL);
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
                                        AddLink(StartFlaggedLink, URL);
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
                                        AddLink(StartFlaggedLink, URL);
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                //no filter given
                                else
                                {
                                    AddLink(StartFlaggedLink, URL);
                                }
                            }
                        }
                        else
                        {
                            await Analyze();
                        }
                    }
                }

                else
                {
                    System.Windows.Forms.MessageBox.Show("Error");
                }
            }

            //analyze until all found pages are analyzed
            if (FoundURLs.Count > 0)
            {
                await Analyze();
            }
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

        private void AddLink(FlagedLink parent, string URL)
        {
            var child = new FlagedLink { URL = URL, ParentURL = parent.URL };

            if (child.IsRelaviteURL)
            {
                URL = TextUtils.CreateChildURL(parent.Domain, URL);
                child = new FlagedLink { URL = URL, ParentURL = parent.URL };
            }

            if (child.IsSameDomain)
                parent.ChildLinks.Add(URL);

            linkRepository.AddLink(child);

            Form.setPagesText(GlobalLinkCatalog.Links.Count.ToString());
            Form.setDomainsText(GlobalLinkCatalog.Domains.Count.ToString());
        }

        private bool InvalidURL(string URL)
        {
            return URL.StartsWith("#") || URL == "#" || URL == "" || URL == "/";
        }
        private bool PageVisited(string URL)
        {
            return VisitedURLs.Contains(URL) || VisitedURLs.Contains(URL.Replace("http", "https")) || VisitedURLs.Contains(URL.Replace("https", "http"));
        }
    }
}
