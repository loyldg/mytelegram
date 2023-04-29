// ReSharper disable All

using MyTelegram.Schema.Channels;

namespace MyTelegram.Handlers.Channels;

public class DeactivateAllUsernamesHandler : RpcResultObjectHandler<RequestDeactivateAllUsernames, IBool>,
    Channels.IDeactivateAllUsernamesHandler, IProcessedHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestDeactivateAllUsernames obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
