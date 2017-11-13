using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Miscellaneous;
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
        public LinkRepository Repository { get; set; } = new LinkRepository();

        public void HighlightNodes(List<string> nodesId)
        {
            foreach (var item in nodesId)
            {
                var node = viewer.Graph.FindNode(item);
                viewer.Graph.FindNode(item).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
                viewer.Graph.FindNode(item).Label.FontColor = Microsoft.Msagl.Drawing.Color.White;
            }
            viewer.Refresh();
        }

        public void DeHighlightNodes(List<string> nodesId)
        {
            foreach (var item in nodesId)
            {
                var node = viewer.Graph.FindNode(item);
                viewer.Graph.FindNode(item).Attr.FillColor = Microsoft.Msagl.Drawing.Color.White;
                viewer.Graph.FindNode(item).Label.FontColor = Microsoft.Msagl.Drawing.Color.Black;
            }
            viewer.Refresh();
        }

        public Graph_diagram(Graph graph)
        {
            InitializeComponent();

            viewer = new GViewer();
            viewer.EdgeInsertButtonVisible = false;
            viewer.MouseDoubleClick += new MouseEventHandler((sender, evt) =>
            {
                var viewerNode = (Node)viewer.SelectedObject;
                if (viewerNode != null)
                {
                    var detailedForm = new DetailedNodeInfo(viewerNode);
                    detailedForm.Show();
                }
            });

            AdjustFontsAndColorizeDomains(graph);

            viewer.Graph = graph;
            viewer.Dock = DockStyle.Fill;
            viewer.Controls.Add(new Button() { Text = "Text" });
            var settings = new Microsoft.Msagl.Layout.MDS.MdsLayoutSettings() { AdjustScale = true };
            LayoutHelpers.CalculateLayout(graph.GeometryGraph, settings, null);

            Controls.Add(viewer);

            ResumeLayout();
        }

        private void AdjustFontsAndColorizeDomains(Graph graph)
        {
            foreach (var item in graph.Nodes)
            {
                item.Label.FontSize = item.Edges.Count() == 0 ? 5 : item.Edges.Count() / 3 + 5;
                item.Attr.LabelMargin = 5;
            }
            foreach (var domain in Repository.GetAllDomains())
            {
                foreach (var link in Repository.GetAllLinksByDomain(domain))
                {
                    graph.FindNode(link).Attr.FillColor = Colorizer.GetRandomColor();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                SearchForm search = new SearchForm(this);
                search.Show();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
