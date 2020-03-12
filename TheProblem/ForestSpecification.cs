using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CCTreeMinerV2;

namespace TheProblem
{
    public class ForestSpecification
    {
        private const int DefaultNumberOfTrees = 10;

        private const int DefaultMaxTreeSize = 10;

        private const int DefaultMinTreeSize = 1;

        private const int DefaultMaxTreeDepth = 5;

        private const int DefaultMaxFanout = 5;

        public static bool IsForestSpecificationValid(ForestSpecification fs, NodeSymbol backTrack, out string errorInformation)
        {
            if (null == fs)
            {
                errorInformation = "Forest Specification is null.";
                return false;
            }

            if (string.IsNullOrEmpty(backTrack))
            {
                errorInformation = "Invalid back track symbol.";
                return false;
            }

            if (null == fs.Labels || fs.Labels.Count <= 0)
            {
                errorInformation = "Number of labels must be larger than 0.";
                return false;
            }

            if (fs.Labels.Contains(backTrack))
            {
                errorInformation = string.Format("The set of labels should not contain '{0}', it is reserved for back tracking.", backTrack);
                return false;
            }

            if (fs.Labels.Distinct().Count() != fs.Labels.Count)
            {
                errorInformation = GetDuplicationInformation(fs.Labels);
                return false;
            }

            errorInformation = "Forest Specification is valid.";
            return true;
        }

        private static string GetDuplicationInformation(IEnumerable<NodeSymbol> labels)
        {
            var duplicates = labels.GroupBy(s => s).SelectMany(s => s.Skip(1));

            var sb = new StringBuilder("The set of labels should not contain duplication. Duplicates:");

            foreach (var duplicate in duplicates)
            {
                sb.AppendLine(string.Format("[{0}]; ", duplicate));
            }

            return sb.ToString();
        }

        public int NumberOfLabels
        {
            get { return (Labels == null) ? 0 : Labels.Count; }
        }

        public ReadOnlyCollection<NodeSymbol> Labels;

        //int numberOfNodes = DefaultNumberOfNodes;

        //public int NumberOfNodes
        //{
        //    get { return numberOfNodes; }
        //    set { numberOfNodes = value; }
        //}

        int numberOfTrees = DefaultNumberOfTrees;

        public int NumberOfTrees
        {
            get { return numberOfTrees; }
            set { numberOfTrees = value; }
        }

        int maxTreeSize = DefaultMaxTreeSize;

        public int MaxTreeSize
        {
            get { return maxTreeSize; }
            set { maxTreeSize = value; }
        }

        int minTreeSize = DefaultMinTreeSize;

        public int MinTreeSize
        {
            get { return minTreeSize; }
            set { minTreeSize = value; }
        }

        int maxTreeDepth = DefaultMaxTreeDepth;

        public int MaxTreeDepth
        {
            get { return maxTreeDepth; }
            set { maxTreeDepth = value; }
        }

        int maxFanout = DefaultMaxFanout;

        public int MaxDegree
        {
            get { return maxFanout; }
            set { maxFanout = value; }
        }

        public ForestSpecification(List<NodeSymbol> symbols)
        {
            if (symbols == null || symbols.Count <= 0)
            {
                throw new ArgumentNullException("symbols");
            }

            if (symbols.Distinct().Count() != symbols.Count)
            {
                throw new ArgumentException("The set of symbols should not contain duplication.");
            }

            Labels = symbols.AsReadOnly();
        }

        public ForestSpecification(List<NodeSymbol> symbols, //int numberOfNodes,
             int numberOfTrees, int maxTreeSize, int maxTreeDepth, int maxFanout)
            : this(symbols)
        {
            //this.numberOfNodes = numberOfNodes;
            this.numberOfTrees = numberOfTrees;
            this.maxTreeSize = maxTreeSize;
            this.maxTreeDepth = maxTreeDepth;
            this.maxFanout = maxFanout;
        }
    }
}
