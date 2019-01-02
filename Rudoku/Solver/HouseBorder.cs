using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudoku.UI;

namespace Rudoku.Solver
{
    public class HouseBorder : IHintVisual
    {
        private House _house;
        private Color _color;

        public HouseBorder(House h, Color c)
        {
            _house = h;
            _color = c;
        }

        public void DrawElement(Graphics g, SudokuGridUI grid)
        {
            int x1 = _house.Cells[0].X;
            int y1 = _house.Cells[0].Y;
            var _first = grid.Cells[x1, y1];

            int x2 = _house.Cells[_house.Cells.Count - 1].X;
            int y2 = _house.Cells[_house.Cells.Count - 1].Y;
            var _last = grid.Cells[x2, y2];

            int left = _first.Location.X+1;
            int top = _first.Location.Y+1;
            int width = _last.Location.X + grid.CellWidth - left-1;
            int height = _last.Location.Y + grid.CellWidth - top-1;

            Pen p = new Pen(_color, 2);
            p.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

            g.DrawRectangle(p, left, top, width, height);
            p.Dispose();
        }
    }
}
