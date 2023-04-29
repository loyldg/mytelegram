// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class ReorderUsernamesHandler : RpcResultObjectHandler<RequestReorderUsernames, IBool>,
    Channels.IReorderUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReorderUsernames obj)
    {
        throw new NotImplementedException();
    }
}
