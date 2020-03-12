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
    public static class PatternExtension
    {
        internal static NodeSymbol[] CombinePreorderRepresentation(this PatternTree xPattern, PatternTree yPattern)
        {
            if (xPattern.FirstSymbol != yPattern.FirstSymbol)
            {
                throw new InvalidOperationException("Cannot combine these two preorder strings.");
            }
            
            var nodeSymbols1 = xPattern.PreorderRepresentation;
            var nodeSymbols2 = yPattern.PreorderRepresentation;
            
            var temp = new List<NodeSymbol>();

            for (var i = 0; i < nodeSymbols1.Count - 1; i++) temp.Add(nodeSymbols1[i]);

            for (var i = 1; i < nodeSymbols2.Count; i++) temp.Add(nodeSymbols2[i]);
           
            return temp.ToArray();
        }

        internal static NodeSymbol[] ConnectPreorderRepresentation(this PatternTree twoPattern, PatternTree pattern)
        {
            if (!twoPattern.Is2Pattern) throw new InvalidOperationException("2-pattern is required.");
            if (twoPattern.SecondSymbol != pattern.FirstSymbol)
            {
                throw new InvalidOperationException("Cannot connect these two preorder strings.");
            }
            
            var nodeSymbolsP2 = twoPattern.PreorderRepresentation;
            var nodeSymbolsPt = pattern.PreorderRepresentation;

            var temp = new List<NodeSymbol> { nodeSymbolsP2[0] };

            temp.AddRange(nodeSymbolsPt);

            temp.Add(nodeSymbolsP2[nodeSymbolsP2.Count - 1]);

            return temp.ToArray();
        }

        internal static bool HasNewCombineOccurrenceAtDepth(this PatternTree xPattern, PatternTree yPattern, Depth depth)
        {
            if (xPattern == null) throw new ArgumentNullException("xPattern");
            if (yPattern == null) throw new ArgumentNullException("yPattern");

            if (xPattern.FirstSymbol != yPattern.FirstSymbol) return false;
            if (!xPattern.ContainsDepth(depth) || !yPattern.ContainsDepth(depth)) return false;

            foreach (TreeOccSet tree in xPattern[depth].GetTreeSet())
            {
                if (!yPattern.ContainsTreeAtDepth(depth, tree.TreeId)) continue;
                foreach (RootOcc rSet in tree.GetRootSet())
                {
                    if (!yPattern[depth][tree.TreeId].ContainsRootIndex(rSet.RootIndex)) continue;
                    foreach (IOccurrence occY in yPattern[depth][tree.TreeId][rSet.RootIndex].GetRightMostSet())
                    {
                        if (rSet.FirstOcc.RightMostIndex < occY.SecondIndex) return true;
                    }
                }
            }

            return false;
        }

        internal static bool HasNewConnectOccurrenceAtDepth(this PatternTree p2, PatternTree pt, Depth depth)
        {
            if (p2 == null) throw new ArgumentNullException("p2");
            if (!p2.Is2Pattern) throw new ArgumentException("The connect pattern must be a 2-pattern.");
            if (pt == null) throw new ArgumentNullException("pt");
            if (p2.SecondSymbol != pt.FirstSymbol) return false;

            var depthConnect = depth;
            var depthToBeConnected = depthConnect + 1;

            if (!p2.ContainsDepth(depthConnect) || !pt.ContainsDepth(depthToBeConnected)) return false;

            foreach (TreeOccSet tSet in p2[depthConnect].GetTreeSet())
            {// For every tree that contains p2 at 'depthConnect'
                if (!pt.ContainsTreeAtDepth(depthToBeConnected, tSet.TreeId)) continue;
                foreach (RootOcc rSet in tSet.GetRootSet())
                {// For every root occurrence, check its leaves 
                    foreach (IOccurrence iOcc in rSet.GetRightMostSet())
                    {// checks each leaf, if a leaf of root occurrence of p2 is the root of an occurrence of pt, there might be a new pattern.
                        if (pt[depthToBeConnected][tSet.TreeId].ContainsRootIndex(iOcc.RightMostIndex))
                        {// An occurrence of p2 has a leaf which is the root of an occurrence of pt, a new pattern should be extended.
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        
    }
}
