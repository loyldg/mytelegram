// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class GetExtendedMediaHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetExtendedMedia, MyTelegram.Schema.IUpdates>,
    Messages.IGetExtendedMediaHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetExtendedMedia obj)
    {
        throw new NotImplementedException();
    }
}
