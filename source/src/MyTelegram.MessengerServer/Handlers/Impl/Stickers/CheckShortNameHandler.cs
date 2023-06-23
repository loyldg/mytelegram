using MyTelegram.Handlers.Stickers;
using MyTelegram.Schema.Stickers;

namespace MyTelegram.MessengerServer.Handlers.Impl.Stickers;

public class CheckShortNameHandler : RpcResultObjectHandler<RequestCheckShortName, IBool>,
    ICheckShortNameHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestCheckShortName obj)
    {
        throw new NotImplementedException();
    }
}