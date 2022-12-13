using AdventOfCode22Day13;
using AdventOfCode22Day13.Properties;

string input = Resources.Input1;

List<ValuePair> Values = new();

foreach (string pair in input.Split(Environment.NewLine + Environment.NewLine))
{
    string[] pairSplit = pair.Split(Environment.NewLine);
    var value1 = ValueSet.CreateValueSet(pairSplit[0]);
    var value2 = ValueSet.CreateValueSet(pairSplit[1]);
    Values.Add(new(value1, value2));
}

int i = 1;
int IndexSum = 0;
foreach (ValuePair pair in Values)
{
    if (ValueSet.CheckOrder(pair.Value1, pair.Value2) != false)
        IndexSum += i;
    i++;
}

Console.WriteLine($"Sum of indices of correct packet pairs: {IndexSum}");

internal record ValuePair(ValueSet Value1, ValueSet Value2) { }