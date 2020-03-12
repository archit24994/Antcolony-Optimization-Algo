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
using System.Text;

namespace CCTreeMinerV2
{
    class OccInduced : IOccurrence
    {
        internal static IOccurrence Create(TreeId treeId, Depth depth, IList<int> preorderCode)
        {
            return new OccInduced(treeId, depth, preorderCode);
        }

        readonly string treeId;
        public string TreeId
        {
            get { return treeId; }
        }

        readonly int depth;
        public int Depth
        {
            get { return depth; }
        }

        public int RootIndex
        {
            get { return PreorderCode[0]; }
        }

        public int RightMostIndex
        {
            get { return PreorderCode[PreorderCode.Count - 1]; }
        }

        public int SecondIndex
        {
            get
            {
                if (PreorderCode.Count < 2)
                {
                    throw new InvalidOperationException("This is an occurrence of 1-pattern.");
                }
                return PreorderCode[1];
            }
        }

        public bool AbleToConnect { get; set; }

        public bool AbleToBeConnected { get; set; }

        private readonly ReadOnlyCollection<int> preorderCode;
        public ReadOnlyCollection<int> PreorderCode
        {
            get { return preorderCode; }
        }
         
        OccInduced(TreeId treeId, Depth depth, IList<int> preorderCode)
        {
            if (string.IsNullOrEmpty(treeId))
            {
                throw new ArgumentOutOfRangeException("treeId");
            }

            if (depth < 0)
            {
                throw new ArgumentOutOfRangeException("depth", "Depth of an occurrence should be larger than or equal to 0.");
            }

            if (preorderCode == null || preorderCode.Count <= 0)
            {
                throw new ArgumentNullException("preorderCode");
            }

            this.treeId = treeId;
            this.depth = depth;
            this.preorderCode = new ReadOnlyCollection<int>(preorderCode);

            AbleToConnect = (PreorderCode.Count == 2);
            AbleToBeConnected = preorderCode[0] > 0;
        }

        public bool Contains(IOccurrence occ) 
        {
            if (occ == null) return false;
            if (occ.TreeId != TreeId || occ.RootIndex != RootIndex) return false;
            if (PreorderCode.Count < occ.PreorderCode.Count) return false;
            if (RightMostIndex < occ.RightMostIndex) return false;

            for (var i = 1; i < occ.PreorderCode.Count; i++)
            {
                if (!PreorderCode.Contains(occ.PreorderCode[i])) return false;
            }

            return true;
        }

        public IOccurrence Connect(IOccurrence occToBeConnected)
        {
            var occ = occToBeConnected as OccInduced;
            if (occ == null) throw new InvalidOperationException("Not an induced occurrence.");

            if (TreeId != occ.TreeId) throw new InvalidOperationException("The two occurrences are not from the same tree.");
            if (RightMostIndex != occ.RootIndex) throw new InvalidOperationException("The two occurrences cannot be connected.");

            var preList = new List<int> { RootIndex };
            
            preList.AddRange(occToBeConnected.PreorderCode);

            return Create(TreeId, Depth, preList);
        }

        public IOccurrence Combine(IOccurrence occToBeCombined)
        {
            var occ = occToBeCombined as OccInduced;
            if (occ == null) throw new InvalidOperationException("Not an induced occurrence.");

            if (TreeId != occ.TreeId) throw new InvalidOperationException("The two occurrences are not from the same tree.");
            if (RootIndex != occ.RootIndex) throw new InvalidOperationException("Cannot combine, the two occurrences do not share the same root.");
            if (RightMostIndex >= occ.SecondIndex) throw new InvalidOperationException("Illegal combination.");

            var preList = new List<int>(PreorderCode);

            for (var i = 1; i < occToBeCombined.PreorderCode.Count; i++)
            {
                preList.Add(occToBeCombined.PreorderCode[i]);
            }

            return Create(TreeId, Depth, preList);
        }

        public override string ToString()
        {
            var sb = new StringBuilder(string.Format("ID:{0} [", treeId));

            for (var i = 0; i < PreorderCode.Count - 1; i++)
            {
                sb.Append(PreorderCode[i] + ", ");
            }

            sb.Append(PreorderCode[PreorderCode.Count - 1] + "]");

            return sb.ToString();
        }
    }
}
