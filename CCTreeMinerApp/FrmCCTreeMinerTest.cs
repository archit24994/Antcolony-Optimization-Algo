using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using CCTreeMinerV2;
using TheProblem;

namespace CCTreeMinerApp
{
    public partial class FrmCCTreeMinerTest : Form
    {
        //private readonly FTreeViewer treeViewer = new FTreeViewer();

        private List<ITextTree> forest;
        private MiningParams param;

        private MiningResults miningResults;

        public FrmCCTreeMinerTest()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //treeViewer.Location = new Point(0, 100);
            //treeViewer.Show();
            //treeViewer.WindowState = FormWindowState.Minimized;
        }   

        private void ShowForest()
        {
            if (InvokeRequired)
            {
                var a = new Action(ShowForest);
                Invoke(a);
            }
            else
            {
                lboxForest.Items.Clear();
                if (forest == null) return;

                foreach (var myTree in forest)
                {
                    lboxForest.Items.Add(myTree);
                }

                lblForest.Text = "Forest: Number of Trees = " + lboxForest.Items.Count;
            }            
        }

        private void btnMine_Click(object sender, EventArgs e)
        {
            if (forest == null) return;

            if (isWorking) return;

            gboxForest.Enabled = false;
            gboxParams.Enabled = false;
            ClearListBoxs();

            Task.Factory.StartNew(DoWork).ContinueWith(Callback);
        }

        void Callback(IAsyncResult itfAr)
        {
            if (InvokeRequired)
            {
                Action a = () => Callback(itfAr);
                Invoke(a);
            }
            else
            {
                gboxForest.Enabled = true;
                gboxParams.Enabled = true;
                //MessageBox.Show(treeMiner.MiningInformation, "CCTreeMiner: Mining Result.");
            }            
        }

        bool isWorking;
        private void DoWork()
        {
            if (isWorking) throw new InvalidOperationException();

            try
            {
                isWorking = true;
                param = new MiningParams(
                    subtreeType: GetSubtreeType(),
                    mineOrdered: cbOrdered.Checked,
                    mineFrequent: cbMineFrequent.Checked,
                    mineClosed: cbMineClosed.Checked,
                    mineMaximal: cbMineMiximal.Checked,
                    supportType: GetSupportType(),
                    thresholdRoot: Convert.ToInt32(txtRootSupport.Text),
                    thresholdTransaction: Convert.ToInt32(txtTransactionSupport.Text),
                    separator: ',',
                    backTrackSymbol: "^");

                if (!param.MineOrdered)
                {
                    foreach (var tree in forest)
                    {
                        Canonicalizer.Canonicalize(tree);
                        PreorderIndexBuilder.BuildPreorderIndex(tree);
                    }
                    ShowForest();
                }

                var trees = forest.ToList();

                var rslt = CCTreeMiner.Mine(trees, param);

                ShowResults(rslt);
            }            
            finally
            {
                isWorking = false;
            }
        }

        private SupportType GetSupportType()
        {
            if (rbTransaction.Checked) return SupportType.Transaction;
            if (rbRootOccurrence.Checked) return SupportType.RootOccurrence;
            if (rbHybrid.Checked) return SupportType.Hybrid;
            
            throw new Exception();
        }

        private SubtreeType GetSubtreeType()
        {
            if (rbBottomUp.Checked) return SubtreeType.BottomUp;
            if (rbInduced.Checked) return SubtreeType.Induced;
            if (rbEmbedded.Checked) return SubtreeType.Embedded;

            throw new Exception();
        }
        
        private void ShowResults(MiningResults rslt)
        {
            if (InvokeRequired)
            {
                var a = new Action<MiningResults>(ShowResults);
                Invoke(a, new object[] {rslt});
            }
            else
            {
                ClearListBoxs();
                
                if (param.MineFrequent)
                {
                    foreach (var v in rslt.FrequentPatterns) lboxFrequent.Items.Add(v);
                    lblFrequent.Text = "Frequent = " + lboxFrequent.Items.Count;
                }

                if (param.MineClosed)
                {
                    foreach (var v in rslt.ClosedPatterns) lboxClosed.Items.Add(v);
                    lblClosed.Text = "Closed = " + lboxClosed.Items.Count;
                }

                if (param.MineMaximal)
                {
                    foreach (var v in rslt.MaximalPatterns) lboxMaximal.Items.Add(v);
                    lblMaximal.Text = "Maximal = " + lboxMaximal.Items.Count;
                }
                miningResults = rslt;
                lblMiningParams.Text = rslt.MiningParams.ToString();

                lblMiningResult.Text = rslt.ToString().Substring(lblMiningParams.Text.Length + 2);
               
                MessageBox.Show(rslt.ToString());
            }
        }

        private void ClearListBoxs()
        {
            lboxFrequent.Items.Clear();
            lblFrequent.Text = "Frequent:";

            lboxClosed.Items.Clear();
            lblClosed.Text = "Closed:";
            
            lboxMaximal.Items.Clear();
            lblMaximal.Text = "Maximal:";

            lboxOccurrences.Items.Clear();
            lblOccurrence.Text = "Occurrence:";
        }
        
