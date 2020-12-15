using AoCHelper;
using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public class Day_14 : BaseDay
    {
        private readonly string _input;
        private readonly string[] _lines;

        public Day_14()
        {
            _input = File.ReadAllText(InputFilePath);
            _lines = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            string mask = "";
            var mem = new Dictionary<int, string>();

            for (int i = 0; i < _lines.Length; i++)
            {
                if (_lines[i].Contains("mask"))
                {
                    mask = _lines[i].Split("=")[1].Trim();
                }
                else
                {
                    var numberMem = _lines[i].Substring(_lines[i].IndexOf('[') + 1, _lines[i].IndexOf(']') - _lines[i].IndexOf('[') - 1);
                    var memPosition = int.Parse(numberMem);
                    var memBin = _lines[i].Split("=")[1].Trim();

                    var binStr = Convert.ToString(int.Parse(memBin), 2);

                    binStr = ApplyMask(mask, binStr);

                    if (mem.ContainsKey(memPosition))
                    {
                        mem[memPosition] = binStr;
                    }
                    else
                    {
                        mem.Add(memPosition, binStr);
                    }
                }
            }

            UInt64 result = 0;
            foreach (var (key, value) in mem)
            {
                result += Convert.ToUInt64(value, 2);
            }

            return result.ToString();
        }

        private string ApplyMask(string mask, string binStr)
        {
            var newStr = new StringBuilder();

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] != 'X')
                {
                    newStr.Append(mask[i]);
                }
                else if (i < mask.Length - binStr.Length)
                {
                    newStr.Append('0');
                }
                else
                {
                    newStr.Append(binStr[i - mask.Length + binStr.Length]);
                }
            }

            return newStr.ToString();
        }

        public override string Solve_2()
        {
            string mask = "";
            var mem = new Dictionary<ulong, string>();

            for (int i = 0; i < _lines.Length; i++)
            {
                if (_lines[i].Contains("mask"))
                {
                    mask = _lines[i].Split("=")[1].Trim();
                }
                else
                {
                    var numberMem = _lines[i].Substring(_lines[i].IndexOf('[') + 1, _lines[i].IndexOf(']') - _lines[i].IndexOf('[') - 1);
                    var memPosition = int.Parse(numberMem);
                    var memBin = _lines[i].Split("=")[1].Trim();

                    var binStr = Convert.ToString(int.Parse(numberMem), 2);

                    binStr = ApplyMask2(mask, binStr);

                    var allMemories = GetAllValues(new List<string> { binStr });

                    for (int j = 0; j < allMemories.Count; j++)
                    {
                        var k = Convert.ToUInt64(allMemories[j], 2);

                        if (mem.ContainsKey(k))
                        {
                            mem[k] = memBin;
                        }
                        else
                        {
                            mem.Add(k, memBin);
                        }
                    }
                }
            }

            UInt64 result = 0;
            foreach (var (key, value) in mem)
            {
                result += ulong.Parse(value);
            }

            return result.ToString();
        }

        private List<string> GetAllValues(List<string> value)
        {
            if (value[0].IndexOf('X') == -1)
                return value;

            var r = new List<string>();

            foreach (var item in value)
            {
                var i = item.IndexOf('X');
                r.Add(item.Substring(0, i) + "1" + item.Substring(i + 1));
                r.Add(item.Substring(0, i) + "0" + item.Substring(i + 1));
            }

            return GetAllValues(r);
        }

        private string ApplyMask2(string mask, string binStr)
        {
            var newStr = new StringBuilder();

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[i] == 'X')
                {
                    newStr.Append('X');
                }
                else if (mask[i] == '0' && i >= mask.Length - binStr.Length)
                {
                    newStr.Append(binStr[i - mask.Length + binStr.Length]);
                }
                else if (mask[i] == '1')
                {
                    newStr.Append('1');
                }
                else if (i < mask.Length - binStr.Length)
                {
                    newStr.Append('0');
                }
                else
                {
                    newStr.Append(binStr[i - mask.Length + binStr.Length]);
                }
            }

            return newStr.ToString();
        }
    }
}