//using System.Reflection;
//using System.Text.Json;

//namespace MyTelegram.Domain;

//public class SystemTextJsonSingleValueObjectConverter<T> : JsonConverter<T>
//    where T : ISingleValueObject
//{
//    private static Func<string, T>? _createInstanceFunc;

//    public override bool CanConvert(Type typeToConvert)
//    {
//        return typeof(T).GetTypeInfo().IsAssignableFrom(typeToConvert);
//    }

//    public override T Read(ref Utf8JsonReader reader,
//        Type typeToConvert,
//        JsonSerializerOptions options)
//    {
//        var text = reader.GetString();
//        if (text == null)
//        {
//            throw new ArgumentNullException(nameof(text), "Value can not be null");
//        }

//        _createInstanceFunc ??= ReflectionHelper.CompileConstructor<string, T>();

//        return _createInstanceFunc(text);
//    }

//    public override void Write(Utf8JsonWriter writer,
//        T value,
//        JsonSerializerOptions options)
//    {
//        writer.WriteStringValue(value.GetValue().ToString());
//    }
//}


