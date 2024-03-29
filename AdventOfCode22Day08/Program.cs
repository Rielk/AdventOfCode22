﻿using AdventOfCode22Day08;
using AdventOfCode22Day08.Properties;

string input = Resources.Input1;

int[][] array = input.Split(Environment.NewLine).Select(l => l.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
Forest Forest = new(array);

Console.WriteLine($"Number of visible Trees: {Forest.CountVisible()}");
Console.WriteLine();
Console.WriteLine($"Highest Scenic Score: {Forest.FindHighestScenicScore()}");