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

namespace CCTreeMinerV2
{
    public class TextTreeBuilder<T, TN> where T : ITextTree, new() where TN : ITreeNode, new()
    {
        public static T ConvertToTextTree(string treeInString, char separator, NodeSymbol backTrack)
        {
            if (string.IsNullOrEmpty(treeInString)) throw new ArgumentNullException("treeInString");
            if (string.IsNullOrEmpty(backTrack)) throw new ArgumentNullException("backTrack");
            
            var treeInStringArr = treeInString.Split(new[] { separator, ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var tree = new T { BackTrack = backTrack, Separator = separator };

            DoConvert(treeInStringArr, tree);

            return tree;
        }

        private static void DoConvert(IList<string> treeInStringArr, T tree)
        {
            if (treeInStringArr == null)
            {
                throw new ArgumentNullException("treeInStringArr");
            }

            if (treeInStringArr.Count - 1 < 2)
            {
                throw new TreeStringFormatException("Cannot convert to an ITextTree.", "Illegal tree string format: not enough symbols.");
            }

            if (treeInStringArr[0] == tree.BackTrack)
            {
                throw new TreeStringFormatException("Cannot convert to an ITextTree.",
                    string.Format("Illegal tree string format: The first symbol '{0}' should not be a back track symbol.", tree.BackTrack));
            }

            tree.TreeId = treeInStringArr[0];
            var start = 1;
            var curNode = new TN { Symbol = treeInStringArr[start++], Tree = tree };
            tree.Root = curNode;

            for (var i = start; i < treeInStringArr.Count - 1; i++)
            {
                if (treeInStringArr[i] == tree.BackTrack)
                {
                    if (curNode.IsRoot)
                    {
                        throw new TreeStringFormatException(
                            string.Format("The input contains redundant back tracking symble. Colume=[{0}].", start + 1),
                            "Illegal tree string format.");
                    }
                    curNode = (TN)curNode.Parent;
                }
                else
                {
                    var node = new TN { Symbol = treeInStringArr[i], Tree = tree };
                    node.SetParent(curNode);
                    curNode = node;
                }
            }
        }


    }
}
