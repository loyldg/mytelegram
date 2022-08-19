namespace MyTelegram.MessengerServer.Services.Impl;

public class DialogAppService : BaseAppService, IDialogAppService
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;

    public DialogAppService(ICommandBus commandBus,
        IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _queryProcessor = queryProcessor;
    }

    public async Task ReorderPinnedDialogsAsync(ReorderPinnedDialogsInput input)
    {
        var order = 0;
        foreach (var peer in input.OrderedPeerList)
        {
            var command = new SetPinnedOrderCommand(DialogId.Create(input.SelfUserId, peer), order);
            await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
            order++;
        }
    }

    public async Task<GetDialogOutput> GetDialogsAsync(GetDialogInput input)
    {
        var offset = GetOffset(input);
        DateTime? offsetDate = null;
        if (input.OffsetPeer != null && input.OffsetPeer.PeerType != PeerType.Empty)
        {
            var dialogId = DialogId.Create(input.OwnerId, input.OffsetPeer.PeerType, input.OffsetPeer.PeerId);
            var dialog = await _queryProcessor.ProcessAsync(new GetDialogByIdQuery(dialogId),
                CancellationToken.None).ConfigureAwait(false);
            offsetDate = dialog?.CreationTime;
        }

        var query = new GetDialogsQuery(input.OwnerId,
            input.Pinned,
            offsetDate,
            offset,
            input.Limit,
            input.PeerIdList);
        var dialogList = await _queryProcessor.ProcessAsync(query, CancellationToken.None).ConfigureAwait(false);
        if (input.Pinned == true)
        {
            dialogList = dialogList.OrderBy(p => p.PinnedOrder).ToList();
        }

        //var peerNotifySettingsIdList = dialogList
        //    .Select(p => PeerNotifySettingsId.Create(p.OwnerId, p.ToPeerType, p.ToPeerId)).ToList();
        //var peerNotifySettingsList = await QueryProcessor
        //    .ProcessAsync(new GetPeerNotifySettingsListQuery(peerNotifySettingsIdList), CancellationToken.None).ConfigureAwait(false);

        var channelIdList = dialogList.Where(p => p.ToPeerType == PeerType.Channel).Select(p => p.ToPeerId)
            .ToList();
        var channelList = channelIdList.Count == 0
            ? new List<IChannelReadModel>()
            : await _queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(channelIdList), CancellationToken.None)
                .ConfigureAwait(false);

        //
        var topMessageIdList = dialogList.Where(p => p.ToPeerType != PeerType.Channel)
            .Select(p => MessageId.Create(p.OwnerId, p.TopMessage).Value).ToList();
        topMessageIdList.AddRange(channelList.Select(p => MessageId.Create(p.ChannelId, p.TopMessageId).Value));
        var minIdList = dialogList.Where(p => p.ChannelHistoryMinId > 0)
            .Select(p => MessageId.Create(input.OwnerId, p.ChannelHistoryMinId).Value).ToList();
        topMessageIdList.RemoveAll(p => minIdList.Contains(p));
        var messagesList =
            await _queryProcessor.ProcessAsync(new GetMessagesByIdListQuery(topMessageIdList),
                CancellationToken.None).ConfigureAwait(false);

        // 删除的消息为topMessage时，需要重新读取topMessage，由于在domain层获取topMessage不是很方便，所以在读取的时候，如果发现topMessage不存在时，
        // 就重新从ReadModel读取一下最新的topMessage
        if (messagesList.Count != topMessageIdList.Count)
        {
        }

        var extraChatUserIdList = new List<long>();
        foreach (var box in messagesList)
        {
            switch (box.MessageActionType)
            {
                case MessageActionType.ChatAddUser:
                    var addedUserIdList = box.MessageActionData!.ToBytes().ToTObject<TMessageActionChatAddUser>()
                        .Users.ToList();
                    extraChatUserIdList.AddRange(addedUserIdList);
                    break;
                case MessageActionType.ChatDeleteUser:
                    var deletedUserId = box.MessageActionData!.ToBytes().ToTObject<TMessageActionChatDeleteUser>()
                        .UserId;
                    extraChatUserIdList.Add(deletedUserId);
                    break;
            }

            extraChatUserIdList.Add(box.SenderPeerId);
        }

        var chatIdList = messagesList.Where(p => p.ToPeerType == PeerType.Chat).Select(p => p.ToPeerId).ToList();

        var uidList = messagesList.Where(p => p.ToPeerType == PeerType.User).Select(p => p.ToPeerId).ToList();
        //var channelIdList = messageBoxList.Where(p => p.ToPeerType == PeerType.Channel).Select(p => p.ToPeerId)
        //    .ToList();

        if (dialogList.Count > 0 || messagesList.Count > 0)
        {
            uidList.Add(input.OwnerId);
        }

        uidList.AddRange(extraChatUserIdList);

        var userList =
            await _queryProcessor.ProcessAsync(new GetUsersByUidListQuery(uidList), CancellationToken.None)
                .ConfigureAwait(false);

        var chatList = chatIdList.Count == 0
            ? new List<IChatReadModel>()
            : await _queryProcessor
                .ProcessAsync(new GetChatByChatIdListQuery(chatIdList), CancellationToken.None).ConfigureAwait(false);

        // Reset dialog top messageId
        var channelDict = channelList.ToDictionary(k => k.ChannelId, v => v);
        foreach (var dialogReadModel in dialogList)
        {
            if (dialogReadModel.ToPeerType == PeerType.Channel)
            {
                if (channelDict.TryGetValue(dialogReadModel.ToPeerId, out var channelReadModel))
                {
                    dialogReadModel.SetNewTopMessageId(channelReadModel.TopMessageId);
                }
            }
        }

        IReadOnlyCollection<IChannelMemberReadModel> channelMemberList = new List<IChannelMemberReadModel>();
        if (channelIdList.Count > 0)
        {
            channelMemberList = await _queryProcessor
                .ProcessAsync(new GetChannelMemberListByChannelIdListQuery(input.OwnerId, channelIdList),
                    default).ConfigureAwait(false);
        }

        var pollIdList = messagesList.Where(p => p.PollId.HasValue).Select(p => p.PollId!.Value).ToList();
        IReadOnlyCollection<IPollReadModel>? pollReadModels = null;
        IReadOnlyCollection<IPollAnswerVoterReadModel>? chosenOptions = null;
        if (pollIdList.Count > 0)
        {
            pollReadModels =
                await _queryProcessor.ProcessAsync(new GetPollsQuery(pollIdList), default).ConfigureAwait(false);
            chosenOptions = await _queryProcessor
                .ProcessAsync(new GetChosenVoteAnswersQuery(pollIdList, query.OwnerId), default)
                .ConfigureAwait(false);
        }
        return new GetDialogOutput(input.OwnerId,
            dialogList,
            messagesList,
            userList,
            chatList,
            channelList,
            channelMemberList,
			pollReadModels,
            chosenOptions,
            input.Limit
            );
    }
}