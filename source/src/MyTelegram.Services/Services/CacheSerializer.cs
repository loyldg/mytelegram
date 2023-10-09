using System.Text;
using EventFlow.Core;
using MyTelegram.Core;

namespace MyTelegram.Services.Services;

public class CacheSerializer : ICacheSerializer
{
    private readonly IJsonSerializer _jsonSerializer;

    public CacheSerializer(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public byte[] Serialize<T>(T obj)
    {
        return Encoding.UTF8.GetBytes(_jsonSerializer.Serialize(obj));
    }

    public T Deserialize<T>(byte[] bytes)
    {
        return _jsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes));
    }
}
