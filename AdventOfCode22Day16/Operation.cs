namespace AdventOfCode22Day16;

internal record Operation(OperationType OperationType, Valve Valve) { }

internal enum OperationType
{
    MoveTo, Open
}
