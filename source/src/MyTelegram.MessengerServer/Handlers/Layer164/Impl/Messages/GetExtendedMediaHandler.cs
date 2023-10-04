// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class GetExtendedMediaHandler : RpcResultObjectHandler<RequestGetExtendedMedia, Schema.IUpdates>,
    Messages.IGetExtendedMediaHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetExtendedMedia obj)
    {
        throw new NotImplementedException();
    }
}