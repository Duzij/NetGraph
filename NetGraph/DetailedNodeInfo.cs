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

namespace NetGraph
{
    public partial class DetailedNodeInfo : Form
    {
        public DetailedNodeInfo(Node node)
        {
            InitializeComponent();
            url_txtbx.Text = node.LabelText;
        }
    }
}
