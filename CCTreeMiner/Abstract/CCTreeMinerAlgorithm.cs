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
using System.Diagnostics;
using System.Linq;

namespace CCTreeMinerV2
{
    /// <summary>
    /// Base class of CCTreeMiner algorithm.
    /// </summary>
    abstract class CCTreeMinerAlgorithm
    {
        static void BuildPreorderIndex(IEnumerable<ITextTree> treeSet)
        {
            foreach (var tree in treeSet)
            {
                PreorderIndexBuilder.BuildPreorderIndex(tree);
            }
        }

        private bool taskFinished;

        protected readonly PatternRecorderExtended PatternsExtended = new PatternRecorderExtended();

        protected readonly PatternRecorderFrequent PatternsFrequent = new PatternRecorderFrequent();

        protected readonly Dictionary<string, PatternTree> OnePatterns = new Dictionary<string, PatternTree>();
        
        protected readonly Dictionary<string, PatternTree> TwoPatterns = new Dictionary<string, PatternTree>();

        protected readonly MiningParams MiningParams;

        internal bool IsMining { get; private set; }

        internal Depth MaxDepth { get; private set; }

        internal MiningResults Mine(IList<ITextTree> treeSet)
        {
            if (taskFinished) throw new InvalidOperationException("Mining has been performed, check the result.");

            if (IsMining) throw new InvalidOperationException("It has been mining.");

            if (treeSet == null) throw new ArgumentNullException("treeSet");

            try
            {
                IsMining = true;

                Canonicalize(treeSet);
                BuildPreorderIndex(treeSet);

                Stopwatch timeCounter = Stopwatch.StartNew();

                MaxDepth = GenerateF1F2(treeSet);
                var depth = MaxDepth - 1;
                while (depth >= 0)
                {
                    Combine(depth);
                    Connect(--depth);
                }

                timeCounter.Stop();

                return CollectResults(timeCounter);
            }
            finally
            {
                IsMining = false;
                taskFinished = true;
            }
        }

        protected CCTreeMinerAlgorithm(ICloneable miningParams)
        {
            if (miningParams == null) throw new ArgumentNullException("miningParams");
            
            IsMining = false;

            MiningParams = (MiningParams)miningParams.Clone();
        }

        protected abstract int GenerateF1F2(IEnumerable<ITextTree> treeSet);

        protected abstract void Combine(Depth depth);

        protected abstract void Connect(Depth depth);

        private void Canonicalize(IEnumerable<ITextTree> treeSet)
        {
            if (MiningParams.MineOrdered) return;

            foreach (var tree in treeSet)
            {
                Canonicalizer.Canonicalize(tree);
            }
        }

        private MiningResults CollectResults(Stopwatch timeCounter)
        {
            var extendedPatternsCount = PatternsExtended.PatternsExtended.Count;
            
            var frequents = PatternsFrequent.Frequents.Values.ToArray();
            Array.Sort(frequents);
            
            var closeds = PatternsFrequent.Closeds.Values.ToArray();
            Array.Sort(closeds);

            var maximals = PatternsFrequent.Maximals.Values.ToArray();
            Array.Sort(maximals);

            var rslt = new MiningResults
            {
                TotalTimeElapsed = timeCounter.ElapsedMilliseconds,
                MiningParams = MiningParams,
                ExtendedPatternsCount = extendedPatternsCount,
                FrequentPatterns = frequents,
                ClosedPatterns = closeds,
                MaximalPatterns = maximals,
                MaxDepth = MaxDepth
            };
            
            return rslt;
        }

    }
}
