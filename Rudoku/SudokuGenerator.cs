using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudoku
{
    delegate List<List<Cell>> CellSymmetryGroups(Sudoku s);

    public class SudokuGenerator
    {
        public static Random _random = new Random();

        private string GetCandidateString(Cell c)
        {
            StringBuilder sb = new StringBuilder(9);
            for (int i=1;i<=9;i++)
            {
                if (c.Candidates[i])
                    sb.Append(i);
            }
            return sb.ToString();
        }

        public int CountHints(Sudoku s)
        {
            int cnt = 0;
            for (int i = 0; i < 81; i++)
                if (s.GetCell(i % 9, i / 9).Value != 0)
                    cnt++;
            return cnt;
        }

        private List<List<Cell>> ShuffleGroups(List<List<Cell>> groups)
        {
            var retVal = new List<List<Cell>>(groups.Count);
            while (groups.Count > 0)
            {
                int r = _random.Next(groups.Count);
                retVal.Add(groups[r]);
                groups.RemoveAt(r);
            }
            return retVal;
        }

        private List<List<Cell>> RotationalSymetryGroups(Sudoku s)
        {
            var retVal = new List<List<Cell>>(25);
            for (int i=0;i<25;i++)
            {
                int x = i % 5, y = i / 5;
                List<Cell> cells = new List<Cell>(4);
                cells.Add(s.GetCell(x, y));
                if (x != 4 || y != 4)
                {
                    cells.Add(s.GetCell(8 - y, x));
                    cells.Add(s.GetCell(8 - x, 8 - y));
                    cells.Add(s.GetCell(y, 8 - x));
                }
                retVal.Add(cells);
            }
            return retVal;
        }

        private List<List<Cell>> VerticalReflectionGroups(Sudoku s)
        {
            var retVal = new List<List<Cell>>(45);
            for (int x = 0;x<5;x++)
                for (int y=0;y<9;y++)
                {
                    List<Cell> cells = new List<Cell>(2);
                    cells.Add(s.GetCell(x, y));
                    if (x != 4)
                        cells.Add(s.GetCell(8 - x, y));
                    retVal.Add(cells);
                }
            return retVal;
        }

        private List<List<Cell>> RandomGroups(Sudoku s)
        {
            var retVal = new List<List<Cell>>(81);
            for (int i=0;i<81;i++)
            {
                List<Cell> cells = new List<Cell>();
                retVal.Add(cells);
                cells.Add(s.GetCell(i % 9, i / 9));
            }
            return retVal;
        }

        private List<List<Cell>> DiagonalGroups(Sudoku s)
        {
            var retVal = new List<List<Cell>>();
            for (int y=0;y<9;y++)
            {
                for (int x=0;x<9-y;x++)
                {
                    List<Cell> cells = new List<Cell>();
                    retVal.Add(cells);
                    cells.Add(s.GetCell(x, y));
                    if (x + y != 8)
                        cells.Add(s.GetCell(8 - y, 8 - x));
                }
            }
            return retVal;
        }

        public Sudoku Generate()
        {
            Sudoku s = GenerateRandomGrid();
            BuildGivens(s, new CellSymmetryGroups(VerticalReflectionGroups));
            return s;
        }
        private void BuildGivens(Sudoku s, CellSymmetryGroups csr)
        {
            List<List<Cell>> removals = ShuffleGroups(csr(s));
            foreach (var cellGroup in removals)
            {
                foreach (Cell c in cellGroup)
                {
                    c.CorrectValue = c.Value;
                    c.Value = 0;
                }
                if (!BruteForceSolver.HasUniqueSolution(s))
                    foreach (Cell c in cellGroup)
                        c.Value = c.CorrectValue;
            }

        }

        public string Scramble(string input)
        {
            // Convert your string into a simple char array:
            char[] a = input.ToCharArray();

            // Scramble the letters using the standard Fisher-Yates shuffle, 
            for (int i = 0; i < a.Length; i++)
            {
                int j = _random.Next(a.Length);
                // Swap letters
                char temp = a[i]; a[i] = a[j]; a[j] = temp;
            }

            return new string(a);
        }

        private int NextCandidate(Cell c, string[,] candidates)
        {
            int x = c.X, y = c.Y;
            if (candidates[x, y].Length == 0)
                return -1;
            int retVal = candidates[x, y][0] - '0';
            candidates[x, y] = candidates[x, y].Substring(1);
            return retVal;
        }

        public Sudoku GenerateRandomGrid()
        {
            Sudoku s = new Sudoku();
            Stack<Cell> stack = new Stack<Cell>(81);
            Cell nextCell = null;
            int nextCandidate = 0;
            string[,] candidates = new string[9, 9];
            while (true)
            {
                nextCell = s.NextEmptyCell();
                if (nextCell == null)
                    return s;
                if (nextCell != null && nextCell.HasCandidates())
                {
                    candidates[nextCell.X, nextCell.Y] = Scramble(GetCandidateString(nextCell));
                    nextCell.Value = NextCandidate(nextCell, candidates);
                    stack.Push(nextCell);
                }

                else//If we get here, we need to backtrak because the current cell does not have possible values
                {
                    while (true)
                    {
                        Cell btCell = stack.Peek();
                        nextCandidate = NextCandidate(btCell,candidates);
                        if (nextCandidate == -1)
                        {
                            btCell.Value = 0;
                            stack.Pop();
                        }
                        else
                        {
                            btCell.Value = nextCandidate;
                            break;
                        }
                    }
                }
            }
        }
    }
}
