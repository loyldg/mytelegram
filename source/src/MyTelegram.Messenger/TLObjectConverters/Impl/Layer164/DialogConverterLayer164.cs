using MyTelegram.Schema.Updates;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer164;

public class DialogConverterLayer164 : LayeredConverterBase, IDialogConverterLayer164
{
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly ILayeredService<IMessageConverter> _layeredMessageService;
    private IChatConverter? _chatConverter;
    private IUserConverter? _userConverter;
    private IMessageConverter? _messageConverter;
    public DialogConverterLayer164(
        ILayeredService<IChatConverter> layeredChatService,
        ILayeredService<IUserConverter> layeredUserService,
        ILayeredService<IMessageConverter> layeredMessageService,
        IObjectMapper objectMapper)
    {
        _layeredChatService = layeredChatService;
        _layeredUserService = layeredUserService;
        _layeredMessageService = layeredMessageService;
        ObjectMapper = objectMapper;
    }

    protected virtual IObjectMapper ObjectMapper { get; }

    public override int Layer => Layers.Layer164;

    public virtual IDialogs ToDialogs(GetDialogOutput output)
    {
        var dialogs = new List<IDialog>();
        var messages = output.MessageList.Where(p => p.ToPeerType == PeerType.Channel)
            .ToDictionary(k => k.Id, v => v);
        //var peerNotifySettingsDict = output.PeerNotifySettingList.ToDictionary(k => k.PeerId, v => v);
        foreach (var dialog in output.DialogList)
        {
            var channelReadModel = dialog.ToPeerType == PeerType.Channel ? output.ChannelList.FirstOrDefault(p => p.ChannelId == dialog.ToPeerId) : null;
            var tDialog = CreateDialog(dialog, messages, channelReadModel);
            if (dialog.ToPeerType == PeerType.Chat)
            {

            }


            dialogs.Add(tDialog);
        }

        var userList = GetUserConverter().ToUserList(output.SelfUserId, output.UserList, output.PhotoList, output.ContactList, privacies: output.PrivacyList);
        var chatList = GetChatConverter().ToChatList(output.SelfUserId, output.ChatList, output.PhotoList);

        var channelList = GetChatConverter().ToChannelList(
            output.SelfUserId,
            output.ChannelList,
            output.PhotoList,
            output.ChannelList.Select(p => p.ChannelId).ToList(),
            output.ChannelMemberList);
        foreach (var chat in channelList)
        {
            chatList.Add(chat);
        }

        var allMessages = output.MessageList.ToList();

        foreach (var chat in channelList)
        {
            if (chat is TChannelForbidden channelForbidden)
            {
                allMessages.RemoveAll(p => p.ToPeerId == channelForbidden.Id);
            }
        }

        var messageList = GetMessageConverter()
            .ToMessages(allMessages, output.PollList, output.ChosenPollOptions, output.SelfUserId);

        if (dialogs.Count == output.Limit)
        {
            return new TDialogsSlice
            {
                Chats = new TVector<IChat>(chatList),
                Dialogs = new TVector<IDialog>(dialogs),
                Messages = new TVector<IMessage>(messageList),
                Users = new TVector<IUser>(userList),
                Count = output.Limit
            };
        }

        return new TDialogs
        {
            Chats = new TVector<IChat>(chatList),
            Dialogs = new TVector<IDialog>(dialogs),
            Messages = new TVector<IMessage>(messageList),
            Users = new TVector<IUser>(userList)
        };
    }

