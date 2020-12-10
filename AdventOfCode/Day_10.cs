using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_10 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_10()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            var adapters = _lines.Select(i => int.Parse(i)).OrderBy(i => i).ToList();
            adapters.Insert(0, 0);
            adapters.Add(adapters.ElementAt(adapters.Count - 1) + 3);

            var countDiffOne = 0;
            var countDiffThree = 0;

            for (int i = 1; i < adapters.Count; i++)
            {
                var diff = adapters.ElementAt(i) - adapters.ElementAt(i - 1);
                if (diff == 1)
                {
                    ++countDiffOne;
                }
                else if (diff == 3)
                {
                    ++countDiffThree;
                }
            }

            return (countDiffOne * countDiffThree).ToString();
        }

        public override string Solve_2()
        {
            var adapters = _lines.Select(i => int.Parse(i)).OrderBy(i => i).ToList();
            adapters.Insert(0, 0);
            adapters.Add(adapters.ElementAt(adapters.Count - 1) + 3);

            var dict = new Dictionary<int, ulong>();
            dict.Add(adapters.First(), 1);

            for (int i = 0; i < adapters.Count; i++)
            {
                var j = i + 1;

                if (j > adapters.Count - 1)
                    break;

                while (adapters.ElementAt(j) - adapters.ElementAt(i) <= 3)
                {
                    if (dict.ContainsKey(adapters.ElementAt(j)))
                    {
                        dict[adapters.ElementAt(j)] += dict[adapters.ElementAt(i)];
                    }
                    else
                    {
                        dict.Add(adapters.ElementAt(j), dict[adapters.ElementAt(i)]);
                    }

                    ++j;

                    if (j > adapters.Count - 1)
                        break;
                }
            }

            return dict[adapters.ElementAt(adapters.Count - 1)].ToString();
        }
    }
}