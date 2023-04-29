using System.Text.Json.Serialization;

namespace MyTelegram.EventBus.RabbitMQ;

public interface IJsonContextProvider
{
    JsonSerializerContext GetSerializerContext();
}
