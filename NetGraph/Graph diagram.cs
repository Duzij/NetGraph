using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
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
        public Graph graph;
        public GViewer viewer = new GViewer();
        public Graph_diagram(Graph graph)
        {
            InitializeComponent();

            this.graph = graph;
            InitializeComponent();
            viewer.EdgeInsertButtonVisible = false;
            viewer.MouseDoubleClick += new MouseEventHandler((sender, evt) => {
                if (viewer.SelectedObject != null)
                {
                    var deatiledGraph = new Graph(viewer.SelectedObject.ToString());
                    Node node = (Node)viewer.SelectedObject;
                    Node a = new Node("a") { LabelText = node.LabelText };
                    graph.AddNode(a);
                    Graph_diagram f = new Graph_diagram(deatiledGraph);
                    f.ShowDialog();
                }
            });

            foreach (var item in graph.Nodes)
            {
                item.Attr.LabelMargin = 5;
            }

            viewer.Graph = graph;
            SuspendLayout();
            viewer.Dock = DockStyle.Bottom;
            Controls.Add(viewer);
            ResumeLayout();
        }
    }
}
