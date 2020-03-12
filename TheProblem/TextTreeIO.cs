using System;
using System.Collections.Generic;
using System.IO;
using CCTreeMinerV2;

namespace TheProblem
{
    public class TextTreeIO
    {
        public static List<ITextTree> ReadForestFromFile(string path, char seperator, NodeSymbol backTrack)
        {
            if (string.IsNullOrEmpty(backTrack)) throw new ArgumentNullException("backTrack");

            var forest = new List<ITextTree>();
            
            using (var f = new FileInfo(path).OpenText())
            {
                string treeInString;

                while ((treeInString = f.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(treeInString) || treeInString.StartsWith("//")) continue;

                    var tree = TextTreeBuilder<TextTree, TreeNode>.ConvertToTextTree(
                        treeInString, seperator, backTrack);

                    if (tree == null) continue;

                    forest.Add(tree);
                }
            }

            return forest;
        }

        public static void SaveForestToFile(List<ITextTree> forest, string path, bool indexed)
        {
            if (forest == null) throw new ArgumentNullException("forest");

            using (var f = new FileInfo(path).CreateText())
            {
                foreach (var tree in forest)
                {
                    f.WriteLine("{0}{1}{2}", tree.TreeId, tree.Separator,
                        indexed ? tree.ToPreorderStringWithIndex() : tree.ToPreorderString());
                }
            }
        }

    }
}
