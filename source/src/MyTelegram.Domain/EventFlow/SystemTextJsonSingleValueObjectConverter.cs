using System.Reflection;
using System.Text.Json;

namespace MyTelegram.Domain.EventFlow;

public class SystemTextJsonSingleValueObjectConverter<T> : JsonConverter<T>
    where T : ISingleValueObject
{
    private static Func<string, T>? _createInstanceFunc;

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(T).GetTypeInfo().IsAssignableFrom(typeToConvert);
    }

    public override T Read(ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var text = reader.GetString();
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text), "Value can not be null");
        }

        _createInstanceFunc ??= MyReflectionHelper.CompileConstructor<string, T>();

        return _createInstanceFunc(text);
    }

    public override void Write(Utf8JsonWriter writer,
        T value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetValue().ToString());
    }
}

//public class SystemTextJsonSingleValueObjectConverterFactory : JsonConverterFactory
//{
//    private static readonly Type ConverterGenericType = typeof(SystemTextJsonSingleValueObjectConverter<>);
//    private static readonly ConcurrentDictionary<Type, JsonConverter> Converters = new();

//    public override bool CanConvert(Type typeToConvert)
//    {
//        return typeof(ISingleValueObject).GetTypeInfo().IsAssignableFrom(typeToConvert);
//    }

//    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
//    {
//        var converter = Converters.GetOrAdd(
//            typeToConvert,
//            _ => {
//                var constructedType = ConverterGenericType.MakeGenericType(typeToConvert.GetGenericArguments());
//                return Activator.CreateInstance(constructedType) as JsonConverter;
//            });

//        return converter;
//    }
//}
