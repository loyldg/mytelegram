namespace MyTelegram.MTProto;

public interface IDataProcessor<in TData>
{
    Task ProcessAsync(TData data);
}
