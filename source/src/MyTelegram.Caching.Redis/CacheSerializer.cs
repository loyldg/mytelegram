using MyTelegram.Core;

namespace MyTelegram.Caching.Redis;

public class CacheSerializer : ICacheSerializer
{
    //private readonly IJsonSerializer _jsonSerializer;

    //public CacheSerializer(IJsonSerializer jsonSerializer)
    //{
    //    _jsonSerializer = jsonSerializer;
    //}

    public byte[] Serialize<T>(T obj)
    {
        //return Encoding.UTF8.GetBytes(_jsonSerializer.Serialize(obj));
        return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(obj);
    }

    public T? Deserialize<T>(byte[] bytes)
    {
        //return _jsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes));
        return System.Text.Json.JsonSerializer.Deserialize<T>(bytes);
    }
}