namespace NetGraph
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.stop_btn = new System.Windows.Forms.Button();
            this.domainsfound_lbl = new System.Windows.Forms.Label();
            this.pagesfound_lbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.browse_btn = new System.Windows.Forms.Button();
            this.max_num_domains = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.max_num_connections = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.url_txt_bx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.max_num_domains)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.max_num_connections)).BeginInit();
            this.SuspendLayout();
            // 
            // stop_btn
            // 
            this.stop_btn.Enabled = false;
            this.stop_btn.Location = new System.Drawing.Point(12, 202);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(259, 27);
            this.stop_btn.TabIndex = 23;
            this.stop_btn.Text = "Stop";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // domainsfound_lbl
            // 
            this.domainsfound_lbl.AutoSize = true;
            this.domainsfound_lbl.Location = new System.Drawing.Point(109, 179);
            this.domainsfound_lbl.Name = "domainsfound_lbl";
            this.domainsfound_lbl.Size = new System.Drawing.Size(13, 13);
            this.domainsfound_lbl.TabIndex = 22;
            this.domainsfound_lbl.Text = "0";
            // 
            // pagesfound_lbl
            // 
            this.pagesfound_lbl.AutoSize = true;
            this.pagesfound_lbl.Location = new System.Drawing.Point(109, 155);
            this.pagesfound_lbl.Name = "pagesfound_lbl";
            this.pagesfound_lbl.Size = new System.Drawing.Size(13, 13);
            this.pagesfound_lbl.TabIndex = 21;
            this.pagesfound_lbl.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Domains found";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Connections found";
            // 
            // browse_btn
            // 
            this.browse_btn.Location = new System.Drawing.Point(12, 115);
            this.browse_btn.Name = "browse_btn";
            this.browse_btn.Size = new System.Drawing.Size(259, 27);
            this.browse_btn.TabIndex = 18;
            this.browse_btn.Text = "Browse";
            this.browse_btn.UseVisualStyleBackColor = true;
            this.browse_btn.Click += new System.EventHandler(this.browse_btn_Click);
            // 
            // max_num_domains
            // 
            this.max_num_domains.Location = new System.Drawing.Point(147, 83);
            this.max_num_domains.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.max_num_domains.Name = "max_num_domains";
            this.max_num_domains.Size = new System.Drawing.Size(124, 20);
            this.max_num_domains.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Max. num. of domains";
            // 
            // max_num_connections
            // 
            this.max_num_connections.Location = new System.Drawing.Point(147, 57);
            this.max_num_connections.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.max_num_connections.Name = "max_num_connections";
            this.max_num_connections.Size = new System.Drawing.Size(124, 20);
            this.max_num_connections.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Max. num. of connections";
            // 
            // url_txt_bx
            // 
            this.url_txt_bx.Location = new System.Drawing.Point(12, 29);
            this.url_txt_bx.Name = "url_txt_bx";
            this.url_txt_bx.Size = new System.Drawing.Size(259, 20);
            this.url_txt_bx.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "URL";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 241);
            this.Controls.Add(this.stop_btn);
            this.Controls.Add(this.domainsfound_lbl);
            this.Controls.Add(this.pagesfound_lbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.browse_btn);
            this.Controls.Add(this.max_num_domains);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.max_num_connections);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.url_txt_bx);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.max_num_domains)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.max_num_connections)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.Label domainsfound_lbl;
        private System.Windows.Forms.Label pagesfound_lbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button browse_btn;
        private System.Windows.Forms.NumericUpDown max_num_domains;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown max_num_connections;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox url_txt_bx;
        private System.Windows.Forms.Label label1;
    }
}

