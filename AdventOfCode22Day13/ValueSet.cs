namespace AdventOfCode22Day13;
internal class ValueSet : IComparable<ValueSet>
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

    private ValueSet(int i) { Values = new object[] { i }; }

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

    public static bool? CheckOrder(ValueSet left, ValueSet right)
    {
        int res = left.CompareTo(right);
        if (res == 0)
            return null;
        else if (res == -1)
            return true;
        else if (res == 1)
            return false;
        throw new NotImplementedException();
    }

    public int CompareTo(int other) => CompareTo(new ValueSet(other));
    public int CompareTo(ValueSet? other)
    {
        if (other == null) throw new NotImplementedException();

        int count = Math.Max(Values.Length, other.Values.Length);
        foreach (int i in Enumerable.Range(0, count))
        {
            if (i >= Values.Length) return -1;
            if (i >= other.Values.Length) return 1;

            int res = (Values[i], other.Values[i]) switch
            {
                (ValueSet LeftVS, ValueSet RightVS) => LeftVS.CompareTo(RightVS),
                (ValueSet LeftVS, int RightInt) => LeftVS.CompareTo(RightInt),
                (int LeftInt, ValueSet RightVS) => -1 * RightVS.CompareTo(LeftInt),
                (int LeftInt, int RightInt) => LeftInt.CompareTo(RightInt),
                _ => throw new NotImplementedException()
            };

            if (res != 0) return res;
        }
        return 0;
    }
}
