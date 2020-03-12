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
    public sealed class TreeComparer : IComparer<ITreeNode>
    {
        private const int Smaller = -1;
        private const int Equal = 0;
        private const int Greater = 1;

        NodeSymbol backTracking = "^";

        public NodeSymbol BackTracking
        {
            get { return backTracking; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                backTracking = value;
            }
        }

        public TreeComparer(NodeSymbol backTracking)
        {
            BackTracking = backTracking;
        }

        int IComparer<ITreeNode>.Compare(ITreeNode nodeX, ITreeNode nodeY)
        {
            var tnX = nodeX;
            var tnY = nodeY;

            if (tnX != null && tnY != null) return Compare(tnX, tnY);

            throw new ArgumentException("Parameter is not a ITextTree!");
        }

        private int Compare(ITreeNode nodeX, ITreeNode nodeY)
        {
            if (nodeX == null && nodeY == null) return Equal;

            if (nodeX == null) return Greater;
            if (nodeY == null) return Smaller;

            var strArrX = nodeX.ToPreorderStringArray(nodeX.Tree.BackTrack);
            var strArrY = nodeY.ToPreorderStringArray(nodeY.Tree.BackTrack);

            var min = strArrX.Count < strArrY.Count ? strArrX.Count : strArrY.Count;

            for (var i = 0; i < min; i++)
            {
                if (strArrX[i] != BackTracking && strArrY[i] == BackTracking) return Smaller;
                if (strArrX[i] == BackTracking && strArrY[i] != BackTracking) return Greater;

                var rslt = String.CompareOrdinal(strArrX[i], strArrY[i]);

                if (rslt != Equal) return rslt;
            }

            if (strArrX.Count == strArrY.Count) return Equal;

            return (strArrX.Count > strArrY.Count) ? Smaller : Greater;
        }

    }
}
