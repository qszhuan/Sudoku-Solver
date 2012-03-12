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

        public bool Set(Position position, int value, bool locked)
        {
            if (value == 0)
            {
                _cells[position.Index].Value = value;
                return true;
            }

            if (!_candidateRule.GetCandidates(position).Contains(value)) return false;

            _cells[position.Index].Value = value;
            _cells[position.Index].Locked = locked;
            return true;
        }

        private void Initlize()
        {
            for (var i = 0; i < BoardSize; i++)
            {
                _cells.Add(new CellWrapper());
            }
            _candidateRule = new GeneralCandidateRule(this);
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
                var position = new Position(i);
                _cells[i].Candidates = new Stack<int>(_candidateRule.GetCandidates(position));
            }
            for (var i = 0; i < BoardSize;)
            {
                if (_cells[i].Set())
                {
                    if (++i == BoardSize) break;

                    _cells[i].Candidates = new Stack<int>(_candidateRule.GetCandidates(new Position(i)));
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

        public Cell Get(Position position)
        {
            return _cells[position.Index].Clone;
        }
    }
}