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

namespace CCTreeMinerV2
{
    struct TreeId
    {
        private readonly string value;

        internal TreeId(string value)
        {
            this.value = value;
        }

        public static implicit operator TreeId(string value)
        {
            return new TreeId(value);
        }

        public static implicit operator string(TreeId treeId)
        {
            return treeId.value;
        }

        public override string ToString()
        {
            return string.Format("Tree Id={0}", value);
        }
    }
}
