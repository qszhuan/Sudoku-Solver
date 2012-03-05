namespace SudokuSolverTest
{
    // |NorthWest|North |NorthEast|
    // |West     |Center|East     |
    // |SouthWest|South |SouthEast|
    //
    // stand for every number's position in a 3*3 matrix
    // In a 9*9 matrix, for every 3*3 matrix's position
    //
    public enum Position
    {
        NorthWest = 0,
        North,
        NorthEast,
        West,
        Center,
        East,
        SouthWest,
        South,
        SouthEast

    }
}