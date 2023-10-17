namespace AdventOfCode22Day24;
internal class Blizzard
{
	private bool MovedThisRound { get; set; } = false;

	public Blizzard(Direction direction)
	{
		Direction = direction;
	}

	public Direction Direction { get; }

	public bool Step(List<Blizzard>[,] blizzards, Location currentLocation)
	{
		if (MovedThisRound) return false;

		_ = blizzards[currentLocation.x, currentLocation.y].Remove(this);

		Location newLocation = Direction switch
		{
			Direction.Up => currentLocation.Offset(0, -1),
			Direction.Down => currentLocation.Offset(0, 1),
			Direction.Left => currentLocation.Offset(-1, 0),
			Direction.Right => currentLocation.Offset(1, 0),
			_ => throw new NotImplementedException(),
		};

		int width = blizzards.GetLength(0);
		int height = blizzards.GetLength(1);

		if (newLocation.x < 0) newLocation = new(width - 1, newLocation.y);
		else if (newLocation.x >= width) newLocation = new(0, newLocation.y);
		else if (newLocation.y < 0) newLocation = new(newLocation.x, height - 1);
		else if (newLocation.y >= height) newLocation = new(newLocation.x, 0);

		blizzards[newLocation.x, newLocation.y].Add(this);

		MovedThisRound = true;
		return true;
	}

	public void NewRound()
	{
		MovedThisRound = false;
	}
}
