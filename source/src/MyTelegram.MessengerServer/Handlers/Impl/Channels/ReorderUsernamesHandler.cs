// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class ReorderUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReorderUsernames, IBool>,
    Channels.IReorderUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReorderUsernames obj)
    {
        throw new NotImplementedException();
    }
}
