using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverTest.tokenizer
{
    public class VerticalBarStyleTokenizer : ITokenizerBase
    {
        public List<int> Generate(string problem)
        {
            problem = problem.Replace(Environment.NewLine, string.Empty);
            var strings = problem.Substring(1).Split('|');
            return strings.Select(s => string.IsNullOrWhiteSpace(s) ? 0 : int.Parse(s)).ToList();
        }
    }
}