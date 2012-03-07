using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolverTest
{
    public class SudokuBoard
    {
        public static List<int> Scopes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private readonly List<Cell> _cells = new List<Cell>();
        private ICandidateRule _candidateRule;
        public const int BoardSize = EdgeSize*EdgeSize;
        public const int EdgeSize = 3*3;
        public const int SquareEdgeSize = 3;
        public string Answer
        {
         get
         {
             var builder = new StringBuilder();
             for (var i = 0; i < EdgeSize; i++)
             {
                 builder.AppendFormat("|{0}{1}", string.Join("|", _cells.GetRange(i*EdgeSize, EdgeSize).Select(c=>c.Value)), Environment.NewLine);
             }
             return builder.ToString();
         }   
        }

        public SudokuBoard()
        {
            Initlize();
        }

        public List<Cell> Cells
        {
            get{return _cells.Select(cell => cell.Clone).ToList();}
        }

        public bool Solved 
        {
            get
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    
                }
                return true;
            }
        }


        public bool Set(int xPos, int yPos, int value, bool locked = false)
        {
            if (!_candidateRule.GetCandidates(xPos, yPos).Contains(value)) return false;

            _cells[xPos*9 + yPos].Value = value;
            _cells[xPos*9 + yPos].Locked = locked;
            return true;
        }

        public Cell Get(int xPos, int yPos)
        {
            return _cells[xPos * EdgeSize + yPos].Clone;
        }

        private void Initlize()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                _cells.Add(new Cell());
            }
            _candidateRule = new GeneralCandidateRule(_cells);
        }

        public void Make(List<int> dataList)
        {
            for (var i = 0; i < dataList.Count; i++)
            {
               _cells[i].Value = dataList[i];
                _cells[i].Locked = i != 0;
            }
        }

        public void Solve()
        {
            ResetIndex();

        }

        private int _currentIndex = 0;


        private Cell Current
        {
            get { return _cells[_currentIndex]; }
        }
        private Cell Next
        {
            get
            {
                return _currentIndex == 80 ? null : _cells[++_currentIndex];
            }
        }
        private void ResetIndex()
        {
            _currentIndex = 0;
        }

        private Cell Previous
        {
            get { return _currentIndex == 0 ? null : _cells[--_currentIndex]; }
        }
    }
}