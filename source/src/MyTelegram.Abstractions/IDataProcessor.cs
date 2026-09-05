namespace MyTelegram.Abstractions;

public interface IDataProcessor<in TData>
{
    Task ProcessAsync(TData data, CancellationToken cancellationToken = default);
}