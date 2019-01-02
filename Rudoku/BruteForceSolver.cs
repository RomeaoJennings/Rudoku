using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudoku
{
    public static class BruteForceSolver
    {

        private static string RecordAnswer(Sudoku s)
        {

            StringBuilder sb = new StringBuilder(81);
            for (int i = 0; i < 81; i++)
                sb.Append(s.GetCell(i % 9, i / 9).Value);
            return sb.ToString();
        }

        public static bool HasUniqueSolution(Sudoku s)
        {
            return GetSolutions(s, 2).Count == 1;
        }

        public static List<string> GetSolutions(Sudoku sudoku, int maxSolutions = int.MaxValue)
        {
            Sudoku s = new Sudoku(sudoku.ValueString);
            List<string> retVal = new List<string>();
            Stack<Cell> stack = new Stack<Cell>(81);
            Cell nextCell = null;
            int nextCandidate = 0;

            while (true)
            {
                nextCell = s.NextEmptyCell();
                if (nextCell == null)
                {
                    retVal.Add(RecordAnswer(s));
                    if (retVal.Count == maxSolutions)
                        return retVal;
                }
                if (nextCell != null && nextCell.HasCandidates())
                {
                    nextCell.Value = nextCell.NextCandidate();
                    stack.Push(nextCell);
                }

                else//If we get here, we need to backtrak because the current cell does not have possible values
                {
                    while (true)
                    {
                        if (stack.Count == 0)
                            return retVal;
                        Cell btCell = stack.Peek();
                        nextCandidate = btCell.NextCandidate(btCell.Value);
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
