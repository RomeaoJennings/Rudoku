using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudoku
{
    public class Cell
    {
        private int _value;
        private bool[] _candidates;
        private List<Cell> _neighbors;
        private int _col;
        private int _row;


        public bool AutoCandidates { get; set; } = true;

        public int CorrectValue { get; set; }

        public int X
        {
            get
            {
                return _col;
            }
        }

        public int Y
        {
            get
            {
                return _row;
            }
        }

        public bool IsCorrect
        {
            get
            {
                return CorrectValue == _value;
            }
        }

        public bool IsGiven
        {
            get; set;
        }

        public List<Cell> Neighbors
        {
            get
            {
                return _neighbors;
            }
        }

        public House Row { get; set; } 
        public House Column { get; set; }
        public House Block { get; set; }

        public void UpdateNeighbors()
        {
            _neighbors.Clear();
            var empty = Enumerable.Empty<Cell>();
            List<Cell> cells = new List<Cell>(Row ?? empty);
            cells.AddRange(Column ?? empty);
            cells.AddRange(Block ?? empty);

            foreach (Cell cell in cells)
            {
                if (!object.ReferenceEquals(this,cell) && !_neighbors.Contains(cell))
                    _neighbors.Add(cell);
            }
            
        }

        public bool HasCandidates()
        {
            for (int i = 1; i <= 9; i++)
                if (_candidates[i])
                    return true;
            return false;
        }

        public int NextCandidate(int afterThis = 0)
        {
            for (int i = afterThis + 1; i <= 9; i++)
                if (_candidates[i])
                    return i;
            return -1;
        }

        public void SetCandidate(int candidate, bool value)
        {
            _candidates[candidate] = value;
        }

        public bool CanSeeValue(int value)
        {
            foreach (Cell c in _neighbors)
                if (c._value == value)
                    return true;
            return false;
        }

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value == _value)
                    return;
                int oldValue = _value;
                _value = value;
                if (AutoCandidates)
                {
                    _candidates[oldValue] = !CanSeeValue(oldValue);
                    foreach (Cell c in _neighbors)
                        c._candidates[oldValue] = !c.CanSeeValue(oldValue);
                }

                if (value != 0)
                {
                    _candidates = new bool[] { false, false, false, false, false, false, false, false, false, false };
                    foreach (Cell c in _neighbors)
                        c._candidates[value] = false;
                }
            }
        }

        public void ApplyAutoCandidates()
        {
            for (int i = 1; i <= 9; i++)
                _candidates[i] = !CanSeeValue(i);
        }

        public bool[] Candidates
        {
            get { return _candidates; }
            set
            {
                _candidates = value;
            }
        }

        public Cell(int col, int row)
        {
            _value = 0;
            _col = col;
            _row = row;

            _neighbors = new List<Cell>(20);
            _candidates = new bool[] { true, true, true, true, true, true, true, true, true, true };
            
        }



    }
}
