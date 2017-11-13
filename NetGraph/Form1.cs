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
    public partial class Form1 : Form
    {
        public bool Paused { get; set; }

        public LinkRepository linkRepository { get; set; } = new LinkRepository();
        public int MaxNumPages => Convert.ToInt32(max_num_connections.Value);
        public int MaxNumDomain => Convert.ToInt32(max_num_connections.Value);
        public string StartURL => url_txt_bx.Text;

        public LinkParser linkParser { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        public void setPagesText(string txt)
        {
            if (pagesfound_lbl.InvokeRequired)
            { pagesfound_lbl.Invoke(new Action(() => pagesfound_lbl.Text = txt)); return; }
            pagesfound_lbl.Text = txt;
        }

        public void setDomainsText(string txt)
        {
            if (domainsfound_lbl.InvokeRequired)
            { domainsfound_lbl.Invoke(new Action(() => domainsfound_lbl.Text = txt)); return; }
            domainsfound_lbl.Text = txt;
        }

        private async void stop_btn_Click(object sender, EventArgs e)
        {
            linkParser.ProcessPaused = true;
            browse_btn.Enabled = true;
            stop_btn.Enabled = false;
            await CreateGraph();
        }

        private async void browse_btn_Click(object sender, EventArgs e)
        {
            linkRepository.FlushCatalog();
            browse_btn.Enabled = false;
            stop_btn.Enabled = true;
            await CreateGraph();
            browse_btn.Enabled = true;
            stop_btn.Enabled = false;
        }

        private async Task CreateGraph()
        {
            linkParser = new LinkParser(this);
            await linkParser.Analyze();
            var generator = new GraphGenerator(linkParser.Connections);
            var graph = generator.GenerateGraph();
            var diagram = new Graph_diagram(graph);
            diagram.ShowDialog();
        }
    }
}
