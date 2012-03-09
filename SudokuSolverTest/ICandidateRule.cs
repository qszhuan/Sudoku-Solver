using System.Collections.Generic;

namespace SudokuSolverTest
{
    public interface ICandidateRule
    {
        List<int> GetCandidates(int xPos, int yPos);
        List<int> GetCandidates(int index);
    }
}