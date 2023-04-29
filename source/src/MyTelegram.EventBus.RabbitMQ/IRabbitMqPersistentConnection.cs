namespace MyTelegram.EventBus.RabbitMQ;

// taken from https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/BuildingBlocks/EventBus/EventBusRabbitMQ/IRabbitMQPersistentConnection.cs
public interface IRabbitMqPersistentConnection
    : IDisposable
{
    bool IsConnected { get; }

    IModel CreateModel();

    bool TryConnect();
}