    public IPeerDialogs ToPeerDialogs(GetDialogOutput output)
    {
        var dialogs = ToDialogs(output);
        //             var pts = output.ChannelList?.FirstOrDefault()?.Pts ?? 0;
        //             if (pts == 0)
        //             {
        // pts=output.DialogList.
        //             }

        //var pts = output.DialogList.ElementAtOrDefault(0)?.Pts ?? 0;
        //if (pts == 0)
        //{
        //    pts = output.MessageList.Any() ? output.MessageList.Max(p => p.Pts) : 0;
        //}

        var pts = output.PtsReadModel?.Pts ?? 0;
        if (output.CachedPts > pts)
        {
            pts = output.CachedPts;
        }

        return dialogs switch
        {
            TDialogs dialogs1 => new TPeerDialogs
            {
                Chats = dialogs1.Chats,
                Dialogs = dialogs1.Dialogs,
                Messages = dialogs1.Messages,
                Users = dialogs1.Users,
                State = new TState
                {
                    Pts = pts,
                    Date = DateTime.UtcNow.ToTimestamp(),
                    Qts = output.PtsReadModel?.Qts ?? 0,
                    UnreadCount = output.PtsReadModel?.UnreadCount ?? 0
                }
            },
            TDialogsSlice dialogsSlice => new TPeerDialogs
            {
                Chats = dialogsSlice.Chats,
                Dialogs = dialogsSlice.Dialogs,
                Messages = dialogsSlice.Messages,
                Users = dialogsSlice.Users,
                State = new TState
                {
                    Pts = pts,
                    Date = DateTime.UtcNow.ToTimestamp(),
                    Qts = output.PtsReadModel?.Qts ?? 0,
                    UnreadCount = output.PtsReadModel?.UnreadCount ?? 0
                }
            },
            _ => throw new ArgumentOutOfRangeException(nameof(output))
        };
    }

    protected virtual IDialog CreateDialog(IDialogReadModel dialogReadModel,
        Dictionary<string, IMessageReadModel> messages, IChannelReadModel? channelReadModel)
    {
        var tDialog = ObjectMapper.Map<IDialogReadModel, TDialog>(dialogReadModel);
        if (dialogReadModel.ReadOutboxMaxId == 0 && dialogReadModel.ReadInboxMaxId != 0)
        {
            tDialog.ReadInboxMaxId = dialogReadModel.ReadInboxMaxId;
        }

        if (dialogReadModel.ToPeerType == PeerType.Channel)
        {
            if (channelReadModel != null)
            {
                var maxId = new[]
                {
                    dialogReadModel.MaxSendOutMessageId, dialogReadModel.ReadOutboxMaxId, dialogReadModel.ReadInboxMaxId,
                    dialogReadModel.ChannelHistoryMinId
                }.Max();

                tDialog.Pts = channelReadModel.Pts;
                tDialog.UnreadCount = channelReadModel.TopMessageId - maxId;
                if (tDialog.UnreadCount < 0)
                {
                    tDialog.UnreadCount = 0;
                }
            }

            //var topMessageId = MessageId.Create(dialogReadModel.ToPeerId, dialogReadModel.TopMessage).Value;
            //if (messages.TryGetValue(topMessageId, out var messageReadModel))
            //{
            //    tDialog.Pts = messageReadModel.Pts;
            //}
            //else
            //{
            //    var firstMessage = messages.Values.FirstOrDefault(p => p.ToPeerId == dialogReadModel.ToPeerId);
            //    if (firstMessage != null)
            //    {
            //        tDialog.Pts = firstMessage.Pts;
            //    }
            //}

            //var maxId = new[]
            //{
            //    dialogReadModel.MaxSendOutMessageId, dialogReadModel.ReadOutboxMaxId, dialogReadModel.ReadInboxMaxId,
            //    dialogReadModel.ChannelHistoryMinId
            //}.Max();
            //tDialog.UnreadCount =
            //    tDialog.TopMessage - maxId; // Math.Max(dialog.MaxSendOutMessageId, tDialog.ReadInboxMaxId);
            //if (tDialog.UnreadCount < 0)
            //{
            //    tDialog.UnreadCount = 0;
            //}

            Console.WriteLine($"{dialogReadModel.ToPeerId} Unread count:{tDialog.UnreadCount}");
        }
        else
        {
            tDialog.Pts = 0;
        }

        return tDialog;
    }

    protected virtual IChatConverter GetChatConverter()
    {
        return _chatConverter ??= _layeredChatService.GetConverter(GetLayer());
    }

    protected virtual IMessageConverter GetMessageConverter()
    {
        return _messageConverter ??= _layeredMessageService.GetConverter(GetLayer());
    }

    protected virtual IUserConverter GetUserConverter()
    {
        return _userConverter ??= _layeredUserService.GetConverter(GetLayer());
    }
}