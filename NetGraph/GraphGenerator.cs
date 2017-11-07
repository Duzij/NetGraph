using System;
using System.Collections.Generic;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout.Layered;
using System.Threading.Tasks;

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
            graph = GraphFactory.GetGraph();

            foreach (var link in URLs)
            {
                Node a = new Node(link.URL) { LabelText = link.URL };
                //we dont want to show child pages on main graph, but we show them only if they don't direct somewhere else
                if (!link.IsInParentDomain || link.ChildLinks.Count > 0)
                    graph.AddNode(a);

                foreach (var child in link.ChildLinks)
                {
                    if (!child.IsInParentDomain)
                        graph.AddEdge(link.URL, child.URL);
                }

            }
            SetMaxItemsOnLayer(4);

            return graph;
        }

        public void SetMaxItemsOnLayer(int items)
        {
            var list = new List<Node>(graph.Nodes);
            //we dont want to set parent to same level as children
            list.RemoveAt(0);

            for (int i = 0; i < list.Count; i = i + items)
            {
                Node[] array = new Node[items];
                for (int a = 0; a < array.Length; a++)
                {
                    if ((i + a) < list.Count)
                        array[a] = graph.FindNode(list[i + a].LabelText);
                }
                graph.LayerConstraints.AddSameLayerNeighbors(array);
            }
        }

        public Graph GenerateChildGraph(Node parent)
        {
            graph = GraphFactory.GetGraph();

            var parentNode = new Node(parent.LabelText) { LabelText = parent.LabelText };
            graph.AddNode(parentNode);

            var parentLink = URLs.Find(a => a.URL == parent.LabelText);

            foreach (var child in parentLink.ChildLinks)
            {
                if (child.IsInParentDomain)
                {
                    graph.AddNode(new Node(child.URL) { LabelText = child.URL });
                    graph.AddEdge(parentLink.URL, "", child.URL);
                }
            }
            return graph;
        }



    }
}