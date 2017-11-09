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
        public GraphGenerator()
        {
        }

        public List<Edge> Edges { get; set; } = new List<Edge>();
        public LinkRepository linkRepository { get; set; } = new LinkRepository();

        public Graph graph { get; set; }

        public Graph GenerateGraph()
        {
            graph = GraphFactory.GetGraph();

            foreach (var link in GlobalLinkCatalog.Links)
            {
                Node a = new Node(link.URL) { LabelText = link.URL };
                //we dont want to show child pages on main graph, but we show them only if they don't direct somewhere else
                if (!link.SameAsParentDomain)
                    graph.AddNode(a);

                foreach (var child in link.ChildLinks)
                {
                    if (!linkRepository.GetLink(child).SameAsParentDomain)
                        graph.AddEdge(link.URL, child);
                }

            }

            return graph;
        }

        internal void GenerateEdges(string parentURL)
        {
            var parent = linkRepository.GetLink(parentURL);

            foreach (var child in parent.ChildLinks)
            {
                if (linkRepository.GetLink(child).SameAsParentDomain)
                {
                    Edges.Add(new Edge(parentURL, "", child));
                }
            }
        }

        public Graph GenerateChildGraph(Node parent)
        {
            graph = GraphFactory.GetGraph();

            var parentNode = new Node(parent.LabelText) { LabelText = parent.LabelText };
            graph.AddNode(parentNode);

            var parentLink = linkRepository.GetLink(parentNode.LabelText);

            foreach (var child in parentLink.ChildLinks)
            {
                if (linkRepository.GetLink(child).SameAsParentDomain)
                {
                    graph.AddNode(new Node(child) { LabelText = child });
                    graph.AddEdge(parentLink.URL, "", child);
                }
            }
            return graph;
        }



    }
}