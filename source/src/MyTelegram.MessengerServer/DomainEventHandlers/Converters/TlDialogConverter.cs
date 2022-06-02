using MyTelegram.Schema.Messages;
using MyTelegram.Schema.Updates;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public class TlDialogConverter : ITlDialogConverter
{
    private readonly ITlChatConverter _chatConverter;
    private readonly ITlUserConverter _userConverter;
    private readonly ITlMessageConverter _messageConverter;
    private readonly IObjectMapper _objectMapper;

    public TlDialogConverter(ITlChatConverter chatConverter,
        ITlUserConverter userConverter,
        ITlMessageConverter messageConverter,
        IObjectMapper objectMapper)
    {
        _chatConverter = chatConverter;
        _userConverter = userConverter;
        _messageConverter = messageConverter;
        _objectMapper = objectMapper;
    }

    public IDialogs ToDialogs(GetDialogOutput output)
    {
        var dialogs = new List<IDialog>();
        var boxDict = output.MessageList.Where(p => p.ToPeerType == PeerType.Channel)
            .ToDictionary(k => k.Id, v => v);
        //var peerNotifySettingsDict = output.PeerNotifySettingList.ToDictionary(k => k.PeerId, v => v);
        foreach (var dialog in output.DialogList)
        {
            var tDialog = _objectMapper.Map<IDialogReadModel, TDialog>(dialog);
            //if (dialog.Draft?.Message?.Length > 0)
            //{
            //    tDialog.Draft = new TDraftMessage
            //    {
            //        Date = dialog.Draft.Date,
            //        Message = dialog.Draft.Message,
            //        Entities = dialog.Draft.Entities.ToTObject<TVector<IMessageEntity>>(),
            //        NoWebpage = dialog.Draft.NoWebpage,
            //        ReplyToMsgId = dialog.Draft.ReplyToMsgId,
            //    };
            //}
            //tDialog.Peer = new Peer(dialog.ToPeerType, dialog.ToPeerId).ToPeer();
            if (dialog.ToPeerType == PeerType.Channel)
            {
                var topMessageBoxId = MessageId.Create(dialog.ToPeerId, dialog.TopMessage).Value;
                if (boxDict.TryGetValue(topMessageBoxId, out var box))
                {
                    tDialog.Pts = box.Pts;
                }
                else
                {
                    var firstBox = output.MessageList.FirstOrDefault(p => p.ToPeerId == dialog.ToPeerId);
                    if (firstBox != null)
                    {
                        tDialog.Pts = firstBox.Pts;
                    }
                }

                var maxId = new[] {
                    dialog.MaxSendOutMessageId, dialog.ReadOutboxMaxId, dialog.ReadInboxMaxId,
                    dialog.ChannelHistoryMinId
                }.Max();
                tDialog.UnreadCount =
                    tDialog.TopMessage - maxId; // Math.Max(dialog.MaxSendOutMessageId, tDialog.ReadInboxMaxId);
                // unread count need calculate when fetch messages
                // Console.WriteLine($"### {output.SelfUserId} Dialog:{dialog.ToPeerType}_{dialog.ToPeerId} unreadCount:{tDialog.UnreadCount} pts:{tDialog.Pts} topMsgId:{tDialog.TopMessage}");
            }
            else
            {
                tDialog.Pts = 0;
            }

            ////PeerNotifySettings peerNotifySettings = null;
            //var peerNotifySettings = peerNotifySettingsDict.TryGetValue(dialog.ToPeerId, out var peerNotifySettingsReadModel) ? peerNotifySettingsReadModel.NotifySettings : new PeerNotifySettings();

            //tDialog.NotifySettings ??=
            //    _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(peerNotifySettings);

            //if (tDialog.NotifySettings == null)
            //{
            //    tDialog.NotifySettings = new TPeerNotifySettings
            //    {
            //        ShowPreviews = true,
            //        Silent = false,
            //        Sound = "default",
            //        MuteUntil = 0
            //    };
            //}

            dialogs.Add(tDialog);
        }

        var userList = _userConverter.ToUserList(output.UserList, output.SelfUserId);
        var chatList = _chatConverter.ToChatList(output.ChatList, output.SelfUserId);

        var channelList = _chatConverter.ToChannelList(output.ChannelList,
            output.ChannelList.Select(p => p.ChannelId).ToList(),
            output.ChannelMemberList,
            output.SelfUserId);
        chatList.AddRange(channelList);

        var allBoxList = output.MessageList.ToList();

        foreach (var chat in channelList)
        {
            if (chat is TChannelForbidden channelForbidden)
            {
                allBoxList.RemoveAll(p => p.ToPeerId == channelForbidden.Id);
            }
        }

        var messageList = _messageConverter.ToMessages(allBoxList, output.SelfUserId);

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
}