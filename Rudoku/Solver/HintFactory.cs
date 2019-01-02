using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rudoku.UI;

namespace Rudoku.Solver
{
    public delegate Hint HintGenFunction(Sudoku s);

    public abstract class HintFactory
    {

        protected Dictionary<string, HintGenFunction> _genFunctions;
        protected Dictionary<string, int> _difficultyValues;
        protected int[,] _candidateCounts;

        public void UpdateCounts(Sudoku s)
        {
            foreach (var house in s.Houses)
                house.UpdateCounts();
            _candidateCounts = new int[9, 9];
            for (int i=0;i<81;i++)
            {
                int x = i % 9, y = i / 9;
                Cell cell = s.GetCell(x, y);
                if (cell.Value != 0)
                    continue;
                for (int c = 1; c <= 9; c++)
                    if (cell.Candidates[c])
                        _candidateCounts[x, y]++;
            }
        }

        public string[] HintTypes
        {
            get
            {
                return _genFunctions.Keys.ToArray();
            }
        }

        public Dictionary<string,int> HintDifficulties
        {
            get
            {
                return _difficultyValues;
            }
        }

        public int GetHintDifficulty(string hintType)
        {
            return _difficultyValues[hintType];
        }

        public Hint NextHint(Sudoku s)
        {
            foreach (var entry in _genFunctions)
            {
                Hint h = entry.Value(s);
                if (h != null)
                    return h;
            }
            return null;
        }

        public Hint NextHint(Sudoku s, string hintType)
        {
            return _genFunctions[hintType](s);
        }

        public HintFactory()
        {
            _difficultyValues = new Dictionary<string, int>();
            _genFunctions = new Dictionary<string, HintGenFunction>();
        }
    }
}
