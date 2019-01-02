using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Rudoku.Solver;

namespace Rudoku.UI
{
    public partial class SudokuGridUI : UserControl
    {
        private SudokuCellUI[,] _cells;
        private SudokuCellUI _selectedCell;
        private bool _showInvalidValues;
        private bool _showInvalidCandidates;
        private Hint _hint;






        public bool ShowInvalidValues
        {
            get
            {
                return _showInvalidValues;
            }
            set
            {
                _showInvalidValues = value;
                Invalidate();
            }
        }

        public bool ShowInvalidCandidates
        {
            get
            {
                return _showInvalidCandidates;
            }
            set
            {
                _showInvalidCandidates = value;
                Invalidate();
            }
        }

        private Sudoku _sudoku;

        public bool AutoCandidates
        {
            get
            {
                if (_sudoku == null)
                    return false;
                return _sudoku.AutoCandidates;
            }
            set
            {
                _sudoku.AutoCandidates = value;
                Invalidate();
            }

        }

        public Sudoku Sudoku
        {
            get { return _sudoku; }

            set
            {
                if (value == null)
                    _sudoku = new Sudoku();
                else
                    _sudoku = value;
                LinkCells();
                Invalidate();

            }
        }

        private void LinkCells()
        {
            for (int i=0;i<81;i++)
            {
                int x = i % 9, y = i / 9;
                _cells[x, y].SudokuCell = _sudoku.GetCell(x, y);
            }
        }


        public SudokuGridUI()
        {
            _showInvalidValues = true;
            _showInvalidCandidates = true;
            DoubleBuffered = true;
           
            InitializeComponent();

            _cells = new SudokuCellUI[9, 9];
            SizeCells();
            Sudoku = new Sudoku();
            
        }

        public int CellWidth
        {
            get
            {
                return Math.Min(Width, Height) / 10;
            }
        }

        private int StartLeft
        {
            get
            {
                return (Width - 9 * CellWidth) / 2;
            }
        }

        private int StartTop
        {
            get
            {
                return (Height - 9 * CellWidth) / 2;
            }
        }

        public int BorderWidth
        {
            get
            {
                return 2;
            }
        }

        public void ApplyHint()
        {
            if (_hint == null)
                return;
            foreach (var entry in _hint.CandidatesToRemove)
                _cells[entry.X, entry.Y].SudokuCell.Candidates[entry.Value] = false;
            foreach (var entry in _hint.ValuesToSet)
                _cells[entry.X, entry.Y].SudokuCell.Value = entry.Value;

            for (int i = 0; i < 81; i++)
                _cells[i % 9, i / 9].ResetDefaultColors();
            Invalidate();
        }

        public void SetHint(Hint hint)
        {
            _hint = hint;
            if (hint != null)
            {
                foreach (var entry in hint.CellHighlights)
                    _cells[entry.X, entry.Y].BackColor = entry.BackColor;
                foreach (var entry in hint.ValueHighlights)
                    _cells[entry.X, entry.Y].ForeColor = entry.ForeColor;
                foreach (var entry in hint.CandidateHighlights)
                {
                    _cells[entry.X, entry.Y].CandidateBackColor[entry.Value] = entry.BackColor;
                    _cells[entry.X, entry.Y].CandidateForeColor[entry.Value] = entry.ForeColor;
                }
            }
            Invalidate();
        }

        public SudokuCellUI[,] Cells { get { return _cells; } }

        private void SizeCells()
        {
            int min = Math.Min(Width, Height);
            int width = CellWidth;
            int left = StartLeft+1, top = StartTop+1;

            for (int y=0;y<9;y++)
            {
                left = StartLeft+1;
                for (int x = 0; x < 9; x++)
                {
                    if (_cells[x, y] == null)
                        _cells[x, y] = new SudokuCellUI(this);
                    _cells[x, y].Location = new Point(left, top);
                    _cells[x, y].Width = width;
                    left += width;
                    if (x == 2 || x == 5)
                        left+=BorderWidth;
                }
                top += width;
                if (y == 2 || y == 5)
                    top+=BorderWidth; 
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            base.OnPaint(e);

            for (int i = 0; i < 81; i++)
                _cells[i / 9, i % 9].Draw(e.Graphics);
            int width = CellWidth * 9 + 3 * BorderWidth+1;
            Pen bPen = new Pen(Color.Black, BorderWidth);
            bPen.Alignment = PenAlignment.Outset;
            e.Graphics.DrawRectangle(bPen, StartLeft, StartTop, width, width);
            bPen.Alignment = PenAlignment.Center;
            bPen.Width = 1;
            float threeCell = 3 * CellWidth + 2;
            float horizontal = StartTop + threeCell;
            float vertical = StartLeft + threeCell;

            for (int i = 1; i < 3; i++)
            {
                e.Graphics.DrawLine(bPen, StartLeft, horizontal, StartLeft + width, horizontal);
                e.Graphics.DrawLine(bPen, vertical, StartTop, vertical, StartTop + width);
                horizontal += threeCell;
                vertical += threeCell;

            }
            if (_hint != null)
            {
                foreach (var visual in _hint.HintVisuals)
                    visual.DrawElement(e.Graphics, this);
            }

        }

        private void SudokuGridUI_Resize(object sender, EventArgs e)
        {
            SizeCells();
            Invalidate();
        }

        private void SudokuGridUI_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i =0;i<81;i++)
                {
                    int x = i % 9, y = i / 9;
                    if (_cells[x,y].ClickIsInSquare(new Point(e.X,e.Y)))
                    {
                        if (_selectedCell != null)
                            _selectedCell.Selected = false;
                        _selectedCell = _cells[x, y];
                        _selectedCell.Selected = true;
                        Invalidate();
                        return;
                    }
                     
                }
            }
        }

        private void SudokuGridUI_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Keys k = e.KeyCode;

            if (k == Keys.Up || k == Keys.Down || k == Keys.Left || k == Keys.Right)
                e.IsInputKey = true;
                
        }

        private void SudokuGridUI_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyData = e.KeyCode;
            Keys[] arrows = new Keys[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };
            Keys[] numbers = new Keys[] {Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4,
                                         Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9 };

            if (arrows.Contains(e.KeyCode))
            {
                //Unselect previously selected cell
                if (_selectedCell == null)                   
                {
                    _selectedCell = _cells[0,0];
                    _selectedCell.Selected = true;
                    Invalidate();
                    return;
                }

                _selectedCell.Selected = false;
                int x = _selectedCell.SudokuCell.X;
                int y = _selectedCell.SudokuCell.Y;
                

                if (keyData == Keys.Left)
                    x = Math.Max(0, x - 1);
                else if (keyData == Keys.Right)
                    x = Math.Min(8, x + 1);
                else if (keyData == Keys.Up)
                    y = Math.Max(0, y - 1);
                else
                    y = Math.Min(8, y + 1);

                _selectedCell = _cells[x, y];
                _selectedCell.Selected = true;
            }

            if (numbers.Contains(e.KeyCode) && _selectedCell != null && !_selectedCell.SudokuCell.IsGiven)
            {
                int value = (int)e.KeyCode - (int)Keys.D0;
                if (e.Control)    //Editing Candidates
                    _selectedCell.SudokuCell.Candidates[value] = !_selectedCell.SudokuCell.Candidates[value];
                else
                    _selectedCell.SudokuCell.Value = value;
            }
            Invalidate();
        }
    }
}
