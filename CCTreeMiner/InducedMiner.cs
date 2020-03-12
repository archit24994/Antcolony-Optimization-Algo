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
using System.Linq;

namespace CCTreeMinerV2
{ 
    class InducedMiner : CCTreeMinerAlgorithm
    {
        public InducedMiner(MiningParams miningParams) : base(miningParams){}

        protected override int GenerateF1F2(IEnumerable<ITextTree> treeSet)
        {
            Depth maxDepth = -1;

            foreach (var tree in treeSet) ScanP1P2(tree.Root, ref maxDepth);

            PatternsFrequent.SetDepth(maxDepth);

            EvaluateFrequency();

            return maxDepth;
        }

        /// <summary>
        /// Frequency evaluation of each pattern (either 1-pattern or 2-pattern) 
        /// generated after scanning the input transaction data set.
        /// </summary>
        private void EvaluateFrequency()
        {
            foreach (var pt in OnePatterns.Values.Where(pt => pt.IsFrequent))
            {
                PatternsFrequent.AddFrequentPattern(pt);
            }

            foreach (var pt in TwoPatterns.Values.Where(pt => pt.IsFrequent))
            {
                PatternsFrequent.AddFrequentPattern(pt);
            }

            if (MiningParams.MineClosed || MiningParams.MineMaximal) EvaluateF1();
        }

        /// <summary>
        /// For closed and maximal property if required.
        /// </summary>
        private void EvaluateF1()
        {
            if (!MiningParams.MineClosed && !MiningParams.MineMaximal) return;
            
            var f1Set = PatternsFrequent.Frequent1Pts;
            var f2Set = PatternsFrequent.Frequent2Pts;

            foreach (OnePatternTree f1 in f1Set.Values)
            {
                foreach (var f2 in f2Set.Values)
                {
                    if (!f1.IsSuperPattern(f2)) continue;

                    f1.CheckMatch(f2);

                    if (f1.HasOccurrenceMatch != YesNoUnknown.Unknown &&
                        f1.HasTransactionMatch != YesNoUnknown.Unknown) break;
                }
               
                if (MiningParams.MineClosed)
                {
                    f1.DetermineClosed();
                    if (f1.IsClosed == YesNoUnknown.Yes) PatternsFrequent.AddClosed(f1);
                }

                if (MiningParams.MineMaximal)
                {
                    f1.DetermineMaximal();
                    if (f1.IsMaximal == YesNoUnknown.Yes) PatternsFrequent.AddMaximal(f1);
                }                
            }
        }

        void ScanP1P2(ITreeNode tn, ref Depth maxDepth)
        {
            if (maxDepth <= tn.Depth) maxDepth = tn.Depth;

            var treeId = tn.Tree.TreeId;

            var preList1P = new[] { tn.Symbol, MiningParams.BackTrackSymbol };
            var patternKey1P = preList1P.ToPreorderString(MiningParams.Separator);

            if (!OnePatterns.ContainsKey(patternKey1P))
            {
                var onePt = PatternTree.Create(preList1P, false,MiningParams);
                PatternsExtended.AddPattern(onePt);
                OnePatterns.Add(onePt.PreorderString, onePt);
            }

            OnePatterns[patternKey1P].AddOccurrence(OccInduced.Create(treeId, tn.Depth, new[] { tn.PreorderIndex }));
            
            if (tn.Children == null) return;
           
            foreach (var child in tn.Children)
            {// Scan for 2-patterns, and each child implies an existence of right-most 2-occurrence.
                var preList2P = new[] { tn.Symbol, child.Symbol, MiningParams.BackTrackSymbol, MiningParams.BackTrackSymbol };
                var patternKey2P = preList2P.ToPreorderString(MiningParams.Separator);

                if (!TwoPatterns.ContainsKey(patternKey2P))
                {
                    var twoPt = PatternTree.Create(preList2P, true, MiningParams);
                    PatternsExtended.AddPattern(twoPt);
                    TwoPatterns.Add(twoPt.PreorderString, twoPt);
                }

                var occ = OccInduced.Create(treeId, tn.Depth, new[] {tn.PreorderIndex, child.PreorderIndex});
                if (child.IsLeaf) occ.AbleToConnect = false;

                TwoPatterns[patternKey2P].AddOccurrence(occ);

                ScanP1P2(child, ref maxDepth);
            }
        }
        
