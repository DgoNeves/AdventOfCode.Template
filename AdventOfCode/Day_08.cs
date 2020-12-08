using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_08 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_08()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            return Solve(_lines).Item2;
        }

        public override string Solve_2()
        {
            var allInputs = GenerateInput();
            var t = allInputs.Select(x => Solve(x)).First(x => x.Item1 == 1);
            return t.Item2;
        }

        private List<string[]> GenerateInput()
        {
            var current = 0;
            var inputs = new List<string[]>();

            while (current < _input.Length)
            {
                current = _input.IndexOf("jmp", current);
                if (current == -1)
                    break;

                var str = _input.Substring(0, current) + "nop" + _input.Substring(current + 3);

                inputs.Add(str.Split("\n"));
                ++current;
            }

            current = 0;
            while (current < _input.Length)
            {
                current = _input.IndexOf("nop", current);
                if (current == -1)
                    break;

                var str = _input.Substring(0, current) + "jmp" + _input.Substring(current + 3);
                inputs.Add(str.Split("\n"));
                ++current;
            }

            return inputs;
        }

        public (int, string) Solve(string[] lines)
        {
            var mem = new int[lines.Length];
            var pointer = 0;
            var count = 0;
            var acc = 0;

            while (pointer < lines.Length)
            {
                if (mem[pointer] != 0)
                {
                    return (0, acc.ToString());
                }

                ++count;

                var action = lines.ElementAt(pointer).Split(" ");
                var op = action[0];
                var inc = int.Parse(action[1]);

                if (op == "nop")
                {
                    ++pointer;
                    continue;
                }

                if (op == "acc")
                {
                    acc += inc;
                    ++mem[pointer];
                    ++pointer;
                    continue;
                }

                if (op == "jmp")
                {
                    pointer += inc;
                }
            }

            return (1, acc.ToString());
        }
    }
}