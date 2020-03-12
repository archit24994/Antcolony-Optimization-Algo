﻿// Copyright ?2014-2015 Claude He (何永恩)
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
namespace CCTreeMinerV2
{
    struct RightMostLeaf
    {
        private readonly int value;

        internal RightMostLeaf(int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException("value");
            this.value = value;
        }

        public static implicit operator RightMostLeaf(int value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException("value");
            return new RightMostLeaf(value);
        }

        public static implicit operator int(RightMostLeaf rootIndex)
        {
            return rootIndex.value;
        }

        public override string ToString()
        {
            return string.Format("Right Most Leaf={0}", value);
        }
    }
}
