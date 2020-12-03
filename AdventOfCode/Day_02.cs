using AoCHelper;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_02 : BaseDay
    {
        private readonly string _input;

        public Day_02()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            var lines = _input.Split('\n');
            var count = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                var part = lines[i].Split(' ');

                if (part[2].Count(x => x == part[1][0]) >= int.Parse(part[0].Split("-")[0])
                    && part[2].Count(x => x == part[1][0]) <= int.Parse(part[0].Split("-")[1]))
                {
                    count++;
                }
            }

            return count.ToString();
        }

        public override string Solve_2()
        {
            var lines = _input.Split('\n');
            var count = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                var part = lines[i].Split(' ');

                if (part[2][int.Parse(part[0].Split('-')[0]) - 1] == part[1][0]
                    ^ part[2][int.Parse(part[0].Split('-')[1]) - 1] == part[1][0])
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}