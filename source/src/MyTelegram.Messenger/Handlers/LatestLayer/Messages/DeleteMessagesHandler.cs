namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Deletes messages by their identifiers.
/// Possible errors
/// Code Type Description
/// 403 BOT_ACCESS_FORBIDDEN The specified method <em>can</em> be used over a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business connection</a> for some operations, but the specified query attempted an operation that is not allowed over a business connection.
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 403 MESSAGE_DELETE_FORBIDDEN You can't delete one of the messages you tried to delete, most likely because it is a service message.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 SELF_DELETE_RESTRICTED Business bots can't delete messages just for the user, <code>revoke</code> <strong>must</strong> be set.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.deleteMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteMessagesHandler(ICommandBus commandBus, IPtsHelper ptsHelper, IQueryProcessor queryProcessor) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteMessages, MyTelegram.Schema.Messages.IAffectedMessages>
{
    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestDeleteMessages obj)
    {
        if (obj.Id.Count > 0)
        {
            var messageIds = obj.Id.ToList();
            var messageItemsToBeDeletedList = await queryProcessor.ProcessAsync(new GetMessageItemListToBeDeletedQuery(input.UserId, messageIds, obj.Revoke));
            int? newTopMessageId = null;
            int? newTopMessageIdForOtherParticipant = null;
            // Not set top message id for group chat
            if (messageItemsToBeDeletedList.Any(p => p.ToPeerType == PeerType.User))
            {
                newTopMessageId = await queryProcessor.ProcessAsync(new GetTopMessageIdQuery(input.UserId, messageIds));
                if (obj.Revoke)
                {
                    var toPeerMessageItem = messageItemsToBeDeletedList.FirstOrDefault(p => p.OwnerUserId != input.UserId);
                    if (toPeerMessageItem != null)
                    {
                        var toPeerMessageIds = messageItemsToBeDeletedList.Where(p => p.OwnerUserId != input.UserId).Select(p => p.MessageId).ToList();
                        newTopMessageIdForOtherParticipant = await queryProcessor.ProcessAsync(new GetTopMessageIdQuery(toPeerMessageItem.OwnerUserId, toPeerMessageIds));
                    }
                }
            }

            var command = new StartDeleteMessagesCommand(TempId.New, input.ToRequestInfo(), messageItemsToBeDeletedList, obj.Revoke, obj.Revoke, newTopMessageId, newTopMessageIdForOtherParticipant);
            await commandBus.PublishAsync(command);
            return null !;
        }

        var pts = ptsHelper.GetCachedPts(input.UserId);
        return new TAffectedMessages
        {
            Pts = pts,
            PtsCount = 0
        };
    }
}