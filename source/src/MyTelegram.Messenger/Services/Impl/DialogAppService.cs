namespace MyTelegram.Messenger.Services.Impl;

public class DialogAppService : BaseAppService, IDialogAppService
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;
    private readonly IOffsetHelper _offsetHelper;

    public DialogAppService(ICommandBus commandBus,
        IQueryProcessor queryProcessor, IPhotoAppService photoAppService, IPrivacyAppService privacyAppService, IOffsetHelper offsetHelper)
    {
        _commandBus = commandBus;
        _queryProcessor = queryProcessor;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
        _offsetHelper = offsetHelper;
    }

    public async Task ReorderPinnedDialogsAsync(ReorderPinnedDialogsInput input)
    {
        var order = 0;
        foreach (var peer in input.OrderedPeerList)
        {
            var command = new SetPinnedOrderCommand(DialogId.Create(input.SelfUserId, peer), order);
            await _commandBus.PublishAsync(command, CancellationToken.None);
            order++;
        }
    }

    public async Task<GetDialogOutput> GetDialogsAsync(GetDialogInput input)
    {
        var offset = _offsetHelper.GetOffsetInfo(input);
        DateTime? offsetDate = null;
        if (input.OffsetPeer != null && input.OffsetPeer.PeerType != PeerType.Empty)
        {
            var dialogId = DialogId.Create(input.OwnerId, input.OffsetPeer.PeerType, input.OffsetPeer.PeerId);
            var dialog = await _queryProcessor.ProcessAsync(new GetDialogByIdQuery(dialogId),
                CancellationToken.None);
            offsetDate = dialog?.CreationTime;
        }

        var query = new GetDialogsQuery(input.OwnerId,
            input.Pinned,
            offsetDate,
            offset,
            input.Limit,
            input.PeerIdList);
        var dialogList = await _queryProcessor.ProcessAsync(query, CancellationToken.None);
        if (input.Pinned == true)
        {
            dialogList = dialogList.OrderBy(p => p.PinnedOrder).ToList();
        }

        //var peerNotifySettingsIdList = dialogList
        //    .Select(p => PeerNotifySettingsId.Create(p.OwnerId, p.ToPeerType, p.ToPeerId)).ToList();
        //var peerNotifySettingsList = await QueryProcessor
        //    .ProcessAsync(new GetPeerNotifySettingsListQuery(peerNotifySettingsIdList), CancellationToken.None);

        var channelIdList = dialogList.Where(p => p.ToPeerType == PeerType.Channel).Select(p => p.ToPeerId)
            .ToList();
        var channelList = channelIdList.Count == 0
            ? new List<IChannelReadModel>()
            : await _queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(channelIdList))
         ;

        //
        var topMessageIdList = dialogList.Where(p => p.ToPeerType != PeerType.Channel)
            .Select(p => MessageId.Create(p.OwnerId, p.TopMessage).Value).ToList();
        topMessageIdList.AddRange(channelList.Select(p => MessageId.Create(p.ChannelId, p.TopMessageId).Value));
        var minIdList = dialogList.Where(p => p.ChannelHistoryMinId > 0)
            .Select(p => MessageId.Create(input.OwnerId, p.ChannelHistoryMinId).Value).ToList();
        topMessageIdList.RemoveAll(p => minIdList.Contains(p));
        var messageBoxList =
            await _queryProcessor.ProcessAsync(new GetMessagesByIdListQuery(topMessageIdList));

        var extraChatUserIdList = new List<long>();
        foreach (var box in messageBoxList)
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

        var chatIdList = messageBoxList.Where(p => p.ToPeerType == PeerType.Chat).Select(p => p.ToPeerId).ToList();
        var userIdList = messageBoxList.Where(p => p.ToPeerType == PeerType.User).Select(p => p.ToPeerId).ToList();

        if (dialogList.Count > 0 || messageBoxList.Count > 0)
        {
            userIdList.Add(input.OwnerId);
        }
        userIdList.AddRange(dialogList.Where(p => p.ToPeerType == PeerType.User).Select(p => p.ToPeerId));
        userIdList.AddRange(extraChatUserIdList);

        var userList =
            await _queryProcessor.ProcessAsync(new GetUsersByUidListQuery(userIdList))
         ;
        //   var contactList = await _queryProcessor
        //       .ProcessAsync(new GetContactListQuery(input.OwnerId, userIdList))
        //;
        var contactList = new List<IContactReadModel>();

        var chatList = chatIdList.Count == 0
            ? new List<IChatReadModel>()
            : await _queryProcessor
                .ProcessAsync(new GetChatByChatIdListQuery(chatIdList));

        var privacyList = await _privacyAppService.GetPrivacyListAsync(userIdList);
        // reset dialog top message box id
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
                    default);
        }

        var pollIdList = messageBoxList.Where(p => p.PollId.HasValue).Select(p => p.PollId!.Value).ToList();
        IReadOnlyCollection<IPollReadModel>? pollReadModels = null;
        IReadOnlyCollection<IPollAnswerVoterReadModel>? chosenOptions = null;

        if (pollIdList.Count > 0)
        {
            pollReadModels =
                await _queryProcessor.ProcessAsync(new GetPollsQuery(pollIdList));
            chosenOptions = await _queryProcessor
                .ProcessAsync(new GetChosenVoteAnswersQuery(pollIdList, query.OwnerId))
         ;
        }

        var photoIds = new List<long>();
        photoIds.AddRange(chatList.Select(p => p.PhotoId ?? 0));
        photoIds.AddRange(channelList.Select(p => p.PhotoId ?? 0));
        photoIds.AddRange(userList.Select(p => p.ProfilePhotoId ?? 0));
        photoIds.AddRange(userList.Select(p => p.FallbackPhotoId ?? 0));
        //photoIds.AddRange(contactList.Select(p => p.PhotoId ?? 0));
        photoIds.RemoveAll(p => p == 0);

        var photos = await _photoAppService.GetPhotosAsync(photoIds);

        return new GetDialogOutput(input.OwnerId,
            dialogList,
            messageBoxList,
            userList,
            photos,
            chatList,
            channelList,
            contactList,
            privacyList,
            channelMemberList,
            pollReadModels,
            chosenOptions,
            input.Limit
            );
    }
}