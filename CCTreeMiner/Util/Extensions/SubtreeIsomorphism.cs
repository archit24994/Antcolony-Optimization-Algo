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

using System.Collections.ObjectModel;

namespace CCTreeMinerV2
{
    public static class SubtreeIsomorphism
    {
        public static bool IsInducedSuperPattern(this PatternTree pt, PatternTree largerPt, NodeSymbol backTrack)
        {
            if (pt.Size > largerPt.Size) return false;
            var largeList = largerPt.PreorderRepresentation;
            var smallList = pt.PreorderRepresentation;

            const int upperBound = 1;
            const int lowerBound = int.MaxValue;
            return IsInducedMatch(largeList, 0, 0, upperBound, lowerBound, smallList, 0, backTrack);

        }

        private static bool IsInducedMatch(ReadOnlyCollection<NodeSymbol> large,
                                           int currentIndex,
                                           int currentDepth,
                                           int upperBound,
                                           int lowerBound,
                                           ReadOnlyCollection<NodeSymbol> small,
                                           int matchIndex, NodeSymbol backTrack)
        {
            var curDepth = currentDepth;
            for (var l = currentIndex; l < large.Count; l++)
            {
                if (large.Count - l < small.Count - matchIndex) break; // Cannot contain a subtree in this branch
                if (large[l] == backTrack) 
                {
                    if (--curDepth < upperBound) return false; // End of this branch, no match
                    continue; // Continue this branch
                }

                curDepth++; // Reach to a deep depth
                if (large[l] != small[matchIndex] || curDepth > lowerBound) continue; // this is induced subtree, so cannot over go a node
                // Got an identical and the virtual matched sequent list is increased by one. 
                // Try to find a next match in the larger tree with the next node in the smaller tree. 
                var offset = 0; // Here 'offset' records the change of depth.
                for (var s = matchIndex + 1; s < small.Count; s++)
                {
                    if (small[s] == backTrack)
                    {
                        if (s == small.Count - 1) return true;// It is subtree.
                        offset--;
                        continue;
                    }

                    offset++;
                    var lowerBoundNext = curDepth + offset;// Lower bound is used when induced subtree is the case
                    var upperBoundNext = lowerBoundNext - 1;
                    // Recursively to try to match the next node 
                    if (IsInducedMatch(large, l + 1, curDepth, upperBoundNext, lowerBoundNext, small, s, backTrack)) return true;
                    break; // Not match, break and try to match in an other branch in the larger tree
                }
            }
            return false; // Not match found.
        }

    }
}
