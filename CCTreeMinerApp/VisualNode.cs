using System.Collections.Generic;
using CCTreeMinerV2;

namespace CCTreeMinerApp
{
    class VisualNode
    {
        public int X { get; set; }

        public int Y { get; set; }

        public ITreeNode TreeNode { get; set; }

        public List<VisualNode> Children { get; set; }

        internal static VisualNode Convert(ITextTree tree)
        {
            if (tree == null || tree.Root == null) return null;

            return BreadthFirstEnumaration(tree);
        }

        static VisualNode BreadthFirstEnumaration(ITextTree tree)
        {
            var nodeQueue = new Queue<VisualNode>();
            
            var root = new VisualNode { X = 0, Y = 0, TreeNode = tree.Root };
            nodeQueue.Enqueue(root);

            var curDepth = 0;
            var x = 0;
            
            while (nodeQueue.Count > 0)
            {
                var node = nodeQueue.Dequeue();

                if (curDepth != node.Y)
                {
                    x = node.X;
                    curDepth = node.Y;
                }

                if (node.TreeNode.Children == null) continue;

                node.Children = new List<VisualNode>();

                foreach (var tn in node.TreeNode.Children)
                {
                    var vn = new VisualNode { X = x ++, Y = node.Y + 1, TreeNode = tn };
                    node.Children.Add(vn);
                    nodeQueue.Enqueue(vn);
                }
            }

            return root;
        }

        //private static void SetNode(VisualNode vNode, int x)
        //{
        //    var treeNodeChildren = vNode.TreeNode.Children;

        //    if (treeNodeChildren == null) return;
        //    vNode.Children = new List<VisualNode>();

        //    var xIndex = x;

        //    VisualNode preNode;
        //    for (var i = 0; i < treeNodeChildren.Count; i++)
        //    {
        //        preNode = new VisualNode { X = xIndex++, Y = vNode.Y + 1, TreeNode = treeNodeChildren[i] };
        //        vNode.Children.Add(preNode);

        //        SetNode(preNode, preNode.X);
        //    }
        //}
    }
}
