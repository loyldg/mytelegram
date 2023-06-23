using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class BlockHandler : RpcResultObjectHandler<RequestBlock, IBool>,
    IBlockHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestBlock obj)
    {
        throw new NotImplementedException();
    }
}