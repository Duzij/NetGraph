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
    public partial class SearchForm : Form
    {
        public SearchForm(Graph_diagram graph_Diagram)
        {
            InitializeComponent();
            Graph_Diagram = graph_Diagram;
        }

        public LinkRepository linkRepository { get; set; } = new LinkRepository();
        public Graph_diagram Graph_Diagram { get; }
        public List<string> FoundResults { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            FoundResults = linkRepository.Search(textBox1.Text);
            Graph_Diagram.HighlightNodes(FoundResults);
        }

        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Graph_Diagram.DeHighlightNodes(FoundResults);
        }
    }
}
