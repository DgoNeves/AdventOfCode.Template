using AoCHelper;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_03 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_03()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = _input.Split('\n').Select(x => x.Trim()).ToArray();
        }

        public override string Solve_1() => Solve_1(3, 1).ToString();

        public long Solve_1(int right, int down)
        {
            var pos = (x: 0, y: 0);

            long result = 0;

            while (_lines.Count() > pos.y)
            {
                if (_lines[pos.y][pos.x] == '#')
                {
                    result++;
                }

                pos.x += right;
                pos.y += down;

                if (pos.x >= _lines[pos.y - down].Count())
                {
                    pos.x -= _lines[pos.y - down].Count();
                }
            }

            return result;
        }

        public override string Solve_2()
        {
            return (Solve_1(1, 1) * Solve_1(3, 1) * Solve_1(5, 1) * Solve_1(7, 1) * Solve_1(1, 2)).ToString();
        }
    }
}