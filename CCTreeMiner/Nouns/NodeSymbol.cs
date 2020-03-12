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
    public struct NodeSymbol : IComparable
    {
        private readonly string symbol;
        public string Symbol
        {
            get { return symbol; }
        }

        internal NodeSymbol(string symbol)
        {
            if (string.IsNullOrEmpty(symbol)) throw new ArgumentNullException("symbol");

            this.symbol = symbol;
        }

        public static implicit operator NodeSymbol(string symbol)
        {
            return new NodeSymbol(symbol);
        }

        public static implicit operator string(NodeSymbol symbol)
        {
            return symbol.symbol;
        }

        public override string ToString()
        {
            return symbol;
        }

        public int CompareTo(object otherNode)
        {
            if (otherNode == null) return 1;

            if (!(otherNode is NodeSymbol))
                throw new ArgumentException("NodeSymbol struct required");

            return String.Compare(symbol, ((NodeSymbol)otherNode).symbol, StringComparison.Ordinal);
        }

        public static bool operator <(NodeSymbol ns1, NodeSymbol ns2)
        {
            return ns1.CompareTo(ns2) < 0;
        }

        public static bool operator >(NodeSymbol ns1, NodeSymbol ns2)
        {
            return ns1.CompareTo(ns2) > 0;
        }

        public static bool operator <=(NodeSymbol ns1, NodeSymbol ns2)
        {
            return ns1.CompareTo(ns2) <= 0;
        }

        public static bool operator >=(NodeSymbol ns1, NodeSymbol ns2)
        {
            return ns1.CompareTo(ns2) >= 0;
        }

        public static bool operator ==(NodeSymbol ns1, NodeSymbol ns2)
        {
            return ns1.CompareTo(ns2) == 0;
        }

        public static bool operator !=(NodeSymbol ns1, NodeSymbol ns2)
        {
            return ns1.CompareTo(ns2) != 0;
        }

        public bool Equals(NodeSymbol other)
        {
            return string.Equals(symbol, other.symbol);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is NodeSymbol && Equals((NodeSymbol)obj);
        }

        public override int GetHashCode()
        {
            return (symbol != null ? symbol.GetHashCode() : 0);
        }
    }
}
