using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Rudoku.UI;

namespace Rudoku.Solver
{
    public class SinglesFactory : HintFactory
    {
        public SinglesFactory()
        {
            var hintTypes = new string[] { "Full House", "Hidden Single", "Naked Single" };
            var hintDifficulties = new int[] { 2, 5, 5 };
            var funcs = new HintGenFunction[] { new HintGenFunction(GenerateFullHouse),
                                                new HintGenFunction(GenerateHiddenSingle),
                                                new HintGenFunction(GenerateNakedSingle) };

            for (int i=0;i<hintTypes.Length;i++)
            {
                _genFunctions.Add(hintTypes[i], funcs[i]);
                _difficultyValues.Add(hintTypes[i], hintDifficulties[i]);
            }
        }

        private Hint GenerateFullHouse(Sudoku s)
        { 
            foreach (House h in s.Houses)
            {
                //if 8 of 9 values are set, then we have a full house.
                if (h.ValueCount == 8)
                {
                    Cell cell = h.NextEmptyCell;
                    Hint hint = new Hint();
                    CellValue cv = new CellValue(cell.X, cell.Y, cell.NextCandidate(), Color.Black, Color.LightGreen);
                    hint.ValuesToSet.Add(cv);
                    hint.HintVisuals.Add(new HouseBorder(h, Color.Orange));
                    hint.CandidateHighlights.Add(cv);
                    return hint;
                }
            }
            return null;
        }

        private Hint GenerateHiddenSingle(Sudoku s)
        {
            foreach (House h in s.Houses)
            {
                var cbc = h.CellsByCandidate;
                for (int i=1;i<=9;i++)
                {
                    if (cbc[i].Count == 1)  //found candidate with exactly 1 cell.  Must be hidden single
                    { 
                        Hint hint = new Hint();
                        Cell cell = cbc[i][0];
                        CellValue cv = new CellValue(cell.X, cell.Y, i, Color.Black, Color.Green);
                        hint.CandidateHighlights.Add(cv);
                        hint.ValuesToSet.Add(cv);
                        hint.HintVisuals.Add(new HouseBorder(h, Color.Orange));
                        return hint;
                    }
                }
            }
            return null;

        }

        private Hint GenerateNakedSingle(Sudoku s)
        {
            for (int i=0;i<81;i++)
            {
                int x = i % 9, y = i / 9;
                if (_candidateCounts[x, y] == 1)   //Exactly 1 candidate in a cell means naked single
                {
                    Hint hint = new Hint();
                    CellValue cv = new CellValue(x, y, s.GetCell(x,y).NextCandidate(), Color.Black, Color.Green);
                    hint.CandidateHighlights.Add(cv);
                    hint.ValuesToSet.Add(cv);
                    return hint;
                }
            }
            return null;
        }
    }
}
