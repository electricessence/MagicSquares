
namespace MagicSquaresFinder
{
    partial class MaSqFinder
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.MainTabs = new System.Windows.Forms.TabControl();
			this.CombinationPane = new System.Windows.Forms.TabPage();
			this.CombinationsLayout = new System.Windows.Forms.TableLayoutPanel();
			this.CombinationParamPane = new System.Windows.Forms.Panel();
			this.TotalCombinationsField = new System.Windows.Forms.TextBox();
			this.TotalCombinationLabel = new System.Windows.Forms.Label();
			this.CombinationLengthPane = new System.Windows.Forms.Panel();
			this.CombinationLengthField = new System.Windows.Forms.NumericUpDown();
			this.CombinationLengthLabel = new System.Windows.Forms.Label();
			this.CombinationView = new System.Windows.Forms.DataGridView();
			this.AddensPane = new System.Windows.Forms.TabPage();
			this.PossibleAddensLayout = new System.Windows.Forms.TableLayoutPanel();
			this.PossibleAddensParamPane = new System.Windows.Forms.Panel();
			this.TotalAddensField = new System.Windows.Forms.TextBox();
			this.TotalAddensLabel = new System.Windows.Forms.Label();
			this.PossibleAddensSumPane = new System.Windows.Forms.Panel();
			this.PossibleAddensSumField = new System.Windows.Forms.NumericUpDown();
			this.PossibleAddensSumLabel = new System.Windows.Forms.Label();
			this.PossibleAddensCountPane = new System.Windows.Forms.Panel();
			this.PossibleAddensCountField = new System.Windows.Forms.NumericUpDown();
			this.PossibleAddensCountLabel = new System.Windows.Forms.Label();
			this.PossibleAddensView = new System.Windows.Forms.DataGridView();
			this.MainTabs.SuspendLayout();
			this.CombinationPane.SuspendLayout();
			this.CombinationsLayout.SuspendLayout();
			this.CombinationParamPane.SuspendLayout();
			this.CombinationLengthPane.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CombinationLengthField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CombinationView)).BeginInit();
			this.AddensPane.SuspendLayout();
			this.PossibleAddensLayout.SuspendLayout();
			this.PossibleAddensParamPane.SuspendLayout();
			this.PossibleAddensSumPane.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PossibleAddensSumField)).BeginInit();
			this.PossibleAddensCountPane.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PossibleAddensCountField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PossibleAddensView)).BeginInit();
			this.SuspendLayout();
			// 
			// MainTabs
			// 
			this.MainTabs.Controls.Add(this.CombinationPane);
			this.MainTabs.Controls.Add(this.AddensPane);
			this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTabs.Location = new System.Drawing.Point(0, 0);
			this.MainTabs.Margin = new System.Windows.Forms.Padding(2);
			this.MainTabs.Name = "MainTabs";
			this.MainTabs.SelectedIndex = 0;
			this.MainTabs.Size = new System.Drawing.Size(932, 638);
			this.MainTabs.TabIndex = 0;
			// 
			// CombinationPane
			// 
			this.CombinationPane.Controls.Add(this.CombinationsLayout);
			this.CombinationPane.Location = new System.Drawing.Point(4, 34);
			this.CombinationPane.Margin = new System.Windows.Forms.Padding(2);
			this.CombinationPane.Name = "CombinationPane";
			this.CombinationPane.Padding = new System.Windows.Forms.Padding(2);
			this.CombinationPane.Size = new System.Drawing.Size(924, 600);
			this.CombinationPane.TabIndex = 0;
			this.CombinationPane.Text = "Combinations";
			this.CombinationPane.UseVisualStyleBackColor = true;
			// 
			// CombinationsLayout
			// 
			this.CombinationsLayout.ColumnCount = 2;
			this.CombinationsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.CombinationsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.CombinationsLayout.Controls.Add(this.CombinationParamPane, 0, 0);
			this.CombinationsLayout.Controls.Add(this.CombinationView, 1, 0);
			this.CombinationsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CombinationsLayout.Location = new System.Drawing.Point(2, 2);
			this.CombinationsLayout.Margin = new System.Windows.Forms.Padding(2);
			this.CombinationsLayout.Name = "CombinationsLayout";
			this.CombinationsLayout.RowCount = 1;
			this.CombinationsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.CombinationsLayout.Size = new System.Drawing.Size(920, 596);
			this.CombinationsLayout.TabIndex = 3;
			// 
			// CombinationParamPane
			// 
			this.CombinationParamPane.BackColor = System.Drawing.Color.Gainsboro;
			this.CombinationParamPane.Controls.Add(this.TotalCombinationsField);
			this.CombinationParamPane.Controls.Add(this.TotalCombinationLabel);
			this.CombinationParamPane.Controls.Add(this.CombinationLengthPane);
			this.CombinationParamPane.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CombinationParamPane.Location = new System.Drawing.Point(2, 2);
			this.CombinationParamPane.Margin = new System.Windows.Forms.Padding(2);
			this.CombinationParamPane.Name = "CombinationParamPane";
			this.CombinationParamPane.Size = new System.Drawing.Size(180, 592);
			this.CombinationParamPane.TabIndex = 1;
			// 
			// TotalCombinationsField
			// 
			this.TotalCombinationsField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TotalCombinationsField.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TotalCombinationsField.Location = new System.Drawing.Point(4, 98);
			this.TotalCombinationsField.Margin = new System.Windows.Forms.Padding(2);
			this.TotalCombinationsField.Name = "TotalCombinationsField";
			this.TotalCombinationsField.ReadOnly = true;
			this.TotalCombinationsField.Size = new System.Drawing.Size(174, 31);
			this.TotalCombinationsField.TabIndex = 2;
			this.TotalCombinationsField.Text = "0";
			// 
			// TotalCombinationLabel
			// 
			this.TotalCombinationLabel.AutoSize = true;
			this.TotalCombinationLabel.Location = new System.Drawing.Point(4, 70);
			this.TotalCombinationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.TotalCombinationLabel.Name = "TotalCombinationLabel";
			this.TotalCombinationLabel.Size = new System.Drawing.Size(165, 25);
			this.TotalCombinationLabel.TabIndex = 1;
			this.TotalCombinationLabel.Text = "Total Combinations";
			// 
			// CombinationLengthPane
			// 
			this.CombinationLengthPane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CombinationLengthPane.Controls.Add(this.CombinationLengthField);
			this.CombinationLengthPane.Controls.Add(this.CombinationLengthLabel);
			this.CombinationLengthPane.Location = new System.Drawing.Point(4, 2);
			this.CombinationLengthPane.Margin = new System.Windows.Forms.Padding(2);
			this.CombinationLengthPane.Name = "CombinationLengthPane";
			this.CombinationLengthPane.Size = new System.Drawing.Size(173, 56);
			this.CombinationLengthPane.TabIndex = 0;
			// 
			// CombinationLengthField
			// 
			this.CombinationLengthField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CombinationLengthField.Location = new System.Drawing.Point(0, 22);
			this.CombinationLengthField.Margin = new System.Windows.Forms.Padding(2);
			this.CombinationLengthField.Name = "CombinationLengthField";
			this.CombinationLengthField.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.CombinationLengthField.Size = new System.Drawing.Size(173, 31);
			this.CombinationLengthField.TabIndex = 1;
			this.CombinationLengthField.ValueChanged += new System.EventHandler(this.CombinationLength_ValueChanged);
			// 
			// CombinationLengthLabel
			// 
			this.CombinationLengthLabel.AutoSize = true;
			this.CombinationLengthLabel.Location = new System.Drawing.Point(0, 0);
			this.CombinationLengthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.CombinationLengthLabel.Name = "CombinationLengthLabel";
			this.CombinationLengthLabel.Size = new System.Drawing.Size(66, 25);
			this.CombinationLengthLabel.TabIndex = 0;
			this.CombinationLengthLabel.Text = "Length";
			// 
			// CombinationView
			// 
			this.CombinationView.AllowUserToAddRows = false;
			this.CombinationView.AllowUserToDeleteRows = false;
			this.CombinationView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.CombinationView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.CombinationView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.CombinationView.ColumnHeadersVisible = false;
			this.CombinationView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CombinationView.GridColor = System.Drawing.SystemColors.ControlLight;
			this.CombinationView.Location = new System.Drawing.Point(186, 2);
			this.CombinationView.Margin = new System.Windows.Forms.Padding(2);
			this.CombinationView.Name = "CombinationView";
			this.CombinationView.ReadOnly = true;
			this.CombinationView.RowHeadersVisible = false;
			this.CombinationView.RowHeadersWidth = 72;
			this.CombinationView.RowTemplate.Height = 37;
			this.CombinationView.Size = new System.Drawing.Size(732, 592);
			this.CombinationView.TabIndex = 2;
			// 
			// AddensPane
			// 
			this.AddensPane.Controls.Add(this.PossibleAddensLayout);
			this.AddensPane.Location = new System.Drawing.Point(4, 34);
			this.AddensPane.Margin = new System.Windows.Forms.Padding(2);
			this.AddensPane.Name = "AddensPane";
			this.AddensPane.Padding = new System.Windows.Forms.Padding(2);
			this.AddensPane.Size = new System.Drawing.Size(924, 600);
			this.AddensPane.TabIndex = 1;
			this.AddensPane.Text = "Possible Addens";
			this.AddensPane.UseVisualStyleBackColor = true;
			// 
			// PossibleAddensLayout
			// 
			this.PossibleAddensLayout.ColumnCount = 2;
			this.PossibleAddensLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.PossibleAddensLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.PossibleAddensLayout.Controls.Add(this.PossibleAddensParamPane, 0, 0);
			this.PossibleAddensLayout.Controls.Add(this.PossibleAddensView, 1, 0);
			this.PossibleAddensLayout.Location = new System.Drawing.Point(2, 2);
			this.PossibleAddensLayout.Name = "PossibleAddensLayout";
			this.PossibleAddensLayout.RowCount = 1;
			this.PossibleAddensLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.PossibleAddensLayout.Size = new System.Drawing.Size(920, 596);
			this.PossibleAddensLayout.TabIndex = 0;
			// 
			// PossibleAddensParamPane
			// 
			this.PossibleAddensParamPane.BackColor = System.Drawing.Color.LightGray;
			this.PossibleAddensParamPane.Controls.Add(this.TotalAddensField);
			this.PossibleAddensParamPane.Controls.Add(this.TotalAddensLabel);
			this.PossibleAddensParamPane.Controls.Add(this.PossibleAddensSumPane);
			this.PossibleAddensParamPane.Controls.Add(this.PossibleAddensCountPane);
			this.PossibleAddensParamPane.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PossibleAddensParamPane.Location = new System.Drawing.Point(3, 3);
			this.PossibleAddensParamPane.Name = "PossibleAddensParamPane";
			this.PossibleAddensParamPane.Size = new System.Drawing.Size(178, 590);
			this.PossibleAddensParamPane.TabIndex = 0;
			// 
			// TotalAddensField
			// 
			this.TotalAddensField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TotalAddensField.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TotalAddensField.Location = new System.Drawing.Point(2, 173);
			this.TotalAddensField.Margin = new System.Windows.Forms.Padding(2);
			this.TotalAddensField.Name = "TotalAddensField";
			this.TotalAddensField.ReadOnly = true;
			this.TotalAddensField.Size = new System.Drawing.Size(174, 31);
			this.TotalAddensField.TabIndex = 4;
			this.TotalAddensField.Text = "0";
			// 
			// TotalAddensLabel
			// 
			this.TotalAddensLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TotalAddensLabel.AutoSize = true;
			this.TotalAddensLabel.Location = new System.Drawing.Point(2, 145);
			this.TotalAddensLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.TotalAddensLabel.Name = "TotalAddensLabel";
			this.TotalAddensLabel.Size = new System.Drawing.Size(165, 25);
			this.TotalAddensLabel.TabIndex = 3;
			this.TotalAddensLabel.Text = "Total Combinations";
			// 
			// PossibleAddensSumPane
			// 
			this.PossibleAddensSumPane.Controls.Add(this.PossibleAddensSumField);
			this.PossibleAddensSumPane.Controls.Add(this.PossibleAddensSumLabel);
			this.PossibleAddensSumPane.Location = new System.Drawing.Point(2, 2);
			this.PossibleAddensSumPane.Margin = new System.Windows.Forms.Padding(2);
			this.PossibleAddensSumPane.Name = "PossibleAddensSumPane";
			this.PossibleAddensSumPane.Size = new System.Drawing.Size(174, 63);
			this.PossibleAddensSumPane.TabIndex = 2;
			// 
			// PossibleAddensSumField
			// 
			this.PossibleAddensSumField.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PossibleAddensSumField.Location = new System.Drawing.Point(0, 32);
			this.PossibleAddensSumField.Margin = new System.Windows.Forms.Padding(2);
			this.PossibleAddensSumField.Name = "PossibleAddensSumField";
			this.PossibleAddensSumField.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.PossibleAddensSumField.Size = new System.Drawing.Size(174, 31);
			this.PossibleAddensSumField.TabIndex = 1;
			this.PossibleAddensSumField.ValueChanged += new System.EventHandler(this.PossibleAddensSumField_ValueChanged);
			// 
			// PossibleAddensSumLabel
			// 
			this.PossibleAddensSumLabel.AutoSize = true;
			this.PossibleAddensSumLabel.Location = new System.Drawing.Point(0, 0);
			this.PossibleAddensSumLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.PossibleAddensSumLabel.Name = "PossibleAddensSumLabel";
			this.PossibleAddensSumLabel.Size = new System.Drawing.Size(48, 25);
			this.PossibleAddensSumLabel.TabIndex = 0;
			this.PossibleAddensSumLabel.Text = "Sum";
			// 
			// PossibleAddensCountPane
			// 
			this.PossibleAddensCountPane.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PossibleAddensCountPane.Controls.Add(this.PossibleAddensCountField);
			this.PossibleAddensCountPane.Controls.Add(this.PossibleAddensCountLabel);
			this.PossibleAddensCountPane.Location = new System.Drawing.Point(2, 69);
			this.PossibleAddensCountPane.Margin = new System.Windows.Forms.Padding(2);
			this.PossibleAddensCountPane.Name = "PossibleAddensCountPane";
			this.PossibleAddensCountPane.Size = new System.Drawing.Size(171, 63);
			this.PossibleAddensCountPane.TabIndex = 1;
			// 
			// PossibleAddensCountField
			// 
			this.PossibleAddensCountField.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PossibleAddensCountField.Location = new System.Drawing.Point(0, 32);
			this.PossibleAddensCountField.Margin = new System.Windows.Forms.Padding(2);
			this.PossibleAddensCountField.Name = "PossibleAddensCountField";
			this.PossibleAddensCountField.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.PossibleAddensCountField.Size = new System.Drawing.Size(171, 31);
			this.PossibleAddensCountField.TabIndex = 1;
			this.PossibleAddensCountField.ValueChanged += new System.EventHandler(this.PossibleAddensCountField_ValueChanged);
			// 
			// PossibleAddensCountLabel
			// 
			this.PossibleAddensCountLabel.AutoSize = true;
			this.PossibleAddensCountLabel.Location = new System.Drawing.Point(0, 0);
			this.PossibleAddensCountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.PossibleAddensCountLabel.Name = "PossibleAddensCountLabel";
			this.PossibleAddensCountLabel.Size = new System.Drawing.Size(60, 25);
			this.PossibleAddensCountLabel.TabIndex = 0;
			this.PossibleAddensCountLabel.Text = "Count";
			// 
			// PossibleAddensView
			// 
			this.PossibleAddensView.AllowUserToAddRows = false;
			this.PossibleAddensView.AllowUserToDeleteRows = false;
			this.PossibleAddensView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PossibleAddensView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.PossibleAddensView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PossibleAddensView.ColumnHeadersVisible = false;
			this.PossibleAddensView.GridColor = System.Drawing.SystemColors.ControlLight;
			this.PossibleAddensView.Location = new System.Drawing.Point(187, 3);
			this.PossibleAddensView.Name = "PossibleAddensView";
			this.PossibleAddensView.ReadOnly = true;
			this.PossibleAddensView.RowHeadersVisible = false;
			this.PossibleAddensView.RowHeadersWidth = 62;
			this.PossibleAddensView.RowTemplate.Height = 33;
			this.PossibleAddensView.Size = new System.Drawing.Size(730, 590);
			this.PossibleAddensView.TabIndex = 1;
			// 
			// MaSqFinder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(932, 638);
			this.Controls.Add(this.MainTabs);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MaSqFinder";
			this.Text = "Magic Squares Finder";
			this.Load += new System.EventHandler(this.MaSqFinder_Load);
			this.MainTabs.ResumeLayout(false);
			this.CombinationPane.ResumeLayout(false);
			this.CombinationsLayout.ResumeLayout(false);
			this.CombinationParamPane.ResumeLayout(false);
			this.CombinationParamPane.PerformLayout();
			this.CombinationLengthPane.ResumeLayout(false);
			this.CombinationLengthPane.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CombinationLengthField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CombinationView)).EndInit();
			this.AddensPane.ResumeLayout(false);
			this.PossibleAddensLayout.ResumeLayout(false);
			this.PossibleAddensParamPane.ResumeLayout(false);
			this.PossibleAddensParamPane.PerformLayout();
			this.PossibleAddensSumPane.ResumeLayout(false);
			this.PossibleAddensSumPane.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PossibleAddensSumField)).EndInit();
			this.PossibleAddensCountPane.ResumeLayout(false);
			this.PossibleAddensCountPane.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PossibleAddensCountField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PossibleAddensView)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.TabPage CombinationPane;
        private System.Windows.Forms.TabPage AddensPane;
        private System.Windows.Forms.Panel CombinationParamPane;
        private System.Windows.Forms.Panel CombinationLengthPane;
        private System.Windows.Forms.NumericUpDown CombinationLengthField;
        private System.Windows.Forms.Label CombinationLengthLabel;
        private System.Windows.Forms.TableLayoutPanel CombinationsLayout;
        private System.Windows.Forms.DataGridView CombinationView;
        private System.Windows.Forms.TextBox TotalCombinationsField;
        private System.Windows.Forms.Label TotalCombinationLabel;
		private System.Windows.Forms.TableLayoutPanel PossibleAddensLayout;
		private System.Windows.Forms.Panel PossibleAddensParamPane;
		private System.Windows.Forms.Panel PossibleAddensSumPane;
		private System.Windows.Forms.NumericUpDown PossibleAddensSumField;
		private System.Windows.Forms.Label PossibleAddensSumLabel;
		private System.Windows.Forms.Panel PossibleAddensCountPane;
		private System.Windows.Forms.NumericUpDown PossibleAddensCountField;
		private System.Windows.Forms.Label PossibleAddensCountLabel;
		private System.Windows.Forms.DataGridView PossibleAddensView;
		private System.Windows.Forms.TextBox TotalAddensField;
		private System.Windows.Forms.Label TotalAddensLabel;
	}
}

