using System.Text.Json;
using EventFlow.Core.Caching;

namespace MyTelegram.Domain.EventFlow;

public static class SystemTextJsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions AddSingleValueObjects(this JsonSerializerOptions options)
    {
        options.Converters.Add(new SystemTextJsonSingleValueObjectConverter<CacheKey>());
        //options.Converters.Add(new SystemTextJsonSingleValueObjectConverterFactory());
        return options;
    }
}
