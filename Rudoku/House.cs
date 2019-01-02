using System;
using System.Collections;
using System.Collections.Generic;


namespace Rudoku
{
    public class House : IEnumerable<Cell>
    {
        private List<Cell> _cells;

        private int[] _candidateCounts;
        private int _valueCount;
        private List<Cell>[] _cellsByCandidate;

        public List<Cell>[] CellsByCandidate { get { return _cellsByCandidate; } }

        public House()
        {
            _cells = new List<Cell>(9);
            _candidateCounts = new int[10];
            _cellsByCandidate = new List<Cell>[10];
            for (int i = 1; i <= 9; i++)
                _cellsByCandidate[i] = new List<Cell>();
        }

        public void UpdateCounts()
        {
            _candidateCounts = new int[10];
            for (int i = 1; i <= 9; i++)
                _cellsByCandidate[i].Clear();
            _valueCount = 0;
            foreach (Cell c in _cells)
            {
                if (c.Value != 0)
                    _valueCount++;
                else
                {
                    var candidates = c.Candidates;
                    for (int i = 1; i <= 9; i++)
                    {
                        if (candidates[i])
                        {
                            _candidateCounts[i]++;
                            _cellsByCandidate[i].Add(c);
                        }
                    }
                }
            }
        }

        public Cell NextEmptyCell
        {
            get
            {
                foreach (Cell c in _cells)
                    if (c.Value == 0)
                        return c;
                return null;
            }
        }

        public int ValueCount
        {
            get { return _valueCount; }
        }

        public int[] CandidateCounts
        {
            get
            {
                return _candidateCounts;
            }
        }

        public void Add(Cell c)
        {
            _cells.Add(c);
        }

        public List<Cell> Cells
        {
            get { return _cells; }
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (Cell c in _cells)
                yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
