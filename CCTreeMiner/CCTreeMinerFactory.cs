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
    sealed class CCTreeMinerFactory
    {
        internal static CCTreeMinerAlgorithm GetCCTreeMiner(MiningParams miningParams)
        {
            if (miningParams == null) throw new ArgumentNullException("miningParams");

            switch (miningParams.SubtreeType)
            {
                case SubtreeType.BottomUp:
                    return new BottomUpMiner(miningParams);
                case SubtreeType.Induced:
                    return new InducedMiner(miningParams);
                case SubtreeType.Embedded:
                    return new EmbeddedMiner(miningParams);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
