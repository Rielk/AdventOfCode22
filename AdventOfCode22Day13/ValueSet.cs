namespace AdventOfCode22Day13;
internal class ValueSet
{
    public object[] Values { get; }

    private ValueSet(List<object> input)
    {
        if (!input.Any()) { Values = Array.Empty<object>(); return; }

        List<object> GenValues = new();
        List<object> buffer = new();
        foreach (object item in input)
        {
            if (item is char c && c == ',')
                ProcessAndClearBuffer();
            else
                buffer.Add(item);
        }
        ProcessAndClearBuffer();

        Values = GenValues.ToArray();

        void ProcessAndClearBuffer()
        {
            if (buffer.Count == 1 && buffer[0] is ValueSet vs)
                GenValues.Add(vs);
            else
            {
                int result = int.Parse(new string(buffer.Cast<char>().ToArray()));
                GenValues.Add(result);
            }
            buffer.Clear();
        }
    }

    public static ValueSet CreateValueSet(string input)
    {
        var InputState = input.ToCharArray().Cast<object>().ToList();

        while (InputState.Any(x => x is char))
        {
            List<object> buffer = new();
            int bufferStart = -1;
            foreach ((object item, int index) in InputState.Select((x, i) => (x, i)))
            {
                if (item is char c1 && c1 == '[')
                {
                    bufferStart = index;
                    buffer.Clear();
                }
                else if (item is char c2 && c2 == ']')
                {
                    InputState.RemoveRange(bufferStart, buffer.Count + 2);

                    ValueSet replacement = new(buffer);

                    InputState.Insert(bufferStart, replacement);
                    break;
                }
                else
                    buffer.Add(item);
            }
        }

        if (InputState.Count < 1) throw new ArgumentException();

        return (ValueSet)InputState[0];
    }


}
