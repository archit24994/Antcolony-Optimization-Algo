using System;
using System.Collections.Generic;
using System.Linq;
using CCTreeMinerV2;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace CCTreeMiner.UnitTests
{
    [TestFixture]
    public class CCTreeMinerInducedTests
    {
        const bool Frequent = true;
        const bool Closed = true;
        const bool Maximal = true;
        const bool Ordered = true;

        const int RootOccurrenceThreshold1 = 1;
        const int TransactionThreshold1 = 1;

        const int RootOccurrenceThreshold2 = 2;
        const int TransactionThreshold2 = 2;
                                                     
        //            A
        //           / \    
        //          B   E    
        //         /\   /\
        //        C  D F  G   
        /// <summary>
        /// Suppose all subtree patterns are frequent(transaction threshold is 1 and
        /// root occurrence threshold is 1):
        /// This database contains one transaction, so the transaction itself is the
        /// only pattern that is closed and maximal. 
        /// Tests:
        /// 1. If all these frequent patterns can be discovered.
        /// 2. Discover the only closed pattern.
        /// 3. Discover the only maximal pattern. 
        /// </summary>
        [Test]
        public void Mine_FullBinaryTreeOfDepth3WithNoDuplicateSymbols()
        {
            var treeSet = new List<ITextTree>();

            const string t1 = "A,B,C,^,D,^,^,E,F,^,G,^,^,^";

            treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + t1, Global.Seperator, Global.BackTrack));
            
            var miningParams = new MiningParams(SubtreeType.Induced, 
                                                Ordered, 
                                                Frequent, 
                                                Closed, 
                                                Maximal, 
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold1, 
                                                TransactionThreshold1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            var numberFrequent = treeSet[0].InducedSubPatternUpperBound();
            const int numberClosed = 1;
            const int numberMaximal = 1;

            Assert.AreEqual(numberFrequent, actual.FrequentPatternsCount);
            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);
            Assert.AreEqual(t1, actual.MaximalPatterns[0].PreorderString);
        }
        
        //        A 
        //     ___|______
        //    /   |      \                                                   
        //   B    C       F     
        //       _|_     _|_      
        //      /   \   /   \      
        //     D     E G     J  
        //            / \   / \                             
        //           H   I K   L
        /// <summary>
        /// Suppose all subtree patterns are frequent.
        /// Tests if all these frequent patterns can be discovered.
        /// </summary>
        [Test]
        public void Mine_NoDuplicateSymbols_DiscoverAllFrequents_01()
        {
            var treeSet = new List<ITextTree>();

            const string t1 = "A,B,^,C,D,^,E,^,^,F,G,H,^,I,^,^,J,K,^,L,^,^,^,^";

            treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + t1, Global.Seperator, Global.BackTrack));

            
            var miningParams = new MiningParams(SubtreeType.Induced, 
                                                Ordered, 
                                                Frequent, 
                                                !Closed, 
                                                !Maximal, 
                                                SupportType.Hybrid, 
                                                RootOccurrenceThreshold1,
                                                TransactionThreshold1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            var numberFrequent = treeSet[0].InducedSubPatternUpperBound();

            Assert.AreEqual(numberFrequent, actual.FrequentPatternsCount);
        }

        //            A 
        //     _______|__________
        //    /   |       |      \    
        //   B    C       F       M
        //       _|_     _|_      |
        //      /   \   /   \     |
        //     D     E G     J    N
        //            / \   / \   |
        //           H   I K   L  O
        /// <summary>
        /// Suppose all subtree patterns are frequent.
        /// Tests if all these frequent patterns can be discovered.
        /// </summary>
        [Test]
        public void Mine_NoDuplicateSymbols_DiscoverAllFrequents_02()
        {
            var treeSet = new List<ITextTree>();

            const string t1 = "A,B,^,C,D,^,E,^,^,F,G,H,^,I,^,^,J,K,^,L,^,^,^,M,N,O,^,^,^,^";

            treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + t1, Global.Seperator, Global.BackTrack));

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                Ordered,
                                                Frequent,
                                                !Closed,
                                                !Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold1,
                                                TransactionThreshold1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            var numberFrequent = treeSet[0].InducedSubPatternUpperBound();

            Assert.AreEqual(numberFrequent, actual.FrequentPatternsCount);
        }

        //               A 
        //            ___|______
        //           /   |      \  
        //          A    A       A                                                           
        //              _|_     _|_         
        //             /   \   /   \         
        //            A     A A     A    
        //                   / \   / \ 
        //                  A   A A   A       
        //
        [Test]
        public void Mine_TheSameSymbol_DiscoverClosedAndMaximal_01()
        {
            var treeSet = new List<ITextTree>();

            const string t1 = "A,A,^,A,A,^,A,^,^,A,A,A,^,A,^,^,A,A,^,A,^,^,^,^";

            treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + t1, Global.Seperator, Global.BackTrack));

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold1,
                                                TransactionThreshold1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 4;
            const int numberMaximal = 1;

            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);
            Assert.AreEqual(t1, actual.MaximalPatterns[0].PreorderString);
        }

        //               A                       A 
        //            ___|______              ___|______
        //           /   |      \            /   |      \  
        //          A    A       A          A    A       A                                                       
        //              _|_     _|_             _|_     _|_     
        //             /   \   /   \           /   \   /   \     
        //            A     A A     A         A     A A     A   
        //                   / \   / \               / \   / \ 
        //                  A   A A   A             A   A A   A   
        //                          
        //               Tree 1                   Tree 2   
        /// <summary>
        /// Database contains two identical transaction.
        /// Discover closed and maximal.
        /// </summary>
        [Test]
        public void Mine_TheSameSymbol_DiscoverClosedAndMaximal_02()
        {
            var treeSet = new List<ITextTree>();

            const string t1 = "A,A,^,A,A,^,A,^,^,A,A,A,^,A,^,^,A,A,^,A,^,^,^,^";
            const string t2 = "A,A,^,A,A,^,A,^,^,A,A,A,^,A,^,^,A,A,^,A,^,^,^,^";

            treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("001 " + t1, Global.Seperator, Global.BackTrack));
            treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree("002 " + t2, Global.Seperator, Global.BackTrack));

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold2,
                                                TransactionThreshold2,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 4;
            const int numberMaximal = 1;

            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);
            Assert.AreEqual(t1, actual.MaximalPatterns[0].PreorderString);
        }

        //               A                       A                       A    
        //            ___|______              ___|______                 |
        //           /   |      \            /   |      \                |
        //          A    A       A          A    A       A               A                                         
        //              _|_     _|_             _|_     _|_     
        //             /   \   /   \           /   \   /   \     
        //            A     A A     A         A     A A     A   
        //                   / \   / \               / \   / \ 
        //                  A   A A   A             A   A A   A   
        //                          
        //               Tree 1                   Tree 2               Tree 3
        /// <summary>
        /// Database contains two identical transaction.
        /// Discover closed and maximal.
        /// </summary>
        [Test]
        public void Mine_TheSameSymbol_DiscoverClosedAndMaximal_03()
        {
            var treeSet = new List<ITextTree>();

            var trees = new[]
            {
                "A,A,^,A,A,^,A,^,^,A,A,A,^,A,^,^,A,A,^,A,^,^,^,^",
                "A,A,^,A,A,^,A,^,^,A,A,A,^,A,^,^,A,A,^,A,^,^,^,^",
                "A,A,^,^"
            };

            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold2,
                                                TransactionThreshold2,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 5;
            const int numberMaximal = 1;

            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);
            Assert.AreEqual(trees[0], actual.MaximalPatterns[0].PreorderString);
        }

        #region Ordered vs Unordered 01
        //           A                    A                                               
        //       ____|____            ____|____                                                     
        //      / |  |  | \          / |  |  | \                                              
        //     B  C  B  C  B        B  B  B  C  C                                            
        //     |  |  |  |  |        |  |  |  |  |                                              
        //     B  C  B  C  B        B  B  B  C  C                           
        //          / \            / \                                                
        //         E   D          D   E                                               
        //                                                                         
        //        Tree 1               Tree 2     
        private readonly string[] treesInducedOrderedVsUnordered01 =
        {
            // Tree 1
            "A,B,B,^,^,C,C,^,^,B,B,E,^,D,^,^,^,C,C,^,^,B,B,^,^,^",
            // Tree 2
            "A,B,B,D,^,E,^,^,^,B,B,^,^,B,B,^,^,C,C,^,^,C,C,^,^,^"
        };
        /// <summary>
        /// If ordered, the two trees are different,
        /// Else, they are the same.
        /// Be aware, Tree2 is the canonical form.
        /// </summary>
        [Test]
        public void Mine_OrderedVsUnordered_01_Ordered()
        {
            var treeSet = new List<ITextTree>();

            var trees = treesInducedOrderedVsUnordered01;
            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold2,
                                                TransactionThreshold2,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 11;
            const int numberMaximal = 7;
            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);

            var maximals = new[]
            {
                "A,B,B,D,^,^,^,B,B,^,^,^",
                "A,B,B,D,^,^,^,C,C,^,^,^",
                "A,B,B,E,^,^,^,B,B,^,^,^",
                "A,B,B,E,^,^,^,C,C,^,^,^",
                "A,B,B,^,^,B,B,^,^,B,B,^,^,^",
                "A,B,B,^,^,B,B,^,^,C,C,^,^,^",
                "A,B,B,^,^,B,B,^,^,C,C,^,^,^"
            };

            foreach (var maximal in maximals)
            {
                Assert.IsTrue(Array.Exists(actual.MaximalPatterns, pt => pt.PreorderString.Equals(maximal)));
            }

            var closeds = new[]
            {
                "B,B,^,^",
                "B,^",
                "C,C,^,^",
                "C,^"
            };

            foreach (var closed in closeds)
            {
                Assert.IsTrue(Array.Exists(actual.ClosedPatterns, pt => pt.PreorderString.Equals(closed)));
            }
        }
        /// <summary>
        /// If unordered, the two trees are identical and there should be 
        /// only one maximal subtree pattern with the same structure as Tree2.
        /// Be aware, Tree2 is the canonical form.
        /// </summary>
        [Test]
        public void Mine_OrderedVsUnordered_01_Unordered()
        {
            var treeSet = new List<ITextTree>();

            var trees = treesInducedOrderedVsUnordered01;

            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                !Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold2,
                                                TransactionThreshold2,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 5;
            const int numberMaximal = 1;
            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);
            
            Assert.AreEqual(trees[1], actual.MaximalPatterns[0].PreorderString);

            var closeds = new[]
            {
                "B,B,^,^",
                "B,^",
                "C,C,^,^",
                "C,^"
            };

            foreach (var closed in closeds)
            {
                Assert.IsTrue(Array.Exists(actual.ClosedPatterns, pt => pt.PreorderString.Equals(closed)));
            }
        }
        #endregion

        #region Ordered vs Unordered 02
        //           A                  A                                      
        //          / \                / \                                      
        //         B   E              E   B                                     
        //         |   |              |   |                                     
        //         B   E              E   B                                     
        //        / \                    / \                                         
        //       C   D                  D   C                                       
        //                            
        //        Tree 1             Tree 2                                                           
        private readonly string[] treesInducedOrderedVsUnordered02 =
        {
            // Tree 1
            "A,B,B,C,^,D,^,^,^,E,E,^,^,^",
            // Tree 2
            "A,E,E,^,^,B,B,D,^,C,^,^,^,^"
        };

        [Test]
        public void Mine_OrderedVsUnordered_02_Ordered()
        {
            var treeSet = new List<ITextTree>();

            var trees = treesInducedOrderedVsUnordered02;
            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold2,
                                                TransactionThreshold2,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 5;
            const int numberMaximal = 3;
            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);

            var maximals = new[]
            {
                "A,B,B,C,^,^,^,^",
                "A,B,B,D,^,^,^,^",
                "A,E,E,^,^,^"
            };

            foreach (var maximal in maximals)
            {
                Assert.IsTrue(Array.Exists(actual.MaximalPatterns, pt => pt.PreorderString.Equals(maximal)));
            }

            var closeds = new[]
            {
                "B,^",
                "E,^"
            };

            foreach (var closed in closeds)
            {
                Assert.IsTrue(Array.Exists(actual.ClosedPatterns, pt => pt.PreorderString.Equals(closed)));
            }
        }

        [Test]
        public void Mine_OrderedVsUnordered_02_Unordered()
        {
            var treeSet = new List<ITextTree>();

            var trees = treesInducedOrderedVsUnordered02;
            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                !Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold2,
                                                TransactionThreshold2,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 3;
            const int numberMaximal = 1;
            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);

            var maximals = new[]
            {
                "A,B,B,C,^,D,^,^,^,E,E,^,^,^"
            };

            foreach (var maximal in maximals)
            {
                Assert.IsTrue(Array.Exists(actual.MaximalPatterns, pt => pt.PreorderString.Equals(maximal)));
            }

            var closeds = new[]
            {
                "B,^",
                "E,^"
            };

            foreach (var closed in closeds)
            {
                Assert.IsTrue(Array.Exists(actual.ClosedPatterns, pt => pt.PreorderString.Equals(closed)));
            }
        }
        #endregion

        #region Database 001

        //            A               
        //        ____|_____          
        //       /  |    |  \         
        //      B   B    D   F        
        //      |  / \   |   |        
        //      C C   C  E   A        
        //               ____|_____   
        //              /  |    |  \  
        //             B   B    F   D 
        //             |  / \   |   | 
        //             C C   C  A   E 
        //
        //           Tree 01
        // 
        // 
        private const string Db001Tree01 = "A,B,C,^,^,B,C,^,C,^,^,D,E,^,^,F,A,B,C,^,^,B,C,^,C,^,^,F,A,^,^,D,E,^,^,^,^,^";

        private readonly ReadOnlyCollection<string> database001 = new ReadOnlyCollection<string>(new[]
        {
            Db001Tree01
        });

        [Test]
        public void Mine_Db001_ClosedAndMaxial_Ordered_Threshold1()
        {
            var treeSet = new List<ITextTree>();

            var trees = database001.ToArray();

            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold1,
                                                TransactionThreshold1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 6;
            const int numberMaximal = 1;

            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);

            var maximals = new[]
            {
                Db001Tree01
            };

            foreach (var maximal in maximals)
            {
                Assert.IsTrue(Array.Exists(actual.MaximalPatterns, pt => pt.PreorderString.Equals(maximal)));
            }

            var closeds = new[]
            {
                "A,^",
                "B,C,^,^",
                "C,^",
                "A,B,C,^,^,B,C,^,C,^,^,D,E,^,^,^",
                "A,B,C,^,^,B,C,^,C,^,^,F,A,^,^,^"
            };

            foreach (var closed in closeds)
            {
                Assert.IsTrue(Array.Exists(actual.ClosedPatterns, pt => pt.PreorderString.Equals(closed)));
            }
        }

        [Test]
        public void Mine_Db001_ClosedAndMaxial_Ordered_Threshold2()
        {
            var treeSet = new List<ITextTree>();

            var trees = database001.ToArray();

            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold2,
                                                TransactionThreshold1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 5;
            const int numberMaximal = 2;

            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);

            var maximals = new[]
            {
                "A,B,C,^,^,B,C,^,C,^,^,D,E,^,^,^",
                "A,B,C,^,^,B,C,^,C,^,^,F,A,^,^,^"
            };

            foreach (var maximal in maximals)
            {
                Assert.IsTrue(Array.Exists(actual.MaximalPatterns, pt => pt.PreorderString.Equals(maximal)));
            }

            var closeds = new[]
            {
                "A,^",
                "B,C,^,^",
                "C,^"
            };

            foreach (var closed in closeds)
            {
                Assert.IsTrue(Array.Exists(actual.ClosedPatterns, pt => pt.PreorderString.Equals(closed)));
            }
        }

        [Test]
        public void Mine_Db001_ClosedAndMaxial_Unordered_Threshold1()
        {
            var treeSet = new List<ITextTree>();

            var trees = database001.ToArray();

            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                !Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold1,
                                                TransactionThreshold1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 5;
            const int numberMaximal = 1;

            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);

            var maximals = new[]
            {
                "A,B,C,^,C,^,^,B,C,^,^,D,E,^,^,F,A,B,C,^,C,^,^,B,C,^,^,D,E,^,^,F,A,^,^,^,^,^"
            };

            foreach (var maximal in maximals)
            {
                Assert.IsTrue(Array.Exists(actual.MaximalPatterns, pt => pt.PreorderString.Equals(maximal)));
            }

            var closeds = new[]
            {
                "A,^",
                "B,C,^,^",
                "C,^",
                "A,B,C,^,C,^,^,B,C,^,^,D,E,^,^,F,A,^,^,^"
            };

            foreach (var closed in closeds)
            {
                Assert.IsTrue(Array.Exists(actual.ClosedPatterns, pt => pt.PreorderString.Equals(closed)));
            }
        }

        [Test]
        public void Mine_Db001_ClosedAndMaxial_Unordered_Threshold2()
        {
            var treeSet = new List<ITextTree>();

            var trees = database001.ToArray();

            for (var treeId = 1; treeId <= trees.Length; treeId++)
            {
                treeSet.Add(TextTreeBuilder<TextTree, TreeNode>.
                    ConvertToTextTree(string.Format("{0:000} {1}", treeId, trees[treeId - 1]), Global.Seperator, Global.BackTrack));
            }

            var miningParams = new MiningParams(SubtreeType.Induced,
                                                !Ordered,
                                                !Frequent,
                                                Closed,
                                                Maximal,
                                                SupportType.Hybrid,
                                                RootOccurrenceThreshold1,
                                                TransactionThreshold1,
                                                Global.Seperator,
                                                Global.BackTrack);

            var actual = CCTreeMinerV2.CCTreeMiner.Mine(treeSet, miningParams);

            const int numberClosed = 5;
            const int numberMaximal = 1;

            Assert.AreEqual(numberClosed, actual.ClosedPatternsCount);
            Assert.AreEqual(numberMaximal, actual.MaximalPatternsCount);

            var maximals = new[]
            {
                "A,B,C,^,C,^,^,B,C,^,^,D,E,^,^,F,A,B,C,^,C,^,^,B,C,^,^,D,E,^,^,F,A,^,^,^,^,^"
            };

            foreach (var maximal in maximals)
            {
                Assert.IsTrue(Array.Exists(actual.MaximalPatterns, pt => pt.PreorderString.Equals(maximal)));
            }

            var closeds = new[]
            {
                "A,^",
                "B,C,^,^",
                "C,^",
                "A,B,C,^,C,^,^,B,C,^,^,D,E,^,^,F,A,^,^,^"
            };

            foreach (var closed in closeds)
            {
                Assert.IsTrue(Array.Exists(actual.ClosedPatterns, pt => pt.PreorderString.Equals(closed)));
            }
        }
        #endregion





















    }
}

//     A     
//  ___|____
//  | |    \                                                     
//  B  C  
//    / \  
//   D   E  
//                                                                  
//                                                                       
//                                                                                                                                                       
//            F   
//           / \                                                               
//          /   \                                                                                 
//         G     J                                                                                
//        / \   / \                                                                              
//       H   I K   L                                                                               
//                                                                                      
//                                                                                      
//                                                                                      
//                                                                                      
//                                                                                      
//                                                                                      
//                                                                                      
//                                                                                                             
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                                                                                                              
//                        
//                        