using System;
using System.IO;
using SudokuSolverTest;
using SudokuSolverTest.tokenizer;

namespace SudokuSolver
{
    public class Program
    {
        public static void Main(string [] args)
        {
            var numerBoardTokenizer = new NumberBoardTokenizer();

            using (var streamReader = new StreamReader(new FileStream("./question.txt", FileMode.Open)))
            {
                using (var streamWriter = new StreamWriter(new FileStream("./answer.txt", FileMode.OpenOrCreate)))
                {
                    var lines = streamReader.ReadToEnd().Split(new[] {Environment.NewLine},
                                                               StringSplitOptions.RemoveEmptyEntries);
                    for (var index = 0; index < lines.Length; index += 10)
                    {
                        var header = lines[index];
                        Console.WriteLine(header);
                        streamWriter.WriteLine(header);
                        var generate = numerBoardTokenizer.Generate(string.Join("", lines, index + 1, 9));
                        var sudokuBoard = new SudokuBoard();
                        sudokuBoard.Make(generate);
                        streamWriter.WriteLine(sudokuBoard.Answer);
                        sudokuBoard.Solve();
                        streamWriter.WriteLine(sudokuBoard.Answer);
                    }
                }
            }
        }
    }
}
