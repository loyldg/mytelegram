using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyTelegram.Services.NativeAot;

public static class SystemTextJsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions AddSingleValueObjects(this JsonSerializerOptions options,
        JsonConverter converter)
    {
        //options.Converters.Add(new SystemTextJsonSingleValueObjectConverterFactory());
        options.Converters.Add(converter);

        return options;
    }
}