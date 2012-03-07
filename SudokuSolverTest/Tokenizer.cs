using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverTest
{
    public class Tokenizer
    {
        public static List<int> Generate(string problem)
        {
            problem = problem.Replace(Environment.NewLine, string.Empty);
            var strings = problem.Substring(1).Split('|');
            return strings.Select(s=> string.IsNullOrWhiteSpace(s) ? 0:int.Parse(s)).ToList();
        }
    }
}