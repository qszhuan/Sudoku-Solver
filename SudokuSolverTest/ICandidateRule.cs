using System.Collections.Generic;

namespace SudokuSolverTest
{
    public interface ICandidateRule
    {
        List<int> GetCandidates(Position position);
    }
}