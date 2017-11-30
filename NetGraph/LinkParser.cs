using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            if (!ProcessPaused)
            {
                try
                {
                    if (!PageVisited(StartLink) && !InvalidURL(StartLink))
                    {
                        var links = new List<string>();
                        try
                        {
                            links = await GetAllLinksFromWebsite(StartLink);
                        }
                        catch (WebException ex)
                        {
                            linkRepository.UpdateCode(StartFlaggedLink, ((HttpWebResponse)ex.Response).StatusCode);
                            Connections.Add(new Connection(StartLink, StartFlaggedLink.ParentURL));
                        }

                        if (links?.Any() ?? false)
                        {
                            //tady jsme si jisti, ze jsme web navstivili
                            var savedLinks = GlobalLinkCatalog.Links;
                            var savedDomains = GlobalLinkCatalog.Domains;
                            VisitedURLs.Add(StartLink);
                            FoundURLs.RemoveAll(a => a == StartLink);
                            foreach (string link in links)
                            {
                                var childLink = FormatChildLink(StartFlaggedLink, link);

                                Connections.Add(new Connection(StartLink, childLink.URL));
                                childLink.Code = HttpStatusCode.Accepted;

                                //avoiding recursive links
                                if (!VisitedURLs.Contains(childLink.URL + "/") && !PageVisited(childLink.URL) && !InvalidURL(StartLink))
                                {

                                    if (Form.MaxNumPages != 0 && Form.MaxNumDomain != 0)
                                    {
                                        if (savedLinks.Count < Form.MaxNumPages && savedDomains.Count < Form.MaxNumDomain)
                                        {
                                            AddLink(StartFlaggedLink, childLink);
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
                                            AddLink(StartFlaggedLink, childLink);
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
                                            AddLink(StartFlaggedLink, childLink);
                                        }
                                        else
                                        {
                                            return;
                                        }
                                    }
                                    //no filter given
                                    else
                                    {
                                        AddLink(StartFlaggedLink, childLink);
                                    }
                                }
                            }
                        }
                        else
                        {
                            FoundURLs.RemoveAll(a => a == StartLink);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
            else
            {
                return;
            }

            //analyze until all found pages are analyzed
            if (FoundURLs.Count > 0)
            {
                await Analyze();
            }
        }

        private async Task<List<string>> GetAllLinksFromWebsite(string StartLink)
        {
            using (WebClient client = new WebClient())
            {
                string html = "";
                html = await client.DownloadStringTaskAsync(new Uri(StartLink));

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                var list = doc.DocumentNode.SelectNodes("//a[@href]");
                if (list?.Any() ?? false)
                {
                    var UrlList = list.Select(a => a.GetAttributeValue("href", string.Empty)).ToList();

                    foreach (var item in UrlList.ToList())
                    {
                        if (UrlList.Contains(StartLink.Substring(0, StartLink.Length - 1)) ||
                            UrlList.Contains(StartLink + "/") || UrlList.Contains(StartLink) ||
                            UrlList.Contains(StartLink.Replace("http", "https")) ||
                            UrlList.Contains(StartLink.Replace("https", "http")) ||
                            InvalidURL(item))
                        {
                            UrlList.Remove(item);
                        }
                    }
                    return UrlList.Distinct().ToList();
                }
                return new List<string>();
            }
        }

        private void AddLink(FlagedLink parent, FlagedLink child)
        {
            if (child.IsSameDomain)
                parent.ChildLinks.Add(child.URL);

            FoundURLs.Add(child.URL);
            linkRepository.AddLink(child);

            Form.setPagesText(GlobalLinkCatalog.Links.Count.ToString());
            Form.setDomainsText(GlobalLinkCatalog.Domains.Count.ToString());
        }

        private FlagedLink FormatChildLink(FlagedLink parent, string URL)
        {
            var child = new FlagedLink { URL = URL, ParentURL = parent.URL };

            //relative links only
            if (child.IsRelaviteURL || child.HasNoDomain)
            {
                URL = TextUtils.CreateChildURL(parent.Domain, URL);
                child = new FlagedLink { URL = URL, ParentURL = parent.URL };
            }

            return child;
        }

        private bool InvalidURL(string URL)
        {
            return URL.StartsWith("#") || URL == "#" || URL == "" || URL == "/";
        }
        private bool PageVisited(string URL)
        {
            return VisitedURLs.Contains(URL.Substring(0, URL.Length - 1)) ||
                VisitedURLs.Contains(URL) ||
                VisitedURLs.Contains(URL.Replace("http", "https")) ||
                VisitedURLs.Contains(URL.Replace("https", "http"));
        }

    }
}
