using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Layout.Layered;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetGraph
{
    public partial class Graph_diagram : Form
    {
        public GViewer viewer { get; set; }
        public GraphGenerator graphGenerator { get; set; }

        public Graph_diagram(Graph graph, List<FlagedLink> links)
        {
            InitializeComponent();

            viewer = new GViewer();
            viewer.EdgeInsertButtonVisible = false;
            viewer.MouseDoubleClick += new MouseEventHandler((sender, evt) =>
            {
                if (viewer.SelectedObject != null)
                {
                    graphGenerator = new GraphGenerator(links);
                    var childGraph = graphGenerator.GenerateChildGraph((Node)viewer.SelectedObject);
                    Graph_diagram f = new Graph_diagram(childGraph, links);
                    f.ShowDialog();
                }
            });

            foreach (var item in graph.Nodes)
            {
                item.Attr.LabelMargin = 5;
            }

            
            viewer.Graph = null;
            viewer.Graph = graph;
            SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            Controls.Add(viewer);
            ResumeLayout();
        }
    }
}
