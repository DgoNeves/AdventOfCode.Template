using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_05 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_05()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            var currentMax = 0;

            for (int i = 0; i < _lines.Length; i++)
            {
                var (row, col) = GetRowCol(_lines.ElementAt(i));
                var seatId = row * 8 + col;

                if (currentMax < seatId)
                {
                    currentMax = seatId;
                }
            }

            return currentMax.ToString();
        }

        private (int, int) GetRowCol(string v)
        {
            return (Parse(v.Substring(0, 7)), Parse(v.Substring(7, 3)));
        }

        private int Parse(string s)
        {
            var sb = s.Replace("B", "1")
                .Replace("F", "0")
                .Replace("R", "1")
                .Replace("L", "0");

            return Convert.ToInt32(sb, 2);
        }

        public override string Solve_2()
        {
            var listOfSeats = new List<int>();

            for (int i = 0; i < _lines.Length; i++)
            {
                var (row, col) = GetRowCol(_lines.ElementAt(i));
                var seatId = row * 8 + col;

                listOfSeats.Add(seatId);
            }

            var orderedSeats = listOfSeats.OrderBy(x => x);

            for (int i = 1; i < orderedSeats.Count(); i++)
            {
                if (orderedSeats.ElementAt(i) - orderedSeats.ElementAt(i - 1) == 2)
                {
                    return (orderedSeats.ElementAt(i) - 1).ToString();
                }
            }

            return "Not found!";
        }
    }
}