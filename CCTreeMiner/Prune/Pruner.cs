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
using System.Diagnostics;

namespace CCTreeMinerV2
{
    class Pruner
    {
        internal static void PruneAfterConnection(PatternRecorderFrequent fRecorder, MiningParams param, Depth depth)
        {
            if (!param.MineFrequent && (param.MineClosed || param.MineMaximal))
                PruneCannotBeClosed(fRecorder, param, depth);

            PruneCannotBeExtended(fRecorder, param, depth);
        }

        private static void PruneCannotBeClosed(PatternRecorderFrequent fRecorder, MiningParams param, Depth depth)
        {
            if (param.MineFrequent || !(param.MineClosed || param.MineMaximal)) return;

            var rDi = fRecorder.GetFanout1FrequentsAtDepth(depth);

            rDi.Sort();

            var dic = new Dictionary<NodeSymbol, List<PatternTree>>();
            foreach (var t in rDi)
            {
                if (t.Is2Pattern) continue;

                var key = t.FirstSymbol + "," + t.SecondSymbol;

                if (!dic.ContainsKey(key)) dic.Add(key, new List<PatternTree>());

                dic[key].Add(t);
            }

            foreach (var fpSet in dic)
            {
                var keysRedundant = new HashSet<string>();
                for (var i = 0; i < fpSet.Value.Count; i++)
                {
                    var ti = fpSet.Value[i];
                    for (var j = 0; j < fpSet.Value.Count; j++)
                    {
                        var tj = fpSet.Value[j];

                        if (i == j) continue;

                        if (ti.Size >= tj.Size || ti.TransactionSupport != tj.TransactionSupport || ti.RootSupport != tj.RootSupport) continue;

                        if (!ti.IsSuperPattern(tj, depth)) continue;
                        var maxDif = (param.SupportType == SupportType.Transaction) 
                            ? param.ThresholdTransaction : param.ThresholdRoot;

                        if (ti.NumberOfRightMostOcc - tj.NumberOfRightMostOcc >= maxDif) continue;
                            
                        keysRedundant.Add(ti.PreorderString);
                        break;
                    }
                }

                fRecorder.RemoveRedundantForClosed(keysRedundant);
                Debug.WriteLine("Depth:{0} RemoveRedundantForClosed Number={1}", depth, keysRedundant.Count);
            }
        }
       
        private static void PruneCannotBeExtended(PatternRecorderFrequent fRecorder, MiningParams param, Depth depth)
        {
            var fDi = fRecorder.GetFrequentsAtDepth(depth + 1);

            foreach (var fpt in fDi)
            {
                fpt.PruneAfterConnection(param, depth);
            }

            fRecorder.RemoveCannotBeExtended(depth + 1);
        }
    }
}
