using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolverTest
{
    public class SudokuBoard
    {
        public static List<int> Scopes = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private readonly List<CellWrapper> _cells = new List<CellWrapper>();
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

        public List<CellWrapper> Cells
        {
            
            get { return _cells; }
        }

        public bool Set(int xPos, int yPos, int value, bool locked = false)
        {
            if (value == 0)
            {
                _cells[xPos * 9 + yPos].Value = value;
                return true;
            }

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
                _cells.Add(new CellWrapper());
            }
            _candidateRule = new GeneralCandidateRule(_cells);
        }

        public void Make(List<int> dataList)
        {
            for (var i = 0; i < dataList.Count; i++)
            {
                _cells[i].Locked = false;
                _cells[i].Value = dataList[i];
                _cells[i].Locked = dataList[i] != 0;
            }
        }

        public void Solve()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                _cells[i].Candidates = new Stack<int>(_candidateRule.GetCandidates(i));
            }
            for (var i = 0; i < BoardSize;)
            {
                if (_cells[i].Set())
                {
                    if (++i == BoardSize) break;

                    _cells[i].Candidates = new Stack<int>(_candidateRule.GetCandidates(i));
                    continue;
                }
                while (--i != 0)
                {
                    if (i < 0)
                    {
                        Console.Error.WriteLine("No Answer");
                        break;
                    }
                    if (!_cells[i].Locked) break;
                }
            }
        }
    }
}