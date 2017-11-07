using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout.Layered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGraph
{
    public static class GraphFactory
    {
        public static Graph GetGraph()
        {
            var graph = new Graph();
            var sugiyamaSettings = (SugiyamaLayoutSettings)graph.LayoutAlgorithmSettings;
            sugiyamaSettings.NodeSeparation *= 2;
            return graph;
        }
    }
}
