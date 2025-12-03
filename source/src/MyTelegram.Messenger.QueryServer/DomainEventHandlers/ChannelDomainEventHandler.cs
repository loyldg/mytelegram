using MyTelegram.Domain.Aggregates.ChatInvite;
using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Interfaces;
using TChannelParticipant = MyTelegram.Schema.TChannelParticipant;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ChannelDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IQueryProcessor queryProcessor,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IUserAppService userAppService,
    IPhotoAppService photoAppService,
    IChannelAppService channelAppService,
    IChatEventCacheHelper chatEventCacheHelper,
    IChatConverterService chatConverterService,
    IChatInviteExportedConverterService chatInviteExportedConverterService,
    IUserConverterService userConverterService,
    IUpdatesConverterService updatesConverterService
    )
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService),
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, DiscussionGroupUpdatedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelTitleEditedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelAboutEditedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelDefaultBannedRightsEditedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, SlowModeChangedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, PreHistoryHiddenChangedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent>,
        //ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
        ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
        ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>,
        ISubscribeSynchronousTo<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelCompletedSagaEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelNoForwardsChangedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelSignatureChangedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelColorUpdatedEvent>,
        ISubscribeSynchronousTo<ChatInviteAggregate, ChatInviteId, ChatInviteCreatedEvent>,
        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChatInviteRequestPendingUpdatedEvent>,
        //ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChatJoinRequestHiddenEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelDeletedEvent>,
        //ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedSagaEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelParticipantsHiddenUpdatedEvent>,
        ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelJoinRequestUpdatedEvent>,
        ISubscribeSynchronousTo<JoinChannelAggregate, JoinChannelId, JoinChannelRequestCreatedEvent>,
        ISubscribeSynchronousTo<ApproveJoinChannelSaga, ApproveJoinChannelSagaId, ApproveJoinChannelCompletedSagaEvent>,
        ISubscribeSynchronousTo<ImportChatInviteSaga, ImportChatInviteSagaId, ImportChatInviteCompletedSagaEvent>,
        ISubscribeSynchronousTo<JoinChannelSaga, JoinChannelSagaId, JoinChannelCompletedSagaEvent>,
        ISubscribeSynchronousTo<EditExportedChatInviteSaga, EditExportedChatInviteSagaId, EditExportedChatInviteCompletedSagaEvent>
{
    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelAboutEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            new TBoolTrue());
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId);

        var channel = await chatConverterService.GetChannelAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            false,
            false,
            domainEvent.AggregateEvent.RequestInfo.Layer
        );
        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>([
                new TUpdateChannel
                {
                    ChannelId = domainEvent.AggregateEvent.ChannelId
                }
            ]),
            Chats = new TVector<IChat>([channel]),
            Users = [],
            Date = DateTime.UtcNow.ToTimestamp()
        };
        await PushMessageToPeerAsync(domainEvent.AggregateEvent.UserId.ToUserPeer(), updates);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelColorUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var item = await GetChannelAndPhotoAsync(domainEvent.AggregateEvent.ChannelId);
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId,
            0,
            item.Item1,
            item.Item2
        );
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelAggregate, ChannelId, ChannelDefaultBannedRightsEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var channelForbidden = new TChannelForbidden
        {
            Id = domainEvent.AggregateEvent.ChannelId,
            Broadcast = domainEvent.AggregateEvent.Broadcast,
            Megagroup = domainEvent.AggregateEvent.Megagroup,
            AccessHash = domainEvent.AggregateEvent.AccessHash,
            Title = domainEvent.AggregateEvent.Title
        };
        var updates = new TUpdates
        {
            Updates = [],
            Users = [],
            Chats = new TVector<IChat>(channelForbidden),
            Date = DateTime.UtcNow.ToTimestamp()
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);
        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), updates);
        var updateChannel = new TUpdateChannel
        {
            ChannelId = domainEvent.AggregateEvent.ChannelId
        };
        var updatesChannel = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateChannel),
            Users = [],
            Chats = new TVector<IChat>(channelForbidden)
        };
        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.ChannelId.ToChannelPeer(), updatesChannel,
            excludeUserId: domainEvent.AggregateEvent.RequestInfo.UserId);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelNoForwardsChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var updateChannel = new TUpdateChannel { ChannelId = domainEvent.AggregateEvent.ChannelId };

        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateChannel),
            Users = [],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp()
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);

        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo with { ReqMsgId = 0 },
            domainEvent.AggregateEvent.ChannelId);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelSignatureChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var channel = await chatConverterService.GetChannelAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            false,
            false,
            domainEvent.AggregateEvent.RequestInfo.Layer
        );

        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Users = [],
            Updates = [],
            Date = DateTime.UtcNow.ToTimestamp()
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);

        await PushMessageToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), updates);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelTitleEditedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo with { ReqMsgId = 0 },
                domainEvent.AggregateEvent.ChannelId);
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelUserNameChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, new TBoolTrue());
    }

    //public async Task HandleAsync(
    //    IDomainEvent<ChannelAggregate, ChannelId, ChatInviteRequestPendingUpdatedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    // We should notify all channel admins to approve the request after chatInvites imported 
    //    var update = new TUpdatePendingJoinRequests
    //    {
    //        Peer = new TPeerChannel
    //        {
    //            ChannelId = domainEvent.AggregateEvent.ChannelId
    //        },
    //        RequestsPending = domainEvent.AggregateEvent.RequestsPending ?? 0,
    //        RecentRequesters = new TVector<long>(domainEvent.AggregateEvent.RecentRequesters)
    //    };

    //    var updates = new TUpdates
    //    {
    //        Updates = new TVector<IUpdate>(update),
    //        Chats = [],
    //        Date = DateTime.UtcNow.ToTimestamp(),
    //        Users = []
    //    };

    //    foreach (var userId in domainEvent.AggregateEvent.ChannelAdmins)
    //    {
    //        await PushMessageToPeerAsync(new Peer(PeerType.User, userId), updates);
    //    }
    //}

    //public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChatJoinRequestHiddenEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    var update = new TUpdateChannel
    //    {
    //        ChannelId = domainEvent.AggregateEvent.ChannelId
    //    };
    //    var updates = new TUpdates
    //    {
    //        Chats = [],
    //        Date = DateTime.UtcNow.ToTimestamp(),
    //        Updates = new TVector<IUpdate>(update),
    //        Users = []
    //    };

    //    return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);
    //}

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, DiscussionGroupUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            new TBoolTrue());
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelAggregate, ChannelId, PreHistoryHiddenChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId);
    }

    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, SlowModeChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId);
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var channel = await chatConverterService.GetChannelAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId, false, false, domainEvent.AggregateEvent.RequestInfo.Layer);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, new TUpdates
        {
            Chats = [channel],
            Users = [],
            Updates = [],
            Date = DateTime.UtcNow.ToTimestamp()
        });

        var channelForMember = await chatConverterService.GetChannelAsync(RequestInfo.Empty with { UserId = domainEvent.AggregateEvent.MemberUserId },
            domainEvent.AggregateEvent.ChannelId,
            false,
            false,
            0);
        if (channelForMember is ILayeredChannel layeredChannel)
        {
            layeredChannel.BannedRights = domainEvent.AggregateEvent.BannedRights.ToChatBannedRights();
            layeredChannel.AdminRights = null;
        }

        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.MemberUserId.ToUserPeer(), new TUpdates
        {
            Chats = [channelForMember],
            Users = [],
            Updates = [new TUpdateChannel { ChannelId = domainEvent.AggregateEvent.ChannelId }],
            Date = DateTime.UtcNow.ToTimestamp()
        });
    }

    //public Task HandleAsync(
    //    IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    return SendChannelUpdatedRpcResultAndNotifyChannelMembersAsync(domainEvent.AggregateEvent.ChannelId,
    //        domainEvent.AggregateEvent.MemberUserId, domainEvent.AggregateEvent.RequestInfo,
    //        domainEvent.AggregateEvent.Date);
    //}

    public async Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var channel = await chatConverterService.GetChannelAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            false,
            true,
            domainEvent.AggregateEvent.RequestInfo.Layer
        );

        var updates = new TUpdates
        {
            Updates = [new TUpdateChannel
            {
                ChannelId = domainEvent.AggregateEvent.ChannelId
            }],
            Users = [],
            Chats = [channel]
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);
        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), updates,
            excludeAuthKeyId: domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId);
    }

    public Task HandleAsync(IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var exportedChatInvite = chatInviteExportedConverterService
            .ToExportedChatInvite(domainEvent.AggregateEvent, domainEvent.AggregateEvent.RequestInfo.Layer);
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, exportedChatInvite);
    }

    public async Task HandleAsync(IDomainEvent<ChatInviteAggregate, ChatInviteId, ChatInviteImportedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.ChatInviteRequestState == ChatInviteRequestState.WaitingForApproval)
        {
            var rpcError = new TRpcError
            {
                ErrorCode = RpcErrors.RpcErrors400.InviteRequestSent.ErrorCode,
                ErrorMessage = RpcErrors.RpcErrors400.InviteRequestSent.Message
            };

            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, rpcError);
        }
    }

    public async Task HandleAsync(
        IDomainEvent<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        // When domainEvent.AggregateEvent.Broadcast==false, a service message will be sent
        if (domainEvent.AggregateEvent.Broadcast || domainEvent.AggregateEvent.HasLink)
        {
            var item = await GetChannelAndPhotoAsync(domainEvent.AggregateEvent.ChannelId);

            if (domainEvent.AggregateEvent.Broadcast)
            {
                var channel = chatConverterService.ToChannel(domainEvent.AggregateEvent.RequestInfo,
                    item.Item1,
                    item.Item2,
                    null,
                    false,
                    domainEvent.AggregateEvent.RequestInfo.Layer
                );
                var date = DateTime.UtcNow.ToTimestamp();
                var invitedUsers = new TInvitedUsers
                {
                    Updates = new TUpdates
                    {
                        Updates = [],
                        Users = [],
                        Chats = [channel],
                        Date = date
                    },
                    MissingInvitees = []
                };
                await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, invitedUsers);
            }

            var inviteToChannelDefaultPts = 1;
            foreach (var userId in domainEvent.AggregateEvent.MemberUserIds)
            {
                await NotifyUpdateChannelAsync(
                    domainEvent.AggregateEvent.RequestInfo with { ReqMsgId = 0 },
                    domainEvent.AggregateEvent.ChannelId,
                    userId,
                    item.Item1,
                    item.Item2,
                    inviteToChannelDefaultPts
                );
            }

            // Notify all bots
            if (item.Item1.Bots.Count > 0)
            {
                var updatesForBots = await CreateUpdateChannelParticipantUpdatesAsync(domainEvent.AggregateEvent);
                foreach (var botUserId in item.Item1.Bots)
                {
                    await PushUpdatesToPeerAsync(botUserId.ToUserPeer(), updatesForBots);
                }
            }
        }
    }

    private async Task<IUpdates> CreateUpdateChannelParticipantUpdatesAsync(InviteToChannelCompletedSagaEvent aggregateEvent)
    {
        var date = DateTime.UtcNow.ToTimestamp();

        var updates = new TUpdates
        {
            Updates = [],
            Users = [],
            Chats = [],
            Date = date,
        };
        foreach (var userId in aggregateEvent.MemberUserIds)
        {
            var updateChannelParticipant = new TUpdateChannelParticipant
            {
                ChannelId = aggregateEvent.ChannelId,
                Date = date,
                UserId = userId,
                ViaChatlist = false,
                NewParticipant = new TChannelParticipant
                {
                    UserId = userId,
                    Date = date
                }
            };

            switch (aggregateEvent.ChatJoinType)
            {
                case ChatJoinType.InvitedByAdmin:
                case ChatJoinType.ByRequest:
                    updateChannelParticipant.ActorId = aggregateEvent.RequestInfo.UserId;

                    break;
                case ChatJoinType.BySelf:
                case ChatJoinType.ByLink:
                    updateChannelParticipant.ActorId = userId;

                    break;
            }

            updates.Updates.Add(updateChannelParticipant);
        }

        var channel = await chatConverterService.GetChannelAsync(RequestInfo.Empty, aggregateEvent.ChannelId, false, false,
            aggregateEvent.RequestInfo.Layer);
        updates.Chats.Add(channel);

        var userReadModels = await userAppService.GetListAsync(aggregateEvent.MemberUserIds.ToList());
        var photoReadModels = await photoAppService.GetPhotosAsync(userReadModels);

        var users = userConverterService.ToUserList(RequestInfo.Empty, userReadModels, photoReadModels, [], [],
            aggregateEvent.RequestInfo.Layer);

        updates.Users = new TVector<IUser>(users);

        return updates;
    }

    //public async Task HandleAsync(
    //    IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedSagaEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    var r = updatesConverterService.ToSelfUpdatePinnedMessageUpdates(domainEvent.AggregateEvent);
    //    if (domainEvent.AggregateEvent.PmOneSide || domainEvent.AggregateEvent.ShouldReplyRpcResult)
    //    {
    //        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
    //            r,
    //            domainEvent.AggregateEvent.SenderPeerId,
    //            domainEvent.AggregateEvent.Pts,
    //            domainEvent.AggregateEvent.ToPeer.PeerType
    //        );
    //        await PushUpdatesToPeerAsync(
    //            new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
    //            r,
    //            pts: domainEvent.AggregateEvent.Pts);
    //    }

    //    var updates = updatesConverterService.ToUpdatePinnedMessageUpdates(domainEvent.AggregateEvent);
    //    if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
    //    {
    //        //var (channelReadModel, photoReadModel) =
    //        //    await GetChannelAsync(domainEvent.AggregateEvent.ToPeer.PeerId);
    //    }

    //    await PushUpdatesToPeerAsync(
    //        domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel
    //            ? new Peer(PeerType.Channel, domainEvent.AggregateEvent.OwnerPeerId)
    //            : new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
    //        updates,
    //        excludeUserId: domainEvent.AggregateEvent.SenderPeerId,
    //        pts: domainEvent.AggregateEvent.Pts
    //    );

    //    if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
    //    {
    //        await NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo,
    //            domainEvent.AggregateEvent.ToPeer.PeerId);
    //    }
    //}

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        chatEventCacheHelper.Add(domainEvent.AggregateEvent);
        return Task.CompletedTask;
    }

    //public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, StartInviteToChannelEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    chatEventCacheHelper.Add(domainEvent.AggregateEvent);
    //    return Task.CompletedTask;
    //}

    private async Task<(IChannelReadModel, IPhotoReadModel?)> GetChannelAndPhotoAsync(long channelId)
    {
        var channelReadModel = await channelAppService.GetAsync(channelId);
        var photoReadModel = channelReadModel.PhotoId.HasValue
            ? await photoAppService.GetAsync(channelReadModel.PhotoId.Value)
            : null;

        return (channelReadModel, photoReadModel);
    }

    private async Task NotifyUpdateChannelAsync(RequestInfo requestInfo,
        long channelId,
        long memberUserId = 0,
        IChannelReadModel? channelReadModel = null,
        IPhotoReadModel? channelPhotoReadModel = null,
        int pts = 0
    )
    {
        if (channelReadModel == null)
        {
            var item = await GetChannelAndPhotoAsync(channelId);
            channelReadModel = item.Item1;
            channelPhotoReadModel = item.Item2;
        }

        var selfUserId = requestInfo.UserId;
        if (memberUserId != 0)
        {
            selfUserId = memberUserId;
        }

        var updates = updatesConverterService.ToChannelUpdates(requestInfo, channelReadModel, channelPhotoReadModel, requestInfo.Layer);
        var updatesForMember = updatesConverterService.ToChannelUpdates(RequestInfo.Empty with { UserId = memberUserId }, channelReadModel, channelPhotoReadModel, 0);

        await SendRpcMessageToClientAsync(requestInfo, updates);
        if (memberUserId != 0)
        {
            await PushUpdatesToPeerAsync(new Peer(PeerType.Channel, channelId),
                updatesForMember,
                onlySendToUserId: memberUserId,
                pts: pts);
        }
        else
        {
            await PushUpdatesToPeerAsync(new Peer(PeerType.Channel, channelId),
                updatesForMember,
                excludeUserId: requestInfo.UserId
            );
        }
    }

    private async Task SendChannelUpdatedRpcResultAndNotifyChannelMembersAsync(long channelId,
                long channelMemberUserId,
        RequestInfo requestInfo,
        int date
    )
    {
        var channel =
            await chatConverterService.GetChannelAsync(requestInfo with { UserId = channelMemberUserId }, channelId, false, false, requestInfo.Layer);

        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Date = date,
            Seq = 0,
            Users = [],
            Updates = []
        };

        await SendRpcMessageToClientAsync(requestInfo, updates);
        await NotifyUpdateChannelAsync(requestInfo with { ReqMsgId = 0 }, channelId);
    }
    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelParticipantsHiddenUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId);
    }
    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelJoinRequestUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return NotifyUpdateChannelAsync(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId);
    }
    public async Task HandleAsync(IDomainEvent<JoinChannelAggregate, JoinChannelId, JoinChannelRequestCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, RpcErrors.RpcErrors400.InviteRequestSent.ToRpcError());
        var channelId = domainEvent.AggregateEvent.ChannelId;
        var pendingRequestsCount =
            await queryProcessor.ProcessAsync(new GetPendingRequestsCountQuery(channelId), cancellationToken);
        var recentRequesters =
            await queryProcessor.ProcessAsync(
                new GetRecentRequestUserIdListQuery(channelId, 5), cancellationToken);
        var channelReadModel = await channelAppService.GetAsync(channelId);
        var updatePendingJoinRequests = new TUpdatePendingJoinRequests
        {
            Peer = channelId.ToChannelPeer().ToPeer(),
            RecentRequesters = [.. recentRequesters],
            RequestsPending = pendingRequestsCount
        };
        var users = await userConverterService.GetUserListAsync(RequestInfo.Empty, [.. recentRequesters]);

        var updates = new TUpdates
        {
            Updates = [updatePendingJoinRequests],
            Users = [.. users],
            Chats = [],
            Date = DateTime.UtcNow.ToTimestamp()
        };
        foreach (var chatAdmin in channelReadModel.AdminList)
        {
            if (chatAdmin.AdminRights.InviteUsers)
            {
                await PushUpdatesToPeerAsync(chatAdmin.UserId.ToUserPeer(), updates);
            }
        }
    }

    public async Task HandleAsync(IDomainEvent<ApproveJoinChannelSaga, ApproveJoinChannelSagaId, ApproveJoinChannelCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var channel = await chatConverterService.GetChannelAsync(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId, false, false, domainEvent.AggregateEvent.RequestInfo.Layer);
        var updates = new TUpdates
        {
            Updates = [],
            Users = [],
            Chats = [channel],
            Date = DateTime.UtcNow.ToTimestamp()
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);

        // After joining a supergroup, a service message will be sent, so there is no need to notify current joiner.
        // After joining a channel, no service message will be sent, so it is necessary to notify the current joiner.
        if (domainEvent.AggregateEvent.Approved && domainEvent.AggregateEvent.Broadcast)
        {
            var channelForJoinedMember = await chatConverterService.GetChannelAsync(domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId, false, false);
            var updatesForJoinedMember = new TUpdates
            {
                Updates = [new TUpdateChannel
                {
                    ChannelId=domainEvent.AggregateEvent.ChannelId
                }],
                Users = [],
                Chats = [channelForJoinedMember],
                Date = DateTime.UtcNow.ToTimestamp()
            };
            await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.UserId.ToUserPeer(), updatesForJoinedMember);
        }
    }

    public async Task HandleAsync(IDomainEvent<ImportChatInviteSaga, ImportChatInviteSagaId, ImportChatInviteCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        // Notify only the joining user when they join a channel. In supergroups, a join notification will be sent to everyone.
        if (domainEvent.AggregateEvent.Broadcast)
        {
            var channel = await chatConverterService.GetChannelAsync(domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId, false, false, domainEvent.AggregateEvent.RequestInfo.Layer);
            var updates = new TUpdates
            {
                Updates = [],
                Users = [],
                Chats = [channel],
                Date = DateTime.UtcNow.ToTimestamp()
            };

            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);
        }
    }

    public async Task HandleAsync(IDomainEvent<JoinChannelSaga, JoinChannelSagaId, JoinChannelCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.Broadcast)
        {
            var channel = await chatConverterService.GetChannelAsync(domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId, false, false, domainEvent.AggregateEvent.RequestInfo.Layer);
            var updates = new TUpdates
            {
                Updates = [new TUpdateChannel
                {
                    ChannelId=domainEvent.AggregateEvent.ChannelId
                }],
                Users = [],
                Chats = [channel],
                Date = DateTime.UtcNow.ToTimestamp()
            };

            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);
        }
    }

    public Task HandleAsync(IDomainEvent<EditExportedChatInviteSaga, EditExportedChatInviteSagaId, EditExportedChatInviteCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var aggregateEvent = domainEvent.AggregateEvent.ChatInviteEditedEvent;
        var requestInfo = domainEvent.AggregateEvent.ChatInviteEditedEvent.RequestInfo;
        if (aggregateEvent is { Permanent: true, Revoked: true })
        {
            var invite = chatInviteExportedConverterService
                .ToExportedChatInvite(domainEvent.AggregateEvent.ChatInviteEditedEvent,
                    domainEvent.AggregateEvent.ChatInviteEditedEvent.Hash,
                    requestInfo.Layer);
            var newInvite = chatInviteExportedConverterService
                .ToExportedChatInvite(domainEvent.AggregateEvent.ChatInviteEditedEvent,
                    domainEvent.AggregateEvent.ChatInviteEditedEvent.NewHash!,
                    requestInfo.Layer);
            newInvite.Revoked = false;

            var exportedChatInvite = new TExportedChatInviteReplaced
            {
                Invite = invite,
                NewInvite = newInvite,
                Users = []
            };

            return SendRpcMessageToClientAsync(requestInfo, exportedChatInvite);
        }
        else
        {
            var invite = chatInviteExportedConverterService
                .ToExportedChatInvite(aggregateEvent, aggregateEvent.Hash, requestInfo.Layer);

            var exportedChatInvite = new TExportedChatInvite
            {
                Invite = invite,
                Users = []
            };

            return SendRpcMessageToClientAsync(requestInfo, exportedChatInvite);
        }
    }
}