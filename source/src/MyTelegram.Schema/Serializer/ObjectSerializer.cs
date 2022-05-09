namespace MyTelegram.Schema.Serializer;

public class ObjectSerializer<T> : ISerializer<T> //where T : IObject
{
    public void Serialize(T value,
        BinaryWriter writer)
    {
        if (value is IObject tlObject)
        {
            tlObject.Serialize(writer);
        } else
        {
            throw new NotSupportedException($"Only support `IObject`,but input type is `{typeof(T).Name}` ");
        }
    }

    public T Deserialize(BinaryReader reader)
    {
        var constructorId = reader.ReadUInt32();
        if (!SerializerObjectMappings.TryGetTlObject(constructorId, out var func))
        {
            SerializerObjectMappings.TryGetTlObject(typeof(T), out func);
        }

        if (func != null)
        {
            var obj = func();
            obj.Deserialize(reader);

            return (T)obj;
        }

        //if (typeof(T).IsInterface)
        //{

        //}

        // TVector<T>
        var vectorObjFunc = MyReflectionHelper.CompileConstructor<IObject>(typeof(T));
        SerializerObjectMappings.TryAddTlObjectFuncToCache(typeof(T), vectorObjFunc);

        var vectorObj = vectorObjFunc();
        vectorObj.Deserialize(reader);

        return (T)vectorObj;
    }
}