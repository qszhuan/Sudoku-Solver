using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverTest
{
    public class GeneralCandidateRule : ICandidateRule
    {
        private readonly SudokuBoard _sudokuBoard;

        public GeneralCandidateRule(SudokuBoard sudokuBoard)
        {
            _sudokuBoard = sudokuBoard;
        }

        public List<int> GetCandidates(Position position)
        {
            var cadidates = new List<int>();

            if (_sudokuBoard.Get(position).Locked) return cadidates;

            var rowCadidates = GetRowCadidates(position);
            var columnCadidates = GetColumnCadidates(position);
            var squareCadidates = GetSquareCadidates(position);

            var valuesSet = rowCadidates.Union(columnCadidates).Union(squareCadidates).Distinct();
            return SudokuBoard.Scopes.Except(valuesSet).ToList();
        }

        private IEnumerable<int> GetRowCadidates(Position position)
        {
            var rowCadidates = new List<int>();
            for (var y = 0; y < SudokuBoard.EdgeSize; y++)
            {

                rowCadidates.Add(_sudokuBoard.Get(new Position(position.XPos, y)).Value);
            }
            rowCadidates.RemoveAt(position.YPos);
            return rowCadidates;
        }

        private IEnumerable<int> GetColumnCadidates(Position position)
        {
            var columnCadidates = new List<int>();
            for (var i = 0; i < SudokuBoard.EdgeSize; i++)
            {
                columnCadidates.Add(_sudokuBoard.Get(new Position(i, position.YPos)).Value);
            }
            columnCadidates.RemoveAt(position.XPos);
            return columnCadidates; 
        }

        private IEnumerable<int> GetSquareCadidates(Position position)
        {
            var ints = new List<int>();
            var squareOriginX = position.SquareOriginX();
            var squareOriginY = position.SquareOriginY();

            for (var x = squareOriginX; x < squareOriginX + SudokuBoard.SquareEdgeSize; x++)
            {
                for (var y = squareOriginY; y < squareOriginY + SudokuBoard.SquareEdgeSize; y++)
                {
                    ints.Add(_sudokuBoard.Get(new Position(x,y)).Value);
                }
            }
            return ints;
        }
    }
}