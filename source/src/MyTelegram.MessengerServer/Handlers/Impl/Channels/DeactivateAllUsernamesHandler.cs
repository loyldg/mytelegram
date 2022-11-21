// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

public class DeactivateAllUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeactivateAllUsernames, IBool>,
    Channels.IDeactivateAllUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeactivateAllUsernames obj)
    {
        throw new NotImplementedException();
    }
}
