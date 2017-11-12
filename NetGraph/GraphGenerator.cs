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
            graph = GraphFactory.GetGraph();

            if (Connections != null)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    graph.AddEdge(Connections[i].ParentPage, Connections[i].ChildPage);
                }
            }

            return graph;
        }

        public Graph GenerateChildGraph(Node parent)
        {
            graph = GraphFactory.GetGraph();

            var parentNode = new Node(parent.LabelText) { LabelText = parent.LabelText };
            graph.AddNode(parentNode);

            var parentLink = linkRepository.GetLink(parentNode.LabelText);

            foreach (var child in parentLink.ChildLinks)
            {
                if (linkRepository.GetLink(child).IsRelaviteURL)
                {
                    graph.AddNode(new Node(child) { LabelText = child });
                    graph.AddEdge(parentLink.URL, "", child);
                }
            }
            return graph;
        }



    }
}