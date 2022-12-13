using AdventOfCode22Day13;
using AdventOfCode22Day13.Properties;

string input = Resources.InputTest;

List<ValuePair> Values = new();

foreach (string pair in input.Split(Environment.NewLine + Environment.NewLine))
{
    string[] pairSplit = pair.Split(Environment.NewLine);
    var value1 = ValueSet.CreateValueSet(pairSplit[0]);
    var value2 = ValueSet.CreateValueSet(pairSplit[1]);
    Values.Add(new(value1, value2));
}

Console.WriteLine();

internal record ValuePair(ValueSet Value1, ValueSet Value2) { }