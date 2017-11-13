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

            return graph;
        }
    }
}