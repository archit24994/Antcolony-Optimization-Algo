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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CCTreeMinerV2
{
    class TreeOccSet
    {
        readonly TreeId treeId;
        internal TreeId TreeId
        {
            get { return treeId; }
        }

        readonly Dictionary<PreorderIndex, RootOcc> rootSet = new Dictionary<PreorderIndex, RootOcc>();
        internal Dictionary<PreorderIndex, RootOcc> RootSet
        {
            get { return rootSet; }
        }

        internal RootOcc this[PreorderIndex rootIndex]
        {
            get
            {
                return RootSet.ContainsKey(rootIndex) ? RootSet[rootIndex] : null;
            }
        }

        internal TreeOccSet(TreeId treeId)
        {
            this.treeId = treeId;
        }

        internal IEnumerable GetRootSet()
        {
            return RootSet.Select(r => r.Value);
        }

        internal bool ContainsOccurrence(IOccurrence occ) 
        {
            return RootSet.ContainsKey(occ.RootIndex) && RootSet[occ.RootIndex].ContainsOccurrence(occ);
        }

        internal int AddOccurrence(IOccurrence occ)
        {
            if (!RootSet.ContainsKey(occ.RootIndex))
            {
                RootSet.Add(occ.RootIndex, new RootOcc(TreeId, occ.Depth, occ.RootIndex));
            }

            return RootSet[occ.RootIndex].AddOccurrence(occ);
        }

        internal bool ContainsRootIndex(PreorderIndex rootIndex)
        {
            return RootSet.ContainsKey(rootIndex);
        }

        public override string ToString()
        {
            return string.Format("TreeId:{0}; RootOccCount:{1}.", TreeId, RootSet.Count);
        }
    }
}
