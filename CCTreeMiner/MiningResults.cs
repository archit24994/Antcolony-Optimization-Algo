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
using System.Text;

namespace CCTreeMinerV2
{
    public sealed class MiningResults
    {
        public int MaxDepth { get; internal set; }

        public long TotalTimeElapsed { get; internal set; }

        public MiningParams MiningParams { get; internal set; }

        public int ExtendedPatternsCount { get; internal set; }

        public PatternTree[] FrequentPatterns { get; internal set; }

        public int FrequentPatternsCount
        {
            get
            {
                return FrequentPatterns == null ? 0 : FrequentPatterns.Length;
            }
        }

        public PatternTree[] ClosedPatterns { get; internal set; }

        public int ClosedPatternsCount
        {
            get
            {
                return ClosedPatterns == null ? 0 : ClosedPatterns.Length;
            }
        }

        public PatternTree[] MaximalPatterns { get; internal set; }

        public int MaximalPatternsCount
        {
            get
            {
                return MaximalPatterns == null ? 0 : MaximalPatterns.Length;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(MiningParams.ToString());

            sb.Append(Environment.NewLine);
            sb.AppendLine("Maximal Depth: " + MaxDepth);
            sb.AppendLine(string.Format("Total Time Elapsed: {0}ms", TotalTimeElapsed));
            sb.AppendLine("Extended Patterns Count: " + ExtendedPatternsCount);
            sb.AppendLine("Frequent Patterns Count: " + FrequentPatternsCount);
            sb.AppendLine("Closed Patterns Count: " + ClosedPatternsCount);
            sb.AppendLine("Maximal Patterns Count: " + MaximalPatternsCount);

            return sb.ToString();
        }
    }
}
