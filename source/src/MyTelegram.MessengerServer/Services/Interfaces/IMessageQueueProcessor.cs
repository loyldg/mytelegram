namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IMessageQueueProcessor<in TData>
{
    void Enqueue(TData message,
        long key);

    Task ProcessAsync();
}
