// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Reorder active usernames
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.reorderUsernames" />
///</summary>
internal sealed class ReorderUsernamesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReorderUsernames, IBool>,
    Channels.IReorderUsernamesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReorderUsernames obj)
    {
        throw new NotImplementedException();
    }
}
