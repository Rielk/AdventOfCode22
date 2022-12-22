using System.Numerics;

namespace AdventOfCode22Day22;
internal enum Axis3D
{
    Up, Down, Left, Right, Front, Back
}

internal static class Axis3DExtensions
{
    public static Vector3 ToVector3(this Axis3D axis) => axis switch
    {
        Axis3D.Up => new(1, 0, 0),
        Axis3D.Down => new(-1, 0, 0),
        Axis3D.Left => new(0, 1, 0),
        Axis3D.Right => new(0, -1, 0),
        Axis3D.Front => new(0, 0, 1),
        Axis3D.Back => new(0, 0, -1),
        _ => throw new NotImplementedException(),
    };

    public static Axis3D ToAxis3D(this Vector3 vector) => (vector.X, vector.Y, vector.Z) switch
    {
        (1, 0, 0) => Axis3D.Up,
        (-1, 0, 0) => Axis3D.Down,
        (0, 1, 0) => Axis3D.Left,
        (0, -1, 0) => Axis3D.Right,
        (0, 0, 1) => Axis3D.Front,
        (0, 0, -1) => Axis3D.Back,
        _ => throw new NotImplementedException(),
    };
}
