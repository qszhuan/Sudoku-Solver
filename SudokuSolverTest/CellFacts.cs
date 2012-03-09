using System;
using System.Collections.Generic;
using Xunit;

namespace SudokuSolverTest
{
    public class CellFacts
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
            Assert.Equal(true, cell.Locked);
            cell.Value = 3;
            Assert.Equal(2, cell.Value);
            cell.Locked = false;
            cell.Value = 3;
            Assert.Equal(3, cell.Value);
        }

        [Fact]
        public void should_be_as_expected()
        {
            var cellWrapper = new CellWrapper();
            cellWrapper.Candidates.Push(1);
            Assert.True(cellWrapper.Set());
            Assert.False(cellWrapper.Set());
        }
    }
}