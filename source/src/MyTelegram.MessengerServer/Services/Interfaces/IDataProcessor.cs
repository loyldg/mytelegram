namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IDataProcessor<in TData>
{
    Task ProcessAsync(TData data);
}