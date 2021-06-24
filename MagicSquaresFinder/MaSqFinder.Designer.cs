
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CombinationLengthField = new System.Windows.Forms.NumericUpDown();
            this.CombinationLengthLabel = new System.Windows.Forms.Label();
            this.CombinationView = new System.Windows.Forms.DataGridView();
            this.AddensPane = new System.Windows.Forms.TabPage();
            this.TotalCombinationLabel = new System.Windows.Forms.Label();
            this.TotalCombinationsField = new System.Windows.Forms.TextBox();
            this.MainTabs.SuspendLayout();
            this.CombinationPane.SuspendLayout();
            this.CombinationsLayout.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationLengthField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationView)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTabs
            // 
            this.MainTabs.Controls.Add(this.CombinationPane);
            this.MainTabs.Controls.Add(this.AddensPane);
            this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabs.Location = new System.Drawing.Point(0, 0);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(1118, 766);
            this.MainTabs.TabIndex = 0;
            // 
            // CombinationPane
            // 
            this.CombinationPane.Controls.Add(this.CombinationsLayout);
            this.CombinationPane.Location = new System.Drawing.Point(4, 39);
            this.CombinationPane.Name = "CombinationPane";
            this.CombinationPane.Padding = new System.Windows.Forms.Padding(3);
            this.CombinationPane.Size = new System.Drawing.Size(1110, 723);
            this.CombinationPane.TabIndex = 0;
            this.CombinationPane.Text = "Combinations";
            this.CombinationPane.UseVisualStyleBackColor = true;
            // 
            // CombinationsLayout
            // 
            this.CombinationsLayout.ColumnCount = 2;
            this.CombinationsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.83696F));
            this.CombinationsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.16304F));
            this.CombinationsLayout.Controls.Add(this.panel2, 0, 0);
            this.CombinationsLayout.Controls.Add(this.CombinationView, 1, 0);
            this.CombinationsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CombinationsLayout.Location = new System.Drawing.Point(3, 3);
            this.CombinationsLayout.Name = "CombinationsLayout";
            this.CombinationsLayout.RowCount = 1;
            this.CombinationsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CombinationsLayout.Size = new System.Drawing.Size(1104, 717);
            this.CombinationsLayout.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.TotalCombinationsField);
            this.panel2.Controls.Add(this.TotalCombinationLabel);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 711);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.CombinationLengthField);
            this.panel1.Controls.Add(this.CombinationLengthLabel);
            this.panel1.Location = new System.Drawing.Point(5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 67);
            this.panel1.TabIndex = 0;
            // 
            // CombinationLengthField
            // 
            this.CombinationLengthField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CombinationLengthField.Location = new System.Drawing.Point(0, 32);
            this.CombinationLengthField.Name = "CombinationLengthField";
            this.CombinationLengthField.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CombinationLengthField.Size = new System.Drawing.Size(205, 35);
            this.CombinationLengthField.TabIndex = 1;
            this.CombinationLengthField.ValueChanged += new System.EventHandler(this.CombinationLength_ValueChanged);
            // 
            // CombinationLengthLabel
            // 
            this.CombinationLengthLabel.AutoSize = true;
            this.CombinationLengthLabel.Location = new System.Drawing.Point(0, 0);
            this.CombinationLengthLabel.Name = "CombinationLengthLabel";
            this.CombinationLengthLabel.Size = new System.Drawing.Size(77, 30);
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
            this.CombinationView.Location = new System.Drawing.Point(222, 3);
            this.CombinationView.Name = "CombinationView";
            this.CombinationView.ReadOnly = true;
            this.CombinationView.RowHeadersVisible = false;
            this.CombinationView.RowHeadersWidth = 72;
            this.CombinationView.RowTemplate.Height = 37;
            this.CombinationView.Size = new System.Drawing.Size(879, 711);
            this.CombinationView.TabIndex = 2;
            // 
            // AddensPane
            // 
            this.AddensPane.Location = new System.Drawing.Point(4, 39);
            this.AddensPane.Name = "AddensPane";
            this.AddensPane.Padding = new System.Windows.Forms.Padding(3);
            this.AddensPane.Size = new System.Drawing.Size(1110, 723);
            this.AddensPane.TabIndex = 1;
            this.AddensPane.Text = "tabPage2";
            this.AddensPane.UseVisualStyleBackColor = true;
            // 
            // TotalCombinationLabel
            // 
            this.TotalCombinationLabel.AutoSize = true;
            this.TotalCombinationLabel.Location = new System.Drawing.Point(5, 84);
            this.TotalCombinationLabel.Name = "TotalCombinationLabel";
            this.TotalCombinationLabel.Size = new System.Drawing.Size(191, 30);
            this.TotalCombinationLabel.TabIndex = 1;
            this.TotalCombinationLabel.Text = "Total Combinations";
            // 
            // TotalCombinationsField
            // 
            this.TotalCombinationsField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalCombinationsField.BackColor = System.Drawing.SystemColors.ControlLight;
            this.TotalCombinationsField.Location = new System.Drawing.Point(5, 117);
            this.TotalCombinationsField.Name = "TotalCombinationsField";
            this.TotalCombinationsField.ReadOnly = true;
            this.TotalCombinationsField.Size = new System.Drawing.Size(205, 35);
            this.TotalCombinationsField.TabIndex = 2;
            this.TotalCombinationsField.Text = "0";
            // 
            // MaSqFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 766);
            this.Controls.Add(this.MainTabs);
            this.Name = "MaSqFinder";
            this.Text = "Magic Squares Finder";
            this.Load += new System.EventHandler(this.MaSqFinder_Load);
            this.MainTabs.ResumeLayout(false);
            this.CombinationPane.ResumeLayout(false);
            this.CombinationsLayout.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationLengthField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CombinationView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.TabPage CombinationPane;
        private System.Windows.Forms.TabPage AddensPane;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown CombinationLengthField;
        private System.Windows.Forms.Label CombinationLengthLabel;
        private System.Windows.Forms.TableLayoutPanel CombinationsLayout;
        private System.Windows.Forms.DataGridView CombinationView;
        private System.Windows.Forms.TextBox TotalCombinationsField;
        private System.Windows.Forms.Label TotalCombinationLabel;
    }
}

