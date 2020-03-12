// Copyright ?2014-2015 Claude He (何永恩)
// Notice: The source code is licensed under the GNU General Public license.

/*------------------------------------------------------------------------------*
 * Author: 何永恩 (Claude He)   
 * Email: heyongn@126.com
 *   
 * Description:
 *  This is a C# implementation of CCTreeMiner, an algorithm for subtree mining.
 *  This algorithm was proposed in the my master's thesis (written in Chinese
 *  and tutored by Liu Li(刘莉)) in 2009.
 *  
 * If you find bugs, please be free to contact me for improvement, thanks!
/-------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace CCTreeMinerV2
{
    public class TreeNode : ITreeNode
    {
        public NodeSymbol Symbol { get; set; }
        
        public int PreorderIndex { get; set; }

        public int Depth { get; set; }

        public bool IsRoot
        {
            get { return Parent == null; }
        }

        public bool IsLeaf
        {
            get { return (Children == null || Children.Count <= 0); }
        }

        public ITreeNode Parent { get; set; }

        public List<ITreeNode> Children { get; set; }

        public ITextTree Tree { get; set; }

        public int CompareTo(ITreeNode other)
        {
            return Symbol.CompareTo(other.Symbol);
        }

        public override string ToString()
        {
            var backTrack = Tree == null ? TextTree.DefaultBackTrack : Tree.BackTrack;
            var separator = Tree == null ? TextTree.DefaultSeparator : Tree.Separator;

            return this.ToPreorderString(separator, backTrack);
        }
    }
}
