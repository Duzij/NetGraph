using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout.Layered;
using System.Threading.Tasks;
using System.Linq;

namespace NetGraph
{
    public class GraphGenerator
    {
        public GraphGenerator(List<Connection> connnections = null)
        {
            Connections = connnections;
        }

        public List<Edge> Edges { get; set; } = new List<Edge>();
        public LinkRepository linkRepository { get; set; } = new LinkRepository();

        public Graph graph { get; set; }
        public List<Connection> Connections { get; }

        public Graph GenerateGraph()
        {
            graph = new Graph();

            CreateConnections();
            ColorizeNodes();
            AdjustNodeLayout();

            return graph;
        }

        private void AdjustNodeLayout()
        {
            foreach (var item in graph.Nodes)
            {
                item.Label.FontSize = item.Edges.Count() == 0 ? 5 : item.Edges.Count() / 3 + 5;
                item.Attr.LabelMargin = 5;
            }
        }

        private void ColorizeNodes()
        {
            foreach (var domain in linkRepository.GetAllDomains())
            {
                var color = Colorizer.GetRandomColor();
                foreach (var link in linkRepository.GetAllLinksByDomain(domain))
                {
                    if (linkRepository.GetLink(link).Code != System.Net.HttpStatusCode.Accepted && linkRepository.GetLink(link).Code != 0)
                    {
                        graph.FindNode(link).Attr.FillColor = Color.Red;
                        graph.FindNode(link).Label.FontColor = Color.White;
                    }
                    else
                    {
                        graph.FindNode(link).Attr.FillColor = color;
                    }
                }
            }
        }

        private void CreateConnections()
        {
            if (Connections != null)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    try
                    {
                        graph.AddEdge(Connections[i].ParentPage, Connections[i].ChildPage);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}