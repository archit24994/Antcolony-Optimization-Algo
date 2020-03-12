using CCTreeMinerV2;
using NUnit.Framework;

namespace CCTreeMiner.UnitTests
{
    [TestFixture]
    public class SubPatternNumberPredictorTests
    {
        //              A                                                 
        //             _|_    
        //            /   \        
        //           B     E                                
        //          / \   / \                                                 
        //         C   D F   G   
        //                                                                
        // Full Binary Tree of Depth 3.                    
        /// <summary>
        /// Number of subtree pattern: 37
        /// </summary>
        [Test]
        public void SubPatternUpperBoundTest_FullBinaryTreeOfDepth3WithNoDuplicationSymbols_37SubPatterns()
        {
            const string fullBinaryTreeDepth3 = "A,B,C,^,D,^,^,E,F,^,G,^,^,^";
            ITextTree tree = TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + fullBinaryTreeDepth3, Global.Seperator, Global.BackTrack);

            const ulong expected = 37;
            var actual = tree.InducedSubPatternUpperBound();
            Assert.AreEqual(expected, actual);
        }

        //                     A                                                       
        //                 ____|____       
        //                /         \           
        //               B           I                                   
        //              _|_         _|_                                                    
        //             /   \       /   \                                                   
        //            C     F     J     M                                                  
        //           / \   / \   / \   / \                                                 
        //          D   E G   H K   L N   O                     
        //                                                                
        // Full Binary Tree of Depth 4.                    
        /// <summary>
        /// Number of subtree pattern: 750
        /// </summary>
        [Test]
        public void SubPatternUpperBoundTest_FullBinaryTreeOfDepth4WithNoDuplicationSymbols_750SubPatterns()
        {
            const string fullBinaryTreeDepth4 = "A,B,C,D,^,E,^,^,F,G,^,H,^,^,^,I,J,K,^,L,^,^,M,N,^,O,^,^,^,^";
            ITextTree tree = TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + fullBinaryTreeDepth4, Global.Seperator, Global.BackTrack);

            const ulong expected = 750;
            var actual = tree.InducedSubPatternUpperBound();
            Assert.AreEqual(expected, actual);
        }

        //                                    A                                                                              
        //                         ___________|____________    
        //                        /                        \                 
        //                       B                          Q                                         
        //                   ____|____                  ____|____                                                       
        //                  /         \                /         \                                                      
        //                 C           J              R           Y                                                     
        //                _|_         _|_            _|_         _|_                                                    
        //               /   \       /   \          /   \       /   \                                                   
        //              D     G     K     N        S     V     Z     c                                                  
        //             / \   / \   / \   / \      / \   / \   / \   / \                                                 
        //            E   F H   I L   M O   P    T   U W   X a   b d   e                                                
        //                                                                
        // Full Binary Tree of Depth 5.                    
        /// <summary>
        /// Number of subtree pattern: 459829
        /// Suppose the symbols are case sensitive.
        /// </summary>
        [Test]
        public void SubPatternUpperBoundTest_FullBinaryTreeOfDepth5WithNoDuplicationSymbols_459829SubPatterns()
        {
            const string fullBinaryTreeDepth5 = "A,B,C,D,E,^,F,^,^,G,H,^,I,^,^,^,J,K,L,^,M,^,^,N,O,^,P,^,^,^,^,Q,R,S,T,^,U,^,^,V,W,^,X,^,^,^,Y,Z,a,^,b,^,^,c,d,^,e,^,^,^,^,^";
            ITextTree tree = TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + fullBinaryTreeDepth5, Global.Seperator, Global.BackTrack);

            const ulong expected = 459829;
            var actual = tree.InducedSubPatternUpperBound();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubPatternUpperBoundTest_FullBinaryTreeOfDepth6WithNoDuplicationSymbols_210067308558SubPatterns()
        {
            const string fullBinaryTreeDepth5 = "Root,A,B,C,D,E,^,F,^,^,G,H,^,I,^,^,^,J,K,L,^,M,^,^,N,O,^,P,^,^,^,^,Q,R,S,T,^,U,^,^,V,W,^,X,^,^,^,Y,Z,a,^,b,^,^,c,d,^,e,^,^,^,^,^,A,B,C,D,E,^,F,^,^,G,H,^,I,^,^,^,J,K,L,^,M,^,^,N,O,^,P,^,^,^,^,Q,R,S,T,^,U,^,^,V,W,^,X,^,^,^,Y,Z,a,^,b,^,^,c,d,^,e,^,^,^,^,^,^";
            ITextTree tree = TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + fullBinaryTreeDepth5, Global.Seperator, Global.BackTrack);

            const ulong expected = 210067308558;
            var actual = tree.InducedSubPatternUpperBound();
            Assert.AreEqual(expected, actual);
        }


        //               A 
        //            ___|______
        //           /   |      \  
        //          B    C       F                                                           
        //              _|_     _|_         
        //             /   \   /   \         
        //            D     E G     J    
        //                   / \   / \ 
        //                  H   I K   L       
        //
        //  This tree contains 304 subtree patterns.                       
        /// <summary>
        /// Number of subtree pattern: 304
        /// </summary>
        [Test]
        public void SubPatternUpperBoundTest_BinaryTreeWithNoDuplicationSymbols_304SubPatterns()
        {
            const string t1 = "A,B,^,C,D,^,E,^,^,F,G,H,^,I,^,^,J,K,^,L,^,^,^,^";
            ITextTree tree = TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + t1, Global.Seperator, Global.BackTrack);

            const ulong expected = 304;
            var actual = tree.InducedSubPatternUpperBound();
            Assert.AreEqual(expected, actual);
        }

    }
}
