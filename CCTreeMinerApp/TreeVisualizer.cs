
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using CCTreeMinerV2;

namespace CCTreeMinerApp
{
    public partial class TreeVisualizer : UserControl
    {
        //private bool showSingleOccurrence;

        readonly Font nodeFont = new Font("Arial", 10);
        readonly Font indexFont = new Font("Arial", 7);

        readonly SolidBrush normalSymbolBrush = new SolidBrush(Color.Blue);
        readonly SolidBrush normalIndexBrush = new SolidBrush(Color.Green);

        readonly SolidBrush highLightSymbolBrush = new SolidBrush(Color.Red);
        readonly SolidBrush highLightIndexBrush = new SolidBrush(Color.Green);

        readonly Pen normalEdgePen = new Pen(Color.Black);
        readonly Pen highLightEdgePen = new Pen(Color.Red);

        readonly Pen normalNodePen = new Pen(Color.Blue);
        readonly Pen highLightNodePen = new Pen(Color.Red);
        
        private VisualNode node;

        private readonly List<IOccurrence> occurrences = new List<IOccurrence>();
       
        int nodeWidth = 12;
        public int NodeWidth
        {
            get { return nodeWidth; }
            set
            {
                nodeWidth = value;
                GridSizeChanged();
            }
        }
        
        int nodeHeight = 16;
        public int NodeHeight
        {
            get { return nodeHeight; }
            set
            {
                nodeHeight = value;
                GridSizeChanged();
            }
        }
        
        int gridWidth = 25;
        public int GridWidth
        {
            get { return gridWidth; }
            set
            {
                gridWidth = value;
                GridSizeChanged();
            }
        }

        int gridHeight = 25;
        public int GridHeight
        {
            get { return gridHeight; }
            set
            {
                gridHeight = value;
                GridSizeChanged();
            }
        }

        private readonly Dictionary<int, int> indent = new Dictionary<int, int>();
        
        public TreeVisualizer()
        {
            InitializeComponent();

            normalNodePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            highLightNodePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            lblOccNumber.Text = string.Empty; 
        }

        public void SetHighLightOccurrence(IOccurrence occ, PatternTree pt)
        {
            lblOccNumber.Text = string.Empty;
            occurrences.Clear();
            txtPatternTree.Text = string.Empty;

            if (occ == null) return;

            occurrences.Add(occ);

            txtPatternTree.Text = pt == null ? string.Empty : pt.ToString();
        }

        public void SetHighLightOccurrences(PatternTree pt)
        {
            lblOccNumber.Text = string.Empty; 
            occurrences.Clear();
            txtPatternTree.Text = string.Empty;

            if (pt == null) return;
            lblOccNumber.Text = string.Format("Occurrence Number: {0}", GetOccNumberInTree(pt));
            foreach (var occ in pt.Occurrences)
            {
                occurrences.Add(occ);
            }

            txtPatternTree.Text = pt.ToString();
        }

        private int GetOccNumberInTree(PatternTree pt)
        {
            if (pt == null || node == null) return 0;

            return pt.Occurrences.Count(occ => occ.TreeId == node.TreeNode.Tree.TreeId);
        }

        public void SetTree(ITextTree tree)
        {
            if (tree != null)
            {
                txtTextTree.Text = string.Format("TreeID:{0}\t{1}", tree.TreeId, tree.ToPreorderString());
            }

            node = VisualNode.Convert(tree);
            indent.Clear();
            BuildIndent(node);

            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (node == null) return;

            var g = e.Graphics;

            var max = indent.Values.Concat(new[] { int.MinValue }).Max();

            DrawNode(node, g, max);
        }

        private void BuildIndent(VisualNode vn)
        {
            if (indent.ContainsKey(vn.Y))
            {
                if (indent[vn.Y] < vn.X + 1) indent[vn.Y] = vn.X + 1;
            }
            else
            {
                indent.Add(vn.Y, vn.X + 1);
            }

            if (vn.Children == null) return;

            foreach (var cvn in vn.Children) BuildIndent(cvn);
        }
        
        private void DrawNode(VisualNode vn, Graphics g, int max)
        {
            var x = Convert.ToInt32((vn.X + 1f) / (indent[vn.Y] + 1) * max * GridWidth);
            
            var pnt = new Point(x, vn.Y * GridHeight);

            var width = NodeHeight > NodeWidth ? NodeHeight : NodeWidth;
            
            g.DrawEllipse(GetNodePen(vn), new Rectangle(pnt.X, pnt.Y, width, width));
            // Draw symbol
            g.DrawString(vn.TreeNode.Symbol, nodeFont, GetSymbolBrush(vn), pnt);
            // Draw preorder index
            g.DrawString(vn.TreeNode.PreorderIndex.ToString(CultureInfo.InvariantCulture), 
                indexFont, GetIndexBrush(vn), pnt.X + NodeWidth / 2 + 2, pnt.Y + NodeHeight / 3 );

            if (vn.Children == null) return;
            // Draw children
            var pntOut = new Point(pnt.X + NodeWidth / 2 + 1,pnt.Y + NodeHeight);
            foreach (var child in vn.Children)
            {// Draw edge
                var xc = Convert.ToInt32((child.X + 1f) / (indent[child.Y] + 1) * max * GridWidth);
                var pntIn = new Point(xc + NodeWidth / 2 + 1, child.Y * GridHeight);
                g.DrawLine(GetEdgePen(vn, child), pntOut, pntIn);
                
                DrawNode(child, g, max);
            }
        }

        private void GridSizeChanged()
        {
            pnlCanvas.Invalidate();
        }

        private SolidBrush GetSymbolBrush(VisualNode vn)
        {
            return NeedHighLight(vn.TreeNode.Tree.TreeId, vn.TreeNode.PreorderIndex)
                ? highLightSymbolBrush : normalSymbolBrush;
        }

        private SolidBrush GetIndexBrush(VisualNode vn)
        {
            return NeedHighLight(vn.TreeNode.Tree.TreeId, vn.TreeNode.PreorderIndex)
                ? highLightIndexBrush : normalIndexBrush;
        }


        private Pen GetNodePen(VisualNode vn)
        {
            return NeedHighLight(vn.TreeNode.Tree.TreeId, vn.TreeNode.PreorderIndex)
               ? highLightNodePen : normalNodePen;
        }

        private Pen GetEdgePen(VisualNode parent, VisualNode child)
        {
            return NeedHighLight(parent.TreeNode.Tree.TreeId, parent.TreeNode.PreorderIndex, child.TreeNode.PreorderIndex)
                ? highLightEdgePen : normalEdgePen;
        }
       
        private void BtnChangeGridSize(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            const int increase = 3;
            if (btn.Equals(btnIncreaseGridHeight)) GridHeight += increase;
            else if (btn.Equals(btnDecreaseGridHeight)) GridHeight -= increase;
            else if (btn.Equals(btnIncreaseGridWidth)) GridWidth += increase;
            else if (btn.Equals(btnDecreaseGridWidth)) GridWidth -= increase;

        }
        
        private bool NeedHighLight(string treeId, int index)
        {
            return occurrences.Any(occ => 
                occ.TreeId.Equals(treeId) && occ.PreorderCode.Contains(index));
        }

        private bool NeedHighLight(string treeId, int parentIndex, int childIndex)
        {
            return occurrences.Any(occ => 
                occ.TreeId.Equals(treeId) && 
                occ.PreorderCode.Contains(parentIndex) && 
                occ.PreorderCode.Contains(childIndex));
        }
    }
}
