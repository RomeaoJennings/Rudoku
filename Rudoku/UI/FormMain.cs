using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Rudoku.Solver;

namespace Rudoku.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            
            
            InitializeComponent();
            //Sudoku s = new Sudoku(".5.6.324...3..4..76......51...3...7....412....3...5...36......84..2..7...985.6.2.");
            SudokuGenerator sg = new SudokuGenerator();
            Sudoku s = sg.Generate();
            s.InSolveState = true;
            sudokuGridUI1.Sudoku = s;
            

        }

        private void chkAutoPips_CheckedChanged(object sender, EventArgs e)
        {
            sudokuGridUI1.AutoCandidates = chkAutoPips.Checked;
        }

        private void chkInvalidValues_CheckedChanged(object sender, EventArgs e)
        {
            sudokuGridUI1.ShowInvalidValues = chkInvalidValues.Checked;
        }

        private void chkInvalidCandidates_CheckedChanged(object sender, EventArgs e)
        {
            sudokuGridUI1.ShowInvalidCandidates = chkInvalidCandidates.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SinglesFactory sf = new SinglesFactory();
            sf.UpdateCounts(sudokuGridUI1.Sudoku);
            sudokuGridUI1.SetHint(sf.NextHint(sudokuGridUI1.Sudoku));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sudokuGridUI1.ApplyHint();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                button1.PerformClick();
                button2.PerformClick();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SudokuGenerator gen = new SudokuGenerator();
            sudokuGridUI1.Sudoku = gen.Generate();
            sudokuGridUI1.Sudoku.InSolveState = true;
            Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var s = sudokuGridUI1.Sudoku;
            for (int i = 0; i < 81; i++)
                if (!s.GetCell(i % 9, i / 9).IsGiven)
                    s.SetValue(i % 9, i / 9, 0);
            Invalidate();
        }
    }
}
