using System.Text;

namespace AdventOfCode22Day25;
internal static class SNAFU
{
    public static long FromSNAFU(this string input)
    {
        long value = 0;
        long columnValue = 1;
        foreach (char c in input.Reverse())
        {
            value += c switch
            {
                '2' => columnValue * 2,
                '1' => columnValue,
                '0' => 0,
                '-' => -columnValue,
                '=' => -columnValue * 2,
                _ => throw new NotImplementedException()
            };
            columnValue *= 5;
        }
        return value;
    }

    public static string ToSNAFU(this long value)
    {
        if (value < 0) throw new ArgumentException($"Argument {nameof(value)} must be positive", nameof(value));
        if (value == 0) return "0";

        StringBuilder ret = new();
        while (value > 0)
        {
            int n = (int)(value % 5);
            value /= 5;
            if (n > 2) value++;
            char c = n switch
            {
                0 => '0',
                1 => '1',
                2 => '2',
                3 => '=',
                4 => '-',
                _ => throw new Exception()
            };
            ret.Insert(0, c);
        }

        return ret.ToString();
    }
}
