using MyTelegram.Handlers.Contacts;
using MyTelegram.Schema.Contacts;

namespace MyTelegram.MessengerServer.Handlers.Impl.Contacts;

public class DeleteByPhonesHandler : RpcResultObjectHandler<RequestDeleteByPhones, IBool>,
    IDeleteByPhonesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeleteByPhones obj)
    {
        throw new NotImplementedException();
    }
}
