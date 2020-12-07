using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_07 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_07()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            var dicts = ParseInput();

            var sum = 0;

            for (int i = 0; i < dicts.Count; i++)
            {
                if (Solver(dicts, "shiny gold", dicts.ElementAt(i).Key) != 0)
                {
                    sum++;
                }
            }

            return sum.ToString();
        }

        private int Solver(Dictionary<string, List<string>> dicts, string lookingFor, string current)
        {
            var c = dicts[current];

            if (c.Count == 0)
                return 0;

            if (c.Contains(lookingFor))
                return 1;

            return c.Sum(x => Solver(dicts, lookingFor, x));
        }

        private int Solver2(Dictionary<string, Dictionary<string, int>> dicts, string current)
        {
            var c = dicts[current];

            if (c.Count == 0)
            {
                return 0;
            }

            return c.Sum(x => x.Value + x.Value * Solver2(dicts, x.Key));
        }

        private Dictionary<string, List<string>> ParseInput()
        {
            var dict = new Dictionary<string, List<string>>();

            for (int i = 0; i < _lines.Count(); i++)
            {
                var half = _lines.ElementAt(i).Split(" bags contain ");
                var mainBag = half[0];

                if (_lines.ElementAt(i).Contains("no other bags"))
                {
                    dict.Add(mainBag, new List<string>());
                    continue;
                }

                var otherBags = half[1].Split(",").Select(x => x.Trim())
                    .Select(x => x.Split(" ")[1] + " " + x.Split(" ")[2]).ToList();

                dict.Add(mainBag, otherBags);
            }

            return dict;
        }

        private Dictionary<string, Dictionary<string, int>> ParseInput2()
        {
            var dict = new Dictionary<string, Dictionary<string, int>>();

            for (int i = 0; i < _lines.Count(); i++)
            {
                var half = _lines.ElementAt(i).Split(" bags contain ");
                var mainBag = half[0];

                if (_lines.ElementAt(i).Contains("no other bags"))
                {
                    dict.Add(mainBag, new Dictionary<string, int>());
                    continue;
                }
                var d = new Dictionary<string, int>();

                half[1].Split(",").Select(x => x.Trim())
                    .Select(x => new { S = x.Split(" ")[1] + " " + x.Split(" ")[2], I = int.Parse(x.Split(" ")[0]) })
                    .ToList()
                    .ForEach(x => d.Add(x.S, x.I));

                dict.Add(mainBag, d);
            }

            return dict;
        }

        public override string Solve_2()
        {
            var dict = ParseInput2();
            return Solver2(dict, "shiny gold").ToString();
        }
    }
}