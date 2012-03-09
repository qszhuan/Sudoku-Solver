using SudokuSolverTest.tokenizer;
using Xunit;
using Xunit.Extensions;

namespace SudokuSolverTest
{
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