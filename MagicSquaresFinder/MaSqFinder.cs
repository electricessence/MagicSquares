using MagicSquares;
using Open.Threading;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicSquaresFinder
{
    public partial class MaSqFinder : Form
    {
        public MaSqFinder()
        {
            InitializeComponent();
        }

        readonly Combinations Combinations = new();

        public int CombinationLength => (int)CombinationLengthField.Value;

        public IReadOnlyList<ImmutableArray<int>> CombinationValues
            => Combinations.GetIndexes(CombinationLength);

        readonly DataTable CombinationSource = new();

        private void MaSqFinder_Load(object sender, EventArgs e)
        {
            CombinationView.DataSource = CombinationSource;
        }

        private void CombinationLength_ValueChanged(object sender, EventArgs e)
        {
            ThreadSafety.LockConditional(
                CombinationSource,
                () => CombinationLengthField.Enabled,
                () =>
                {
                    CombinationLengthField.Enabled = false;
                    CombinationSource.Clear();
                    CombinationSource.Columns.Clear();
                    var len = CombinationLength;
                    for (var i = 0; i < len; i++)
                        CombinationSource.Columns.Add();

                    CombinationView.DataSource = null;

                    var n = 0;
                    foreach (var c in CombinationValues)
                    {
                        var r = CombinationSource.NewRow();
                        r.ItemArray = c.Cast<object>().ToArray();
                        CombinationSource.Rows.Add(r);
                        ++n;
                        if(n%100==99)
                        {
                            TotalCombinationsField.Text = (++n).ToString();
                            TotalCombinationsField.Update();
                        }
                    }

                    TotalCombinationsField.Text = n.ToString();
                    CombinationLengthField.Enabled = true;
                    CombinationView.DataSource = CombinationSource;
                });
        }
    }
}
