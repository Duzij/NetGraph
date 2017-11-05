using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;

namespace NetGraph
{
    internal class GraphGenerator
    {
        public GraphGenerator(List<FlagedLink> uRLs)
        {
            URLs = uRLs;
        }

        public List<FlagedLink> URLs { get; }

        public Graph GenerateGraph()
        {
            var graph = new Graph();

            foreach (var link in URLs)
            {
                Node a = new Node(link.URL) { LabelText = link.URL };
                graph.AddNode(a);
            }

            return graph;
        }
    }
}