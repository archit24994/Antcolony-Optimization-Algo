using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using CCTreeMinerV2;
using TreeNode = CCTreeMinerV2.TreeNode;

namespace TheProblem
{
    public class ForestGenerator
    {
        static readonly Random Random = new Random();

        public static List<ITextTree> Generate(ForestSpecification fs, NodeSymbol backTrack)
        {
            if (!IsForestSpecificationValid(fs, backTrack)) return null;

            var forest = new List<ITextTree>();
            var ts = new TreeSpecification(fs.Labels) { MaxDepth = fs.MaxTreeDepth, MaxDegree = fs.MaxDegree };

            for (var i = 0; i < fs.NumberOfTrees; i++)
            {
                var tree = PlantTree(string.Format("{0:00000000}", i), ts, Random);
                forest.Add(tree);
            }

            return forest;
        }

        public static List<ITextTree> ReadForestFromFile(string path, NodeSymbol backTrack, char separator)
        {
            var forest = new List<ITextTree>();

            var fi = new FileInfo(path);
            using (var f = fi.OpenText())
            {
                string treeInString;

                while ((treeInString = f.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(treeInString) || treeInString.StartsWith("//")) continue;

                    var tree = TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree(
                        treeInString, separator, backTrack);

                    if (tree == null) continue;

                    forest.Add(tree);
                }
            }

            return forest;
        }

        public static bool SaveForestToFile(List<ITextTree> forest, string path, bool toPerorderStringWithIndex)
        {
            if (forest == null) throw new ArgumentNullException("forest");

            try
            {
                var fi = new FileInfo(path);
                using (var f = fi.CreateText())
                {
                    for (var id = 0; id < forest.Count; id++)
                    {
                        var str = toPerorderStringWithIndex ?
                            forest[id].ToPreorderStringWithIndex() : forest[id].ToPreorderString();
                        f.WriteLine("{0}{1}{2}", id, forest[id].Separator, str);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private static bool IsForestSpecificationValid(ForestSpecification fs, NodeSymbol backTrack)
        {
            string errMsg;
            if (ForestSpecification.IsForestSpecificationValid(fs, backTrack, out errMsg)) return true;

            MessageBox.Show(errMsg);
            return false;
        }

        private static TextTree PlantTree(string treeId, TreeSpecification ts, Random r)
        {
            var root = new TreeNode { Symbol = ts.Lables[r.Next(0, ts.Lables.Count)] };
            root.SetParent(null);

            Germinate(root, ts.MaxDepth, ts.MaxDegree, ts.Lables, r);

            var mytree = new TextTree { TreeId = treeId, Root = root };

            return mytree;
        }

        private static void Germinate(ITreeNode root, int maxDepth, int maxDegree, ReadOnlyCollection<NodeSymbol> lables, Random r)
        {
            var node = new TreeNode { Symbol = lables[r.Next(0, lables.Count)] };
            node.SetParent(root);

            if (node.Depth == maxDepth - 1) return;

            var degree = Random.Next(0, maxDegree + 1);

            for (var b = 0; b < degree; b++)
            {
                Germinate(node, maxDepth, maxDegree, lables, r);

                node.Children.Sort();
            }
        }


    }
}
