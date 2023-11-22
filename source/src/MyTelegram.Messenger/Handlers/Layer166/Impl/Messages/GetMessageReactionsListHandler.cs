// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/reactions">message reaction</a> list, along with the sender of each reaction.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 BROADCAST_FORBIDDEN Participants of polls in channels should stay anonymous.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/messages.getMessageReactionsList" />
///</summary>
internal sealed class GetMessageReactionsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessageReactionsList, MyTelegram.Schema.Messages.IMessageReactionsList>,
    Messages.IGetMessageReactionsListHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessageReactionsList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetMessageReactionsList obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IMessageReactionsList>(new TMessageReactionsList
        {
            Chats = new(),
            Reactions = new(),
            Users = new(),
        });
    }
}
