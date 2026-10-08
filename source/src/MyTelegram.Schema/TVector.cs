namespace MyTelegram.Schema;

[TlObject(0x1cb5c415)]
public class TVector<T> : IObject, IList<T>
{
    private static readonly ISerializer<T> Serializer = SerializerFactory.CreateSerializer<T>();
    private readonly List<T> _list;

    public TVector()
    {
        _list = [];
    }
    public TVector(int count)
    {
        _list = new List<T>(count);
    }

    public TVector(IEnumerable<T> collection)
    {
        _list = new List<T>(collection);
    }

    public TVector(params T[] items)
    {
        _list = new List<T>(items);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T item)
    {
        _list.Add(item);
    }

    public void Clear()
    {
        _list.Clear();
    }

    public bool Contains(T item)
    {
        return _list.Contains(item);
    }

    public void CopyTo(T[] array,
        int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    public bool Remove(T item)
    {
        return _list.Remove(item);
    }

    public int Count => _list.Count;
    public bool IsReadOnly => false;

    public int IndexOf(T item)
    {
        return _list.IndexOf(item);
    }

    public void Insert(int index,
        T item)
    {
        _list.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        _list.RemoveAt(index);
    }

    public T this[int index]
    {
        get => _list[index];
        set => _list[index] = value;
    }

    public uint ConstructorId => 0x1cb5c415;

    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(_list.Count);
        foreach (var item in _list)
        {
            Serializer.Serialize(item, writer);
        }
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        var tempBuffer = buffer;

        var count = buffer.ReadInt32();
        if (count > 0)
        {
            _list.Capacity = count;
            for (var i = 0; i < count; i++)
            {
                try
                {
                    var item = Serializer.Deserialize(ref buffer);
                    _list.Add(item);
                }
                catch
                {
                    Console.WriteLine($"Vector:Deserialize failed:{Convert.ToHexStringLower(tempBuffer.Span)}");
                    throw;
                }
            }
        }
    }

    public int GetLength()
    {
        int length = 4 + 4; // ConstructorId + Count
        foreach (var item in _list)
        {
            switch (item)
            {
                case IObject obj:
                    length += obj.GetLength();
                    break;
                case int or uint:
                    length += 4;
                    break;
                case long or double:
                    length += 8;
                    break;
                case string stringItem:
                    length += stringItem.GetTLLength();
                    break;
                case byte[] bytesItem:
                    length += bytesItem.GetTLLength();
                    break;
                case ReadOnlyMemory<byte> readOnlyMemoryItem:
                    length += readOnlyMemoryItem.GetTLLength();
                    break;
            }
        }
        return length;
    }

    public void AddRange(IEnumerable<T> items)
    {
        _list.AddRange(items);
    }
}