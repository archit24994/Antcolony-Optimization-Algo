using System;
using System.Collections.ObjectModel;
using CCTreeMinerV2;

namespace TheProblem
{
    public class TreeSpecification
    {
        //public int Size { get; set; }

        public int MaxDepth { get; set; }

        public int MaxDegree { get; set; }

        public ReadOnlyCollection<NodeSymbol> Lables;

        public TreeSpecification(ReadOnlyCollection<NodeSymbol> lables)
        {
            if (lables == null) throw new ArgumentNullException("lables");
            
            Lables = lables;
        }
    }
}
