namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
///     Fetch (a subset or all) messages in a
///     <a href="https://corefork.telegram.org/api/business#quick-reply-shortcuts">quick reply shortcut »</a>.
///     Possible errors
///     Code Type Description
///     400 SHORTCUT_INVALID The specified shortcut is invalid.
///     <para>
///         <c>See <a href="https://corefork.telegram.org/method/messages.getQuickReplyMessages" /> </c>
///     </para>
/// </summary>
/// <remarks>
///     Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetQuickReplyMessagesHandler : RpcResultObjectHandler<RequestGetQuickReplyMessages, IMessages>
{
    protected override Task<IMessages> HandleCoreAsync(IRequestInput input, RequestGetQuickReplyMessages obj)
    {
        return Task.FromResult<IMessages>(new TMessages
        {
            Chats = [],
            Messages = [],
            Users = [],
            Topics = []
        });
    }
}