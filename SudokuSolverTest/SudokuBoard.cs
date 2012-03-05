using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverTest
{
    public class SudokuBoard
    {
        private List<int> _scopes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private readonly List<Cell> _cells = new List<Cell>();
        private const int BoardSize = 81;

        public SudokuBoard()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                _cells.Add(new Cell());
            }
        }

        public List<Cell> Cells
        {
            get
            {
                return _cells.Select(cell => cell.Clone).ToList();
            }
        }

        
        public void Set(int xPos, int yPos, int value, bool immutable = false)
        {
            _cells[xPos*9 + yPos].Value = value;
            _cells[xPos*9 + yPos].Immutable = immutable;
        }

        public Cell Get(int xPos, int yPos)
        {
            return _cells[xPos*9 + yPos].Clone;
        }

        public List<int> GetCandidates(int xPos, int yPos)
        {
            var cadidates = new List<int>();
            if (_cells[xPos*9 + yPos].Immutable)
            {
                return cadidates;
            }
            var rowCadidates = GetRowCadidates(xPos, yPos);
            var columnCadidates = GetColumnCadidates(xPos, yPos);
            var squareCadidates = GetSquareCadidates(xPos, yPos);

            var valuesSet = rowCadidates.Union(columnCadidates).Union(squareCadidates).Distinct();
            return  _scopes.Except(valuesSet).ToList();
        }

        private IEnumerable<int> GetSquareCadidates(int xPos, int yPos)
        {
            var ints = new List<int>();
            var quadrantY = yPos/3;
            var quadrantX = xPos/3;
            for (var x = quadrantX*3; x < quadrantX*3+3; x++)
            {
                for (var y = quadrantY; y < quadrantY*3+3; y++)
                {
                    ints.Add(_cells[x*9+y].Value);
                }
            }
            return ints;
        }

        private IEnumerable<int> GetColumnCadidates(int xPos, int yPos)
        {
            var columnCadidates = new List<int>();
            for (var i = 0 ; i < 9; i++)
            {
                columnCadidates.Add(_cells[i*9 + yPos].Value);
            }
            columnCadidates.RemoveAt(xPos*9 + yPos);
            return columnCadidates; 
        }

        private IEnumerable<int> GetRowCadidates(int xPos, int yPos)
        {
            var rowCadidates = new List<int>();
            for (var i = xPos*9; i < (xPos + 1)*9; i++)
            {
                rowCadidates.Add(_cells[i].Value);
            }
            rowCadidates.RemoveAt(yPos);
            return rowCadidates;
        }
    }
}