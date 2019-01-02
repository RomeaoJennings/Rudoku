namespace Rudoku.UI
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Rudoku.Sudoku sudoku1 = new Rudoku.Sudoku();
            this.chkAutoPips = new System.Windows.Forms.CheckBox();
            this.chkInvalidValues = new System.Windows.Forms.CheckBox();
            this.chkInvalidCandidates = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.sudokuGridUI1 = new Rudoku.UI.SudokuGridUI();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkAutoPips
            // 
            this.chkAutoPips.AutoSize = true;
            this.chkAutoPips.Checked = true;
            this.chkAutoPips.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoPips.Location = new System.Drawing.Point(564, 112);
            this.chkAutoPips.Name = "chkAutoPips";
            this.chkAutoPips.Size = new System.Drawing.Size(142, 17);
            this.chkAutoPips.TabIndex = 1;
            this.chkAutoPips.Text = "Auto Update Candidates";
            this.chkAutoPips.UseVisualStyleBackColor = true;
            this.chkAutoPips.CheckedChanged += new System.EventHandler(this.chkAutoPips_CheckedChanged);
            // 
            // chkInvalidValues
            // 
            this.chkInvalidValues.AutoSize = true;
            this.chkInvalidValues.Checked = true;
            this.chkInvalidValues.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInvalidValues.Location = new System.Drawing.Point(564, 158);
            this.chkInvalidValues.Name = "chkInvalidValues";
            this.chkInvalidValues.Size = new System.Drawing.Size(122, 17);
            this.chkInvalidValues.TabIndex = 2;
            this.chkInvalidValues.Text = "Show Invalid Values";
            this.chkInvalidValues.UseVisualStyleBackColor = true;
            this.chkInvalidValues.CheckedChanged += new System.EventHandler(this.chkInvalidValues_CheckedChanged);
            // 
            // chkInvalidCandidates
            // 
            this.chkInvalidCandidates.AutoSize = true;
            this.chkInvalidCandidates.Checked = true;
            this.chkInvalidCandidates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInvalidCandidates.Location = new System.Drawing.Point(564, 198);
            this.chkInvalidCandidates.Name = "chkInvalidCandidates";
            this.chkInvalidCandidates.Size = new System.Drawing.Size(143, 17);
            this.chkInvalidCandidates.TabIndex = 3;
            this.chkInvalidCandidates.Text = "Show Invalid Candidates";
            this.chkInvalidCandidates.UseVisualStyleBackColor = true;
            this.chkInvalidCandidates.CheckedChanged += new System.EventHandler(this.chkInvalidCandidates_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(603, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Hint";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(603, 311);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "ApplyHint";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(603, 340);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Find and Apply";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // sudokuGridUI1
            // 
            this.sudokuGridUI1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sudokuGridUI1.AutoCandidates = true;
            this.sudokuGridUI1.Location = new System.Drawing.Point(12, 12);
            this.sudokuGridUI1.Name = "sudokuGridUI1";
            this.sudokuGridUI1.ShowInvalidCandidates = true;
            this.sudokuGridUI1.ShowInvalidValues = true;
            this.sudokuGridUI1.Size = new System.Drawing.Size(527, 505);
            sudoku1.AutoCandidates = true;
            sudoku1.InSolveState = false;
            this.sudokuGridUI1.Sudoku = sudoku1;
            this.sudokuGridUI1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(603, 369);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(103, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "New";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(604, 398);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(103, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Clear";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 529);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkInvalidCandidates);
            this.Controls.Add(this.chkInvalidValues);
            this.Controls.Add(this.chkAutoPips);
            this.Controls.Add(this.sudokuGridUI1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SudokuGridUI sudokuGridUI1;
        private System.Windows.Forms.CheckBox chkAutoPips;
        private System.Windows.Forms.CheckBox chkInvalidValues;
        private System.Windows.Forms.CheckBox chkInvalidCandidates;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}