namespace CCTreeMinerApp
{
    partial class TreeVisualizer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPatternTree = new System.Windows.Forms.TextBox();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtTextTree = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnIncreaseGridWidth = new System.Windows.Forms.Button();
            this.btnDecreaseGridWidth = new System.Windows.Forms.Button();
            this.btnDecreaseGridHeight = new System.Windows.Forms.Button();
            this.btnIncreaseGridHeight = new System.Windows.Forms.Button();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.lblOccNumber = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlScroll.SuspendLayout();
            this.pnlCanvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtPatternTree, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlInfo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlScroll, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(253, 306);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtPatternTree
            // 
            this.txtPatternTree.BackColor = System.Drawing.SystemColors.Window;
            this.txtPatternTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatternTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPatternTree.ForeColor = System.Drawing.Color.Red;
            this.txtPatternTree.Location = new System.Drawing.Point(3, 274);
            this.txtPatternTree.Multiline = true;
            this.txtPatternTree.Name = "txtPatternTree";
            this.txtPatternTree.ReadOnly = true;
            this.txtPatternTree.Size = new System.Drawing.Size(247, 29);
            this.txtPatternTree.TabIndex = 3;
            this.txtPatternTree.Text = "Pattern:";
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.tableLayoutPanel2);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlInfo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(253, 45);
            this.pnlInfo.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.txtTextTree, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(253, 45);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // txtTextTree
            // 
            this.txtTextTree.BackColor = System.Drawing.SystemColors.Window;
            this.txtTextTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTextTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTextTree.Location = new System.Drawing.Point(73, 3);
            this.txtTextTree.Multiline = true;
            this.txtTextTree.Name = "txtTextTree";
            this.txtTextTree.ReadOnly = true;
            this.txtTextTree.Size = new System.Drawing.Size(177, 39);
            this.txtTextTree.TabIndex = 0;
            this.txtTextTree.Text = "TextTree:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnIncreaseGridWidth);
            this.panel1.Controls.Add(this.btnDecreaseGridWidth);
            this.panel1.Controls.Add(this.btnDecreaseGridHeight);
            this.panel1.Controls.Add(this.btnIncreaseGridHeight);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(70, 45);
            this.panel1.TabIndex = 1;
            // 
            // btnIncreaseGridWidth
            // 
            this.btnIncreaseGridWidth.Location = new System.Drawing.Point(45, 12);
            this.btnIncreaseGridWidth.Name = "btnIncreaseGridWidth";
            this.btnIncreaseGridWidth.Size = new System.Drawing.Size(20, 20);
            this.btnIncreaseGridWidth.TabIndex = 2;
            this.btnIncreaseGridWidth.Text = "+";
            this.btnIncreaseGridWidth.UseVisualStyleBackColor = true;
            this.btnIncreaseGridWidth.Click += new System.EventHandler(this.BtnChangeGridSize);
            // 
            // btnDecreaseGridWidth
            // 
            this.btnDecreaseGridWidth.Location = new System.Drawing.Point(5, 12);
            this.btnDecreaseGridWidth.Name = "btnDecreaseGridWidth";
            this.btnDecreaseGridWidth.Size = new System.Drawing.Size(20, 20);
            this.btnDecreaseGridWidth.TabIndex = 3;
            this.btnDecreaseGridWidth.Text = "-";
            this.btnDecreaseGridWidth.UseVisualStyleBackColor = true;
            this.btnDecreaseGridWidth.Click += new System.EventHandler(this.BtnChangeGridSize);
            // 
            // btnDecreaseGridHeight
            // 
            this.btnDecreaseGridHeight.Location = new System.Drawing.Point(25, 2);
            this.btnDecreaseGridHeight.Name = "btnDecreaseGridHeight";
            this.btnDecreaseGridHeight.Size = new System.Drawing.Size(20, 20);
            this.btnDecreaseGridHeight.TabIndex = 1;
            this.btnDecreaseGridHeight.Text = "-";
            this.btnDecreaseGridHeight.UseVisualStyleBackColor = true;
            this.btnDecreaseGridHeight.Click += new System.EventHandler(this.BtnChangeGridSize);
            // 
            // btnIncreaseGridHeight
            // 
            this.btnIncreaseGridHeight.Location = new System.Drawing.Point(25, 21);
            this.btnIncreaseGridHeight.Name = "btnIncreaseGridHeight";
            this.btnIncreaseGridHeight.Size = new System.Drawing.Size(20, 20);
            this.btnIncreaseGridHeight.TabIndex = 0;
            this.btnIncreaseGridHeight.Text = "+";
            this.btnIncreaseGridHeight.UseVisualStyleBackColor = true;
            this.btnIncreaseGridHeight.Click += new System.EventHandler(this.BtnChangeGridSize);
            // 
            // pnlScroll
            // 
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.Controls.Add(this.pnlCanvas);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(3, 48);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(247, 220);
            this.pnlScroll.TabIndex = 4;
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.Controls.Add(this.lblOccNumber);
            this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvas.Location = new System.Drawing.Point(0, 0);
            this.pnlCanvas.Margin = new System.Windows.Forms.Padding(5);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(247, 220);
            this.pnlCanvas.TabIndex = 0;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            // 
            // lblOccNumber
            // 
            this.lblOccNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOccNumber.AutoSize = true;
            this.lblOccNumber.ForeColor = System.Drawing.Color.Green;
            this.lblOccNumber.Location = new System.Drawing.Point(89, 0);
            this.lblOccNumber.Name = "lblOccNumber";
            this.lblOccNumber.Size = new System.Drawing.Size(106, 13);
            this.lblOccNumber.TabIndex = 0;
            this.lblOccNumber.Text = "Occurrence Number:";
            // 
            // TreeVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TreeVisualizer";
            this.Size = new System.Drawing.Size(253, 306);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlScroll.ResumeLayout(false);
            this.pnlCanvas.ResumeLayout(false);
            this.pnlCanvas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.Button btnDecreaseGridWidth;
        private System.Windows.Forms.Button btnIncreaseGridWidth;
        private System.Windows.Forms.Button btnDecreaseGridHeight;
        private System.Windows.Forms.Button btnIncreaseGridHeight;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.TextBox txtTextTree;
        private System.Windows.Forms.TextBox txtPatternTree;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.Label lblOccNumber;
    }
}
