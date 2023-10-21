// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Disable all purchased usernames of a supergroup or channel
/// See <a href="https://corefork.telegram.org/method/channels.deactivateAllUsernames" />
///</summary>
internal sealed class DeactivateAllUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeactivateAllUsernames, IBool>,
    Channels.IDeactivateAllUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeactivateAllUsernames obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
