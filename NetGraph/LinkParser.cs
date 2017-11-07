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
            URLs.Add(new FlagedLink { URL = form.StartURL, ParentURL = "" });
            Form = form;
        }

        public async Task<List<FlagedLink>> Analyze(int startURLIndex)
        {
            var StartURL = URLs[startURLIndex].URL;
            var parentLink = URLs[startURLIndex];
            //avoiding recursive links
            if (URLs.Where(a => a.URL == StartURL).Count() > 0)
            {
                using (WebClient client = new WebClient())
                {
                    string html = await client.DownloadStringTaskAsync(new Uri(StartURL));
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);
                    var links = doc.DocumentNode.SelectNodes("//a[@href]");
                    if (links.Count > 0)
                    {
                        foreach (HtmlNode link in links)
                        {
                            if (!ProcessPaused)
                            {
                                if (Form.MaxNumPages != 0 && Form.MaxNumDomain != 0)
                                {
                                    if (URLs.Count < Form.MaxNumPages && DomainsList.Count < Form.MaxNumDomain)
                                    {
                                        AddLink(parentLink, link);
                                    }
                                }
                                else if (Form.MaxNumPages != 0)
                                {
                                    if (URLs.Count < Form.MaxNumPages)
                                    {
                                        AddLink(parentLink, link);
                                    }
                                    else
                                        return URLs;
                                }
                                else if (Form.MaxNumDomain != 0)
                                {
                                    if (DomainsList.Count < Form.MaxNumDomain)
                                    {
                                        AddLink(parentLink, link);
                                    }
                                    else
                                        return URLs;
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Error");
                    }
                }
            }
            await Analyze(startURLIndex + 1);
            return URLs;
        }

        private void AddLink(FlagedLink parent, HtmlNode link)
        {
            var URL = link.GetAttributeValue("href", string.Empty);
            if (URL.StartsWith("#") || URL == "#" || URL == "" || URL == "/" || URLs.Where(a => a.URL == URL || a.URL.Replace("https","http") == URL).Count() > 0)
            {
                return;
            }
            else
            {
                if (URL.StartsWith("/"))
                    URL = parent.Domain + URL;
                var child = new FlagedLink { URL = URL, ParentURL = parent.URL };
                parent.ChildLinks.Add(child);
                URLs.Add(child);
                Form.setPagesText(URLs.Count.ToString());
                Form.setDomainsText(DomainsList.Count.ToString());
            }
        }
    }
}
