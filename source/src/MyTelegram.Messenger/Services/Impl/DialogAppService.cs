namespace MyTelegram.Messenger.Services.Impl;

public class DialogAppService(
    ICommandBus commandBus,
    IQueryProcessor queryProcessor,
    IPhotoAppService photoAppService,
    IPrivacyAppService privacyAppService,
    IChannelAppService channelAppService,
    IUserAppService userAppService,
    IPeerHelper peerHelper,
    IOffsetHelper offsetHelper)
    : BaseAppService, IDialogAppService, ITransientDependency
{
    public async Task<GetDialogOutput> GetDialogsAsync(GetDialogInput input)
    {
        if (input.Limit > 100 || input.Limit < 1)
        {
            input.Limit = 100;
        }

        var dialogReadModels = await GetDialogListAsync(input);
        var (messageReadModels, channelReadModels) = await GetTopMessagesAndChannelsAsync(input, dialogReadModels);

        HashSet<long> channelIds = [];
        HashSet<long> userIds = [];
        AddPeerIds(input, dialogReadModels, messageReadModels, userIds, channelIds);

        var userIdList = userIds.ToList();
        var userReadModels = await userAppService.GetListAsync(userIdList);
        var contactReadModels = await queryProcessor
            .ProcessAsync(new GetContactListQuery(input.OwnerId, userIdList));

        var privacyReadModels = await privacyAppService.GetPrivacyListAsync(userIdList);
        var channelDict = channelReadModels.ToDictionary(k => k.ChannelId, v => v);
        foreach (var dialogReadModel in dialogReadModels)
        {
            if (dialogReadModel.ToPeerType == PeerType.Channel)
            {
                if (channelDict.TryGetValue(dialogReadModel.ToPeerId, out var channelReadModel))
                {
                    dialogReadModel.TopMessage = channelReadModel.TopMessageId;
                }
            }
        }

        IReadOnlyCollection<IChannelMemberReadModel> channelMemberList = new List<IChannelMemberReadModel>();
        if (channelReadModels.Count > 0)
        {
            var channelIdList = channelReadModels.Select(p => p.ChannelId).ToList();
            channelMemberList = await queryProcessor
                .ProcessAsync(new GetChannelMemberListByChannelIdListQuery(input.OwnerId, channelIdList));
        }

        var pollIdList = messageReadModels.Where(p => p.PollId.HasValue).Select(p => p.PollId!.Value).ToList();
        IReadOnlyCollection<IPollReadModel>? pollReadModels = null;
        IReadOnlyCollection<IPollAnswerVoterReadModel>? chosenOptions = null;

        if (pollIdList.Count > 0)
        {
            pollReadModels =
                await queryProcessor.ProcessAsync(new GetPollsQuery(pollIdList));
            chosenOptions = await queryProcessor
                .ProcessAsync(new GetChosenVoteAnswersQuery(pollIdList, input.OwnerId));
        }

        channelReadModels = await channelAppService.GetListAsync(channelIds);
        var photoReadModels =
            await photoAppService.GetPhotosAsync(userReadModels, contactReadModels, channelReadModels);

        await SetDialogTtlPeriodAsync(dialogReadModels);

        return new GetDialogOutput(input.OwnerId,
            dialogReadModels,
            messageReadModels,
            userReadModels,
            photoReadModels,
            [],
            channelReadModels,
            contactReadModels,
            privacyReadModels,
            channelMemberList,
            pollReadModels,
            chosenOptions,
            [],
            input.Limit
        );
    }

    public async Task ReorderPinnedDialogsAsync(ReorderPinnedDialogsInput input)
    {
        var order = 0;
        foreach (var peer in input.OrderedPeerList)
        {
            var command = new SetPinnedOrderCommand(DialogId.Create(input.SelfUserId, peer), order);
            await commandBus.PublishAsync(command, CancellationToken.None);
            order++;
        }
    }

    private void AddPeerIds(
        GetDialogInput input,
        IReadOnlyCollection<IDialogReadModel> dialogReadModels,
        IReadOnlyCollection<IMessageReadModel> messageReadModels,
        HashSet<long> userIds,
        HashSet<long> channelIds)
    {
        void AddPeerIdIfNeeded(Peer? peer)
        {
            switch (peer?.PeerType)
            {
                case PeerType.Channel:
                    channelIds.Add(peer.PeerId);
                    break;

                case PeerType.User:
                    userIds.Add(peer.PeerId);
                    break;
            }
        }

        if (dialogReadModels.Count > 0 || messageReadModels.Count > 0)
        {
            userIds.Add(input.OwnerId);
        }

        if (input.PeerIdList?.Count > 0)
        {
            foreach (var peerId in input.PeerIdList)
            {
                if (peerHelper.IsChannelPeer(peerId))
                {
                    channelIds.Add(peerId);
                }
            }
        }

        foreach (var dialogReadModel in dialogReadModels)
        {
            switch (dialogReadModel.ToPeerType)
            {
                case PeerType.Channel:
                    channelIds.Add(dialogReadModel.ToPeerId);
                    break;
                case PeerType.User:
                    userIds.Add(dialogReadModel.ToPeerId);
                    break;
            }
        }

        foreach (var messageReadModel in messageReadModels)
        {
            AddPeerIdIfNeeded(messageReadModel.SendAs);
            AddPeerIdIfNeeded(messageReadModel.FwdHeader?.SavedFromPeer);

            var fwd = messageReadModel.FwdHeader;
            AddPeerIdIfNeeded(fwd?.FromId);
            AddPeerIdIfNeeded(fwd?.SavedFromId);
            AddPeerIdIfNeeded(fwd?.SavedFromPeer);
            AddPeerIdIfNeeded(messageReadModel.SendAs);

            switch (messageReadModel.ToPeerType)
            {
                case PeerType.Channel:
                    channelIds.Add(messageReadModel.ToPeerId);
                    channelIds.Add(messageReadModel.SenderPeerId);
                    break;

                case PeerType.User:
                    userIds.Add(messageReadModel.ToPeerId);
                    channelIds.Add(messageReadModel.SenderUserId);
                    break;
            }

            switch (messageReadModel.MessageAction)
            {
                case TMessageActionChatAddUser messageActionChatAddUser:
                    foreach (var userId in messageActionChatAddUser.Users)
                    {
                        userIds.Add(userId);
                    }

                    break;

                case TMessageActionChatJoinedByLink messageActionChatJoinedByLink:
                    userIds.Add(messageActionChatJoinedByLink.InviterId);
                    break;

                case TMessageActionChatJoinedByRequest:

                    break;

                case TMessageActionChatDeleteUser messageActionChatDeleteUser:
                    userIds.Add(messageActionChatDeleteUser.UserId);
                    break;
            }
        }
    }

    private async Task<IReadOnlyCollection<IDialogReadModel>> GetDialogListAsync(GetDialogInput input)
    {
        var offset = offsetHelper.GetOffsetInfo(input);
        int? offsetDate = input.OffsetDate > 0 ? input.OffsetDate : null;
        if (offsetDate == null && input.OffsetPeer != null && input.OffsetPeer.PeerType != PeerType.Empty)
        {
            var dialogId = DialogId.Create(input.OwnerId, input.OffsetPeer.PeerType, input.OffsetPeer.PeerId);
            var dialog = await queryProcessor.ProcessAsync(new GetDialogByIdQuery(dialogId.Value));
            offsetDate = null;//dialog?.LastMessageDate;
            if (dialog != null && offset.OffsetId == 0)
            {
                offset.OffsetId = dialog.TopMessage;
            }
        }

        var query = new GetDialogsQuery(input.OwnerId,
            input.Pinned,
            offsetDate,
            offset,
            input.Limit,
            input.PeerIdList,
            input.FolderId
        );

        var dialogReadModels = await queryProcessor.ProcessAsync(query);
        if (input.Pinned == true)
        {
            dialogReadModels = dialogReadModels.OrderBy(p => p.PinnedOrder).ToList();
        }

        return dialogReadModels;
    }

    private async Task<(IReadOnlyCollection<IMessageReadModel>, IReadOnlyCollection<IChannelReadModel>)>
        GetTopMessagesAndChannelsAsync(GetDialogInput input, IReadOnlyCollection<IDialogReadModel> dialogReadModels)
    {
        var channelIdList = dialogReadModels.Where(p => p.ToPeerType == PeerType.Channel).Select(p => p.ToPeerId)
            .ToList();
        if (input.PeerIdList?.Count > 0)
        {
            foreach (var peerId in input.PeerIdList)
            {
                if (peerHelper.IsChannelPeer(peerId))
                {
                    channelIdList.Add(peerId);
                }
            }
        }

        var channelList = channelIdList.Count == 0
            ? new List<IChannelReadModel>()
            : await channelAppService.GetListAsync(channelIdList);

        var topMessageIdList = dialogReadModels.Where(p => p.ToPeerType != PeerType.Channel)
            .Select(p => MessageId.Create(p.OwnerId, p.TopMessage).Value).ToList();
        topMessageIdList.AddRange(channelList.Select(p => MessageId.Create(p.ChannelId, p.TopMessageId).Value));
        var minIdList = dialogReadModels.Where(p => p.ChannelHistoryMinId > 0)
            .Select(p => MessageId.Create(input.OwnerId, p.ChannelHistoryMinId).Value).ToList();
        topMessageIdList.RemoveAll(minIdList.Contains);

        var messageReadModels = await queryProcessor.ProcessAsync(new GetMessagesByIdListQuery(topMessageIdList));

        return (messageReadModels, channelList);
    }

    private Task SetDialogTtlPeriodAsync(IReadOnlyCollection<IDialogReadModel> dialogs)
    {
        return Task.CompletedTask;
    }
}