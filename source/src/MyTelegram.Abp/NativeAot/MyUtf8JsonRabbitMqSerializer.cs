//namespace MyTelegram.Abp.NativeAot;

//public class MyUtf8JsonRabbitMqSerializer : IRabbitMqSerializer
//{
//    public byte[] Serialize(object obj)
//    {
//        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj, obj.GetType(), MyJsonContext.Default));
//    }

//    public object? Deserialize(byte[] value,
//        Type type)
//    {
//        return JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), type, MyJsonContext.Default);
//    }

//    public T? Deserialize<T>(byte[] value)
//    {
//        return (T?)JsonSerializer.Deserialize(Encoding.UTF8.GetString(value), typeof(T), MyJsonContext.Default);
//    }
//}