using System.Collections.Generic;

namespace SudokuSolverTest
{
    public class CellWrapper :Cell
    {
        public Stack<int> Candidates = new Stack<int>();

        public bool Set()
        {
            Value = 0;

            if (Candidates.Count != 0 )
            {
                Value = Candidates.Pop();
                return true;
            }
            return Locked;
        }
    }
}