using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_15 : BaseDay
    {
        private readonly string _input;

        public Day_15()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            return Solve(2020);
        }

        public override string Solve_2()
        {
            return Solve(30000000);
        }

        public string Solve(int num)
        {
            var init = _input.Split(",").Select(int.Parse).ToList();

            // number, index
            var lastTimeItWas = new Dictionary<int, List<int>>();

            for (int i = 0; i < init.Count(); i++)
            {
                lastTimeItWas.Add(init[i], new List<int>() { i });
            }

            var round = init.Count;

            while (round < num)
            {
                var currentNo = init[round - 1];

                if (lastTimeItWas.ContainsKey(currentNo) && lastTimeItWas[currentNo].Count > 1)
                {
                    var diffLastTwo = lastTimeItWas[currentNo][lastTimeItWas[currentNo].Count - 1] - lastTimeItWas[currentNo][lastTimeItWas[currentNo].Count - 2];

                    init.Add(diffLastTwo);

                    if (!lastTimeItWas.ContainsKey(diffLastTwo))
                    {
                        lastTimeItWas.Add(diffLastTwo, new List<int>() { round });
                    }
                    else
                    {
                        lastTimeItWas[diffLastTwo].Add(round);
                    }
                }
                else
                {
                    init.Add(0);
                    lastTimeItWas[0].Add(round);
                }

                ++round;
            }

            return init[num - 1].ToString();
        }
    }
}