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

using System.Collections.ObjectModel;

namespace CCTreeMinerV2
{
    public interface IOccurrence
    {
        string TreeId { get; }

        int Depth { get; }

        int RootIndex { get; }

        int RightMostIndex { get; }

        int SecondIndex { get; }

        bool AbleToConnect { get; set; }

        bool AbleToBeConnected { get; set; }

        ReadOnlyCollection<int> PreorderCode { get; }

        bool Contains(IOccurrence occ);

        IOccurrence Connect(IOccurrence occToBeConnected);

        IOccurrence Combine(IOccurrence occToBeCombined);
    }
}
