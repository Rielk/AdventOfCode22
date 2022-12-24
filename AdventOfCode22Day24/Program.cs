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

Location StartLoc = new(startColumn, -1);
Location EndLoc = new(endColumn, height);

Map Map = new(charArray);

BreadthFirst BFS = new(Map);
int RequiredSteps = BFS.DoSearch(StartLoc, EndLoc);

Console.WriteLine($"Steps required to reach end: {RequiredSteps}");
Console.WriteLine();

RequiredSteps += BFS.DoSearch(EndLoc, StartLoc);
RequiredSteps += BFS.DoSearch(StartLoc, EndLoc);

Console.WriteLine($"Steps required to reach end, go back and reach end again: {RequiredSteps}");
