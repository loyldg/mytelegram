// ReSharper disable All

using MyTelegram.Schema.Messages;

namespace MyTelegram.Handlers.Messages;

public class ToggleNoForwardsHandler : RpcResultObjectHandler<RequestToggleNoForwards, Schema.IUpdates>,
    Messages.IToggleNoForwardsHandler
{
    protected override Task<Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        RequestToggleNoForwards obj)
    {
        throw new NotImplementedException();
    }
}
