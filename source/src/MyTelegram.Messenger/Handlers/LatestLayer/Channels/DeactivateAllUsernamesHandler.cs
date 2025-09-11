namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Disable all purchased usernames of a supergroup or channel
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.deactivateAllUsernames" />
///</summary>
internal sealed class DeactivateAllUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeactivateAllUsernames, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeactivateAllUsernames obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
