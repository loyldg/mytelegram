// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

public class ToggleNoForwardsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleNoForwards, MyTelegram.Schema.IUpdates>,
    Messages.IToggleNoForwardsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleNoForwards obj)
    {
        throw new NotImplementedException();
    }
}
