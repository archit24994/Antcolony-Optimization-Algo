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
    public class OnePatternTree : PatternTree 
    {
        internal OnePatternTree(IList<NodeSymbol> preorderRepresentation, bool singleChild, MiningParams miningParams)
            : base(preorderRepresentation, singleChild, miningParams) { }

        internal new void CheckMatch(PatternTree superF2)
        {
            if (!superF2.Is2Pattern) throw new InvalidOperationException("Frequent 2 pattern is required.");
            if (!IsSuperPattern(superF2)) throw new InvalidOperationException("Only super-pattern is allowed for match checking.");

            base.CheckMatch(superF2);
        }

        internal bool IsSuperPattern(PatternTree f2)
        {
            return (f2.FirstSymbol == FirstSymbol || f2.SecondSymbol == FirstSymbol);
        }
        
        internal void DetermineClosed()
        {
            if (HasOccurrenceMatch == YesNoUnknown.Unknown)
            {
                HasOccurrenceMatch = YesNoUnknown.No;
            }
            else if (HasTransactionMatch == YesNoUnknown.Unknown)
            {// Occurrence match implies transaction match
                HasTransactionMatch = YesNoUnknown.No;
            }
        }

        internal void DetermineMaximal()
        {
            if (HasSuperFrequentPattern == YesNoUnknown.Unknown) 
                HasSuperFrequentPattern = YesNoUnknown.No;
        }
    }
}
