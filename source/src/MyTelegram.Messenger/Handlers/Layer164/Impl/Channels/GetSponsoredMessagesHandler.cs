// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get a list of sponsored messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.getSponsoredMessages" />
///</summary>
internal sealed class GetSponsoredMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetSponsoredMessages, MyTelegram.Schema.Messages.ISponsoredMessages>,
    Channels.IGetSponsoredMessagesHandler
{
    protected override Task<MyTelegram.Schema.Messages.ISponsoredMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetSponsoredMessages obj)
    {
        throw new NotImplementedException();
    }
}
