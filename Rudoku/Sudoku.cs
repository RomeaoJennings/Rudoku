using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudoku
{
    public class Sudoku
    {

        private Cell[,] _cells;

        private List<House> _rows;
        private List<House> _columns;
        private List<House> _blocks;
        private List<House> _houses;
        private bool _inSolveState;
        private bool _autoCandidates;


        public List<House> Houses
        {
            get
            {
                return _houses;
            }
        }

        public List<House> Rows
        {
            get { return _rows; }
        }

        public bool InSolveState
        {
            get
            {
                return _inSolveState;
            }
            set
            {
                _inSolveState = value;
                if (value)
                {
                    var solutions = BruteForceSolver.GetSolutions(this, 2);
                    if (solutions.Count != 1)
                        throw new ArgumentException("Sudoku has invalid number of solutions (0 or many)");
                    string solution = solutions[0];
                    for (int i = 0; i < 81; i++)
                    {
                        int x = i % 9, y = i / 9;
                        _cells[x, y].CorrectValue = solution[i] - '0';
                        _cells[x, y].IsGiven = _cells[x, y].Value != 0;
                    }
                }
            }
        }


        public bool AutoCandidates
        {
            get
            {
                return _autoCandidates;
            }
            set
            {
                if (_autoCandidates == value)
                    return;
                _autoCandidates = value;
                if (value == false)
                {
                    for (int i = 0; i < 81; i++)
                    {
                        int x = i % 9, y = i / 9;
                        _cells[x, y].Candidates = new bool[] { false, false, false, false, false, false, false, false, false, false };
                        _cells[x, y].AutoCandidates = value;
                    }
                }
                else
                {
                    for (int i = 0; i < 81; i++)
                    {
                        int x = i % 9, y = i / 9;
                        _cells[x, y].ApplyAutoCandidates();
                        _cells[x, y].AutoCandidates = value;
                    }
                }
            }
        }

        public string ValueString
        {
            get
            {
                StringBuilder sb = new StringBuilder(81);
                for (int i = 0; i < 81; i++)
                    sb.Append(_cells[i % 9, i / 9].Value);
                return sb.ToString();
            }
        }



        public Sudoku()
        {
            _autoCandidates = true;
            BuildCells();
            BuildHouses();
        }
        
        public Sudoku(string board)
        {
            _autoCandidates = true;
            if (board.Length != 81)
                throw new ArgumentException();

            BuildCells();
            BuildHouses();
            board = board.Replace('.', '0');
            for (int i = 0; i < 81; i++)
            {
                int x = i % 9, y = i / 9;
                SetValue(x, y, int.Parse(board[i].ToString()));
            }
        }

        public Cell NextEmptyCell()
        {
            for (int i = 0; i < 81; i++)
                if (_cells[i % 9, i / 9].Value == 0)
                    return _cells[i % 9, i / 9];
            return null;
        }

        public Cell GetCell(int col, int row)
        {
            return _cells[col, row];
        }

        private void BuildCells()
        {
            _cells = new Cell[9, 9];
            for (int x = 0; x < 9; x++)
                for (int y = 0; y < 9; y++)
                    _cells[x, y] = new Cell(x, y);
        }

        private void BuildHouses()
        {
            _rows = new List<House>(9);
            _columns = new List<House>(9);
            _blocks = new List<House>(9);
            _houses = new List<House>(27);

            for (int x=0;x<9;x++)
            {
                House col = new House();
                House row = new House();
                House block = new House();

                for (int y=0;y<9;y++)
                {
                    col.Add(_cells[x, y]);
                    _cells[x, y].Column = col;

                    row.Add(_cells[y, x]);
                    _cells[y, x].Row = row;

                    int bx = (x % 3) * 3 + (y % 3);
                    int by = (x / 3) * 3 + (y / 3);
                    block.Add(_cells[bx, by]);
                    _cells[bx, by].Block = block;
                }

                _rows.Add(row);
                _columns.Add(col);
                _blocks.Add(block);
            }
            InitializeCellNeighbors();
            _houses.AddRange(_rows);
            _houses.AddRange(_columns);
            _houses.AddRange(_blocks);
        }

        private void InitializeCellNeighbors()
        {
            for (int i=0;i<81;i++)
            {
                _cells[i % 9, i / 9].UpdateNeighbors();
            }
        }

        public void SetValue(int col, int row, int value)
        {
            _cells[col, row].Value = value;
            if (!_inSolveState)
                _cells[col, row].IsGiven = (value != 0);
        }



        private string DrawLine(char s)
        {
            StringBuilder sb = new StringBuilder(95);
            for (int i = 0; i < 95; i++)
                sb.Append(s);
            sb.Append("\r\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                    sb.Append(DrawLine('='));
                for (int j=0;j<3;j++)
                {   
                    for (int k=0;k<9;k++)
                    {
                        if (k % 3 == 0)
                            sb.Append("|");
                        Cell curr = _cells[k, i];
                        if (curr.Value != 0)
                        {
                            if (j == 1)
                                sb.AppendFormat("|   .{0}.   ", curr.Value);
                            else
                                sb.Append("|         ");
                        }
                        else
                        {
                            sb.Append("|  ");
                            for (int m = j * 3 + 1; m <= j * 3 + 3; m++)
                                sb.AppendFormat("{0} ", curr.Candidates[m] ? m.ToString() : " ");
                            sb.Append(" ");
                        }
                    }
                    sb.Append("||\r\n");
                }
                if (i != 8 && i % 3 != 2)
                    sb.Append(DrawLine('-'));
            }
            sb.Append(DrawLine('='));
            return sb.ToString();
        }
    }   
}
