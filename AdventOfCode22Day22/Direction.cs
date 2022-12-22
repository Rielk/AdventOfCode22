using System.Numerics;

namespace AdventOfCode22Day22;
internal enum Direction
{
    Right, Down, Left, Up
}

internal static class DirectionExtensions
{
    public static int ToScore(this Direction direction) => direction switch
    {
        Direction.Right => 0,
        Direction.Down => 1,
        Direction.Left => 2,
        Direction.Up => 3,
        _ => throw new NotImplementedException(),
    };

    public static void RotateSquare(this Direction direction, Axis3D topIn, Axis3D leftIn, out Axis3D topOut, out Axis3D leftOut)
    {
        topOut = topIn; leftOut = leftIn;
        if (direction == Direction.Up)
            topOut = (-Vector3.Cross(topIn.ToVector3(), leftIn.ToVector3())).ToAxis3D();
        else if (direction == Direction.Down)
            topOut = Vector3.Cross(topIn.ToVector3(), leftIn.ToVector3()).ToAxis3D();
        else if (direction == Direction.Left)
            leftOut = (-Vector3.Cross(topIn.ToVector3(), leftIn.ToVector3())).ToAxis3D();
        else if (direction == Direction.Right)
            leftOut = Vector3.Cross(topIn.ToVector3(), leftIn.ToVector3()).ToAxis3D();
        else
            throw new NotImplementedException();
    }
}
