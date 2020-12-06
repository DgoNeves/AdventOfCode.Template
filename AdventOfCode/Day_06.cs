using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_06 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_06()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            return _lines.Aggregate(string.Empty, (a, s) => s != string.Empty ? a + s.Trim() : a += "\n" + s.Trim())
                            .Split('\n')
                            .Sum(x => x.GroupBy(x => x).Count()).ToString();
        }

        public override string Solve_2()
        {
            return _lines.Aggregate(string.Empty, (a, s) => s != string.Empty ? a + " " + s.Trim() : a += "\n" + s.Trim())
                  .Split('\n')
                  .Sum(x => x.Trim().GroupBy(y => y).Count(z => z.Count() == x.Trim().Split(" ").Count()))
                  .ToString();
        }
    }
}