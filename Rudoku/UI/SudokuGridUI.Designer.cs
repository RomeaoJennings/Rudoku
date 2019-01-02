namespace Rudoku.UI
{
    partial class SudokuGridUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SudokuGridUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SudokuGridUI";
            this.Size = new System.Drawing.Size(500, 500);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SudokuGridUI_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SudokuGridUI_MouseDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SudokuGridUI_PreviewKeyDown);
            this.Resize += new System.EventHandler(this.SudokuGridUI_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
