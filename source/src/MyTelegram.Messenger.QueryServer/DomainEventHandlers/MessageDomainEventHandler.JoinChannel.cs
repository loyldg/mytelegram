namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public partial class MessageDomainEventHandler
{
    private Task HandleJoinChannelAsync(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        switch (aggregateEvent.MessageItem.MessageSubType)
        {
            case MessageSubType.ChatJoinBySelf:
                return HandleJoinBySelfAsync(aggregateEvent);
            case MessageSubType.ChatJoinByChatInvite:
                return HandleJoinByChatInviteAsync(aggregateEvent);
            case MessageSubType.ChatJoinByRequest:
                return HandleJoinBySelfAsync(aggregateEvent);
        }

        return Task.CompletedTask;
    }

    private async Task HandleJoinBySelfAsync(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        var updates =
            joinChannelConverterService.ToJoinChannelUpdates(aggregateEvent, aggregateEvent.RequestInfo.Layer);
        var user = await userConverterService.GetUserAsync(aggregateEvent.RequestInfo,
            aggregateEvent.RequestInfo.UserId, layer: aggregateEvent.RequestInfo.Layer);
        var channel = await chatConverterService.GetChannelAsync(aggregateEvent.RequestInfo,
            aggregateEvent.MessageItem.ToPeer.PeerId,
            false,
            false,
            aggregateEvent.RequestInfo.Layer
        );

        if (updates is TUpdates tUpdates)
        {
            tUpdates.Chats.Add(channel);
            tUpdates.Users.Add(user);
        }

        var joinResult = new TChatInviteJoinResultOk
        {
            Updates = updates
        };

        await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo, joinResult, pts: aggregateEvent.MessageItem.Pts);
        await PushUpdatesToPeerAsync(aggregateEvent.RequestInfo.UserId.ToUserPeer(), updates,
            excludeAuthKeyId: aggregateEvent.RequestInfo.PermAuthKeyId);

        if (channel is TChannel tChannel)
        {
            if (tChannel.Broadcast)
            {
                return;
            }
        }

        var updatesForMember = joinChannelConverterService.ToJoinChannelUpdates(-1, aggregateEvent, 0);
        if (updatesForMember is TUpdates tUpdatesForMember)
        {
            tUpdatesForMember.Chats.Add(channel);
            tUpdatesForMember.Users.Add(await userConverterService.GetUserAsync(RequestInfo.Empty, aggregateEvent.RequestInfo.UserId, false, false));
        }

        await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.ToPeer, updatesForMember,
            excludeUserId: aggregateEvent.RequestInfo.UserId);
    }

    private async Task HandleJoinByChatInviteAsync(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
        var userId = aggregateEvent.MessageItem.SenderUserId;
        var updates = joinChannelConverterService.ToJoinChannelUpdates(aggregateEvent, aggregateEvent.RequestInfo.Layer);

        await UpdateChannelAndUserAsync(aggregateEvent.RequestInfo, updates, aggregateEvent.MessageItem.ToPeer.PeerId,
            [aggregateEvent.RequestInfo.UserId]);

        var joinResult = new TChatInviteJoinResultOk
        {
            Updates = updates
        };

        await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo, joinResult, aggregateEvent.RequestInfo.UserId);

        var selfOtherDeviceUpdates =
            joinChannelConverterService.ToJoinChannelUpdates(aggregateEvent.RequestInfo.UserId, aggregateEvent, 0);
        await PushUpdatesToPeerAsync(aggregateEvent.RequestInfo.UserId.ToUserPeer(), selfOtherDeviceUpdates,
            aggregateEvent.RequestInfo.PermAuthKeyId);

        var updatesForChannelMember = joinChannelConverterService.ToJoinChannelUpdates(0, aggregateEvent, 0);
        await UpdateChannelAndUserAsync(RequestInfo.Empty, updatesForChannelMember, aggregateEvent.MessageItem.ToPeer.PeerId,
            [aggregateEvent.RequestInfo.UserId]);

        await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.ToPeer, updatesForChannelMember,
            excludeUserId: aggregateEvent.RequestInfo.UserId,
            pts: aggregateEvent.MessageItem.Pts
        );
    }
}
