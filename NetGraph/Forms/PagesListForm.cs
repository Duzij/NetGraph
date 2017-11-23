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
        public PagesListForm(List<string> list)
        {
            InitializeComponent();
            listBox1.DataSource = list;
            List = list;
        }

        public List<string> List { get; }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listBox1.SelectedItem.ToString());
        }
    }
}
