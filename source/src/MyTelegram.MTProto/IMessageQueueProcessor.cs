namespace MyTelegram.MTProto;

public interface IMessageQueueProcessor<in TData>
{
    void Enqueue(TData message,
        long key);

    Task ProcessAsync();
}
