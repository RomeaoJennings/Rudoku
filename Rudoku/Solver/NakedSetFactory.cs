using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudoku.Solver
{
    public class NakedSetFactory : HintFactory
    {
        private List<Cell> GetCandidateCells(House h, int maxCount)
        {
            List<Cell> retVal = new List<Cell>();
            foreach (Cell c in h)
            {
                if (_candidateCounts[c.X, c.Y] <= maxCount)
                    retVal.Add(c);
            }
            return retVal;
        }

        private List<int> GetCandidates(List<Cell> list)
        {
            List<int> retVal = new List<int>();
            return null;
        }
        
    }
}
