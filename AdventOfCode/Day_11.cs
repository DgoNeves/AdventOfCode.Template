using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day_11 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;
        private readonly int MAX_X;
        private readonly int MAX_Y;

        public Day_11()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);

            MAX_X = _lines[0].Length;
            MAX_Y = _lines.Length;
        }

        public override string Solve_1()
        {
            // -1 nao pode ocupar (.)
            // 0 livre
            // 1 ocupado
            var currentSeatState = new Dictionary<Point, int>();
            var nextSeatState = new Dictionary<Point, int>();

            for (int i = 0; i < _lines.Length; i++)
            {
                for (int j = 0; j < _lines[i].Length; j++)
                {
                    currentSeatState.Add(new Point(j, i), GetValueSeat(_lines[i][j]));
                }
            }

            var over = false;

            while (!over)
            {
                nextSeatState = IterateSeats(currentSeatState);

                var t = true;

                for (int i = 0; i < nextSeatState.Count; i++)
                {
                    if (nextSeatState.ElementAt(i).Value != currentSeatState[nextSeatState.ElementAt(i).Key])
                    {
                        t = false;
                        break;
                    }
                }

                if (t)
                {
                    over = true;
                    break;
                }

                currentSeatState = nextSeatState;
            }

            var count = 0;

            for (int i = 0; i < nextSeatState.Count; i++)
            {
                if (nextSeatState.ElementAt(i).Value == 1)
                    ++count;
            }

            return count.ToString();
        }

        private Dictionary<Point, int> IterateSeats(Dictionary<Point, int> currentSeatState)
        {
            var nextSeatState = new Dictionary<Point, int>();

            for (int i = 0; i < currentSeatState.Count; i++)
            {
                var seatState = currentSeatState.ElementAt(i);

                if (currentSeatState.ElementAt(i).Value == -1)
                {
                    nextSeatState.Add(seatState.Key, seatState.Value);
                    continue;
                }

                var countAdjacentSeats = GetAdjacentOcupiedSeats(seatState.Key, currentSeatState);

                if (seatState.Value == 0 && countAdjacentSeats == 0)
                {
                    nextSeatState.Add(seatState.Key, 1);
                }
                else if (seatState.Value == 1 && countAdjacentSeats > 4)
                {
                    nextSeatState.Add(seatState.Key, 0);
                }
                else
                {
                    nextSeatState.Add(seatState.Key, seatState.Value);
                }
            }

            // PrintState(nextSeatState);

            return nextSeatState;
        }

        private int GetAdjacentOcupiedSeats(Point key, Dictionary<Point, int> currentSeatState)
        {
            var lista = new List<Point>()
            {
                new Point(-1,-1),
                new Point(-1,1),
                new Point(-1,0),
                new Point(0,-1),
                new Point(0,1),
                new Point(1,-1),
                new Point(1,0),
                new Point(1,1),
            };

            var chekcPoint = new Point(key.X, key.Y);

            return lista.Count(p => isValidSeat(key, p) && currentSeatState[new Point(key.X + p.X, key.Y + p.Y)] == 1);
        }

        private bool isValidSeat(Point key, Point nextCoord)
        {
            return key.X + nextCoord.X >= 0 && key.X + nextCoord.X < MAX_X && key.Y + nextCoord.Y >= 0 && key.Y + nextCoord.Y < MAX_Y;
        }

        private int GetValueSeat(char seat)
        {
            if (seat == '.')
                return -1;

            if (seat == '#')
                return 1;

            return 0;
        }

        public override string Solve_2()
        {
            var currentSeatState = new Dictionary<Point, int>();
            var nextSeatState = new Dictionary<Point, int>();

            for (int i = 0; i < _lines.Length; i++)
            {
                for (int j = 0; j < _lines[i].Length; j++)
                {
                    currentSeatState.Add(new Point(j, i), GetValueSeat(_lines[i][j]));
                }
            }

            var over = false;

            while (!over)
            {
                nextSeatState = IterateSeats2(currentSeatState);

                var t = true;

                for (int i = 0; i < nextSeatState.Count; i++)
                {
                    if (nextSeatState.ElementAt(i).Value != currentSeatState[nextSeatState.ElementAt(i).Key])
                    {
                        t = false;
                        break;
                    }
                }

                if (t)
                {
                    over = true;
                    break;
                }

                currentSeatState = nextSeatState;
            }

            var count = 0;

            for (int i = 0; i < nextSeatState.Count; i++)
            {
                if (nextSeatState.ElementAt(i).Value == 1)
                    ++count;
            }

            return count.ToString();
        }

        private Dictionary<Point, int> IterateSeats2(Dictionary<Point, int> currentSeatState)
        {
            var nextSeatState = new Dictionary<Point, int>();

            for (int i = 0; i < currentSeatState.Count; i++)
            {
                var seatState = currentSeatState.ElementAt(i);

                if (currentSeatState.ElementAt(i).Value == -1)
                {
                    nextSeatState.Add(seatState.Key, seatState.Value);
                    continue;
                }

                var countAdjacentSeats = GetAdjacentOcupiedSeats2(seatState.Key, currentSeatState);

                if (seatState.Value == 0 && countAdjacentSeats == 0)
                {
                    nextSeatState.Add(seatState.Key, 1);
                }
                else if (seatState.Value == 1 && countAdjacentSeats > 4)
                {
                    nextSeatState.Add(seatState.Key, 0);
                }
                else
                {
                    nextSeatState.Add(seatState.Key, seatState.Value);
                }
            }

            // PrintState(nextSeatState);

            return nextSeatState;
        }

        private int GetAdjacentOcupiedSeats2(Point key, Dictionary<Point, int> currentSeatState)
        {
            var lista = new List<Point>()
            {
                new Point(-1,-1),
                new Point(-1,1),
                new Point(-1,0),
                new Point(0,-1),
                new Point(0,1),
                new Point(1,-1),
                new Point(1,0),
                new Point(1,1),
            };

            var countOcupied = 0;
            var chekcPoint = new Point(key.X, key.Y);

            for (int i = 0; i < lista.Count; i++)
            {
                var p = lista[i];

                key = new Point(chekcPoint.X, chekcPoint.Y); ;

                while (isValidSeat(key, p))
                {
                    key = new Point(key.X + p.X, key.Y + p.Y);
                    if (currentSeatState[key] == 1)
                    {
                        ++countOcupied;
                        break;
                    }
                    if (currentSeatState[key] == -1)
                    {
                        continue;
                    }

                    if (currentSeatState[key] == 0)
                    {
                        break;
                    }
                }
            }

            return countOcupied;
            // return lista.Count(p => isValidSeat(key, p) && currentSeatState[new Point(key.X + p.X, key.Y + p.Y)] == 1);
        }
    }

    public class Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Point p)
                return p.X == this.X && p.Y == this.Y;

            return false;
        }

        public override string ToString()
        {
            return $"({this.X} : {this.Y})";
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}