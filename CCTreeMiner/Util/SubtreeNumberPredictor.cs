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
using System.Linq;

namespace CCTreeMinerV2
{
    public static class SubTreeNumberPredictor
    {
        public static ulong InducedSubPatternUpperBound(this ITextTree tree)
        {
            if (tree == null) throw new ArgumentNullException("tree");

            return InducedSubtreeUpperBound(tree.Root);
        }

        private static ulong InducedSubtreeUpperBound(ITreeNode node)
        {
            if (node == null) throw new ArgumentNullException("node");

            if (node.Children == null || node.Children.Count <= 0) return 1;

            var sum = 0UL;
            var product = 1UL; 
            foreach (var child in node.Children)
            {
                sum += InducedSubtreeUpperBound(child);
                product *= (NumberOfSubtreesRootedAt(child) + 1);
            }

            return sum + product;
        }

        private static ulong NumberOfSubtreesRootedAt(ITreeNode node)
        {
            if (node.Children == null || node.Children.Count <= 0) return 1;

            return node.Children.Aggregate(1UL, (current, child) => current*(NumberOfSubtreesRootedAt(child) + 1));
        }
    }
}
