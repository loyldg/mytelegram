namespace MyTelegram.Abstractions;

public interface IShardedMessageQueueProcessor<in TData>
{
    void Enqueue(TData data, long key);
    void Enqueue(TData data, string key);
    Task ProcessAsync(CancellationToken cancellationToken = default);
}