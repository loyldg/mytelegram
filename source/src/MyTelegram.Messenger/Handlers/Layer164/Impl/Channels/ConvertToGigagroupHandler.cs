// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Convert a <a href="https://corefork.telegram.org/api/channel">supergroup</a> to a <a href="https://corefork.telegram.org/api/channel">gigagroup</a>, when requested by <a href="https://corefork.telegram.org/api/config#channel-suggestions">channel suggestions</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_ID_INVALID The specified supergroup ID is invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 PARTICIPANTS_TOO_FEW Not enough participants.
/// See <a href="https://corefork.telegram.org/method/channels.convertToGigagroup" />
///</summary>
internal sealed class ConvertToGigagroupHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestConvertToGigagroup, MyTelegram.Schema.IUpdates>,
    Channels.IConvertToGigagroupHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestConvertToGigagroup obj)
    {
        throw new NotImplementedException();
    }
}