        protected override void Combine(Depth depth)
        {
            var rDi = PatternsFrequent.GetCombinableAtDepth(depth);

            StartTraversal(rDi, depth);

            if (MiningParams.MineClosed || MiningParams.MineMaximal)
                CheckSuperPatternAfterCombination(depth);
        }

        private void CheckSuperPatternAfterCombination(Depth depth)
        {
            var toBeTested = PatternsFrequent.GetClosedAndMaximalUnknownAtDepth(depth);

            var groups = DevideToRelatedGroups(toBeTested);

            foreach (var group in groups) 
            {
                for (var x = 0; x < group.Count; x++)
                {
                    if (group[x].HasOccurrenceMatch != YesNoUnknown.Unknown) continue;

                    for (var y = 0; y < group.Count; y++)
                    {
                        if (x == y || group[x].Size >= group[y].Size) continue;

                        group[x].CheckSuperPatternAfterCombination(group[y], depth, MiningParams.MineClosed);

                        if (group[x].HasOccurrenceMatch != YesNoUnknown.Unknown) break;
                    }
                }
            }
        }
      
        private void StartTraversal(IEnumerable<PatternTree> rDi, Depth depth)
        {
            var groups = DevideToRelatedGroups(rDi);

            foreach (var group in groups)
            {
                if (group.Count < 1) continue;

                foreach (var t in group)
                    for (var y = 0; y < group.Count; y++)
                        Traversal(t, y, group, depth);
            }
        }

        private void Traversal(PatternTree xPattern, int yIndex, IList<PatternTree> group, Depth depth)
        {
            var pX = xPattern;
            var pY = group[yIndex];

            var childPreStr = pX.CombinePreorderRepresentation(pY).ToPreorderString(MiningParams.Separator);

            PatternTree child = null;
            if (PatternsExtended.AlreadyExtended(childPreStr))
            {
                child = PatternsFrequent.GetPatternAtDepth(childPreStr, depth);
            }
            else if (pX.HasNewCombineOccurrenceAtDepth(pY, depth))
            {
                child = Combine2Patterns(pX, pY, depth);
            }

            if (child == null) return;

            for (var i = 0; i < group.Count; i++) Traversal(child, i, group, depth);
        }

        private PatternTree Combine2Patterns(PatternTree px, PatternTree py, Depth depth)
        {
            var preList = px.CombinePreorderRepresentation(py);
            var child = PatternTree.Create(preList, false, MiningParams);
            PatternsExtended.AddPattern(child);
            var curDepth = depth + 1;

            while (--curDepth >= 0)
            {
                if (!px.ContainsDepth(curDepth) || !py.ContainsDepth(curDepth)) continue;

                foreach (TreeOccSet tSet in px[curDepth].GetTreeSet())
                {
                    if (!py.ContainsTreeAtDepth(curDepth, tSet.TreeId)) continue;
                    foreach (RootOcc root in tSet.GetRootSet())
                    {
                        if (!py.ContainsRootIndex(curDepth, tSet.TreeId, root.RootIndex)) continue;

                        var xOcc = px.GetOccurrence(curDepth, tSet.TreeId, root.RootIndex);
                        var yOcc = py.GetFirstOccAfterSpecifiedIndex(xOcc.Depth, xOcc.TreeId, xOcc.RootIndex, xOcc.RightMostIndex);

                        if (yOcc == null) continue;

                        child.AddOccurrence(xOcc.Combine(yOcc));
                    }
                }
            }

            if (!child.IsFrequent) return null;

            PatternsFrequent.AddFrequentPattern(child);

            child.Father = px;
            child.Mother = py;

            px.CheckMatch(child);
            py.CheckMatch(child);

            return child;
        }

        private static IEnumerable<List<PatternTree>> DevideToRelatedGroups(IEnumerable<PatternTree> rDi)
        {
            var dic = new Dictionary<NodeSymbol, List<PatternTree>>();

            foreach (var t in rDi)
            {
                if (!dic.ContainsKey(t.FirstSymbol))
                    dic.Add(t.FirstSymbol, new List<PatternTree>());

                dic[t.FirstSymbol].Add(t);
            }

            return dic.Select(v => v.Value).ToArray();
        }

