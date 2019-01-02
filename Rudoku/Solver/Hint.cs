using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudoku.Solver
{
    public class Hint
    {
        public List<CellValue> ValuesToSet { get; } = new List<CellValue>();
        public List<CellValue> CandidatesToRemove { get; } = new List<CellValue>();

        public List<CellValue> CellHighlights { get; } = new List<CellValue>();
        public List<CellValue> ValueHighlights { get; } = new List<CellValue>();
        public List<CellValue> CandidateHighlights { get; } = new List<CellValue>();
        public List<IHintVisual> HintVisuals { get; } = new List<IHintVisual>();
    }
}
