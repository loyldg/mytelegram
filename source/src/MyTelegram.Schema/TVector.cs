// ReSharper disable All

namespace MyTelegram.Schema;

[TlObject(0x1cb5c415)]
public class TVector<T> : IObject, IList<T> //where T : IObject
{
    private readonly List<T> _list;

    public TVector()
    {
        _list = new List<T>();
    }

    public TVector(IEnumerable<T> collection)
    {
        _list = new List<T>(collection);
    }

    public TVector(params T[] items)
    {
        _list = new List<T>(items);
    }

    public uint ConstructorId => 0x1cb5c415;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(_list.Count);
        var serializer = SerializerFactory.CreateSerializer<T>();
        foreach (var item in _list)
        {
            serializer.Serialize(item, writer);
        }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        //throw new NotImplementedException();
        if (reader.TryReadLittleEndian(out int count))
        {
            if (count > 0)
            {
                var serializer = SerializerFactory.CreateSerializer<T>();
                for (int i = 0; i < count; i++)
                {
                    var item = serializer.Deserialize(ref reader);
                    _list.Add(item);
                }
            }
        }
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    bw.Write(ConstructorId);
    //    bw.Write(_list.Count);
    //    var serializer = SerializerFactory.CreateSerializer<T>();

    //    foreach (var item in _list)
    //    {
    //        serializer.Serialize(item, bw);
    //    }
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    var count = br.ReadInt32();
    //    if (count > 0)
    //    {
    //        var serializer = SerializerFactory.CreateSerializer<T>();

    //        for (int i = 0; i < count; i++)
    //        {
    //            var item = serializer.Deserialize(br);
    //            _list.Add(item);
    //        }
    //    }
    //}

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
    public bool IsReadOnly => ((IList<T>)_list).IsReadOnly;

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
}