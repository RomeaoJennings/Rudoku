using System.Drawing;

namespace Rudoku.Solver
{
    public class CellValue
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Value { get; private set; }
        public Color ForeColor { get; private set; }
        public Color BackColor { get; private set; }


        public CellValue(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public CellValue(int x, int y, int value, Color foreColor, Color backColor)
        {
            X = x;
            Y = y;
            Value = value;
            ForeColor = foreColor;
            BackColor = backColor;
        }
    }
}
