using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_09 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_09()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            var current = 25;

            IOrderedEnumerable<UInt64> ordered25;
            UInt64 n;
            do
            {
                var next25 = _lines.Skip(current - 25).Take(25);
                ordered25 = next25.Select(UInt64.Parse).OrderBy(x => x);
                n = UInt64.Parse(_lines.ElementAt(current));
                ++current;
            } while (IsCorrectNumber(n, ordered25));

            return n.ToString();
        }

        public bool IsCorrectNumber(UInt64 n, IOrderedEnumerable<UInt64> last25)
        {
            int low = 0;
            int high = last25.Count() - 1;

            while (low != high)
            {
                var sum = last25.ElementAt(low) + last25.ElementAt(high);
                if (sum == n)
                    return true;

                if (sum > n)
                {
                    --high;
                }
                if (sum < n)
                {
                    ++low;
                }
            }

            return false;
        }

        public override string Solve_2()
        {
            UInt64 resFromPart1 = 258585477;

            int seq = 2;
            int current = 0;
            var longLines = _lines.Select(x => UInt64.Parse(x)).ToList();

            do
            {
                var numbers = Enumerable.Range(current, seq).Select(i => longLines.ElementAt(i));

                UInt64 sum = 0;

                for (int i = 0; i < numbers.Count(); i++)
                {
                    sum += numbers.ElementAt(i);
                }

                if (sum == resFromPart1)
                {
                    return (numbers.Min() + numbers.Max()).ToString();
                }
                else if (sum > resFromPart1)
                {
                    ++current;
                    seq = 2;
                }
                else
                {
                    ++seq;
                }
            } while (current - seq != longLines.Count);

            return "Not found!";
        }
    }
}