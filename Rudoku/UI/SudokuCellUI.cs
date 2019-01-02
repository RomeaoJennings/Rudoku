using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudoku.UI
{
    public class SudokuCellUI
    {
        #region Defaults

        public static Color DefaultGivenColor;
        public static Color DefaultForeColor;
        public static Color DefaultBackColor;
        public static Color[] DefaultCandidateForeColor;
        public static Color[] DefaultCandidateBackColor;
        public static Color DefaultSelectedColor;
        public static Color DefaultNonGivenColor;
        public static Color DefaultErrorColor;

        static SudokuCellUI()
        {
            DefaultForeColor = Color.FromArgb(140,140,140);
            DefaultBackColor = Color.White;
            Color cbc = Color.FromArgb(0,0,0,0);
            Color cfc = DefaultForeColor;
            DefaultCandidateBackColor = new Color[] { cbc, cbc, cbc, cbc, cbc, cbc, cbc, cbc, cbc, cbc };
            DefaultCandidateForeColor = new Color[] { cfc, cfc, cfc, cfc, cfc, cfc, cfc, cfc, cfc, cfc };
            DefaultSelectedColor = Color.SkyBlue;
            DefaultNonGivenColor = Color.Blue;
            DefaultGivenColor = Color.Black;
            DefaultErrorColor = Color.Red;
        }

        public void ResetDefaultColors()
        {
            _backColor = DefaultBackColor;
            _foreColor = DefaultForeColor;
            Color[] back=new Color[10], fore=new Color[10];
            Array.Copy(DefaultCandidateBackColor, back, 10);
            Array.Copy(DefaultCandidateForeColor, fore, 10);
            _candidateBackColor = back;
            _candidateForeColor = fore;
            _selectedColor = DefaultSelectedColor;
            _nonGivenColor = DefaultNonGivenColor;
            _givenColor = DefaultGivenColor;
            _errorColor = DefaultErrorColor;
        }

        #endregion

        private int _width;
        private Point _location;
        private bool _selected;
        private SudokuGridUI _parent;

        private Color _backColor;
        private Color _foreColor;
        private Color[] _candidateForeColor;
        private Color[] _candidateBackColor;
        private Color _selectedColor;
        private Color _nonGivenColor;
        private Color _givenColor;
        private Color _errorColor;

        public Cell SudokuCell { get; set; }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        public Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
            }
        }

        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
            }
        }

        public Color[] CandidateBackColor
        {
            get
            {
                return _candidateBackColor;
            }
            set
            {
                _candidateBackColor = value;
            }
        }

        public Color[] CandidateForeColor
        {
            get
            {
                return _candidateForeColor;
            }
            set
            {
                _candidateForeColor = value;
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public Point Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(_location, new Size(_width, _width));
            }
        }

        public SudokuCellUI(SudokuGridUI parent)
        {
            ResetDefaultColors();
            _parent = parent;
            _width = 50;
            _location = new Point(0, 0);
            
        }



        public SudokuCellUI(int x, int y, int width, SudokuGridUI parent)
        {
            _parent = parent;
            ResetDefaultColors();
            _width = width;
            _location = new Point(x, y);
        }

        public bool ClickIsInSquare(Point pt)
        {
            if (pt.X < _location.X || pt.X > _location.X + _width)
                return false;
            if (pt.Y < _location.Y || pt.Y > _location.Y + _width)
                return false;
            return true;
        }


        public void Draw(Graphics g)
        {
            Rectangle r = new Rectangle(_location.X, _location.Y, _width, _width);
            g.FillRectangle(new SolidBrush(_backColor), r);
            g.DrawRectangle(new Pen(_foreColor), r);

            if (_selected)
            {
                Rectangle r2 = new Rectangle(r.Left + 1, r.Top + 1, r.Width - 1, r.Width - 1);
                Pen bp = new Pen(_selectedColor, 3);
                bp.Alignment = PenAlignment.Inset;
                g.DrawRectangle(bp, r2);
            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            if (SudokuCell.Value != 0)
            {
                Font f = new Font("Calibri", _width * .8F);
                Color c = _givenColor;
                if (!SudokuCell.IsGiven)
                    c = _nonGivenColor;
                if (_parent.ShowInvalidValues && SudokuCell.Value != SudokuCell.CorrectValue)
                    c = _errorColor;
                Brush b = new SolidBrush(c);


                g.DrawString(SudokuCell.Value.ToString(), f, b, new RectangleF(_location.X, _location.Y, _width, _width), stringFormat);
            }
            else
            {
                Font f = new Font("Calibri", _width * .2F);
                
                int candWidth = _width / 3;
                for (int i =0;i<9;i++)
                {
                    Color c = _candidateForeColor[i+1];
                    int x = i % 3, y = i / 3;
                    RectangleF rf = new RectangleF(_location.X + candWidth * x, _location.Y + candWidth * y, candWidth, candWidth);
                    float shrink = candWidth * .08F;
                    RectangleF rf2 = new RectangleF(_location.X + candWidth * x + shrink / 2, _location.Y + candWidth * y + shrink / 2, candWidth - shrink, candWidth - shrink);
                    Brush br = new SolidBrush(_candidateBackColor[i + 1]);
                    g.FillEllipse(br, rf2);
                    if (SudokuCell.Candidates[i+1])
                    {
                        if (SudokuCell.CanSeeValue(i+1) && _parent.ShowInvalidCandidates)
                            c = _errorColor;
                        Brush b = new SolidBrush(c);
                        g.DrawString((i+1).ToString(), f, b, rf, stringFormat);
                        b.Dispose();
                    }
                    else if (SudokuCell.CorrectValue == i+1 && _parent.AutoCandidates && !SudokuCell.Candidates[i+1] && _parent.ShowInvalidCandidates)
                    {
                        Brush b = new SolidBrush(_errorColor);
                        g.DrawString((i + 1).ToString(), f, b, rf, stringFormat);
                        b.Dispose();
                    }
                }
            }
        }
    }
}
