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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CCTreeMinerV2
{
    static public class CCTreeMinerConvert
    {
        public static string ToPreorderString(this IList<NodeSymbol> preorderRepresentation, char separator)
        {
            if (preorderRepresentation == null) throw new ArgumentNullException("preorderRepresentation");

            var sb = new StringBuilder();

            foreach (var ns in preorderRepresentation) sb.Append(string.Format("{0}{1}", ns, separator));

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string ToPreorderStringWithIndex(this ITextTree tree)
        {
            if (tree == null) throw new ArgumentNullException("tree");

            return tree.Root.ToPreorderStringWithIndex(tree.BackTrack);
        }

        public static string ToPreorderString(this ITextTree tree)
        {
            if (tree == null) throw new ArgumentNullException("tree");
            
            return tree.Root.ToPreorderString(tree.Separator, tree.BackTrack);
        }

        public static string ToPreorderString(this ITreeNode itn, char separator, NodeSymbol backTrack)
        {
            //if (itn == null) throw new ArgumentNullException("itn");
            //if (string.IsNullOrEmpty(seperator)) throw new ArgumentNullException("separator");
            //if (string.IsNullOrEmpty(backTrack)) throw new ArgumentNullException("backTrack");
            
            var str = itn.Symbol + separator.ToString(CultureInfo.InvariantCulture);

            if (itn.Children != null && itn.Children.Count > 0)
            {
                str = itn.Children.Aggregate(str, (current, c) => current + c.ToPreorderString(separator, backTrack));
            }

            return str + backTrack + separator.ToString(CultureInfo.InvariantCulture);
        }

        static string ToPreorderStringWithIndex(this ITreeNode node, NodeSymbol backTrack)
        {
            //if (node == null) throw new ArgumentNullException("node");
            //if (string.IsNullOrEmpty(backTrack)) throw new ArgumentNullException("backTrack");

            var str = string.Format("{0}[{1}]", node.Symbol, node.PreorderIndex);

            if (node.Children != null && node.Children.Count > 0)
            {
                str = node.Children.Aggregate(str, (current, child) => current + child.ToPreorderStringWithIndex(backTrack));
            }

            return str + backTrack;
        }

        static internal List<string> ToPreorderStringArray(this ITreeNode itn, NodeSymbol backTrack)
        {
            var array = new List<string> { itn.Symbol };

            if (itn.Children != null && itn.Children.Count > 0)
            {
                foreach (var child in itn.Children)
                {
                    array.AddRange(ToPreorderStringArray(child, backTrack));
                }
            }

            array.Add(backTrack);

            return array;
        }
    }
}
