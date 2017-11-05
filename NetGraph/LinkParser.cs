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
        public List<FlagedLink> URLs { get; set; }

        public List<string> DomainsList => URLs.Select(a => a.Domain).Distinct().ToList();

        public Form1 Form { get; set; }
        public bool ProcessPaused { get; set; }

        public LinkParser(Form1 form)
        {
            URLs = new List<FlagedLink>();
            URLs.Add(new FlagedLink { URL = form.StartURL });
            Form = form;
        }

        public async Task Analyze(int startURLIndex)
        {
            var StartURL = URLs[startURLIndex].URL;
            if (URLs.Where(a => a.URL == StartURL).Count() > 0)
            {
                using (WebClient client = new WebClient())
                {
                    string html = await client.DownloadStringTaskAsync(new Uri(StartURL));
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);
                    var links = doc.DocumentNode.SelectNodes("//a[@href]");
                    foreach (HtmlNode link in links)
                    {
                        if (!ProcessPaused)
                        {
                            if (Form.MaxNumPages != 0 && Form.MaxNumDomain != 0)
                            {
                                if (URLs.Count < Form.MaxNumPages && DomainsList.Count < Form.MaxNumDomain)
                                {
                                    AddLink(StartURL, link);
                                }
                            }
                            else if (Form.MaxNumPages != 0)
                            {
                                if (URLs.Count < Form.MaxNumPages)
                                {
                                    AddLink(StartURL, link);
                                }
                                else
                                    return;
                            }
                            else if (Form.MaxNumDomain != 0)
                            {
                                if (DomainsList.Count < Form.MaxNumDomain)
                                {
                                    AddLink(StartURL, link);
                                }
                                else
                                    return;
                            }
                        }
                        else {
                            return;
                        }
                    }
                }
            }
        }

        private void AddLink(string StartURL, HtmlNode link)
        {
            URLs.Add(new FlagedLink { URL = link.GetAttributeValue("href", string.Empty), ParentURL = StartURL });
            Form.setPagesText(URLs.Count.ToString());
            Form.setDomainsText(DomainsList.Count.ToString());
        }
    }
}
