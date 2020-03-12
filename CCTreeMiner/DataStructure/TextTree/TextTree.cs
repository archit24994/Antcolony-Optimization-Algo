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
    public class TextTree : ITextTree
    {
        public const char DefaultSeparator = ',';

        public static readonly NodeSymbol DefaultBackTrack = "^";

        public string TreeId { get; set; }

        public ITreeNode Root { get; set; }

        public NodeSymbol BackTrack { get; set; }

        private char separator = DefaultSeparator;
        public char Separator
        {
            get { return separator; }
            set { separator = value; }
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", TreeId, this.ToPreorderStringWithIndex());
        }
    }
}
