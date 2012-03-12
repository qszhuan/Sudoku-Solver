using System;
using System.Linq;
using SudokuSolverTest.tokenizer;
using Xunit;
using Xunit.Extensions;

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
            sudokuBoard.Cells.ForEach(cell => Assert.Equal(0, cell.Value));
            cells[0].Value = 1;
            Assert.Equal(1, cells[0].Value);

        }

        [Fact]
        public void should_set_the_cell_value_for()
        {
            var sudokuBoard = new SudokuBoard();
            var position = new Position(0, 0);
            sudokuBoard.Set(position, 9, false);
            Assert.Equal(9, sudokuBoard.Get(position).Value);

            position = new Position(8, 8);
            sudokuBoard.Set(position, 1, true);
            Assert.Equal(1, sudokuBoard.Get(position).Value);
            Assert.Equal(true, sudokuBoard.Get(position).Locked);
        }

        [Fact]
        public void should_get_empty_candidates_for_locked_cell()
        {
            var sudokuBoard = new SudokuBoard();
            var position = new Position(0, 0);
            sudokuBoard.Set(position, 2, true);

            var candidates = new GeneralCandidateRule(sudokuBoard).GetCandidates(position);
            Assert.Equal(0, candidates.Count);
        }

        [Fact]
        public void should_get_candidates_for_unlocked_cell()
        {
            var sudokuBoard = new SudokuBoard();
            var position = new Position(0,0);
            var candidates = new GeneralCandidateRule(sudokuBoard).GetCandidates(position);
            Assert.Equal(9, candidates.Count);

            sudokuBoard.Set(new Position(0, 1), 2, false);
            sudokuBoard.Set(new Position(0, 2), 3, false);
            sudokuBoard.Set(new Position(0, 3), 4, false);
            sudokuBoard.Set(new Position(0, 4), 5, false);
            sudokuBoard.Set(new Position(0, 5), 6, false);
            sudokuBoard.Set(new Position(0, 6), 7, false);
            sudokuBoard.Set(new Position(0, 7), 8, false);
            sudokuBoard.Set(new Position(0, 8), 9, false);
            candidates = new GeneralCandidateRule(sudokuBoard).GetCandidates(position);
            Assert.Equal(1, candidates.Count);
            Assert.Equal(1, candidates.First());
        }

        [Fact]
        public void should_not_break_the_unique_rule()
        {
            var sudokuBoard = new SudokuBoard();
            var position = new Position(0, 0);
            sudokuBoard.Set(position, 1, false);
            Assert.Equal(1,sudokuBoard.Get(position).Value);
            
            position = new Position(0, 1);
            sudokuBoard.Set(position, 1, false);
            Assert.NotEqual(1, sudokuBoard.Get(position).Value);
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

    public class TokenizerFacts
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

        [Theory]
        [InlineData(Problem, 7,2,0,5,8)]
        [InlineData(Expert, 4,9,0,0,0)]
        public void should_generate_data_list(string input, int _0, int _1, int _2, int _18, int _80)
        {
            var dataList = new VerticalBarStyleTokenizer().Generate(input);
            Assert.Equal(81, dataList.Count);
            Assert.Equal(_0, dataList[0]);
            Assert.Equal(_1, dataList[1]);
            Assert.Equal(_2, dataList[2]);
            Assert.Equal(_18, dataList[18]);
            Assert.Equal(_80, dataList[80]);
        }

        [Theory]
        [InlineData(@"003020600
900305001
001806400
008102900
700000008
006708200
002609500
800203009
005010300", 0, 0, 3, 0, 0)]
        public void should_generate_data_list_(string input, int _0, int _1, int _2, int _18, int _80 )
        {
            var dataList = new NumberBoardTokenizer().Generate(input);
            Assert.Equal(81, dataList.Count);
            Assert.Equal(_0, dataList[0]);
            Assert.Equal(_1, dataList[1]);
            Assert.Equal(_2, dataList[2]);
            Assert.Equal(_18, dataList[18]);
            Assert.Equal(_80, dataList[80]);
        }
    }
}
