using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day_04 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_04()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = _input.Split('\n').Select(x => x.Trim()).ToArray();
        }

        public override string Solve_1()
        {
            return _lines.Aggregate(string.Empty, (a, s) => s != string.Empty ? a + s.Trim() : a += "\n" + s.Trim())
                .Split('\n')
                .Count(x =>
                        x.Contains("byr")
                     && x.Contains("iyr")
                     && x.Contains("eyr")
                     && x.Contains("hgt")
                     && x.Contains("hcl")
                     && x.Contains("ecl")
                     && x.Contains("pid")
                 ).ToString();
        }

        public override string Solve_2()
        {
            var passports = _lines.Aggregate(string.Empty, (a, s) => s != string.Empty ? a + " " + s : a += "\n" + s).Split('\n');

            var rules = new List<Func<(string, string), bool>>() {
                { (x) => x.Item1 == "byr" && int.TryParse(x.Item2, out int val2) && val2 <= 2002 && val2 >= 1920 },
                { (x) => x.Item1 == "iyr" && int.TryParse(x.Item2, out int val2) && val2 <= 2020 && val2 >= 2010 },
                { (x) => x.Item1 == "eyr" && int.TryParse(x.Item2, out int val2) && val2 <= 2030 && val2 >= 2020 },

                { (x) => x.Item1 == "hgt" && x.Item2.EndsWith("cm") ?   int.Parse(x.Item2.Replace("cm", "")) <= 193 &&
                                                                        int.Parse(x.Item2.Replace("cm", "")) >= 150 :
                                             x.Item2.EndsWith("in") ?   int.Parse(x.Item2.Replace("in", "")) <= 76 &&
                                                                        int.Parse(x.Item2.Replace("in", "")) >= 59
                                                                        : false },
                { (x) => x.Item1 == "hcl" && x.Item2.Count() == 7 && x.Item2.StartsWith("#") && x.Item2.Skip(1).All(c => (c >= 48 && c <= 57) || (c >= 97 && c <= 102) ) },
                { (x) => x.Item1 == "ecl" && x.Item2 == "amb" || x.Item2 == "blu" ||  x.Item2 == "brn" || x.Item2 == "gry"  || x.Item2 == "grn" || x.Item2 == "hzl" || x.Item2 == "oth" },
                { (x) => x.Item1 == "pid" && x.Item2.Count() == 9 && int.TryParse(x.Item2, out _) },
            };

            var validNo = 0;

            for (int i = 0; i < passports.Length; i++)
            {
                if (!passports.ElementAt(i).Contains("byr") ||
                    !passports.ElementAt(i).Contains("iyr") ||
                    !passports.ElementAt(i).Contains("eyr") ||
                    !passports.ElementAt(i).Contains("hgt") ||
                    !passports.ElementAt(i).Contains("hcl") ||
                    !passports.ElementAt(i).Contains("ecl") ||
                    !passports.ElementAt(i).Contains("pid")
                    )

                    continue;

                var fields = passports.ElementAt(i).Split(" ");

                if (fields.Length < 7)
                    continue;

                var found = true;

                for (int j = 0; j < fields.Length; j++)
                {
                    if (fields.ElementAt(j) == "")
                        continue;

                    var key = fields.ElementAt(j).Split(':')[0];
                    var value = fields.ElementAt(j).Split(':')[1];

                    if (key == "cid")
                        continue;

                    if (!rules.Any(x => x.Invoke((key, value))))
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    validNo++;
            }

            return validNo.ToString();
        }
    }
}