        protected override void Connect(Depth depth)
        {
            var f2Di = PatternsFrequent.GetConnectableAtDepth(depth);
            var fDj = PatternsFrequent.GetToBeConnectableAtDepth(depth + 1);

            foreach (var f2 in f2Di)
            {
                var toBeConnected = SelectPatternsOfSameRoot(f2.SecondSymbol, fDj, depth + 1);

                foreach (var fpt in toBeConnected)
                {
                    var childPreStr = f2.ConnectPreorderRepresentation(fpt).ToPreorderString(MiningParams.Separator);

                    if (PatternsExtended.AlreadyExtended(childPreStr)) continue;
                    if (!f2.HasNewConnectOccurrenceAtDepth(fpt, depth)) continue;

                    ConnectTwoPatterns(f2, fpt, depth);
                }
            }

            if (MiningParams.MineClosed) DetermineClosed(depth);
            if (MiningParams.MineMaximal) DetermineMaximal(depth);

            Pruner.PruneAfterConnection(PatternsFrequent, MiningParams, depth);
        }

        private void DetermineMaximal(Depth depth)
        {
            var couldBeMaximal = PatternsFrequent.GetPotentialMaximalAtDepth(depth + 1);
            foreach (var pt in couldBeMaximal)
            {
                pt.DetermineMaximal(depth + 1);
                if (pt.IsMaximal == YesNoUnknown.Yes)
                    PatternsFrequent.AddMaximal(pt);
            }
        }

        private void DetermineClosed(Depth depth)
        {
            var fdi = PatternsFrequent.GetFrequentsAtDepth(depth + 1);

            foreach (var pt in fdi)
            {
                pt.DetermineClosed(depth + 1);
                if (pt.IsClosed == YesNoUnknown.Yes)
                {
                    PatternsFrequent.AddClosed(pt);
                }
            }
        }

        private void ConnectTwoPatterns(PatternTree f2, PatternTree fpt, Depth depth)
        {
            if (f2.Size != 2) throw new InvalidOperationException("The connect pattern must be 2-pattern.");

            var preList = f2.ConnectPreorderRepresentation(fpt);
            var child = PatternTree.Create(preList, true, MiningParams);
            PatternsExtended.AddPattern(child);

            var depthC = depth + 1; // Depth of connect
            while (--depthC >= 0)
            {
                if (!f2.ContainsDepth(depthC)) continue;
                var depthTbc = depthC + 1; // Depth of to be connected
                if (!fpt.ContainsDepth(depthTbc)) continue;

                foreach (TreeOccSet tSet in f2[depthC].GetTreeSet())
                {
                    if (!fpt.ContainsTreeAtDepth(depthTbc, tSet.TreeId)) continue;

                    foreach (RootOcc root in tSet.GetRootSet())
                    {
                        foreach (IOccurrence f2Occ in root.GetRightMostSet())
                        {
                            if (!fpt[depthTbc][tSet.TreeId].RootSet.ContainsKey(f2Occ.SecondIndex)) continue;
                            
                            var newOcc = f2Occ.Connect(fpt[depthTbc][tSet.TreeId][f2Occ.SecondIndex].FirstOcc);
                            child.AddOccurrence(newOcc);
                        }
                    }
                }
            }

            if (!child.IsFrequent) return;
            PatternsFrequent.AddFrequentPattern(child);
            
            child.Father = f2;
            child.Mother = fpt;

            f2.CheckMatch(child);
            fpt.CheckMatch(child);
        }

        private IEnumerable<PatternTree> SelectPatternsOfSameRoot(NodeSymbol symbol, ICollection<PatternTree> fDi, Depth depth)
        { 
            var pts = new List<PatternTree>();

            if (fDi == null || fDi.Count <= 0) return pts;

            pts.AddRange(fDi.Where(pt => pt.FirstSymbol == symbol && pt.AbleToBeConnected && pt.ContainsDepth(depth)));

            return pts;
        }
    }
}
