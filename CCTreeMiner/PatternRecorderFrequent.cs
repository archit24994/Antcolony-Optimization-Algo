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
using System.Linq;

namespace CCTreeMinerV2
{
    internal class PatternRecorderFrequent
    {
        internal Depth MaxDepth { get; private set; }
       
        private readonly Dictionary<string, PatternTree> frequent1Pts = new Dictionary<string, PatternTree>();
        internal Dictionary<string, PatternTree> Frequent1Pts
        {
            get { return frequent1Pts; }
        }

        private readonly Dictionary<string, PatternTree> frequent2Pts = new Dictionary<string, PatternTree>();
        internal Dictionary<string, PatternTree> Frequent2Pts
        {
            get { return frequent2Pts; }
        }

        private readonly Dictionary<string, PatternTree> frequents = new Dictionary<string, PatternTree>();
        internal Dictionary<string, PatternTree> Frequents
        {
            get { return frequents; }
        }
        
        private readonly Dictionary<Depth, Dictionary<string, PatternTree>> depthBasedFrequentPts 
            = new Dictionary<Depth, Dictionary<string, PatternTree>>();
        
        protected Dictionary<Depth, Dictionary<string, PatternTree>> DepthBasedFrequentPts
        {
            get { return depthBasedFrequentPts; }
        }

        private readonly Dictionary<string, PatternTree> closeds = new Dictionary<string, PatternTree>();
        public Dictionary<string, PatternTree> Closeds
        {
            get { return closeds; }
        }

        private readonly Dictionary<string, PatternTree> maximals = new Dictionary<string, PatternTree>();
        public Dictionary<string, PatternTree> Maximals
        {
            get { return maximals; }
        }
        
        internal void AddFrequentPattern(PatternTree fpt)
        {
            if (!Frequents.ContainsKey(fpt.PreorderString))
            {
                Frequents.Add(fpt.PreorderString, fpt);
            }

            if (fpt.Is1Pattern && !Frequent1Pts.ContainsKey(fpt.PreorderString))
            {
                Frequent1Pts.Add(fpt.PreorderString, fpt);
                return;
            }

            if (fpt.Is2Pattern && !Frequent2Pts.ContainsKey(fpt.PreorderString))
            {
                Frequent2Pts.Add(fpt.PreorderString, fpt);
            }

            for (Depth d = 0; d < MaxDepth; d++)
            {
                if (fpt.ContainsDepth(d) && !DepthBasedFrequentPts[d].ContainsKey(fpt.PreorderString))
                {
                    DepthBasedFrequentPts[d].Add(fpt.PreorderString, fpt);
                }
            }
        }
        
        internal void SetDepth(Depth maxDepth)
        {
            MaxDepth = maxDepth;
            for (var i = 0; i < MaxDepth; i++)
            {
                DepthBasedFrequentPts.Add(i, new Dictionary<string, PatternTree>());
            }
        }

        /// <summary>
        /// Get the set of frequent patterns, in which, everyone has at least one 
        /// occurrence at the specified depth and has fanout of 1. 
        /// </summary>
        /// <param name="depth">Specified depth.</param>
        /// <returns></returns>
        internal List<PatternTree> GetFanout1FrequentsAtDepth(Depth depth)
        {
            var rDi = new List<PatternTree>();
            if (DepthBasedFrequentPts.ContainsKey(depth))
            {
                rDi.AddRange(from fpt in DepthBasedFrequentPts[depth] where fpt.Value.SingleChild select fpt.Value);
            }

            return rDi;
        }

        /// <summary>
        /// Get the set of frequent patterns, in which, everyone has at least one 
        /// occurrence at the specified depth. 
        /// </summary>
        /// <param name="depth">Specified depth.</param>
        /// <returns></returns>
        internal List<PatternTree> GetFrequentsAtDepth(Depth depth)
        {
            var fDi = new List<PatternTree>();
            if (DepthBasedFrequentPts.ContainsKey(depth))
            {
                fDi.AddRange(from fpt in DepthBasedFrequentPts[depth] select fpt.Value);
            }

            return fDi;
        }

        internal PatternTree[] GetToBeConnectableAtDepth(Depth depth)
        {
            var fDi = new List<PatternTree>();
            if (DepthBasedFrequentPts.ContainsKey(depth))
            {
                fDi.AddRange(DepthBasedFrequentPts[depth].Values.Where(pt => pt.AbleToBeConnected));
            }

            return fDi.ToArray();
        }
        

