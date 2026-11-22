using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Schema.Serializer;

public class ObjectSerializer<T> : ISerializer<T>
{
    private const uint VectorConstructorId = 0x1cb5c415;

    // Non-null only when T is TVector<TItem>
    private static readonly Func<IObject>? VectorOfTConstructor = CreateVectorOfTConstructor();

    public void Serialize(T value,
        IBufferWriter<byte> writer)
    {
        if (value is IObject tlObject)
        {
            tlObject.Serialize(writer);
        }
        else
        {
            throw new NotSupportedException($"Only support `IObject`, but input type is `{typeof(T).Name}` ");
        }
    }

    public T Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        // Nested objects are read recursively; limit the depth so a deeply nested payload
        // (e.g. invokeAfterMsg(invokeAfterMsg(...))) can not overflow the stack.
        if (++TlDeserializationDepth.Current > TlDeserializationDepth.MaxDepth)
        {
            TlDeserializationDepth.Current--;
            throw new InvalidDataException($"TL object nesting exceeds {TlDeserializationDepth.MaxDepth} levels");
        }

        try
        {
            return DeserializeCore(ref buffer);
        }
        finally
        {
            TlDeserializationDepth.Current--;
        }
    }

    private static T DeserializeCore(ref ReadOnlyMemory<byte> buffer)
    {
        var constructorId = buffer.ReadUInt32();

        if (SerializerObjectMappings.TryGetTlObject(constructorId, out var func) && func != null)
        {
            var obj = func();
            obj.Deserialize(ref buffer);

            return (T)obj;
        }

        if (constructorId == VectorConstructorId)
        {
            // typeOfT=TVector<TItem>
            if (VectorOfTConstructor != null)
            {
                var vectorObj = VectorOfTConstructor();
                vectorObj.Deserialize(ref buffer);

                return (T)vectorObj;
            }

            // typeOfT=IObject (or another interface), the item type has to be detected from the payload
            if (typeof(T).IsInterface && VectorOfUnknownItemTypeDeserializer.TryDeserialize(ref buffer, out var vector))
            {
                return (T)vector;
            }
        }

        // All supported constructor ids are loaded in memory when first time access SerializerObjectMappings
        // if can not find object type by constructorId, may be telegram client version is mismatch
        throw new NotSupportedException($"Unsupported constructorId:0x{constructorId:x2},type:{typeof(T).Namespace}.{typeof(T).Name}");
    }

    private static Func<IObject>? CreateVectorOfTConstructor()
    {
        var typeOfT = typeof(T);
        if (typeOfT.IsGenericType && typeOfT.GetGenericTypeDefinition() == typeof(TVector<>))
        {
            return MyReflectionHelper.CompileConstructor<IObject>(typeOfT);
        }

        return null;
    }
}

/// <summary>
/// Deserializes a boxed vector that is read as an interface (e.g. rpc_result.result), the item type is not known
/// by the caller and has to be detected from the payload.
/// </summary>
internal static class VectorOfUnknownItemTypeDeserializer
{
    private static readonly Func<IObject> VectorOfInt32Constructor = static () => new TVector<int>();
    private static readonly Func<IObject> VectorOfInt64Constructor = static () => new TVector<long>();
    private static readonly ConcurrentDictionary<uint, Func<IObject>> VectorOfObjectConstructors = new();

    /// <param name="buffer">The vector payload after the vector constructor id: [count][items...]</param>
    /// <param name="vector">The deserialized vector</param>
    public static bool TryDeserialize(ref ReadOnlyMemory<byte> buffer, [NotNullWhen(true)] out IObject? vector)
    {
        vector = null;
        if (buffer.Length < 4)
        {
            return false;
        }

        var objectVectorFunc = GetVectorOfObjectConstructor(buffer.Span);
        var bareVectorFunc = GetVectorOfBareItemConstructor(buffer.Span);

        if (objectVectorFunc != null)
        {
            if (bareVectorFunc == null)
            {
                vector = Deserialize(objectVectorFunc, ref buffer);
                return true;
            }

            // The first bare item may be equal to a known constructor id by accident,
            // use the bare item vector if the items can not be read as TL objects that fill the whole buffer
            var tempBuffer = buffer;
            try
            {
                var objectVector = Deserialize(objectVectorFunc, ref tempBuffer);
                if (tempBuffer.Length == 0)
                {
                    buffer = tempBuffer;
                    vector = objectVector;
                    return true;
                }
            }
            catch (Exception)
            {
                // Ignore, fall back to the bare item vector
            }
        }

        if (bareVectorFunc != null)
        {
            vector = Deserialize(bareVectorFunc, ref buffer);
            return true;
        }

        return false;
    }

    private static IObject Deserialize(Func<IObject> func, ref ReadOnlyMemory<byte> buffer)
    {
        var vector = func();
        vector.Deserialize(ref buffer);

        return vector;
    }

    private static Func<IObject>? GetVectorOfObjectConstructor(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < 8 || BinaryPrimitives.ReadInt32LittleEndian(buffer) <= 0)
        {
            return null;
        }

        var itemConstructorId = BinaryPrimitives.ReadUInt32LittleEndian(buffer[4..]);
        if (VectorOfObjectConstructors.TryGetValue(itemConstructorId, out var func))
        {
            return func;
        }

        if (!SerializerObjectMappings.TryGetTlObjectType(itemConstructorId, out var itemType))
        {
            return null;
        }

        var vectorType = typeof(TVector<>).MakeGenericType(GetItemInterfaceType(itemType));

        return VectorOfObjectConstructors.GetOrAdd(itemConstructorId,
            MyReflectionHelper.CompileConstructor<IObject>(vectorType));
    }

    // Vector<int>/Vector<long>, the item size is detected from the remaining length, so it only works when the vector
    // is the tail of the buffer, which is true when the vector is read as an interface (rpc_result.result, gzip_packed data...)
    private static Func<IObject>? GetVectorOfBareItemConstructor(ReadOnlySpan<byte> buffer)
    {
        var count = BinaryPrimitives.ReadInt32LittleEndian(buffer);
        if (count == 0)
        {
            // An empty vector has the same binary representation whatever the item type is
            return VectorOfInt32Constructor;
        }

        var itemsLength = buffer.Length - 4;
        if (count > 0)
        {
            if (itemsLength == (long)count * sizeof(int))
            {
                return VectorOfInt32Constructor;
            }

            if (itemsLength == (long)count * sizeof(long))
            {
                return VectorOfInt64Constructor;
            }
        }

        return null;
    }

    private static Type GetItemInterfaceType(Type itemType)
    {
        var baseType = typeof(IObject);
        var interfaceType = itemType.GetInterfaces()
            .FirstOrDefault(p => p.IsAssignableTo(baseType) && p != baseType);
        // fix ILayeredXXX interface
        if (interfaceType?.Name.StartsWith("ILayered") ?? false)
        {
            interfaceType = interfaceType.GetInterfaces()
                .FirstOrDefault(p => p.IsAssignableTo(baseType) && p != baseType);
        }

        return interfaceType ?? itemType;
    }
}

public static class TlDeserializationDepth
{
    public const int MaxDepth = 128;

    [ThreadStatic] internal static int Current;
}
