// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete all messages sent by a specific participant of a given supergroup
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PARTICIPANT_ID_INVALID The specified participant ID is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.deleteParticipantHistory" />
///</summary>
internal sealed class DeleteParticipantHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteParticipantHistory, MyTelegram.Schema.Messages.IAffectedHistory>,
    Channels.IDeleteParticipantHistoryHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteParticipantHistory obj)
    {
        throw new NotImplementedException();
    }
}