        /// <summary>
        /// Get the set of frequent patterns, in which, everyone has at least one 
        /// occurrence at the specified depth. 
        /// </summary>
        /// <param name="depth">Specified depth.</param>
        /// <returns></returns>
        internal List<PatternTree> GetToBeConnectedAtDepth(Depth depth)
        {
            var cDi = new List<PatternTree>();

            if (DepthBasedFrequentPts.ContainsKey(depth))
            {
                cDi.AddRange(from v in DepthBasedFrequentPts[depth] where v.Value.AbleToBeConnected select v.Value);
            }

            return cDi;
        }
      
        internal PatternTree[] GetClosedAndMaximalUnknownAtDepth(Depth depth)
        {
            if (DepthBasedFrequentPts.ContainsKey(depth))
            {
                return DepthBasedFrequentPts[depth].Values.Where(
                    pt => pt.IsClosed == YesNoUnknown.Unknown ||
                          pt.IsMaximal == YesNoUnknown.Unknown).ToArray();
            }

            return new PatternTree[] { };
        }

        internal List<PatternTree> GetConnectableAtDepth(Depth depth)
        {
            return Frequent2Pts.Values.Where(f2 => f2.AbleToConnect && f2.ContainsDepth(depth)).ToList();
        }

        internal PatternTree[] GetClosedAtDepth(Depth depth)
        {
            return Closeds.Values.Where(pt => pt.ContainsDepth(depth)).ToArray();
        }

        internal PatternTree GetPatternAtDepth(string ptPreorderString, Depth depth)
        {
            if (DepthBasedFrequentPts.ContainsKey(depth) && DepthBasedFrequentPts[depth].ContainsKey(ptPreorderString))
            {
                return DepthBasedFrequentPts[depth][ptPreorderString];
            }
            return null;
        }

        internal PatternTree[] GetPotentialMaximalAtDepth(Depth depth)
        {
            if (DepthBasedFrequentPts.ContainsKey(depth))
            {
                return DepthBasedFrequentPts[depth].Values.Where(pt => pt.HasSuperFrequentPattern == YesNoUnknown.Unknown).ToArray();
            }

            return new PatternTree[] { };
        }

        internal PatternTree[] GetCombinableAtDepth(Depth depth)
        {
            var rDi = new List<PatternTree>();
            if (DepthBasedFrequentPts.ContainsKey(depth))
            {
                rDi.AddRange(DepthBasedFrequentPts[depth].Values.Where(pt => pt.AbleToCombine && pt.SingleChild));
            }

            return rDi.ToArray();
        }

        internal void RemoveCannotBeExtended(Depth depth)
        {
            var keysToRemove = (from fpt in DepthBasedFrequentPts[depth].Values 
                                where !fpt.AbleToConnect && !fpt.AbleToBeConnected && !fpt.AbleToCombine 
                                select fpt.PreorderString).ToList();

            RemovePatterns(keysToRemove);
        }

        private void RemovePatterns(IEnumerable<string> keysToRemove)
        {
            foreach (var key in keysToRemove)
            {
                for (var i = 0; i < MaxDepth; i++)
                {
                    if (DepthBasedFrequentPts[i].ContainsKey(key))
                    {
                        DepthBasedFrequentPts[i].Remove(key);
                    }
                }
            }
        }

        internal void AddClosed(PatternTree pt)
        {
            if (!Closeds.ContainsKey(pt.PreorderString))
            {
                Closeds.Add(pt.PreorderString, pt);
            }
        }

        internal void AddMaximal(PatternTree pt)
        {
            if (!Maximals.ContainsKey(pt.PreorderString))
            {
                Maximals.Add(pt.PreorderString, pt);
            }
        }
        
        internal void RemoveRedundantForClosed(HashSet<string> keysRedundant)
        {
            foreach (var key in keysRedundant)
            {
                for (var i = 0; i < MaxDepth; i++)
                {
                    if (DepthBasedFrequentPts[i].ContainsKey(key))
                    {
                        DepthBasedFrequentPts[i].Remove(key);
                    }
                }
            }
        }
        
    }
}
