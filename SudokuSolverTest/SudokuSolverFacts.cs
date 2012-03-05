using System;
using Xunit;

namespace SudokuSolverTest
{
    public class SudokuSolverFacts
    {
        [Fact]
        public void should_raise_exception_when_set_value_not_in_0_to_9_and_value_is_mutable()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cell(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Cell().Value = 10);
            Assert.DoesNotThrow(()=> new Cell(1,true).Value = 10);
        }

        [Fact]
        public void should_can_not_set_value_when_cell_is_fixed()
        {
            var cell = new Cell(2, true);
            Assert.Equal(true, cell.Immutable);
            cell.Value = 3;
            Assert.Equal(2, cell.Value);
            cell.Immutable = false;
            cell.Value = 3;
            Assert.Equal(3, cell.Value);
        }

        [Fact]
        public void should_get_a_clone_of_cell()
        {
            var initial = new Cell();
            var clone = initial.Clone;
            Assert.Equal(initial.Value, clone.Value);
            Assert.Equal(initial.Immutable, clone.Immutable);
            clone.Value = 2;
            Assert.NotEqual(initial.Value, clone.Value);
        }

        [Fact]
        public void should_get_81__cloned_cells_for_sudoku_board()
        {
            var sudokuBoard = new SudokuBoard();
            var cells = sudokuBoard.Cells;
            cells[0].Value = 1;

            Assert.Equal(81, cells.Count);
            sudokuBoard.Cells.ForEach(cell=> Assert.Equal(0, cell.Value));
        }

        [Fact]
        public void should_set_the_cell_value_for()
        {
            var sudokuBoard = new SudokuBoard();
            sudokuBoard.Set(0, 0, 9);
            sudokuBoard.Set(8, 8, 1, true);

            Assert.Equal(9, sudokuBoard.Get(0,0).Value);
            Assert.Equal(1, sudokuBoard.Get(8,8).Value);
            Assert.Equal(true, sudokuBoard.Get(8,8).Immutable);
        }

        [Fact]
        public void should_get_empty_candidates_for_immutable_cell()
        {
            var sudokuBoard = new SudokuBoard();
            sudokuBoard.Set(0,0,2,true);

            var candidates = sudokuBoard.GetCandidates(0,0);
            Assert.Equal(0, candidates.Count);
        }

        [Fact]
        public void should_get_candidates_for_mutable_cell()
        {
            var sudokuBoard = new SudokuBoard();
            var candidates = sudokuBoard.GetCandidates(0,0);
            Assert.Equal(9, candidates.Count);

        }
    }
}
