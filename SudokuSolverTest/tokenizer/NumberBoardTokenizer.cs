using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverTest.tokenizer
{
    public class NumberBoardTokenizer : ITokenizerBase
    {
        public List<int> Generate(string problem)
        {
            var normalized = problem.Replace(Environment.NewLine, string.Empty);
            var list = normalized.Select(c=> int.Parse(c.ToString())).ToList();
            return list;
        }
    }
}