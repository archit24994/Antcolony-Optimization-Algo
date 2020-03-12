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
    public class Canonicalizer
    {
        public static void Canonicalize(ITextTree tree)
        {
            if (tree == null) throw new ArgumentNullException("tree");

            if (tree.Root != null) Sort(tree.Root);
        }

        private static void Sort(ITreeNode node)
        {
            //if (node == null) throw new ArgumentNullException("node");

            if (node.Children == null) return;

            foreach (var child in node.Children) Sort(child);

            node.Children.Sort(new TreeComparer(node.Tree.BackTrack));
        }
    }
}
