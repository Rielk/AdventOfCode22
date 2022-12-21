using AdventOfCode22Day21;
using AdventOfCode22Day21.Properties;

string input = Resources.Input1;

Dictionary<string, Monkey> Monkeys = new();
List<Action<Dictionary<string, Monkey>>> SetupActions = new();
foreach (string line in input.Split(Environment.NewLine))
{
    string[] split1 = line.Split(':');
    string name = split1[0];

    string operation = split1[1];
    if (operation.Contains('+'))
    {
        AdditionMonkey newMonkey = new(name);
        string[] monkeyS = operation.Split("+").Select(s => s.Trim()).ToArray();
        SetupActions.Add(newMonkey.GetSetupAction(monkeyS[0], monkeyS[1]));
        Monkeys.Add(name, newMonkey);
    }
    else if (operation.Contains('-'))
    {
        SubtractionMonkey newMonkey = new(name);
        string[] monkeyS = operation.Split("-").Select(s => s.Trim()).ToArray();
        SetupActions.Add(newMonkey.GetSetupAction(monkeyS[0], monkeyS[1]));
        Monkeys.Add(name, newMonkey);
    }
    else if (operation.Contains('*'))
    {
        MultiplicationMonkey newMonkey = new(name);
        string[] monkeyS = operation.Split("*").Select(s => s.Trim()).ToArray();
        SetupActions.Add(newMonkey.GetSetupAction(monkeyS[0], monkeyS[1]));
        Monkeys.Add(name, newMonkey);
    }
    else if (operation.Contains('/'))
    {
        DivisionMonkey newMonkey = new(name);
        string[] monkeyS = operation.Split("/").Select(s => s.Trim()).ToArray();
        SetupActions.Add(newMonkey.GetSetupAction(monkeyS[0], monkeyS[1]));
        Monkeys.Add(name, newMonkey);
    }
    else if (int.TryParse(operation, out int v))
    {
        ValueMonkey newMonkey = new(name, v);
        Monkeys.Add(name, newMonkey);
    }
    else
    {
        throw new NotImplementedException();
    }
}

foreach (Action<Dictionary<string, Monkey>> action in SetupActions)
    action.Invoke(Monkeys);
SetupActions.Clear();

var RootMonkey = (OperatorMonkey)Monkeys["root"];

Console.WriteLine($"Root monkey will yell: {RootMonkey.Value}");
Console.WriteLine();

var HumanMonkey = (ValueMonkey)Monkeys["humn"];
HumanMonkey.NullifyValue();
RootMonkey.ClearCachedValues();

if (RootMonkey.Monkey1.Value == null)
    RootMonkey.Monkey1.AssertEqual(RootMonkey.Monkey2.Value ?? throw new Exception());
else
    RootMonkey.Monkey2.AssertEqual(RootMonkey.Monkey1.Value ?? throw new Exception());
Console.WriteLine($"Human must yell: {HumanMonkey.Value}");
