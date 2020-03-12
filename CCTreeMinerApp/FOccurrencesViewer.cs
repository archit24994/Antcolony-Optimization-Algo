using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CCTreeMinerV2;
using System.Collections.ObjectModel;

namespace CCTreeMinerApp
{
    public partial class FOccurrencesViewer : Form
    {
        private readonly ReadOnlyCollection<ITextTree> textTrees;
        private readonly PatternTree pattern;
        private readonly int range;
        private int startIndex;

        private readonly TreeVisualizer[] visualizers = new TreeVisualizer[4];

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public FOccurrencesViewer(IList<ITextTree> textTrees, PatternTree pattern)
        {
            if (textTrees == null) throw new ArgumentNullException("textTrees");
            if (pattern == null) throw new ArgumentNullException("pattern");
            if (pattern.TransactionSupport != textTrees.Count)
                throw new ArgumentException();

            InitializeComponent();

            this.textTrees = new ReadOnlyCollection<ITextTree>(textTrees);
            this.pattern = pattern;
            range = this.textTrees.Count;

            Text += string.Format("    {0}; TransactionSupport={1}; RootOccurrenceSuppoer={2}",
                this.pattern.PreorderString, this.pattern.TransactionSupport, this.pattern.RootSupport);
        }

        private void FTreeViewer_Load(object sender, EventArgs e)
        {
            visualizers[0] = treeVisualizer1;
            visualizers[1] = treeVisualizer2;
            visualizers[2] = treeVisualizer3;
            visualizers[3] = treeVisualizer4;

            Whirl(0);
        }
        
        private void Whirl(int step)
        {
            if (range == 0) return;
            
            startIndex = (startIndex + step + range) % range;

            var max = range > 4 ? 4 : range;
            var treeIndex = startIndex;

            for (var i = 0; i < max; i++)
            {
                var tree = textTrees[(treeIndex + i) % range];

                visualizers[i].SetTree(tree);
                visualizers[i].SetHighLightOccurrences(pattern);
            }

            pnlIndicator.Invalidate();
        }

        /// <summary>
        /// Indicates which text-trees are being displayed. 
        /// It is optional but I think it's fun.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlIndicator_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            const int y = 10;
            const int width = 8;
            const int space = 10;
            const int height = 5;
            const int xOffset = 10;

            for (var i = 0; i < range; i++)
            {
                var x = xOffset + i * space;
                g.FillRectangle(Brushes.Blue, x, y, width, height);
            }

            var treeIndex = startIndex;
            var max = range > visualizers.Length ? visualizers.Length : range;
            for (var i = 0; i < max; i++)
            {
                var hight = (treeIndex + i) % range;
                var x = xOffset + hight * space;
                g.FillRectangle(Brushes.Red, x, y, width, height);

                if (i == 0) g.FillRectangle(Brushes.Green, x, y - 6, height, height);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            if (btn.Equals(btnPre)) Whirl(-1);
            else if (btn.Equals(btnNxt)) Whirl(1);
        }

        

       
    }
}
