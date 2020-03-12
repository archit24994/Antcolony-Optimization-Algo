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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CCTreeMinerV2
{
    class DepthOccSet
    {
        private readonly Depth depth;

        internal Depth Depth
        {
            get { return depth; }
        }

        private readonly Dictionary<TreeId, TreeOccSet> treeSet = new Dictionary<TreeId, TreeOccSet>();

        internal Dictionary<TreeId, TreeOccSet> TreeSet
        {
            get { return treeSet; }
        }

        internal TreeOccSet this[TreeId treeId]
        {
            get
            {
                return TreeSet.ContainsKey(treeId) ? TreeSet[treeId] : null;
            }
        }

        internal int RootOccurrenceCount { get; private set; }

        internal DepthOccSet(Depth depth)
        {
            this.depth = depth;
            RootOccurrenceCount = 0;
        }

        internal int AddOccurrence(IOccurrence occ)
        {
            if (occ == null) throw new ArgumentNullException("occ");
            if (occ.Depth != depth) throw new InvalidOperationException("Depth mismatch.");

            if (!TreeSet.ContainsKey(occ.TreeId))
            {
                TreeSet.Add(occ.TreeId, new TreeOccSet(occ.TreeId));
            }

            var temp = TreeSet[occ.TreeId].AddOccurrence(occ);
            RootOccurrenceCount += temp;

            return temp;
        }

        internal bool ContainsOccurrence(IOccurrence occ)
        {
            if (occ == null) throw new ArgumentNullException("occ");

            return TreeSet.ContainsKey(occ.TreeId) && TreeSet[occ.TreeId].ContainsOccurrence(occ);
        }

        public override string ToString()
        {
            return string.Format("Depth:{0}; TreeNumber:{1}.", depth, TreeSet.Count);
        }

        internal IEnumerable GetTreeSet()
        {
            return TreeSet.Select(t => t.Value);
        }

        internal bool ContainsTree(TreeId treeId)
        {
            return TreeSet.ContainsKey(treeId);
        }

        internal bool ContainsRootIndex(TreeId treeId, PreorderIndex rootIndex)
        {
            return ContainsTree(treeId) && TreeSet[treeId].ContainsRootIndex(rootIndex);
        }
    }
}
