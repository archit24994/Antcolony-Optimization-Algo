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
    public interface ITreeNode : IComparable<ITreeNode>
    {
        NodeSymbol Symbol { get; set; }

        int PreorderIndex { get; set; }

        int Depth { get; set; }

        bool IsRoot { get; }

        bool IsLeaf { get; }

        ITreeNode Parent { get; set; }

        List<ITreeNode> Children { get; set; }

        ITextTree Tree { get; set; }
    }
}
