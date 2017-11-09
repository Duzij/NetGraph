namespace NetGraph
{
    partial class DetailedNodeInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.url_txtbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.response_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL";
            // 
            // url_txtbx
            // 
            this.url_txtbx.Location = new System.Drawing.Point(48, 10);
            this.url_txtbx.Name = "url_txtbx";
            this.url_txtbx.Size = new System.Drawing.Size(224, 20);
            this.url_txtbx.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "HTTP response: ";
            // 
            // response_lbl
            // 
            this.response_lbl.AutoSize = true;
            this.response_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.response_lbl.Location = new System.Drawing.Point(96, 43);
            this.response_lbl.Name = "response_lbl";
            this.response_lbl.Size = new System.Drawing.Size(28, 13);
            this.response_lbl.TabIndex = 3;
            this.response_lbl.Text = "200";
            // 
            // DetailedNodeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 73);
            this.Controls.Add(this.response_lbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.url_txtbx);
            this.Controls.Add(this.label1);
            this.Name = "DetailedNodeInfo";
            this.Text = "DetailedNodeInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox url_txtbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label response_lbl;
    }
}