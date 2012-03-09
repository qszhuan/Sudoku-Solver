using System.Collections.Generic;

namespace SudokuSolverTest.tokenizer
{
    public interface ITokenizerBase
    {
        List<int> Generate(string problem);

    }
}