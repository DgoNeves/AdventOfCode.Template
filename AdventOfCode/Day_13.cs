using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_13 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;
        private readonly int _target;

        public Day_13()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
            _target = int.Parse(_lines[0]);
        }

        public override string Solve_1()
        {
            var values = _lines[1].Split(',').Where(x => x != "x").Select(int.Parse);

            var c = values.Select((x, i) => new { val = Math.Abs((_target % x) - x), ori = x });
            var val = c.OrderBy(x => x.val).First();

            return (val.ori * val.val).ToString();
        }

        public override string Solve_2()
        {
            return "";
        }
    }
}