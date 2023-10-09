namespace MyTelegram.Services.Services;

public interface IDataProcessor<in TData>
{
    Task ProcessAsync(TData data);
}