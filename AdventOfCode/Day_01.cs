using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_01 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_01()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 1";

        public override string Solve_2() => $"Solution to {ClassPrefix} {CalculateIndex()}, part 2";
    }
}