using System;

namespace SudokuSolverTest
{
    public class Cell
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if (Immutable) return;
                if(value > 9 || value < 0)
                    throw new ArgumentOutOfRangeException("Value","value is not in [0-9]");
                _value = value;
            }
        }

        public bool Immutable { get; set; }

        public Cell Clone
        {
            get { return new Cell(_value, Immutable); }
        }

        public Cell(int value = 0, bool immutable = false)
        {
            Value = value;
            Immutable = immutable;
        }
    }
}