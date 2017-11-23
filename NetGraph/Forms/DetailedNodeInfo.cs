﻿using Microsoft.Msagl.Drawing;
using NetGraph.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetGraph
{
    public partial class DetailedNodeInfo : Form
    {
        public LinkRepository LinkRepository { get; set; } = new LinkRepository();
        public List<string> Pages { get; set; } = new List<string>();
        public DetailedNodeInfo(Node node)
        {
            InitializeComponent();
            url_txtbx.Text = node.LabelText;
            response_lbl.Text = LinkRepository.GetLink(node.LabelText).Code.ToString();
            webBrowser1.Navigate(node.LabelText);

            var pagesList = node.Edges.Select(a => a.Target).Distinct().ToList();
            pagesList.AddRange(node.Edges.Select(a => a.Source).Distinct().ToList());
            Pages = pagesList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new PagesListForm(Pages);
            form.ShowDialog();
        }
    }
}
