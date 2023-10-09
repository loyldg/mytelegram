// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Mark a specific sponsored message as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// See <a href="https://corefork.telegram.org/method/channels.viewSponsoredMessage" />
///</summary>
internal sealed class ViewSponsoredMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestViewSponsoredMessage, IBool>,
    Channels.IViewSponsoredMessageHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestViewSponsoredMessage obj)
    {
        throw new NotImplementedException();
    }
}
