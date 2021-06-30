using MagicSquares;
using Open.Collections;
using Open.Numeric;
using Open.Threading;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MagicSquaresFinder
{
    public partial class MaSqFinder : Form
	{
		public MaSqFinder()
		{
			InitializeComponent();
		}

		readonly PossibleAddens PossibleAddens = new();

		public int CombinationLength => (int)CombinationLengthField.Value;

		public int PossibleAddensCount => (int)PossibleAddensCountField.Value;

		public int PossibleAddensSum => (int)PossibleAddensSumField.Value;


		public IEnumerable<int[]> CombinationValues
			=> Enumerable.Range(0, CombinationLength).Permutations();

		public IReadOnlyList<IReadOnlyList<int>> PossibleAddenValues
			=> PossibleAddens.UniqueAddensFor(PossibleAddensSum, (int)PossibleAddensCount);

		readonly DataTable CombinationSource = new();
		readonly DataTable PossibleAddensSource = new();

		private void MaSqFinder_Load(object sender, EventArgs e)
		{
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
						if (n % 100 == 99)
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

		void UpdatePossibleAddens()
		{
			ThreadSafety.LockConditional(
				PossibleAddensSource,
				() => PossibleAddensSumField.Enabled,
				() =>
				{
					PossibleAddensSumField.Enabled = false;
					PossibleAddensCountField.Enabled = false;
					PossibleAddensSource.Clear();
					PossibleAddensSource.Columns.Clear();
					var len = PossibleAddensCount;
					for (var i = 0; i < len; i++)
						PossibleAddensSource.Columns.Add();

					PossibleAddensView.DataSource = null;

					var n = 0;
					foreach (var c in PossibleAddenValues)
					{
						var r = PossibleAddensSource.NewRow();
						r.ItemArray = c.Cast<object>().ToArray();
						PossibleAddensSource.Rows.Add(r);
						++n;
						if (n % 100 == 99)
						{
							TotalAddensField.Text = (++n).ToString();
							TotalAddensField.Update();
						}
					}

					TotalAddensField.Text = n.ToString();
					PossibleAddensSumField.Enabled = true;
					PossibleAddensCountField.Enabled = true;
					PossibleAddensView.DataSource = PossibleAddensSource;
				});

		}

		private void PossibleAddensSumField_ValueChanged(object sender, EventArgs e)
		{
			UpdatePossibleAddens();
		}

		private void PossibleAddensCountField_ValueChanged(object sender, EventArgs e)
		{
			UpdatePossibleAddens();
		}
	}
}
