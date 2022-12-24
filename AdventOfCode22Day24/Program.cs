using AdventOfCode22Day24;
using AdventOfCode22Day24.Properties;

string input = Resources.Input1;

string[] lines = input.Split(Environment.NewLine);
int height = lines.Length - 2;
int width = lines[0].Length - 2;

char[,] charArray = new char[width, height];
foreach (int i in Enumerable.Range(0, width))
    foreach (int j in Enumerable.Range(0, height))
        charArray[i, j] = lines[j + 1][i + 1];

int startColumn = lines[0].IndexOf('.') - 1;
int endColumn = lines[height + 1].IndexOf('.') - 1;

Map Map = new(charArray);

BreadthFirst BFS = new(Map, startColumn, endColumn);
int RequiredSteps = BFS.DoSearch();

Console.WriteLine($"Steps required to reach end: {RequiredSteps}");
Console.WriteLine();
