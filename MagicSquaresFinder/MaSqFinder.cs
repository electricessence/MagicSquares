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
		readonly PossibleAddens PossibleAddens = new();

		public int CombinationLength => (int)CombinationLengthField.Value;

		public int PossibleAddensCount => (int)PossibleAddensCountField.Value;

		public uint PossibleAddensSum => (uint)PossibleAddensSumField.Value;


		public IReadOnlyList<ImmutableArray<int>> CombinationValues
			=> Combinations.GetIndexes(CombinationLength);

		public IReadOnlyList<IReadOnlyList<uint>> PossibleAddenValues
			=> PossibleAddens.UniqueAddensFor(PossibleAddensSum, (uint)PossibleAddensCount);

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
