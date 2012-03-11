using System;

namespace SudokuSolverTest
{
    public class Position
    {
        public int XPos { get; private set; }
        public int YPos { get; private set; }
        public int Index
        {
            get { return XPos*SudokuBoard.EdgeSize + YPos; }
        }

        public Position(int xPos, int yPos)
        {
            Validation(xPos);
            Validation(yPos);

            XPos = xPos;
            YPos = yPos;
        }

        private static void Validation(int axis)
        {
            if (axis >= SudokuBoard.EdgeSize || axis < 0)
            {
                throw new IndexOutOfRangeException("position should be in range [0,8]");
            }
        }

        public Position(int index)
        {
            if(index <0 || index > 81) throw new IndexOutOfRangeException("index should be in range [0, 80]");
            XPos = index/SudokuBoard.EdgeSize;
            YPos = index%SudokuBoard.EdgeSize;
        }

        public int SquareOriginX()
        {
            return XPos/SudokuBoard.SquareEdgeSize*SudokuBoard.SquareEdgeSize;
        }

        public int SquareOriginY()
        {
            return YPos/SudokuBoard.SquareEdgeSize*SudokuBoard.SquareEdgeSize;
        }
    }
}