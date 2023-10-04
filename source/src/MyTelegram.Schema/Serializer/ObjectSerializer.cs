using System.Buffers.Binary;

namespace MyTelegram.Schema.Serializer;

public class ObjectSerializer<T> : ISerializer<T>//, ISerializer2<T> //where T : IObject
{
    private const uint VectorConstructorId = 0x1cb5c415;
    //public void Serialize(T value,
    //    BinaryWriter writer)
    //{
    //    if (value is IObject tlObject)
    //    {
    //        tlObject.Serialize(writer);
    //    }
    //    else
    //    {
    //        throw new NotSupportedException($"Only support `IObject`,but input type is `{typeof(T).Name}` ");
    //    }
    //}

    //public T Deserialize(BinaryReader reader)
    //{
    //    var constructorId = reader.ReadUInt32();
    //    if (!SerializerObjectMappings.TryGetTlObject(constructorId, out var func))
    //    {
    //        SerializerObjectMappings.TryGetTlObject(typeof(T), out func);
    //    }

    //    if (func != null)
    //    {
    //        var obj = func();
    //        obj.Deserialize(reader);

    //        return (T)obj;
    //    }

    //    if (typeof(T).IsInterface)
    //    {
    //        if (constructorId != VectorConstructorId)
    //        {
    //            // All supported constructor ids are loaded in memory when first time access SerializerObjectMappings
    //            // if can not find object type by constructorId,may be telegram client version is mismatch  
    //            throw new NotSupportedException($"Unsupported constructorId:0x{constructorId:x2}");
    //        }
    //    }

    //    // TVector<T> 
    //    var vectorObjFunc = MyReflectionHelper.CompileConstructor<IObject>(typeof(T));
    //    SerializerObjectMappings.TryAddTlObjectFuncToCache(typeof(T), vectorObjFunc);

    //    var vectorObj = vectorObjFunc();
    //    vectorObj.Deserialize(reader);

    //    return (T)vectorObj;
    //}

    public void Serialize(T value,
        IBufferWriter<byte> writer)
    {
        if (value is IObject tlObject)
        {
            tlObject.Serialize(writer);
        }
        else
        {
            throw new NotSupportedException($"Only support `IObject`,but input type is `{typeof(T).Name}` ");
        }
    }

    public T Deserialize(ref SequenceReader<byte> reader)
    {
        var constructorId = reader.ReadUInt32();
        if (!SerializerObjectMappings.TryGetTlObject(constructorId, out var func))
        {
            SerializerObjectMappings.TryGetTlObject(typeof(T), out func);
        }

        if (func != null)
        {
            var obj = func();
            obj.Deserialize(ref reader);

            return (T)obj;
        }

        if (typeof(T).IsInterface)
        {
            if (constructorId != VectorConstructorId)
            {
                // All supported constructor ids are loaded in memory when first time access SerializerObjectMappings
                // if can not find object type by constructorId,may be telegram client version is mismatch  
                throw new NotSupportedException($"Unsupported constructorId:0x{constructorId:x2}");
            }
        }

        // TVector<T> 
        var vectorObjFunc = MyReflectionHelper.CompileConstructor<IObject>(typeof(T));
        SerializerObjectMappings.TryAddTlObjectFuncToCache(typeof(T), vectorObjFunc);

        var vectorObj = vectorObjFunc();
        vectorObj.Deserialize(ref reader);

        return (T)vectorObj;
    }

    //public T Deserialize(ref ReadOnlySequence<byte> buffer)
    //{
    //    throw new NotImplementedException();
    //    //var reader = new SequenceReader<byte>(buffer);
    //    //var constructorIdBytes = new byte[4];
    //    //reader.TryCopyTo(constructorIdBytes);
    //    //var constructorId = BinaryPrimitives.ReadUInt32LittleEndian(constructorIdBytes);

    //    ////if (!BinaryPrimitives.TryReadUInt32LittleEndian(reader.CurrentSpan, out var constructorId))
    //    ////{
    //    ////    throw new ArgumentException("Read constructorId from buffer failed");
    //    ////}

    //    //buffer = buffer.Slice(4);
    //    ////var constructorId = reader.ReadUInt32();
    //    //if (!SerializerObjectMappings.TryGetTlObject(constructorId, out var func))
    //    //{
    //    //    SerializerObjectMappings.TryGetTlObject(typeof(T), out func);
    //    //}

    //    //if (func != null)
    //    //{
    //    //    var obj = func();
    //    //    obj.Deserialize(ref buffer);

    //    //    return (T)obj;
    //    //}

    //    //if (typeof(T).IsInterface)
    //    //{
    //    //    if (constructorId != VectorConstructorId)
    //    //    {
    //    //        // All supported constructor ids are loaded in memory when first time access SerializerObjectMappings
    //    //        // if can not find object type by constructorId,may be telegram client version is mismatch
    //    //        throw new NotSupportedException($"Unsupported constructorId:0x{constructorId:x2}");
    //    //    }
    //    //}

    //    //// TVector<T> 
    //    //var vectorObjFunc = MyReflectionHelper.CompileConstructor<IObject>(typeof(T));
    //    //SerializerObjectMappings.TryAddTlObjectFuncToCache(typeof(T), vectorObjFunc);

    //    //var vectorObj = vectorObjFunc();
    //    //vectorObj.Deserialize(ref buffer);

    //    //return (T)vectorObj;
    //}
}