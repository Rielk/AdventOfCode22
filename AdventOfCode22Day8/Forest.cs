namespace AdventOfCode22Day8;
internal class Forest
{
    public int Width { get; }
    public int Height { get; }

    public Tree[][] TreeArray { get; }

    public Forest(int[][] input)
    {
        Width = input.Length;
        Height = input.First().Length;
        TreeArray = new Tree[Height][];
        foreach ((int[] RowValues, int rowIndex) in input.Select((x, i) => (x, i)))
        {
            TreeArray[rowIndex] = new Tree[Width];
            foreach ((int Value, int columnIndex) in RowValues.Select((x, i) => (x, i)))
            {
                TreeArray[rowIndex][columnIndex] = new(Value);
            }
        }

        foreach (Tree[] row in TreeArray)
        {
            //Look from left
            int canSee = 0;
            foreach (Tree tree in row)
            {
                if (tree.Height >= canSee)
                {
                    tree.SetVisibility(true);
                    canSee = tree.Height + 1;
                    if (canSee >= 10) break;
                }
            }

            //Look from right
            canSee = 0;
            foreach (Tree tree in row.Reverse())
            {
                if (tree.Height >= canSee)
                {
                    tree.SetVisibility(true);
                    canSee = tree.Height + 1;
                    if (canSee >= 10) break;
                }
            }
        }

        for (int i = 0; i < Width; i++)
        {
            List<Tree> columnList = new();
            foreach (Tree[] row in TreeArray)
                columnList.Add(row[i]);
            Tree[] column = columnList.ToArray();

            //Look from top
            int canSee = 0;
            foreach (Tree tree in column)
            {
                if (tree.Height >= canSee)
                {
                    tree.SetVisibility(true);
                    canSee = tree.Height + 1;
                    if (canSee >= 10) break;
                }
            }

            //Look from bottom
            canSee = 0;
            foreach (Tree tree in column.Reverse())
            {
                if (tree.Height >= canSee)
                {
                    tree.SetVisibility(true);
                    canSee = tree.Height + 1;
                    if (canSee >= 10) break;
                }
            }
        }
    }

    internal int CountVisible()
    {
        int count = 0;
        foreach (Tree[] x in TreeArray)
            foreach (Tree y in x)
                if (y.Visible)
                    count++;
        return count;
    }

    internal int FindHighestScenicScore()
    {
        int highestScore = 0;

        for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
            {
                int h = TreeArray[i][j].Height;
                int lView = 0, rView = 0, uView = 0, dView = 0;

                int compare = -1;
                int rIndex = i;
                int cIndex = j;
                while (compare < h && cIndex > 0)
                {
                    cIndex--;
                    compare = TreeArray[rIndex][cIndex].Height;
                    lView++;
                }
                compare = -1;
                rIndex = i;
                cIndex = j;
                while (compare < h && cIndex < Width - 1)
                {
                    cIndex++;
                    compare = TreeArray[rIndex][cIndex].Height;
                    rView++;
                }

                compare = -1;
                rIndex = i;
                cIndex = j;
                while (compare < h && rIndex > 0)
                {
                    rIndex--;
                    compare = TreeArray[rIndex][cIndex].Height;
                    uView++;
                }
                compare = -1;
                rIndex = i;
                cIndex = j;
                while (compare < h && rIndex < Height - 1)
                {
                    rIndex++;
                    compare = TreeArray[rIndex][cIndex].Height;
                    dView++;
                }

                int thisScore = lView * rView * uView * dView;
                if (thisScore > highestScore)
                    highestScore = thisScore;
            }

        return highestScore;
    }
}
