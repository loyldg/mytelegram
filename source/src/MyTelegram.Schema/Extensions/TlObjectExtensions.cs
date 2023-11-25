using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MyTelegram.Schema.Serializer;

namespace MyTelegram.Schema.Extensions;

public sealed class ArrayPoolBufferWriterWrapper<T> : IDisposable
{
    private readonly ArrayBufferWriter<T> _writer;

    public ArrayPoolBufferWriterWrapper(ArrayBufferWriter<T> writer)
    {
        _writer = writer;
    }

    public ArrayBufferWriter<T> Writer => _writer;

    public int WrittenCount => _writer.WrittenCount;

    public void Dispose()
    {
        _writer.Clear();
        ArrayBufferWriterPool<T>.Return(this);
    }
}
public class ArrayBufferWriterPool : ArrayBufferWriterPool<byte> { }

public class ArrayBufferWriterPool<T>
{
    private static readonly ConcurrentQueue<ArrayPoolBufferWriterWrapper<T>> _queue = new();

    public static ArrayPoolBufferWriterWrapper<T> Rent(int initialCapacity = 1024)
    {
        if (_queue.TryDequeue(out var writer))
        {
            return writer;
        }
        return new ArrayPoolBufferWriterWrapper<T>(new ArrayBufferWriter<T>(1024));
    }

    public static void Return(ArrayPoolBufferWriterWrapper<T> writerWrapper)
    {
        writerWrapper.Writer.Clear();
        _queue.Enqueue(writerWrapper);
    }
}

public static class TlObjectExtensions
{
    //private static readonly BytesSerializer BytesSerializer = new();
    //[return: NotNullIfNotNull("obj")]
    //public static byte[]? ToBytes(this IObject? obj)
    //{
    //    if (obj == null)
    //    {
    //        return null;
    //    }

    //    var stream = new MemoryStream();
    //    var bw = new BinaryWriter(stream);
    //    obj.Serialize(bw);

    //    return stream.ToArray();
    //}

    [return:NotNullIfNotNull(nameof(inputReplyTo))]
    public static int? ToReplyToMsgId(this IInputReplyTo? inputReplyTo)
    {
        switch (inputReplyTo)
        {
            case TInputReplyToMessage inputReplyToMessage:
                return inputReplyToMessage.ReplyToMsgId;
            case TInputReplyToStory inputReplyToStory:
                return inputReplyToStory.StoryId;
        }

        return null;
    }

    public static int GetLength(this IObject? obj)
    {
        if (obj == null)
        {
            return 0;
        }

        var writer = ArrayBufferWriterPool.Rent();
        int count = 0;
        try
        {
            obj.Serialize(writer.Writer);
            count = writer.WrittenCount;
        }
        finally
        {
            ArrayBufferWriterPool.Return(writer);
        }

        return count;
    }

    [return: NotNullIfNotNull("obj")]
    public static byte[]? ToBytes(this IObject? obj)
    {
        if (obj == null)
        {
            return null;
        }
        var writer = ArrayBufferWriterPool.Rent();

        try
        {
            obj.Serialize(writer.Writer);
            var bytes = writer.Writer.WrittenSpan.ToArray();

            return bytes;
        }
        finally
        {
            ArrayBufferWriterPool.Return(writer);
        }

        //throw new NotImplementedException();
        //if (obj == null)
        //{
        //    return null;
        //}

        //var stream = new MemoryStream();
        //var bw = new BinaryWriter(stream);
        //obj.Serialize(bw);

        //return stream.ToArray();
    }

    //public static ReadOnlyMemory<byte> ToReadonlyMemory(this IObject? obj)
    //{
    //    if (obj == null)
    //    {
    //        return ReadOnlyMemory<byte>.Empty;
    //    }

    //}

    //public static byte[]? ToBytes(this IObject? obj)
    //{
    //    if (obj == null)
    //    {
    //        return null;
    //    }

    //    using var writer = ArrayBufferWriterPool.Rent();
    //    obj.Serialize(writer);
    //    return writer.Writer.WrittenSpan.ToArray();
    //}

    //public static TObject? ToTObject<TObject>(this ReadOnlyMemory<byte> memory) where TObject : IObject
    //{
    //    if (memory.IsEmpty)
    //    {
    //        return default;
    //    }
    //    var serializer = SerializerFactory.CreateObjectSerializer<TObject>();
    //    var buffer = new ReadOnlySequence<byte>(memory);
    //    return serializer.Deserialize(ref buffer);
    //}


    [return: NotNullIfNotNull("readOnlyMemory")]
    public static TObject? ToTObject<TObject>(this ReadOnlyMemory<byte>? readOnlyMemory) where TObject : IObject
    {
        if (readOnlyMemory?.Length > 0)
        {
            var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(readOnlyMemory.Value));
            return reader.Read<TObject>();
        }

        return default;
    }


    [return: NotNullIfNotNull("bytes")]
    public static TObject? ToTObject<TObject>(this byte[]? bytes) where TObject : IObject
    {
        return ToTObject<TObject>(readOnlyMemory: bytes);
    }

    public static TObject ToTObject<TObject>(this Memory<byte> memory) where TObject : IObject
    {
        return ToTObject<TObject>(readOnlyMemory: memory);
    }

    //public static void Serialize<T>(this IBufferWriter<byte> writer,
    //    T value)
    //{
    //    SerializerFactory.CreateSerializer<T>().Serialize(value, writer);
    //}

    //public static T Deserialize<T>(this ref ReadOnlySequence<byte> buffer)
    //{
    //    return SerializerFactory.CreateSerializer<T>().Deserialize(ref buffer);
    //}

    //public static void Serialize<T>(this T value, ArrayPoolBufferWriterWrapper<byte> writerWrapper) where T : IObject
    //{
    //    value.Serialize(writerWrapper.Writer);
    //}

    //public static void Serialize<T>(this ArrayPoolBufferWriterWrapper<byte> writerWrapper,
    //    T value)
    //{
    //    writerWrapper.Writer.Serialize(value);
    //}

    //public static void Serialize<T>(this BinaryWriter writer, T value)
    //{
    //    SerializerFactory.CreateSerializer<T>().Serialize(value, writer);
    //}

    //public static T Deserialize<T>(this BinaryReader reader)
    //{
    //    return SerializerFactory.CreateSerializer<T>().Deserialize(reader);
    //}
}