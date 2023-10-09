// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete the history of a <a href="https://corefork.telegram.org/api/channel">supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PARICIPANT_MISSING The current user is not in the channel.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHANNEL_TOO_BIG This channel has too many participants (&gt;1000) to be deleted.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// See <a href="https://corefork.telegram.org/method/channels.deleteHistory" />
///</summary>
internal sealed class DeleteHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteHistory, MyTelegram.Schema.IUpdates>,
    Channels.IDeleteHistoryHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteHistory obj)
    {
        throw new NotImplementedException();
    }
}
