using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;

namespace NetGraph
{
    public class GraphGenerator
    {
        public GraphGenerator(List<FlagedLink> uRLs)
        {
            URLs = uRLs;
        }

        public List<FlagedLink> URLs { get; }

        public Graph graph { get; set; }

        public Graph GenerateGraph()
        {
            graph = new Graph();

            foreach (var link in URLs)
            {
                Node a = new Node(link.URL) { LabelText = link.URL };
                //we dont want to show child pages on main graph, but we show them only if they don't direct somewhere else
                if (!link.IsInParentDomain || link.ChildLinks.Exists(child => child.ChildLinks.Count > 0))
                    graph.AddNode(a);

                foreach (var child in link.ChildLinks)
                {
                    if (!child.IsInParentDomain)
                        graph.AddEdge(link.URL, child.URL);
                }
            }

            return graph;
        }

        public Graph GenerateChildGraph(Node parent)
        {
            graph = new Graph(parent.LabelText, parent.LabelText);
            graph.AddNode(parent);

            var parentLink = URLs.Find(a => a.URL == parent.LabelText);

            foreach (var child in parentLink.ChildLinks)
            {
                if (child.IsInParentDomain)
                    graph.AddEdge(parentLink.URL, child.URL);
            }
            return graph;
        }



    }
}