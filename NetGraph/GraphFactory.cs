using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Layout.MDS;
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

            SugiyamaLayoutSettings ss = graph.LayoutAlgorithmSettings as SugiyamaLayoutSettings;
            ss.MaxAspectRatioEccentricity = 100;
            ss.FallbackLayoutSettings = new MdsLayoutSettings {  };

            return graph;
        }
    }
}
