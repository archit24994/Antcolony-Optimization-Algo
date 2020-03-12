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
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

namespace CCTreeMinerV2
{
    public class PatternTree : IComparable
    {
        public static PatternTree Create(NodeSymbol[] preorderRepresentation,
            bool singleChild, MiningParams miningParams) 
        {
            if (preorderRepresentation == null || preorderRepresentation.Length < 2)
                throw new ArgumentException("Invalid preorder representation.");
            
            if (preorderRepresentation.Length == 2)
            {
                return new OnePatternTree(preorderRepresentation, singleChild, miningParams);
            }

            return new PatternTree(preorderRepresentation, singleChild, miningParams);
        }
        
        public MiningParams MiningParams { get; internal set; }

        public PatternTree Father { get; internal set; }

        public PatternTree Mother { get; internal set; }

        public NodeSymbol FirstSymbol
        {
            get { return PreorderRepresentation[0]; }
        }

        public NodeSymbol SecondSymbol
        {
            get
            {
                if (Is1Pattern) throw new InvalidOperationException("This is a 1-pattern.");

                return PreorderRepresentation[1];
            }
        }

        public bool IsFrequent
        {
            get
            {
                bool isFrequent;
                switch (MiningParams.SupportType)
                {
                    case SupportType.Transaction:
                        isFrequent = TransactionSupport >= MiningParams.ThresholdTransaction;
                        break;
                    case SupportType.RootOccurrence:
                        isFrequent = RootSupport >= MiningParams.ThresholdRoot;
                        break;
                    case SupportType.Hybrid:
                        isFrequent = TransactionSupport >= MiningParams.ThresholdTransaction 
                            && RootSupport >= MiningParams.ThresholdRoot;
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
                return isFrequent;
            }
        }
        
        public YesNoUnknown IsClosed
        {
            get
            {
                var match = MiningParams.SupportType == SupportType.Transaction
                    ? HasTransactionMatch : HasOccurrenceMatch;

                switch (match)
                {
                    case YesNoUnknown.Unknown: return YesNoUnknown.Unknown;
                    case YesNoUnknown.Yes: return YesNoUnknown.No;
                    case YesNoUnknown.No: return YesNoUnknown.Yes;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public YesNoUnknown IsMaximal
        {
            get
            {
                switch (HasSuperFrequentPattern)
                {
                    case YesNoUnknown.Unknown: return YesNoUnknown.Unknown;
                    case YesNoUnknown.Yes: return YesNoUnknown.No;
                    case YesNoUnknown.No: return YesNoUnknown.Yes;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        private YesNoUnknown hasSuperFrequentPattern;
        public YesNoUnknown HasSuperFrequentPattern
        {
            get { return hasSuperFrequentPattern; }
            protected set
            {
                Debug.Assert(value != YesNoUnknown.Unknown);

                hasSuperFrequentPattern = value;
            }
        }

        private YesNoUnknown hasTransactionMatch;
        public YesNoUnknown HasTransactionMatch
        {
            get { return hasTransactionMatch; }
            protected set
            {
                Debug.Assert(value != YesNoUnknown.Unknown);

                hasTransactionMatch = value;
            }
        }

        private YesNoUnknown hasOccurrenceMatch;

        /// <summary>
        /// Root occurrence match implies transaction match.
        /// </summary>
        public YesNoUnknown HasOccurrenceMatch
        {
            get { return hasOccurrenceMatch; }
            protected set
            {
                Debug.Assert(value != YesNoUnknown.Unknown);

                hasOccurrenceMatch = value;

                if (value != YesNoUnknown.Yes) return;
                
                // Occurrence match implies transaction match
                Debug.Assert(HasTransactionMatch != YesNoUnknown.No);
                HasTransactionMatch = YesNoUnknown.Yes;
                //IsClosed = YesNoUnknown.No;
            }
        }

        public string PreorderString { get; private set; }

        public readonly ReadOnlyCollection<NodeSymbol> PreorderRepresentation;

        public bool AbleToCombine { get; protected set; }

        public bool AbleToConnect { get; protected set; }

        public bool AbleToBeConnected { get; protected set; }

        public int Size
        {
            get { return PreorderRepresentation.Count / 2; }
        }

        public bool Is2Pattern
        {
            get { return 2 == Size; }
        }

        public bool Is1Pattern
        {
            get { return 1 == Size; }
        }

        public readonly bool SingleChild;

        readonly Dictionary<Depth, DepthOccSet> depthOccSet;
        internal Dictionary<Depth, DepthOccSet> DepthOccSet
        {
            get { return depthOccSet; }
        }

        internal DepthOccSet this[Depth depth]
        {
            get
            {
                return DepthOccSet.ContainsKey(depth) ? DepthOccSet[depth] : null;
            }
        }

        readonly HashSet<TreeId> supportTransactionSet;
        internal HashSet<TreeId> SupportTransactionSet
        {
            get { return supportTransactionSet; }
        }

        public string[] SupportedTransectionIds
        {
            get
            {
                return SupportTransactionSet.Select(id => (string) id).ToArray();
            }
        }
        
        public int RootSupport { get; private set; }

        public int TransactionSupport
        {
            get { return SupportTransactionSet.Count; }
        }

        public List<IOccurrence> Occurrences
        {
            get
            {
                return (from dSet in DepthOccSet
                        from TreeOccSet tSet in dSet.Value.GetTreeSet()
                        from RootOcc rSet in tSet.GetRootSet()
                        select rSet.FirstOcc).ToList();
            }
        }

        internal int TransactionSupportAbove(Depth depth, bool includingRoot)
        {
            var hashSet = new HashSet<TreeId>();

            while (--depth >= 0)
            {
                if (depth == 0 && !includingRoot) break;

                if (!ContainsDepth(depth)) continue;

                foreach (TreeOccSet tree in this[depth].GetTreeSet())
                {
                    hashSet.Add(tree.TreeId);
                }
            }

            return hashSet.Count;
        }

        internal int RootSupportAbove(Depth depth, bool includingRoot)
        {
            var count = 0;
            while (--depth >= 0)
            {
                if (depth == 0 && !includingRoot) break;

                if (ContainsDepth(depth))
                {
                    count += this[depth].RootOccurrenceCount;
                }
            }
            return count;
        }

        public int NumberOfRightMostOcc { get; private set; }

        /// <summary>
        /// This constructor is for test purpose.
        /// </summary>
        protected PatternTree(IList<NodeSymbol> preorderRepresentation, bool singleChild, char separator)
        {
            if (preorderRepresentation == null) throw new ArgumentNullException("preorderRepresentation");
            if (preorderRepresentation.Count < 2 || preorderRepresentation.Count % 2 != 0)
            {
                throw new ArgumentException("Invalid preorder representation.");
            }

            PreorderString = preorderRepresentation.ToPreorderString(separator);

            SingleChild = singleChild;

            PreorderRepresentation = new ReadOnlyCollection<NodeSymbol>(preorderRepresentation);
        }

        protected PatternTree(IList<NodeSymbol> preorderRepresentation, bool singleChild, MiningParams miningParams)
        {
            if (preorderRepresentation == null) throw new ArgumentNullException("preorderRepresentation");
            if (preorderRepresentation.Count < 2 || preorderRepresentation.Count % 2 != 0)
            {
                throw new ArgumentException("Invalid preorder representation.");
            }

            PreorderString = preorderRepresentation.ToPreorderString(miningParams.Separator);

            SingleChild = singleChild;

            PreorderRepresentation = new ReadOnlyCollection<NodeSymbol>(preorderRepresentation);

            depthOccSet = new Dictionary<Depth, DepthOccSet>();

            supportTransactionSet = new HashSet<TreeId>();

            NumberOfRightMostOcc = 0;

            RootSupport = 0;

            AbleToConnect = Is2Pattern;
            AbleToBeConnected = !Is1Pattern;
            AbleToCombine = true;

            MiningParams = miningParams;
        }
        
        internal void AddOccurrence(IOccurrence iocc)
        {
            if (!DepthOccSet.ContainsKey(iocc.Depth))
            {// Create a container for occurrences of this depth for this pattern.
                DepthOccSet.Add(iocc.Depth, new DepthOccSet(iocc.Depth));
            }

            if (DepthOccSet[iocc.Depth].ContainsOccurrence(iocc))
            {
                throw new InvalidOperationException("Occurrence has already been added.");
            }

            if (!SupportTransactionSet.Contains(iocc.TreeId))
            {
                SupportTransactionSet.Add(iocc.TreeId);
            }

            var newRootOcc = depthOccSet[iocc.Depth].AddOccurrence(iocc);

            RootSupport += newRootOcc;

            NumberOfRightMostOcc++;
        }
        
        internal void DetermineClosed(Depth depth)
        {
            if (IsClosed == YesNoUnknown.Unknown && ContainsDepth(depth))
                HasOccurrenceMatch = YesNoUnknown.No;
        }

        internal void DetermineMaximal(Depth depth)
        {
            if (HasSuperFrequentPattern != YesNoUnknown.Unknown || !ContainsDepth(depth)) return;
            
            switch (MiningParams.SupportType)
            {
                case SupportType.Transaction:
                    if (TransactionSupportAbove(depth, true) < MiningParams.ThresholdTransaction)
                        HasSuperFrequentPattern = YesNoUnknown.No;
                    break;
                case SupportType.RootOccurrence:
                    if (RootSupportAbove(depth, true) < MiningParams.ThresholdRoot)
                        HasSuperFrequentPattern = YesNoUnknown.No;
                    break;
                case SupportType.Hybrid:
                    if (TransactionSupportAbove(depth, true) < MiningParams.ThresholdTransaction || RootSupportAbove(depth, true) < MiningParams.ThresholdRoot)
                        HasSuperFrequentPattern = YesNoUnknown.No;  
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            string str;
            if (IsMaximal == YesNoUnknown.Yes) str = "Maximal";
            else if (IsClosed == YesNoUnknown.Yes) str = "Closed";
            else str = IsFrequent ? "Frequent" : "Infrequent";

            return string.Format("TS={0}; RS={1}; {2}; {3};", TransactionSupport, RootSupport, str, PreorderString);
        }

        internal void CheckSuperPatternAfterCombination(PatternTree pt, Depth depth, bool mineClosed)
        {
            if (HasSuperFrequentPattern != YesNoUnknown.Unknown && !mineClosed) return;

            if (!IsSuperPattern(pt, depth)) return;
            HasSuperFrequentPattern = YesNoUnknown.Yes;

            if (!mineClosed) return;

            if (HasTransactionMatch != YesNoUnknown.Unknown && HasOccurrenceMatch != YesNoUnknown.Unknown) return;

            if (RootSupport == pt.RootSupport) HasOccurrenceMatch = YesNoUnknown.Yes;
            else if (TransactionSupport == pt.TransactionSupport) HasTransactionMatch = YesNoUnknown.Yes;
        }

        internal bool IsSuperPattern(PatternTree largerPt, Depth depth)
        {
            return this.IsInducedSuperPattern(largerPt, MiningParams.BackTrackSymbol);
        }

        /// <summary>
        /// Check transaction match and root occurrence match after a super pattern
        /// is generated via connection or combination.
        /// </summary>
        /// <param name="superPt"></param>
        internal void CheckMatch(PatternTree superPt)
        {
            if (HasOccurrenceMatch != YesNoUnknown.Unknown && HasTransactionMatch != YesNoUnknown.Unknown) return;

            if (superPt.IsFrequent) HasSuperFrequentPattern = YesNoUnknown.Yes;

            if (RootSupport.Equals(superPt.RootSupport))
            {
                HasOccurrenceMatch = YesNoUnknown.Yes;
            }
            else if (TransactionSupport.Equals(superPt.TransactionSupport))
            {
                HasTransactionMatch = YesNoUnknown.Yes;
            }
        }

        internal bool ContainsDepth(Depth depth)
        {
            return DepthOccSet.ContainsKey(depth);
        }

        public int CompareTo(object obj)
        {
            var pt = obj as PatternTree;
            if (pt != null)
                return String.Compare(PreorderString, pt.PreorderString, StringComparison.Ordinal);

            throw new ArgumentException("Parameter is not a PatternTree!");
        }

        internal bool ContainsTreeAtDepth(Depth depth, TreeId treeId)
        {
            return (DepthOccSet.ContainsKey(depth) && DepthOccSet[depth].ContainsTree(treeId));
        }

        internal bool ContainsRootIndex(int depth, TreeId treeId, PreorderIndex rootIndex)
        {
            return DepthOccSet.ContainsKey(depth) && (DepthOccSet[depth].ContainsRootIndex(treeId, rootIndex));
        }

        internal IOccurrence GetOccurrence(int depth, TreeId treeId, PreorderIndex rootIndex)
        {
            return ContainsRootIndex(depth, treeId, rootIndex) ? this[depth][treeId][rootIndex].FirstOcc : null;
        }

        internal List<IOccurrence> GetRightMostOccurrences(int depth, TreeId treeId, PreorderIndex rootIndex)
        {
            return ContainsRootIndex(depth, treeId, rootIndex) ? this[depth][treeId][rootIndex].RightMostSet : null;
        }

        internal IOccurrence GetFirstOccAfterSpecifiedIndex(Depth depth, TreeId treeId, PreorderIndex rootIndex, PreorderIndex specifiedIndex)
        {
            if (!ContainsRootIndex(depth, treeId, rootIndex)) return null;

            var rOcc = this[depth][treeId][rootIndex];

            return rOcc.RightMostSet.FirstOrDefault(t => t.SecondIndex > specifiedIndex);
        }

        internal IOccurrence GetAnyOccurrenceAtDepth(Depth depth)
        {
            if (!ContainsDepth(depth)) return null;

            return (from TreeOccSet tSet in this[depth].GetTreeSet()
                    from RootOcc root in tSet.GetRootSet()
                    from IOccurrence iOcc in root.GetRightMostSet()
                    select iOcc).FirstOrDefault();
        }

        internal void PruneAfterConnection(MiningParams param, Depth depth)
        {
            switch (param.SupportType)
            {
                case SupportType.Transaction:
                    {
                        if (AbleToCombine || AbleToConnect)
                        {
                            var t = TransactionSupportAbove(depth + 1, true);
                            if (t < param.ThresholdTransaction)
                            {
                                AbleToCombine = false;
                                AbleToConnect = false;
                            }
                        }

                        if (AbleToBeConnected)
                        {
                            var t = TransactionSupportAbove(depth + 1, false);
                            if (t < param.ThresholdTransaction) AbleToBeConnected = false;
                        }

                    }
                    break;
                case SupportType.RootOccurrence:
                {
                    if (AbleToCombine || AbleToConnect)
                    {
                        var r = RootSupportAbove(depth + 1, true);
                        if (r < param.ThresholdRoot)
                        {
                            AbleToCombine = false;
                            AbleToConnect = false;
                        }
                    }

                    if (AbleToBeConnected)
                    {
                        var r = RootSupportAbove(depth + 1, false);
                        if (r < param.ThresholdRoot) AbleToBeConnected = false;
                    }
                }
                    break;
                case SupportType.Hybrid:
                {
                    if (AbleToCombine || AbleToConnect)
                    {
                        var t = TransactionSupportAbove(depth + 1, true);
                        if (t < param.ThresholdTransaction)
                        {
                            AbleToCombine = false;
                            AbleToConnect = false;
                        }
                        else
                        {
                            var r = RootSupportAbove(depth + 1, true);
                            if (r < param.ThresholdRoot)
                            {
                                AbleToCombine = false;
                                AbleToConnect = false;
                            }
                        }
                    }

                    if (AbleToBeConnected)
                    {
                        var t = RootSupportAbove(depth + 1, false);
                        if (t < param.ThresholdTransaction) AbleToBeConnected = false;
                        else
                        {
                            var r = RootSupportAbove(depth + 1, false);
                            if (r < param.ThresholdRoot) AbleToBeConnected = false;
                        }
                    }
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }







        
    }
}
