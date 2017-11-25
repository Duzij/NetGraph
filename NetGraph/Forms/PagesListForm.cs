using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetGraph.Forms
{
    public partial class PagesListForm : Form
    {
        public PagesListForm(IEnumerable<Edge> list)
        {
            InitializeComponent();  
            listBox2.DataSource = list.Select(a => a.Target).Distinct().ToList();
            listBox1.DataSource = list.Select(a => a.Source).Distinct().ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listBox1.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listBox2.SelectedItem.ToString());
        }
    }
}
