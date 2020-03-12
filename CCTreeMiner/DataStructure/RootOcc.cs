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
    class RootOcc
    {
        readonly TreeId treeId;
        internal TreeId TreeId
        {
            get { return treeId; }
        }

        readonly Depth depth;
        internal Depth Depth
        {
            get { return depth; }
        }

        readonly PreorderIndex rootIndex;
        internal PreorderIndex RootIndex
        {
            get { return rootIndex; }
        }

        public List<IOccurrence> RightMostSet { get; private set; }

        internal IOccurrence FirstOcc
        {
            get
            {
                if (RightMostSet != null && RightMostSet.Count > 0) return RightMostSet[0];
               
                return null;
            }
        }

        public RootOcc(TreeId treeId, Depth depth, PreorderIndex rootIndex)
        {
            this.treeId = treeId;
            this.depth = depth;
            this.rootIndex = rootIndex;
        }

        internal IEnumerable GetRightMostSet()
        {
            return RightMostSet;
        }

        internal int AddOccurrence(IOccurrence occ)
        {
            if (occ.TreeId != TreeId || occ.RootIndex != RootIndex)
            {
                throw new InvalidOperationException();
            }

            if (RightMostSet == null)
            {
                RightMostSet = new List<IOccurrence>();
            }

            RightMostSet.Add(occ);

            return RightMostSet.Count == 1 ? 1 : 0;
        }

        internal bool ContainsOccurrence(IOccurrence iocc)
        {
            if (RightMostSet == null || RightMostSet.Count <= 0)
            {
                return false;
            }

            return RightMostSet.Any(t => t.RightMostIndex == iocc.RightMostIndex);
        }
    }
}
