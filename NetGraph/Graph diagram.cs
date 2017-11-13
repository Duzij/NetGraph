using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Layout.Layered;
using Microsoft.Msagl.Miscellaneous;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public Dictionary<string, Color> NodesColor { get; set; } = new Dictionary<string, Color>();

        public void HighlightNodes(List<string> nodesId)
        {
            foreach (var item in nodesId)
            {
                NodesColor.Add(item, viewer.Graph.FindNode(item).Attr.FillColor);
                ColorizeLabelAndNode(item, Color.White, Color.Green);
            }
            viewer.Refresh();
        }

        public void DeHighlightNodes(List<string> nodesId)
        {
            foreach (var item in nodesId)
            {
                Color color = default(Color);
                NodesColor.TryGetValue(item, out color);
                ColorizeLabelAndNode(item, Color.Black, color);
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
            var settings = new Microsoft.Msagl.Layout.MDS.MdsLayoutSettings() { AdjustScale = true };
            LayoutHelpers.CalculateLayout(graph.GeometryGraph, settings, null);

            Controls.Add(viewer);

            ResumeLayout();
        }

        private void AdjustFontsAndColorizeDomains(Graph graph)
        {
            foreach (var domain in Repository.GetAllDomains())
            {
                var color = Colorizer.GetRandomColor();
                foreach (var link in Repository.GetAllLinksByDomain(domain))
                {
                    if (Repository.GetLink(link).Code != System.Net.HttpStatusCode.Accepted && Repository.GetLink(link).Code != 0)
                    {
                        graph.FindNode(link).Attr.FillColor = Color.Red;
                        graph.FindNode(link).Label.FontColor = Color.White;
                    }
                    else
                    {
                        graph.FindNode(link).Attr.FillColor = color;
                    }
                }
            }

            foreach (var item in graph.Nodes)
            {
                item.Label.FontSize = item.Edges.Count() == 0 ? 5 : item.Edges.Count() / 3 + 5;
                item.Attr.LabelMargin = 5;
            }
        }

        private void ColorizeLabelAndNode(string node, Color fontColor, Color nodeColor)
        {
            viewer.Graph.FindNode(node).Attr.FillColor = nodeColor;
            viewer.Graph.FindNode(node).Label.FontColor = fontColor;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                if (Application.OpenForms.OfType<SearchForm>().Count() == 1)
                    Application.OpenForms.OfType<SearchForm>().First().Show();
                else
                {
                    SearchForm search = new SearchForm(this);
                    search.Show();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
