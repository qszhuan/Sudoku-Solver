using System;
using System.Linq;
using SudokuSolverTest.tokenizer;
using Xunit;

namespace SudokuSolverTest
{
    public class SudokuSolverFacts
    {
        const string Problem = @"
|7|2| | | |9| | |1
| | | | |4|2| |6|
|5|9|4|1| | | |7|
| | |7| |8| |5| |3
|8| | |2| |6| | |9
|2| |3| |7| |6| |
| |5| | | |3|4|9|7
| |8| |5|2| | | |
|3| | |6| | | |5|8";

        private const string Expert =
            @"
| 4 | 9 |   |   |   |   |   |   | 7 
|   |   |   | 1 |   |   |   |   |   
|   |   | 5 | 4 | 3 |   |   |   |   
| 1 |   |   |   | 6 |   |   | 4 | 2 
| 5 |   |   | 2 |   |   |   |   |   
|   |   |   | 7 |   |   |   |   |   
| 6 | 2 |   |   |   | 5 | 7 |   | 4 
|   |   | 1 |   |   |   |   | 2 | 9 
|   | 4 | 9 |   | 2 |   | 6 |   |   ";

        private const string ExpertAnswer =
            @"
| 4 | 9 | 6 | 8 | 5 | 2 | 3 | 1 | 7 
| 2 | 7 | 3 | 1 | 9 | 6 | 4 | 8 | 5 
| 8 | 1 | 5 | 4 | 3 | 7 | 2 | 9 | 6 
| 1 | 8 | 7 | 5 | 6 | 3 | 9 | 4 | 2 
| 5 | 6 | 4 | 2 | 8 | 9 | 1 | 7 | 3 
| 9 | 3 | 2 | 7 | 4 | 1 | 5 | 6 | 8 
| 6 | 2 | 8 | 9 | 1 | 5 | 7 | 3 | 4 
| 3 | 5 | 1 | 6 | 7 | 4 | 8 | 2 | 9 
| 7 | 4 | 9 | 3 | 2 | 8 | 6 | 5 | 1 
";

        private const string Answer = @"
|7|2|6|3|5|9|8|4|1
|1|3|8|7|4|2|9|6|5
|5|9|4|1|6|8|3|7|2
|9|6|7|4|8|1|5|2|3
|8|4|5|2|3|6|7|1|9
|2|1|3|9|7|5|6|8|4
|6|5|2|8|1|3|4|9|7
|4|8|9|5|2|7|1|3|6
|3|7|1|6|9|4|2|5|8";

        [Fact]
        public void should_get_81_cells_for_sudoku_board()
        {
            var sudokuBoard = new SudokuBoard();
            var cells = sudokuBoard.Cells;

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
            Assert.Equal(true, sudokuBoard.Get(8,8).Locked);
        }

        [Fact]
        public void should_get_empty_candidates_for_locked_cell()
        {
            var sudokuBoard = new SudokuBoard();
            sudokuBoard.Set(0,0,2,true);

            var candidates = new GeneralCandidateRule(sudokuBoard.Cells).GetCandidates(0, 0);
            Assert.Equal(0, candidates.Count);
        }

        [Fact]
        public void should_get_candidates_for_unlocked_cell()
        {
            var sudokuBoard = new SudokuBoard();
            var candidates = new GeneralCandidateRule(sudokuBoard.Cells).GetCandidates(0,0);
            Assert.Equal(9, candidates.Count);

            sudokuBoard.Set(0,1,2);
            sudokuBoard.Set(0,2,3);
            sudokuBoard.Set(0,3,4);
            sudokuBoard.Set(0,4,5);
            sudokuBoard.Set(0,5,6);
            sudokuBoard.Set(0,6,7);
            sudokuBoard.Set(0,7,8);
            sudokuBoard.Set(0,8,9);
            candidates = new GeneralCandidateRule(sudokuBoard.Cells).GetCandidates(0, 0);
            Assert.Equal(1, candidates.Count);
            Assert.Equal(1, candidates.First());
        }

        [Fact]
        public void should_not_break_the_unique_rule()
        {
            var sudokuBoard = new SudokuBoard();
            sudokuBoard.Set(0,0,1);
            sudokuBoard.Set(0,1,1);
            Assert.Equal(1,sudokuBoard.Get(0,0).Value);
            Assert.NotEqual(1, sudokuBoard.Get(0,1).Value);
        }

        [Fact]
        public void should_generate_data_list()
        {
            
            var dataList = new VerticalBarStyleTokenizer().Generate(Problem);
            Assert.Equal(81, dataList.Count);
            Assert.Equal(7, dataList.First());
            Assert.Equal(0, dataList[2]);
            Assert.Equal(0, dataList[17]);

        }
        
        [Fact]
        public void should_sovle_sudoku()
        {
            var sudokuBoard = new SudokuBoard();
            CheckAnswer(sudokuBoard, Problem, Answer);
            CheckAnswer(sudokuBoard, Expert, ExpertAnswer);
        }

        private static void CheckAnswer(SudokuBoard sudokuBoard, string problem, string answer)
        {
            var dataList = new VerticalBarStyleTokenizer().Generate(problem);
            sudokuBoard.Make(dataList);
            sudokuBoard.Solve();

            var expected = answer.Replace(Environment.NewLine, string.Empty).Replace(" ", string.Empty);
            var replace = sudokuBoard.Answer.Replace(Environment.NewLine, string.Empty);
            Assert.Equal(expected, replace);
        }
    }

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
    }
}
