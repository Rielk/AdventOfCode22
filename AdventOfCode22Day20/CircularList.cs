using System.Collections;
using System.Collections.ObjectModel;

namespace AdventOfCode22Day20;
internal class CircularList<T> : IEnumerable<T>, ICollection<T>
{
    public ReadOnlyCollection<T> Values { get; }
    private List<T> List { get; }
    public int Count => List.Count;

    public bool IsReadOnly => false;

    public CircularList()
    {
        List = new();
        Values = new(List);
    }

    public T this[int index]
    {
        get => List[index % Count];
        set => List[index % Count] = value;
    }

    public IEnumerator<T> GetEnumerator() => List.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();

    public void Add(T item) => List.Add(item);
    public void Clear() => List.Clear();
    public bool Contains(T item) => List.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);
    public bool Remove(T item) => List.Remove(item);
    public int IndexOf(T item) => List.IndexOf(item);

    public void MoveTo(int startIndex, int endIndex)
    {
        T cahce = List[startIndex % Count];
        List.RemoveAt(startIndex % Count);
        List.Insert(endIndex, cahce);
    }

    public void MoveBy(int index, int amount)
    {
        if (index < 0 || index >= Count) throw new Exception();

        int CountM1 = Count - 1;
        amount %= CountM1;
        int newPosition = index + amount;
        newPosition %= CountM1;
        newPosition = newPosition == 0 ? CountM1 : newPosition;
        if (newPosition < 0)
            newPosition = CountM1 + newPosition;

        T cache = List[index % Count];
        List.RemoveAt(index % Count);
        List.Insert(newPosition, cache);
    }
}
