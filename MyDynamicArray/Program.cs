using System.Runtime.CompilerServices;
using System.Collections;
using System.Numerics;

T ListElementsSum<T>(CustomList<T> numbers) where T : INumber<T>
{
    T res = T.Zero;
    foreach (T item in numbers)
    {
        res += item;
    }
    return res;
}
class CustomList<T> : IEnumerable<T>
{
    private T[] memory = new T[1];
    private int count = 0;
    public int Count => count;
    public void Add(T item)
    {
        if(count == memory.Length)
        {
            T[] temp = new T[memory.Length * 2];
            for(int i = 0; i < memory.Length; i++)
            {
                temp[i] = memory[i];
            }
            memory = temp;
        }
        memory[count] = item;
        count++;
    }
    public void RemoveAt(int index)
    {
        if(index < 0 || index >= count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range");
        }
        for(int i = index; i < count-1; i++)
        {
            memory[i] = memory[i + 1];
        }
        memory[count] = default;
        count--;
    }
    public IEnumerator<T> GetEnumerator()
    {
        for(int i=0; i<count; i++)
        {
            yield return memory[i];
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    public T this[int index]
    {
        get
        {
            if(index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range");
            }
            return memory[index];
        }
        set
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index was out of range");
            }
            memory[index] = value;
        }
    }
    public bool Contains(T item)
    {
        for(int i = 0; i < count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(item, memory[i]))
            {
                return true;
            }
        }
        return false;
    }
    public CustomList<T> Filter(Func<T, bool> condition)
    {
        CustomList<T> filteredList = new CustomList<T>();
        for(int i = 0; i < count; i++)
        {
            if (condition(memory[i]))
            {
                filteredList.Add(memory[i]);
            }
        }
        return filteredList;
    }
    public CustomList<T> Reverse()
    {
        CustomList<T> reversedList = new CustomList<T>();
        for(int i = count - 1; i >= 0; i--)
        {
            reversedList.Add(memory[i]);
        }
        return reversedList;
    }
}

