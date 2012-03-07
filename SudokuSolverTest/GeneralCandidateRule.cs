using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverTest
{
    public class GeneralCandidateRule : ICandidateRule
    {
        private readonly List<Cell> _cells = new List<Cell>();

        public GeneralCandidateRule(List<Cell> cells)
        {
            if (cells.Count != SudokuBoard.BoardSize)
            {
                throw new Exception("cells size is not correct.");
            }
            _cells = cells;
        }

        public List<int> GetCandidates(int xPos, int yPos)
        {
            var cadidates = new List<int>();
            if (_cells[xPos*SudokuBoard.EdgeSize + yPos].Locked)
            {
                return cadidates;
            }
            var rowCadidates = GetRowCadidates(xPos, yPos);
            var columnCadidates = GetColumnCadidates(xPos, yPos);
            var squareCadidates = GetSquareCadidates(xPos, yPos);

            var valuesSet = rowCadidates.Union(columnCadidates).Union(squareCadidates).Distinct();
            return SudokuBoard.Scopes.Except(valuesSet).ToList();
        }

        private IEnumerable<int> GetRowCadidates(int xPos, int yPos)
        {
            var rowCadidates = new List<int>();
            for (var i = xPos * SudokuBoard.EdgeSize; i < (xPos + 1) * SudokuBoard.EdgeSize; i++)
            {
                rowCadidates.Add(_cells[i].Value);
            }
            rowCadidates.RemoveAt(yPos);
            return rowCadidates;
        }

        private IEnumerable<int> GetColumnCadidates(int xPos, int yPos)
        {
            var columnCadidates = new List<int>();
            for (var i = 0; i < SudokuBoard.EdgeSize; i++)
            {
                columnCadidates.Add(_cells[i * SudokuBoard.EdgeSize + yPos].Value);
            }
            columnCadidates.RemoveAt(xPos * SudokuBoard.EdgeSize + yPos);
            return columnCadidates; 
        }

        private IEnumerable<int> GetSquareCadidates(int xPos, int yPos)
        {
            var ints = new List<int>();
            var quadrantY = yPos / SudokuBoard.SquareEdgeSize;
            var quadrantX = xPos / SudokuBoard.SquareEdgeSize;

            for (var x = quadrantX * SudokuBoard.SquareEdgeSize; x < (quadrantX+1) * SudokuBoard.SquareEdgeSize; x++)
            {
                for (var y = quadrantY; y < (quadrantY+1) * SudokuBoard.SquareEdgeSize; y++)
                {
                    ints.Add(_cells[x * SudokuBoard.EdgeSize + y].Value);
                }
            }
            return ints;
        }
    }
}