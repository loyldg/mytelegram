namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// React to message.Starting from layer 159, the reaction will be sent from the peer specified using <a href="https://corefork.telegram.org/method/messages.saveDefaultSendAs">messages.saveDefaultSendAs</a>.
/// Possible errors
/// Code Type Description
/// 403 ANONYMOUS_REACTIONS_DISABLED Sorry, anonymous administrators cannot leave reactions or participate in polls.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 CUSTOM_REACTIONS_TOO_MANY Too many custom reactions were specified.
/// 400 DOCUMENT_INVALID The specified document is invalid.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 MESSAGE_NOT_MODIFIED The provided message data is identical to the previous message data, the message wasn't modified.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// 400 REACTIONS_TOO_MANY The message already has exactly <code>reactions_uniq_max</code> reaction emojis, you can't react with a new emoji, see <a href="https://corefork.telegram.org/api/config#client-configuration">the docs for more info »</a>.
/// 400 REACTION_EMPTY Empty reaction provided.
/// 400 REACTION_INVALID The specified reaction is invalid.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.sendReaction"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SendReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSendReaction, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSendReaction obj)
    {
        throw new NotImplementedException();
    }
}