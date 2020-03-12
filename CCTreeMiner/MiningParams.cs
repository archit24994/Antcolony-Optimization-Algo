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
using System.Text;

namespace CCTreeMinerV2
{
    public class MiningParams : ICloneable
    {
        private readonly SubtreeType subtreeType;

        public SubtreeType SubtreeType
        {
            get { return subtreeType; }
        }

        private readonly bool mineOrdered;
        public bool MineOrdered
        {
            get { return mineOrdered; }
        }

        private readonly bool mineFrequent;
        public bool MineFrequent
        {
            get { return mineFrequent; }
        }

        private readonly bool mineClosed;
        public bool MineClosed
        {
            get { return mineClosed; }
        }

        private readonly bool mineMaximal;
        public bool MineMaximal
        {
            get { return mineMaximal; }
        }

        private readonly SupportType supportType;

        public SupportType SupportType
        {
            get { return supportType; }
        }

        private readonly int thresholdRoot;

        public int ThresholdRoot
        {
            get { return thresholdRoot; }
        }

        private readonly int thresholdTransaction;

        public int ThresholdTransaction
        {
            get { return thresholdTransaction; }
        }

        private readonly char separator;

        public char Separator
        {
            get { return separator; }
        }

        private readonly NodeSymbol backTrackSymbol;

        public NodeSymbol BackTrackSymbol
        {
            get { return backTrackSymbol; }
        }

        public MiningParams(
            SubtreeType subtreeType,
            bool mineOrdered,
            bool mineFrequent,
            bool mineClosed,
            bool mineMaximal,
            SupportType supportType,
            int thresholdRoot,
            int thresholdTransaction,
            char separator,
            string backTrackSymbol)
        {
            switch (subtreeType)
            {
                case SubtreeType.Induced:
                    this.subtreeType = subtreeType;
                    break;
                default:
                    throw new NotSupportedException("subtreeType");
            }

            if (string.IsNullOrEmpty(backTrackSymbol))
                throw new ArgumentNullException("backTrackSymbol");

            this.mineOrdered = mineOrdered;
            
            this.mineFrequent = mineFrequent;
            this.mineClosed = mineClosed;
            this.mineMaximal = mineMaximal;
            
            this.supportType = supportType;
            this.thresholdRoot = thresholdRoot;
            this.thresholdTransaction = thresholdTransaction;
            this.separator = separator;
            this.backTrackSymbol = string.Copy(backTrackSymbol);
        }

        protected MiningParams(MiningParams another)
        {
            subtreeType = another.SubtreeType;

            mineOrdered = another.MineOrdered;

            mineFrequent = another.MineFrequent;
            mineClosed = another.MineClosed;
            mineMaximal = another.MineMaximal;
            
            supportType = another.SupportType;
            thresholdRoot = another.ThresholdRoot;
            thresholdTransaction = another.ThresholdTransaction;

            separator = another.separator;
            backTrackSymbol = string.Copy(another.backTrackSymbol);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Mining Task Specifications:");

            sb.AppendLine(SubtreeType.ToString());
            sb.AppendLine(MineOrdered ? "Ordered" : "Unordered");

            if (MineFrequent) sb.AppendLine("Frequent");
            if (MineClosed) sb.AppendLine("Closed");
            if (MineMaximal) sb.AppendLine("Maximal");

            sb.AppendLine(string.Format("SupportType=[{0}]", SupportType));
            sb.AppendLine(string.Format("RootThreshold=[{0}]", ThresholdRoot));
            sb.AppendLine(string.Format("TransactionThreshold=[{0}]", ThresholdTransaction));

            return sb.ToString();
        }

        /// <summary>
        /// Deep copy of this object.
        /// </summary>
        /// <returns>A deep copy of MiningParams instance.</returns>
        public object Clone()
        {
            return new MiningParams(this);
        }
    }
}
