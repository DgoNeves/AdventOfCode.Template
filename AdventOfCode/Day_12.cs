using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_12 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_12()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            //                           E, S, W, N
            var directions = new int[] { 0, 1, 2, 3 };
            var currentDirection = directions[0];
            var currentPosition = (0, 0);

            for (int i = 0; i < _lines.Length; i++)
            {
                var action = _lines[i][0];
                var number = int.Parse(string.Join("", _lines[i].Skip(1).ToArray()));

                if (action == 'E')
                {
                    currentPosition.Item2 += number;
                }
                else if (action == 'W')
                {
                    currentPosition.Item2 -= number;
                }
                else if (action == 'N')
                {
                    currentPosition.Item1 += number;
                }
                else if (action == 'S')
                {
                    currentPosition.Item1 -= number;
                }
                else if (action == 'R')
                {
                    currentDirection = directions[((currentDirection + (number / 90)) % 4)];
                }
                else if (action == 'L')
                {
                    currentDirection = directions[((4 + (currentDirection - (number / 90))) % 4)];
                }
                else if (action == 'F')
                {
                    if (currentDirection == 0)
                    {
                        currentPosition.Item2 += number;
                    }
                    else if (currentDirection == 2)
                    {
                        currentPosition.Item2 -= number;
                    }
                    else if (currentDirection == 3)
                    {
                        currentPosition.Item1 += number;
                    }
                    else if (currentDirection == 1)
                    {
                        currentPosition.Item1 -= number;
                    }
                }
            }

            return (Math.Abs(currentPosition.Item1) + Math.Abs(currentPosition.Item2)).ToString();
        }

        public override string Solve_2()
        {
            var directions = new int[] { 0, 1, 2, 3 };
            var currentDirection = directions[0];
            var currentPosition = (0, 0);
            var currentWayPointPosition = (1, 10);

            for (int i = 0; i < _lines.Length; i++)
            {
                var action = _lines[i][0];
                var number = int.Parse(string.Join("", _lines[i].Skip(1).ToArray()));

                if (action == 'E')
                {
                    currentWayPointPosition.Item2 += number;
                }
                else if (action == 'W')
                {
                    currentWayPointPosition.Item2 -= number;
                }
                else if (action == 'N')
                {
                    currentWayPointPosition.Item1 += number;
                }
                else if (action == 'S')
                {
                    currentWayPointPosition.Item1 -= number;
                }
                else if (action == 'R')
                {
                    for (int j = 0; j < (number / 90); j++)
                    {
                        var t = currentWayPointPosition.Item1;
                        currentWayPointPosition.Item1 = -currentWayPointPosition.Item2;
                        currentWayPointPosition.Item2 = t;
                    }
                }
                else if (action == 'L')
                {
                    for (int j = 0; j < (number / 90); j++)
                    {
                        var t = -currentWayPointPosition.Item1;
                        currentWayPointPosition.Item1 = currentWayPointPosition.Item2;
                        currentWayPointPosition.Item2 = t;
                    }
                }
                else if (action == 'F')
                {
                    currentPosition.Item1 += currentWayPointPosition.Item1 * number;
                    currentPosition.Item2 += currentWayPointPosition.Item2 * number;
                }
            }

            return (Math.Abs(currentPosition.Item1) + Math.Abs(currentPosition.Item2)).ToString();
        }
    }
}