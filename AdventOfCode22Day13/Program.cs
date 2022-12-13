using AdventOfCode22Day13;
using AdventOfCode22Day13.Properties;

string input = Resources.Input1;

List<ValuePair> ValuePairs = new();

foreach (string pair in input.Split(Environment.NewLine + Environment.NewLine))
{
    string[] pairSplit = pair.Split(Environment.NewLine);
    var value1 = ValueSet.CreateValueSet(pairSplit[0]);
    var value2 = ValueSet.CreateValueSet(pairSplit[1]);
    ValuePairs.Add(new(value1, value2));
}

int i = 1;
int IndexSum = 0;
foreach (ValuePair pair in ValuePairs)
{
    if (ValueSet.CheckOrder(pair.Value1, pair.Value2) != false)
        IndexSum += i;
    i++;
}

Console.WriteLine($"Sum of indices of correct packet pairs: {IndexSum}");
Console.WriteLine();

var Values = ValuePairs.SelectMany(p => new ValueSet[] { p.Value1, p.Value2 }).ToList();
var Divider2 = ValueSet.CreateValueSet("[[2]]");
var Divider6 = ValueSet.CreateValueSet("[[6]]");
Values.Add(Divider2);
Values.Add(Divider6);
Values.Sort();

int IndexOf2 = Values.IndexOf(Divider2) + 1;
int IndexOf6 = Values.IndexOf(Divider6) + 1;
int DecoderKey = IndexOf2 * IndexOf6;

Console.WriteLine($"Decoder Key: {DecoderKey}");