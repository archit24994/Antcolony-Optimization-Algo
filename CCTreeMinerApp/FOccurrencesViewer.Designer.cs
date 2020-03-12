namespace CCTreeMinerApp
{
    partial class FOccurrencesViewer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlIndicator = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnNxt = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeVisualizer4 = new CCTreeMinerApp.TreeVisualizer();
            this.treeVisualizer3 = new CCTreeMinerApp.TreeVisualizer();
            this.treeVisualizer2 = new CCTreeMinerApp.TreeVisualizer();
            this.treeVisualizer1 = new CCTreeMinerApp.TreeVisualizer();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(621, 462);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 430);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(615, 29);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel3.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.pnlIndicator, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(615, 29);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnClose);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(532, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(83, 29);
            this.panel4.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(5, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlIndicator
            // 
            this.pnlIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlIndicator.Location = new System.Drawing.Point(103, 3);
            this.pnlIndicator.Name = "pnlIndicator";
            this.pnlIndicator.Size = new System.Drawing.Size(426, 23);
            this.pnlIndicator.TabIndex = 1;
            this.pnlIndicator.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlIndicator_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPre);
            this.panel2.Controls.Add(this.btnNxt);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 29);
            this.panel2.TabIndex = 0;
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(3, 3);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(42, 23);
            this.btnPre.TabIndex = 0;
            this.btnPre.Text = "<<";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.BtnClicked);
            // 
            // btnNxt
            // 
            this.btnNxt.Location = new System.Drawing.Point(51, 3);
            this.btnNxt.Name = "btnNxt";
            this.btnNxt.Size = new System.Drawing.Size(42, 23);
            this.btnNxt.TabIndex = 1;
            this.btnNxt.Text = ">>";
            this.btnNxt.UseVisualStyleBackColor = true;
            this.btnNxt.Click += new System.EventHandler(this.BtnClicked);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(615, 421);
            this.panel3.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.treeVisualizer4, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.treeVisualizer3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.treeVisualizer2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.treeVisualizer1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(611, 417);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // treeVisualizer4
            // 
            this.treeVisualizer4.BackColor = System.Drawing.Color.White;
            this.treeVisualizer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeVisualizer4.GridHeight = 25;
            this.treeVisualizer4.GridWidth = 25;
            this.treeVisualizer4.Location = new System.Drawing.Point(308, 211);
            this.treeVisualizer4.Name = "treeVisualizer4";
            this.treeVisualizer4.NodeHeight = 16;
            this.treeVisualizer4.NodeWidth = 12;
            this.treeVisualizer4.Size = new System.Drawing.Size(300, 203);
            this.treeVisualizer4.TabIndex = 3;
            // 
            // treeVisualizer3
            // 
            this.treeVisualizer3.BackColor = System.Drawing.Color.White;
            this.treeVisualizer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeVisualizer3.GridHeight = 25;
            this.treeVisualizer3.GridWidth = 25;
            this.treeVisualizer3.Location = new System.Drawing.Point(3, 211);
            this.treeVisualizer3.Name = "treeVisualizer3";
            this.treeVisualizer3.NodeHeight = 16;
            this.treeVisualizer3.NodeWidth = 12;
            this.treeVisualizer3.Size = new System.Drawing.Size(299, 203);
            this.treeVisualizer3.TabIndex = 2;
            // 
            // treeVisualizer2
            // 
            this.treeVisualizer2.BackColor = System.Drawing.Color.White;
            this.treeVisualizer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeVisualizer2.GridHeight = 25;
            this.treeVisualizer2.GridWidth = 25;
            this.treeVisualizer2.Location = new System.Drawing.Point(308, 3);
            this.treeVisualizer2.Name = "treeVisualizer2";
            this.treeVisualizer2.NodeHeight = 16;
            this.treeVisualizer2.NodeWidth = 12;
            this.treeVisualizer2.Size = new System.Drawing.Size(300, 202);
            this.treeVisualizer2.TabIndex = 1;
            // 
            // treeVisualizer1
            // 
            this.treeVisualizer1.BackColor = System.Drawing.Color.White;
            this.treeVisualizer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeVisualizer1.GridHeight = 25;
            this.treeVisualizer1.GridWidth = 25;
            this.treeVisualizer1.Location = new System.Drawing.Point(3, 3);
            this.treeVisualizer1.Name = "treeVisualizer1";
            this.treeVisualizer1.NodeHeight = 16;
            this.treeVisualizer1.NodeWidth = 12;
            this.treeVisualizer1.Size = new System.Drawing.Size(299, 202);
            this.treeVisualizer1.TabIndex = 0;
            // 
            // FOccurrencesViewer
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 462);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FOccurrencesViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "View Occurrences";
            this.Load += new System.EventHandler(this.FTreeViewer_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private TreeVisualizer treeVisualizer4;
        private TreeVisualizer treeVisualizer3;
        private TreeVisualizer treeVisualizer2;
        private TreeVisualizer treeVisualizer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNxt;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel pnlIndicator;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;

    }
}