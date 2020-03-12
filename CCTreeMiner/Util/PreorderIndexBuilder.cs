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

using System;

namespace CCTreeMinerV2
{
    public class PreorderIndexBuilder
    {
        public static void BuildPreorderIndex(ITextTree textTree)
        {
            if (textTree == null) throw new ArgumentNullException("textTree");

            SetPreorderIndex(textTree.Root, 0);
        }

        static int SetPreorderIndex(ITreeNode treeNode, int index)
        {
            if (treeNode.Parent != null && treeNode.Parent.PreorderIndex >= index)
            {
                throw new PreorderIndexOutOfRangeException(string.Format("PreIndex={0}>= Parent's index={1}.",
                    index, treeNode.Parent.PreorderIndex), "Preorder index of a child should be larger than that of its parent.");
            }

            treeNode.PreorderIndex = index;
            var nextIndex = treeNode.PreorderIndex + 1;
            if (treeNode.IsLeaf) return nextIndex;

            var rightMostIndex = nextIndex;
            foreach (var t in treeNode.Children)
            {
                rightMostIndex = SetPreorderIndex(t, nextIndex);
                nextIndex = rightMostIndex;
            }

            return rightMostIndex;
        }
    }
}