        private string GetFilePath()
        {
            var dlg = new OpenFileDialog
            {
                FileName = "sample",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            var startupPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            if (startupPath == null) throw new Exception();

            var dInfo = new DirectoryInfo(startupPath + "\\Forests");
            dInfo.Create();

            dlg.FileName = dInfo + "\\sample.txt";
            var result = dlg.ShowDialog();

            return (result == DialogResult.OK) ? dlg.FileName : string.Empty;
        }

        private void lbox_SelectedValueChanged(object sender, EventArgs e)
        {
            var lbox = sender as ListBox;
            if (lbox == null) return;

            if (lbox != lboxOccurrences) lboxOccurrences.Items.Clear();

            var pt = lbox.SelectedItem as PatternTree;
            if (pt != null)
            {
                lboxOccurrences.Tag = pt;
                if (pt.Occurrences.Count > 0)
                {
                    ShowTree(pt.Occurrences[0].TreeId, pt.Occurrences[0], lboxOccurrences.Tag as PatternTree);

                    foreach (var iocc in pt.Occurrences)
                    {
                        lboxOccurrences.Items.Add(iocc);
                    }
                }

                lblOccurrence.Text = "Occurrence = " + lboxOccurrences.Items.Count;
                return;
            }
            
            var occ = lbox.SelectedItem as IOccurrence;
            if (occ != null)
            {
                ShowTree(occ.TreeId, occ, lboxOccurrences.Tag as PatternTree);
                return;
            }
            
            var tree = lbox.SelectedItem as ITextTree;
            if (tree != null)
            {
                ShowTree(tree.TreeId, null, null);
            }
        }

        private void lbox_DoubleClick(object sender, EventArgs e)
        {
            var lbox = sender as ListBox;
            if (lbox == null) return;

            var pt = lbox.SelectedItem as PatternTree;
            if (pt == null) return;

            var trees = new List<ITextTree>();
            foreach (var treeId in pt.SupportedTransectionIds)
            {
                trees.Add(GetTree(treeId));
            }

            var patternViewer = new FOccurrencesViewer(trees, pt);
            patternViewer.Show();
        }
        
        private void ShowTree(string treeId, IOccurrence occ, PatternTree pt)
        {
            var tree = GetTree(treeId);
            if (tree == null) return;
           
            treeVisualizer.SetTree(tree);
            treeVisualizer.SetHighLightOccurrence(occ, pt);
        }

        private ITextTree GetTree(string treeId)
        {
            return forest.FirstOrDefault(tree => tree.TreeId.Equals(treeId));
        }

        private void btnGenerateForest_Click(object sender, EventArgs e)
        {
            var fs = GetForestSpecification();
            if (fs == null) return;
            
            forest = ForestGenerator.Generate(fs, "^");

            var sfd = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt", 
                FileName = "forest.txt"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrEmpty(sfd.FileName)) return;

            ForestGenerator.SaveForestToFile(forest, sfd.FileName, false);

        }

        private ForestSpecification GetForestSpecification()
        {
            int numberOfTrees, maxFanout, maxTreeDepth, numberOfSymbols;

            try
            {
                numberOfTrees = Convert.ToInt32(txtNumberOfTrees.Text);
                maxFanout = Convert.ToInt32(txtMaximalFanout.Text);
                maxTreeDepth = Convert.ToInt32(txtMaximalDepth.Text);
                numberOfSymbols = Convert.ToInt32(txtNumberOfSymbols.Text);
            }
            catch
            {
                MessageBox.Show(@"Please check your input!");
                return null;
            }

            var symbols = GetSymbols(numberOfSymbols);

            return new ForestSpecification(symbols, numberOfTrees, -1, maxTreeDepth, maxFanout);
        }

        private static List<NodeSymbol> GetSymbols(int number)
        {
            var symbols = new List<NodeSymbol>();

            if (number <= 52)
            {
                const char upperA = 'A';
                var next = 0;

                var iAdded = 0;
                while (iAdded ++ < number)
                {
                    var c = (char)(upperA + (char) next ++);
                    symbols.Add(c.ToString(CultureInfo.InvariantCulture));

                    if ((char)(upperA + next) == 'Z' + 1) 
                        next += 6;
                }
            }
            else
            {
                for (var i = 0; i < number; i++)
                {
                    symbols.Add(i.ToString(CultureInfo.InvariantCulture));
                }
            }

            return symbols;
        }

        private void btnLoadForest_Click(object sender, EventArgs e)
        {
            var path = GetFilePath();
            if (string.IsNullOrEmpty(path)) return;

            try
            {
                forest = ForestGenerator.ReadForestFromFile(path, "^", ',');
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            

            foreach (var tree in forest)
            {
                PreorderIndexBuilder.BuildPreorderIndex(tree);
            }

            ShowForest();
        }

        private void FrmCCTreeMinerTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            //treeViewer.Close();
        }

        

    }
}
