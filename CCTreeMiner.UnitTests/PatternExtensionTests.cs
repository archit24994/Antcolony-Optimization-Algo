using CCTreeMinerV2;
using NUnit.Framework;

namespace CCTreeMiner.UnitTests
{
    [TestFixture]
    public class PatternExtensionTests
    {
        private readonly NodeSymbol backTrack = "^";

        static PatternTree ToPatternTree(string preString)
        {
            const char separator = ',';

            const bool notCare = true;
            var miningParams = new MiningParams(SubtreeType.Induced,
                                                notCare,
                                                notCare,
                                                notCare,
                                                notCare,
                                                SupportType.Hybrid,
                                                1,
                                                1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var segments = preString.Split(new[] {separator});

            var symbols = new NodeSymbol[segments.Length];

            for (var i = 0; i < segments.Length; i++)
            {
                symbols[i] = segments[i];
            }

            return PatternTree.Create(symbols, notCare, miningParams);
        }

        //            X                 A                                                                               
        //            |                 |                                                                                                                                                                                  
        //            Y                / \                                                                                                                                                                                 
        //            |               B   C                                                                             
        //            A              / \                                                                                                  
        //         ___|___          C   D                                                                                          
        //        /   |   \                                                                                              
        //       B    B    C                                                                                             
        //       |   / \                                                                                                 
        //       C  C   D                      
        //                           
        //        SuperTree         Subtree                       
        [Test]
        public void IsInducedSuperPattern_Test_True_01()
        {
            const string largerTree = "X,Y,A,B,C,^,^,B,C,^,D,^,^,C,^,^,^,^";
            const string smallerTree = "A,B,C,^,D,^,^,C,^,^";

            var super = ToPatternTree(largerTree);
            var sub = ToPatternTree(smallerTree);

            var isSuperPattern = sub.IsInducedSuperPattern(super, backTrack);

            Assert.IsTrue(isSuperPattern);
        }

        //            X                 A                                                                               
        //            |                 |                                                                                                                                                                                  
        //            Y                / \                                                                                                                                                                                 
        //            |               B   C                                                                             
        //            A              / \                                                                                                  
        //         ___|___          D   C                                                                                          
        //        /   |   \                                                                                              
        //       B    B    C                                                                                             
        //       |   / \                                                                                                 
        //       C  C   D                      
        //                           
        //        SuperTree         Not a SubTree                       
        [Test]
        public void IsInducedSuperPattern_Test_False_01()
        {
            const string largerTree = "X,Y,A,B,C,^,^,B,C,^,D,^,^,C,^,^,^,^";
            const string smallerTree = "A,B,D,^,C,^,^,C,^,^";

            var larger = ToPatternTree(largerTree);
            var smaller = ToPatternTree(smallerTree);

            var isSuperPattern = smaller.IsInducedSuperPattern(larger, backTrack);

            Assert.IsFalse(isSuperPattern);
        }

        //            X                X                                                                                 
        //            |                |                                                                                                                                                                                    
        //            Y                Y                                                                                                                                                                                    
        //            |                |                                                                                 
        //            A                A                                                                                                   
        //         ___|___          ___|___                                                                                         
        //        /   |   \        /   |   \                                                                              
        //       B    B    C      B    B    C                                                                             
        //       |   / \          |   / \                                                                                 
        //       C  C   D         C  C   D      
        //                           
        //        SuperTree         Subtree                       
        [Test]
        public void IsInducedSuperPattern_Test_True_02()
        {
            const string superTree = "X,Y,A,B,C,^,^,B,C,^,D,^,^,C,^,^,^,^";
            const string subTree = "X,Y,A,B,C,^,^,B,C,^,D,^,^,C,^,^,^,^";

            var super = ToPatternTree(superTree);
            var sub = ToPatternTree(subTree);

            var isSuperPattern = sub.IsInducedSuperPattern(super, backTrack);

            Assert.IsTrue(isSuperPattern);
        }

        //            X                X                                                                                 
        //            |                |                                                                                                                                                                                    
        //            Y                Y                                                                                                                                                                                    
        //            |                |                                                                                 
        //            A                X                                                                                                   
        //         ___|___          ___|___                                                                                         
        //        /   |   \        /   |   \                                                                              
        //       B    B    C      B    B    C                                                                             
        //       |   / \          |   / \                                                                                 
        //       C  C   D         C  C   D      
        //                           
        //        SuperTree       Not a Subtree                       
        [Test]
        public void IsInducedSuperPattern_Test_False_02()
        {
            const string largerTree = "X,Y,A,B,C,^,^,B,C,^,D,^,^,C,^,^,^,^";
            const string smallerTree = "X,Y,X,B,C,^,^,B,C,^,D,^,^,C,^,^,^,^";

            var larger = ToPatternTree(largerTree);
            var smaller = ToPatternTree(smallerTree);

            var isSuperPattern = smaller.IsInducedSuperPattern(larger, backTrack);

            Assert.IsFalse(isSuperPattern);
        }

        //            X                X                                                                                 
        //            |                |                                                                                                                                                                                    
        //            Y                Y                                                                                                                                                                                    
        //            |                |                                                                                 
        //            A                A                                                                                                   
        //         ___|___          ___|___                                                                                         
        //        /   |   \        /   |   \                                                                              
        //       B    B    C      B    B    C                                                                             
        //       |   / \                                                                                         
        //       C  C   D              
        //                           
        //        SuperTree         Subtree                       
        [Test]
        public void IsInducedSuperPattern_Test_True_03()
        {
            const string superTree = "X,Y,A,B,C,^,^,B,C,^,D,^,^,C,^,^,^,^";
            const string subTree = "X,Y,A,B,^,B,^,C,^,^,^,^";

            var super = ToPatternTree(superTree);
            var sub = ToPatternTree(subTree);

            var isSuperPattern = sub.IsInducedSuperPattern(super, backTrack);

            Assert.IsTrue(isSuperPattern);
        }

        //            X                X                                                                                 
        //            |                |                                                                                                                                                                                    
        //            Y                Y                                                                                                                                                                                    
        //            |                |                                                                                 
        //            A                A                                                                                                   
        //         ___|___          ___|___                                                                                         
        //        /   |   \        /  | |  \                                                                              
        //       B    B    C      B   B B   C                                                                             
        //       |   / \                                                                                         
        //       C  C   D              
        //                           
        //        SuperTree       Not a Subtree                       
        [Test]
        public void IsInducedSuperPattern_Test_False_03()
        {
            const string largerTree = "X,Y,A,B,C,^,^,B,C,^,D,^,^,C,^,^,^,^";
            const string smallerTree = "X,Y,A,B,^,B,^,B,^,C,^,^,^,^";

            var larger = ToPatternTree(largerTree);
            var smaller = ToPatternTree(smallerTree);

            var isSuperPattern = smaller.IsInducedSuperPattern(larger, backTrack);

            Assert.IsFalse(isSuperPattern);
        }

        //            A                        A               
        //        ____|_____               ____|_____          
        //       /  |    |  \             /  |    |  \         
        //      B   B    D   F           B   B    D   F        
        //      |  / \   |   |           |  / \   |   |        
        //      C C   C  E   A           C C   C  E   A        
        //               ____|_____               ____|_____   
        //              /  |    |  \             /  |    |  \  
        //             B   B    F   D           B   B    F   D 
        //             |  / \   |   | 
        //             C C   C  A   E 
        //                                    
        //           SuperTree            Subtree                       
        [Test]
        public void IsInducedSuperPattern_Test_True_04()
        {
            const string superTree = "A,B,C,^,^,B,C,^,C,^,^,D,E,^,^,F,A,B,C,^,^,B,C,^,C,^,^,F,A,^,^,D,E,^,^,^,^,^";
            const string subTree = "A,B,C,^,^,B,C,^,C,^,^,D,E,^,^,F,A,B,^,B,^,F,^,D,^,^,^,^";

            var super = ToPatternTree(superTree);
            var sub = ToPatternTree(subTree);

            var isSuperPattern = sub.IsInducedSuperPattern(super, backTrack);

            Assert.IsTrue(isSuperPattern);
        }

        //            A                        A               
        //        ____|_____               ____|_____          
        //       /  |    |  \             /  |    |  \         
        //      B   B    D   F           B   B    D   F        
        //      |  / \   |   |           |  / \   |   |        
        //      C C   C  E   A           C C   C  E   A        
        //               ____|_____               ____|_____   
        //              /  |    |  \             /  |    |  \  
        //             B   B    F   D           B   B    F   D 
        //             |  / \   |   |                    |
        //             C C   C  A   E                    B
        //                                    
        //           SuperTree            Subtree                       
        [Test]
        public void IsInducedSuperPattern_Test_False_04()
        {
            const string largerTree = "A,B,C,^,^,B,C,^,C,^,^,D,E,^,^,F,A,B,C,^,^,B,C,^,C,^,^,F,A,^,^,D,E,^,^,^,^,^";
            const string smallerTree = "A,B,C,^,^,B,C,^,C,^,^,D,E,^,^,F,A,B,^,B,^,F,^,B,^,D,^,^,^,^";

            var larger = ToPatternTree(largerTree);
            var smaller = ToPatternTree(smallerTree);

            var isSuperPattern = smaller.IsInducedSuperPattern(larger, backTrack);

            Assert.IsFalse(isSuperPattern);
        }

        //               A                       A                    
        //            ___|______                 |______             
        //           /   |      \                |      \            
        //          A    A       A               A       A               
        //              _|_     _|_             _|_     _|_     
        //             /   \   /   \           /   \   /   \     
        //            A     A A     A         A     A A     A   
        //                   / \   / \               / \   / \ 
        //                  A   A A   A             A   A A   A   
        //                          
        //               Tree                   SubTree              
        [Test]
        public void IsInducedSuperPattern_Test_True_05()
        {
            const string superTree = "A,A,^,A,A,^,A,^,^,A,A,A,^,A,^,^,A,A,^,A,^,^,^,^";
            const string subTree = "A,A,A,^,A,^,^,A,A,A,^,A,^,^,A,A,^,A,^,^,^,^";

            var super = ToPatternTree(superTree);
            var sub = ToPatternTree(subTree);

            var isSuperPattern = sub.IsInducedSuperPattern(super, backTrack);

            Assert.IsTrue(isSuperPattern);
        }

        //                                    A                                        B                   B                                    
        //                         ___________|____________                        ____|____              / \ 
        //                        /                        \                      /         \            C   J
        //                       B                          Q                    C           J           |   |           
        //                   ____|____                  ____|____               _|_         _|_          G   N                             
        //                  /         \                /         \             /   \       /   \         |   |                             
        //                 C           J              R           Y           D     G     K     N        H   P                             
        //                _|_         _|_            _|_         _|_         / \   / \   / \   / \                                   
        //               /   \       /   \          /   \       /   \       E   F H   I L   M O   P                                  
        //              D     G     K     N        S     V     Z     c                                      
        //             / \   / \   / \   / \      / \   / \   / \   / \                                     
        //            E   F H   I L   M O   P    T   U W   X a   b d   e
        //
        //                                Tree                                    SubTree                 SubTree        
        [Test]
        public void IsInducedSuperPattern_Test_True_06()
        {
            const string fullBinaryTreeNoDuplicationDepth5 = "A,B,C,D,E,^,F,^,^,G,H,^,I,^,^,^,J,K,L,^,M,^,^,N,O,^,P,^,^,^,^,Q,R,S,T,^,U,^,^,V,W,^,X,^,^,^,Y,Z,a,^,b,^,^,c,d,^,e,^,^,^,^,^";

            var subTrees = new[]
            {
                "B,C,D,E,^,F,^,^,G,H,^,I,^,^,^,J,K,L,^,M,^,^,N,O,^,P,^,^,^,^",
                "B,C,G,H,^,^,^,J,N,P,^,^,^,^"
            };

            var super = ToPatternTree(fullBinaryTreeNoDuplicationDepth5);

            foreach (var subTree in subTrees)
            {
                var sub = ToPatternTree(subTree);

                var isSuperPattern = sub.IsInducedSuperPattern(super, backTrack);

                Assert.IsTrue(isSuperPattern);
            }
        }

        //                         A                                    B               B          A                               
        //               __________|___________                     ____|____          / \        / \ 
        //              /                      \                   /         \        C   J      B   B
        //             B                        B                 C           J       |   |      |   |      
        //         ____|____                ____|____            _|_         _|_      G   N      C   J                        
        //        /         \              /         \          /   \       /   \     |   |      |   |                        
        //       C           J            C           J        D     G     K     N    H   P      D   N                        
        //      _|_         _|_          _|_         _|_      / \   / \   / \   / \              |   |                   
        //     /   \       /   \        /   \       /   \    E   F H   I L   M O   P             E   P                    
        //    D     G     K     N      D     G     K     N                               
        //   / \   / \   / \   / \    / \   / \   / \   / \                              
        //  E   F H   I L   M O   P  E   F H   I L   M O   P
        //
        //                      Tree                                 SubTree          SubTree        
        [Test]
        public void IsInducedSuperPattern_Test_True_07()
        {
            const string fullBinaryTreeNoDuplicationDepth5 = "A,B,C,D,E,^,F,^,^,G,H,^,I,^,^,^,J,K,L,^,M,^,^,N,O,^,P,^,^,^,^,B,C,D,E,^,F,^,^,G,H,^,I,^,^,^,J,K,L,^,M,^,^,N,O,^,P,^,^,^,^,^";

            var subTrees = new[]
            {
                "B,C,D,E,^,F,^,^,G,H,^,I,^,^,^,J,K,L,^,M,^,^,N,O,^,P,^,^,^,^",
                "B,C,G,H,^,^,^,J,N,P,^,^,^,^",
                "A,B,C,D,E,^,^,^,^,B,J,N,P,^,^,^,^,^"
            };

            var super = ToPatternTree(fullBinaryTreeNoDuplicationDepth5);

            foreach (var subTree in subTrees)
            {
                var sub = ToPatternTree(subTree);

                var isSuperPattern = sub.IsInducedSuperPattern(super, backTrack);

                Assert.IsTrue(isSuperPattern);
            }
        }
    }
}
