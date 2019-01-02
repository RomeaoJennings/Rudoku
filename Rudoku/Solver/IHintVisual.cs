using System.Drawing;

using Rudoku.UI;

namespace Rudoku.Solver
{
    public interface IHintVisual
    {
        void DrawElement(Graphics g, SudokuGridUI grid);
    }
}
