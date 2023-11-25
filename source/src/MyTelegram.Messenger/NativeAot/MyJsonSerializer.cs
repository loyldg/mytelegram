//using JsonSerializer = System.Text.Json.JsonSerializer;

//namespace MyTelegram.Messenger.NativeAot
//{
//    public class MyJsonSerializer : IJsonSerializer
//    {
//        private readonly IJsonContextProvider _jsonContextProvider;

//        public MyJsonSerializer(IJsonContextProvider jsonContextProvider)
//        {
//            _jsonContextProvider = jsonContextProvider;
//        }

//        public string Serialize(object obj,
//            bool indented = false)
//        {
//            return JsonSerializer.Serialize(obj, obj.GetType().UnderlyingSystemType, _jsonContextProvider.GetSerializerContext());
//        }

//        public object Deserialize(string json,
//            Type type)
//        {
//            return JsonSerializer.Deserialize(json, type, _jsonContextProvider.GetSerializerContext());
//        }

//        public T Deserialize<T>(string json)
//        {
//            //var utf8Bytes = Encoding.UTF8.GetBytes(json);
//            return (T)JsonSerializer.Deserialize(json, typeof(T), _jsonContextProvider.GetSerializerContext());
//            //return JsonSerializer.Deserialize<T>(utf8Bytes, _jsonContextProvider.GetSerializerContext());
//        }
//    }
//}
