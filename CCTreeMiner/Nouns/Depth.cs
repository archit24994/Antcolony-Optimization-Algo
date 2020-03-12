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

namespace CCTreeMinerV2
{
    public struct Depth
    {
        public const int MinValue = -1;

        private readonly int value;

        internal Depth(int value)
        {
            if (value < MinValue) throw new ArgumentOutOfRangeException("value");
            this.value = value;
        }

        public static implicit operator Depth(int value)
        {
            if (value < MinValue) throw new ArgumentOutOfRangeException("value");
            return new Depth(value);
        }

        public static implicit operator int(Depth depth)
        {
            return depth.value;
        }

        public override string ToString()
        {
            return string.Format("Depth={0}", value);
        }
    }
}
