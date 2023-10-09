// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Turn a <a href="https://corefork.telegram.org/api/channel#migration">basic group into a supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_TOO_MUCH You have joined too many channels/supergroups.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.migrateChat" />
///</summary>
internal sealed class MigrateChatHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestMigrateChat, MyTelegram.Schema.IUpdates>,
    Messages.IMigrateChatHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestMigrateChat obj)
    {
        throw new NotImplementedException();
    }
}
