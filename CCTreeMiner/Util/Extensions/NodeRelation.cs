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
    public static class NodeRelation
    {
        public static void SetParent(this ITreeNode child, ITreeNode parent)
        {
            if (child.Parent != null && !Equals(child.Parent, parent))
            {
                child.Parent.Children.Remove(child);
            }

            child.Parent = parent;
            if (parent == null)
            {
                child.Depth = 0;
            }
            else
            {
                child.Depth = child.Parent.Depth + 1;

                if (child.Parent.Children == null) child.Parent.Children = new List<ITreeNode>();

                child.Parent.Children.Add(child);
            }
        }
    }
}
