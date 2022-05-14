using MyTelegram.Schema.Auth;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Contacts;
using MyTelegram.Schema.Help;
using MyTelegram.Schema.Messages;
using MyTelegram.Schema.Phone;
using MyTelegram.Schema.Updates;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;
using IChannelParticipant = MyTelegram.Schema.Channels.IChannelParticipant;
using IChatFull = MyTelegram.Schema.Messages.IChatFull;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;
using IPhoto = MyTelegram.Schema.Photos.IPhoto;
using TAuthorization = MyTelegram.Schema.Auth.TAuthorization;
using TChannelParticipant = MyTelegram.Schema.Channels.TChannelParticipant;
using TChatFull = MyTelegram.Schema.Messages.TChatFull;
using TPhoto = MyTelegram.Schema.Photos.TPhoto;

namespace MyTelegram.MessengerServer.Services.Impl;

public class RpcResultProcessor : IRpcResultProcessor
{
    private readonly IAppSettingManager _appSettingManager;
    private readonly IDataCenterHelper _dataCenterHelper;
    private readonly IObjectMapper _objectMapper;
    private readonly IUserStatusCacheAppService _userStatusAppService;
    private readonly ITlMessageConverter _messageConverter;

    public RpcResultProcessor(IObjectMapper objectMapper,
        IAppSettingManager appSettingManager,
        IUserStatusCacheAppService userStatusAppService,
        IDataCenterHelper dataCenterHelper,
        ITlMessageConverter messageConverter)
    {
        _objectMapper = objectMapper;
        _appSettingManager = appSettingManager;
        _userStatusAppService = userStatusAppService;
        _dataCenterHelper = dataCenterHelper;
        _messageConverter = messageConverter;
    }

    public IFound ToFound(SearchContactOutput output)
    {
        var userList = ToUserList(output.UserList, output.SelfUserId);
        var peerList = output.UserList.Select(p => (IPeer)new TPeerUser { UserId = p.UserId }).ToList();
        peerList.AddRange(output.MyChannelList.Select(p => (IPeer)new TPeerChannel { ChannelId = p.ChannelId }));
        var otherPeerList = output.ChannelList.Select(p => (IPeer)new TPeerChannel { ChannelId = p.ChannelId });
        //var chatList = ToChannelList(output.ChannelList, output.SelfUserId);
        var myChannelList = ToChannelList(output.MyChannelList,
            output.MyChannelList.Select(p => p.ChannelId).ToList(),
            output.ChannelMemberList,
            output.SelfUserId);
        var otherChannelList = ToChannelList(output.ChannelList,
            new List<long>(),
            new List<IChannelMemberReadModel>(),
            output.SelfUserId);
        myChannelList.AddRange(otherChannelList);
        return new TFound
        {
            Chats = new TVector<IChat>(myChannelList),
            MyResults = new TVector<IPeer>(peerList),
            Results = new TVector<IPeer>(otherPeerList),
            Users = new TVector<IUser>(userList)
        };
    }

    public IReadOnlyList<Schema.IAuthorization> ToAuthorizations(IReadOnlyCollection<IDeviceReadModel> deviceList,
        long selfPermAuthKeyId)
    {
        var authList = new List<Schema.IAuthorization>();
        foreach (var deviceReadModel in deviceList)
        {
            authList.Add(ToAuthorization(deviceReadModel, selfPermAuthKeyId));
        }

        return authList;
    }

    public Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel)
    {
        return ToAuthorization(deviceReadModel, -1);
    }

    public IJoinAsPeers ToJoinAsPeers(IUserReadModel userReadModel,
        IChannelReadModel? channelReadModel,
        IChatReadModel? chatReadModel)
    {
        var peerList = new List<IPeer>();
        IChat? chat = null;
        var peer = new TPeerUser { UserId = userReadModel.UserId };
        peerList.Add(peer);
        if (channelReadModel != null)
        {
            peerList.Add(new TPeerChannel { ChannelId = channelReadModel.ChannelId });
            chat = ToChannel(channelReadModel, null, userReadModel.UserId);
        }

        if (chatReadModel != null)
        {
            peerList.Add(new TPeerChat { ChatId = chatReadModel.ChatId });
            chat = ToChat(chatReadModel, userReadModel.UserId);
        }

        return new TJoinAsPeers
        {
            Chats = chat == null ? new TVector<IChat>() : new TVector<IChat>(chat),
            Peers = new TVector<IPeer>(peerList),
            Users = new TVector<IUser>(ToUser(userReadModel, userReadModel.UserId))
        };
    }

    public IUpdates ToDeleteMessagesUpdates(PeerType toPeerType,
        DeletedBoxItem item,
        int date)
    {
        if (toPeerType == PeerType.Channel)
        {
            return new TUpdateShort
            {
                Date = date,
                Update = new TUpdateDeleteChannelMessages
                {
                    ChannelId = item.OwnerPeerId,
                    Messages = new TVector<int>(item.DeletedMessageIdList),
                    Pts = item.Pts,
                    PtsCount = item.PtsCount
                }
            };
        }

        return new TUpdates
        {
            Updates = new TVector<IUpdate>(new TUpdateDeleteMessages
            {
                Messages = new TVector<int>(item.DeletedMessageIdList),
                Pts = item.Pts,
                PtsCount = item.PtsCount
            }),
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Date = date,
            Seq = 0
        };
    }

    public IChannelParticipant ToChannelParticipant(IChannelReadModel channelReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        IUserReadModel userReadModel,
        long selfUserId)
    {
        var participant = ToChannelParticipantCore(channelReadModel, channelMemberReadModel, selfUserId);
        var user = ToUser(userReadModel, selfUserId);
        var channel = ToChannel(channelReadModel, channelMemberReadModel, selfUserId);
        return new TChannelParticipant
        {
            Chats = new TVector<IChat>(channel),
            Participant = participant,
            Users = new TVector<IUser>(user)
        };
    }

    public Schema.LayerN.IChannelParticipant ToChannelParticipantLayerN(IChannelReadModel channelReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        IUserReadModel userReadModel,
        long selfUserId)
    {
        var participant = ToChannelParticipantCore(channelReadModel, channelMemberReadModel, selfUserId);
        var user = ToUser(userReadModel, selfUserId);
        //var channel = ToChannel(channelReadModel, selfUserId);
        return new Schema.LayerN.TChannelParticipant { Participant = participant, Users = new TVector<IUser>(user) };
    }

    public IChannelParticipants ToChannelParticipants(IChannelReadModel channelReadModel,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        IReadOnlyCollection<IUserReadModel> userReadModels,
        long selfUserId,
        DeviceType deviceType,
        bool forceNotLeft)
    {
        var participants = ToChannelParticipantsCore(channelMemberReadModels, channelReadModel);
        var users = ToUserList(userReadModels, selfUserId);
        var channelMemberReadModel = channelMemberReadModels.FirstOrDefault(p => p.UserId == selfUserId);
        var channelMemberIsLeft = true;
        if (channelMemberReadModel == null)
        {
            if (forceNotLeft)
            {
                channelMemberIsLeft = false;
            }
        }
        else
        {
            channelMemberIsLeft = channelMemberReadModel.Left;
        }

        var channel = ToChannel(channelReadModel, channelMemberReadModel, selfUserId, channelMemberIsLeft);

        return new TChannelParticipants
        {
            Chats = new TVector<IChat>(channel),
            Count = participants.Count,
            Participants = new TVector<Schema.IChannelParticipant>(participants),
            Users = new TVector<IUser>(users)
        };
    }

    public IChannelDifference ToChannelDifference(GetMessageOutput output,
        bool isChannelMember,
        IList<IUpdate> updatesList,
        int updatesMaxPts = 0,
        bool resetLeftToFalse = false)
    {
        var timeout = _appSettingManager.GetIntSetting(MyTelegramServerConsts.ChannelGetDifferenceIntervalSeconds);
        if (output.MessageList.Count == 0 && updatesList.Count == 0)
        {
            return new TChannelDifferenceEmpty { Final = true, Pts = output.Pts, Timeout = timeout };
        }

        var maxPts = updatesMaxPts;
        if (output.MessageList.Count > 0)
        {
            var boxMaxPts = output.MessageList.Max(p => p.Pts);
            maxPts = Math.Max(updatesMaxPts, boxMaxPts);
        }

        var messageList = _messageConverter.ToMessages(output.MessageList, output.SelfUserId);
        var chatList = ToChatList(output.ChatList, output.SelfUserId);
        var channelList = ToChannelList(output.ChannelList,
            output.JoinedChannelIdList,
            output.ChannelMemberList,
            output.SelfUserId,
            resetLeftToFalse
        );
        var userList = ToUserList(output.UserList, output.SelfUserId);
        chatList.AddRange(channelList);
        return new TChannelDifference
        {
            Final = output.Pts == maxPts,
            Pts = maxPts,
            Users = new TVector<IUser>(userList),
            OtherUpdates = new TVector<IUpdate>(updatesList),
            Timeout = timeout,
            Chats = new TVector<IChat>(chatList),
            NewMessages = new TVector<IMessage>(messageList)
        };
    }

    public IExportedChatInvite ToExportedChatInvite(ExportChatInviteEvent eventData)
    {
        var item = _objectMapper.Map<ExportChatInviteEvent, TChatInviteExported>(eventData);
        item.Link = $"{_appSettingManager.GetSetting(MyTelegramServerConsts.JoinChatDomain)}/{item.Link}";

        return item;
    }

    public List<IChat> ToChannelList(
        IReadOnlyCollection<IChannelReadModel> channelReadModels,
        IReadOnlyCollection<long> joinedChannelIdList,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        long selfUserId,
        bool resetLeftToFalse = false)
    {
        var channelList = new List<IChat>();
        var memberDict = channelMemberReadModels.ToDictionary(k => k.ChannelId, v => v);
        foreach (var channelReadModel in channelReadModels)
        {
            memberDict.TryGetValue(channelReadModel.ChannelId, out var memberReadModel);

            var channel = ToChannel(channelReadModel, memberReadModel, selfUserId);
            if (channel is TChannel chat)
            {
                if (!joinedChannelIdList.Contains(channelReadModel.ChannelId))
                {
                    chat.Left = true;
                }

                if (resetLeftToFalse)
                {
                    chat.Left = false;
                }
            }

            channelList.Add(channel);
        }

        return channelList;
    }

    public IChat ToChannel(IChannelReadModel channelReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        long selfUserId)
    {
        return ToChannel(channelReadModel,
            channelMemberReadModel,
            selfUserId,
            channelMemberReadModel == null || channelMemberReadModel.Left);
    }

    public IUpdates ToReadHistoryUpdates(ReadHistoryCompletedEvent eventData)
    {
        var peer = eventData.ReaderToPeer.PeerType == PeerType.User
            ? new TPeerUser { UserId = eventData.ReaderUid }
            : eventData.ReaderToPeer.ToPeer();
        var updateReadHistoryOutbox = new TUpdateReadHistoryOutbox
        {
            Pts = eventData.SenderPts,
            MaxId = eventData.SenderMessageId,
            PtsCount = 1,
            Peer = peer
        };

        var updates = new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Updates = new TVector<IUpdate>(updateReadHistoryOutbox),
            Users = new TVector<IUser>(),
            Seq = 0
        };

        return updates;
    }

    public IDifference ToDifference(GetMessageOutput output,
        IPtsReadModel? pts,
        int cachedPts,
        int limit,
        IList<IUpdate> updateList,
        IList<IChat> chatListFromUpdates)
    {
        var messageList = _messageConverter.ToMessages(output.MessageList, output.SelfUserId);
        var userList = ToUserList(output.UserList, output.SelfUserId);
        var chatList = ToChatList(output.ChatList, output.SelfUserId);
        chatList.AddRange(chatListFromUpdates);
        var channelList = ToChannelList(output.ChannelList,
            output.JoinedChannelIdList,
            output.ChannelMemberList,
            output.SelfUserId,
            true);
        chatList.AddRange(channelList);

        if (updateList.Count == limit)
        {
            var differenceSlice = new TDifferenceSlice
            {
                Chats = new TVector<IChat>(chatList),
                NewEncryptedMessages = new TVector<IEncryptedMessage>(),
                NewMessages = new TVector<IMessage>(messageList),
                OtherUpdates = new TVector<IUpdate>(updateList),
                Users = new TVector<IUser>(userList),
                IntermediateState = pts == null ? new TState
                {
                    Date = DateTime.UtcNow.ToTimestamp()
                } : _objectMapper.Map<IPtsReadModel, TState>(pts)
            };

            return differenceSlice;
        }

        var difference = new TDifference
        {
            Chats = new TVector<IChat>(chatList),
            NewEncryptedMessages = new TVector<IEncryptedMessage>(),
            NewMessages = new TVector<IMessage>(messageList),
            OtherUpdates = new TVector<IUpdate>(updateList),
            Users = new TVector<IUser>(userList),
            State = pts == null ? new TState
            {
                Date = DateTime.UtcNow.ToTimestamp()
            } : _objectMapper.Map<IPtsReadModel, TState>(pts)
        };
        if (cachedPts > pts?.Pts)
        {
            difference.State.Pts = cachedPts;
        }

        return difference;
    }

    public IAuthorization CreateAuthorization(SignInSuccessEvent aggregateEvent)
    {
        var tUser = _objectMapper.Map<SignInSuccessEvent, TUser>(aggregateEvent);
        tUser.Phone = aggregateEvent.PhoneNumber;
        tUser.Photo = new TUserProfilePhotoEmpty();
        tUser.Id = aggregateEvent.UserId;
        tUser.Status = new TUserStatusOnline { Expires = DateTime.UtcNow.AddMinutes(5).ToTimestamp() };
        tUser.Self = true;
        var r = new TAuthorization { User = tUser };

        return r;
    }

    public IAuthorization CreateSignUpAuthorization()
    {
        return new TAuthorizationSignUpRequired
        {
            TermsOfService = new TTermsOfService
            {
                Entities = new TVector<IMessageEntity>(),
                Id = new TDataJSON
                {
                    Data =
                        "{\"country\":\"US\",\"min_age\":false,\"terms_key\":\"TERMS_OF_SERVICE\",\"terms_lang\":\"en\",\"terms_version\":1,\"terms_hash\":\"7dca806cb8d387c07c778ce9ef6aac04\"}"
                },
                Text =
                    "By signing up for MyTelegram, you agree not to:\n\n- Use our service to send spam or scam users.\n- Promote violence on publicly viewable Telegram bots, groups or channels.\n- Post pornographic content on publicly viewable MyTelegram bots, groups or channels.\n\nWe reserve the right to update these Terms of Service later."
            }
        };
    }

    public IAuthorization CreateAuthorizationFromUser(IUserReadModel? user)
    {
        if (user == null)
        {
            return new TAuthorizationSignUpRequired
            {
                TermsOfService = new TTermsOfService
                {
                    Entities = new TVector<IMessageEntity>(),
                    Id = new TDataJSON
                    {
                        Data =
                            "{\"country\":\"US\",\"min_age\":false,\"terms_key\":\"TERMS_OF_SERVICE\",\"terms_lang\":\"en\",\"terms_version\":1,\"terms_hash\":\"7dca806cb8d387c07c778ce9ef6aac04\"}"
                    },
                    Text =
                        "By signing up for MyTelegram, you agree not to:\n\n- Use our service to send spam or scam users.\n- Promote violence on publicly viewable Telegram bots, groups or channels.\n- Post pornographic content on publicly viewable MyTelegram bots, groups or channels.\n\nWe reserve the right to update these Terms of Service later."
                }
            };
        }

        var tUser = ToUser(user, user.UserId);
        var r = new TAuthorization { User = tUser };

        return r;
    }

    public IAuthorization CreateAuthorizationFromUser(UserCreatedEvent userCreatedEvent)
    {
        var tUser = ToUser(userCreatedEvent);
        return new TAuthorization { User = tUser };
    }

    public IPhoto ToPhoto(UserProfilePhotoChangedEvent aggregateEvent)
    {
        var user = ToUser(aggregateEvent.UserItem);
        var photo = aggregateEvent.UserItem.ProfilePhoto.ToTObject<Schema.IPhoto>();
        return new TPhoto { Photo = photo, Users = new TVector<IUser>(user) };
    }

    public IUser ToUser(UserNameUpdatedEvent aggregateEvent)
    {
        return ToUser(aggregateEvent.UserItem);
        //var tUser = _objectMapper.Map<UserItem, TUser>(aggregateEvent.UserItem);
        //tUser.Id = aggregateEvent.UserItem.UserId;
        //tUser.Self = true;
        //tUser.Photo = new TUserProfilePhotoEmpty();
        //tUser.Status = _userStatusAppService.GetUserStatus(aggregateEvent.UserItem.UserId);

        //return tUser;
    }

    public IUser ToUser(IUserReadModel user,
        long selfUserId)
    {
        var tUser = _objectMapper.Map<IUserReadModel, TUser>(user);
        tUser.Self = selfUserId == user.UserId;
        tUser.Photo = GetProfilePhoto(user.ProfilePhoto);

        //var cachedStatus = _userStatusAppService.GetUserStatus(user.UserId);

        tUser.Status =
            _userStatusAppService
                .GetUserStatus(user
                    .UserId); // GetUserStatus(cachedStatus?.LastUpdateDate ?? user.LastUpdateDate, cachedStatus?.Online ?? user.IsOnline);
        if (user.Bot)
        {
            tUser.BotInfoVersion = 1;
            tUser.BotChatHistory = true; // bot.AllowAccessGroupMessages;
        }

        return tUser;
    }

    public IList<IUser> ToUserList(IReadOnlyCollection<IUserReadModel> userList,
        long selfUserId)
    {
        var tUserList = new List<IUser>();
        foreach (var user in userList)
        {
            tUserList.Add(ToUser(user, selfUserId));
        }

        return tUserList;
    }

    public Task<MyTelegram.Schema.Users.IUserFull> ToUserFullAsync(IUserReadModel user,
        long fromUid,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel
    )
    {
        //var disallowPhoneCall = false;
        var isOfficialId = user.UserId == MyTelegramServerDomainConsts.OfficialUserId;
        var tUser = ToUser(user, fromUid);
        var userFull = new MyTelegram.Schema.Users.TUserFull()
        {
            Chats = new TVector<IChat>(),
            FullUser = new TUserFull
            {
                Id=user.UserId,
                About = user.About,
                Blocked = false,
                CanPinMessage = !isOfficialId,
                PhoneCallsAvailable = !user.Bot && !isOfficialId,
                VideoCallsAvailable = !user.Bot && !isOfficialId,
                PhoneCallsPrivate = isOfficialId,
                // FolderId = 0,
                PinnedMsgId = user.PinnedMsgId,
                ProfilePhoto = user.ProfilePhoto.ToTObject<Schema.IPhoto>() ?? new TPhotoEmpty(),
                Settings = new MyTelegram.Schema.TPeerSettings(),
                NotifySettings =
                    _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(
                        peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings),
            },
            Users = new TVector<IUser>(tUser)
        };

        return Task.FromResult<MyTelegram.Schema.Users.IUserFull>(userFull);
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

        var userList = ToUserList(output.UserList, output.SelfUserId);
        var chatList = ToChatList(output.ChatList, output.SelfUserId);

        var channelList = ToChannelList(output.ChannelList,
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

    public IChatFull ToChatFull(IChannelReadModel channelReadModel,
        IChannelFullReadModel channelFullReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        long selfUserId
    )
    {
        var channel = ToChannel(channelReadModel, channelMemberReadModel, selfUserId);
        return new TChatFull
        {
            Chats = new TVector<IChat>(channel),
            FullChat = ToChannelFull(channelReadModel, channelFullReadModel, peerNotifySettingsReadModel, selfUserId),
            Users = new TVector<IUser>()
        };
    }

    public IChat ToChat(IChatReadModel chat,
        long userId)
    {
        if (chat.ChatMembers.All(p => p.UserId != userId))
        {
            return new TChatForbidden { Id = chat.ChatId, Title = chat.Title };
        }

        var tChat = _objectMapper.Map<IChatReadModel, TChat>(chat);
        tChat.Id = chat.ChatId;
        tChat.Creator = chat.CreatorUid == userId;
        tChat.Photo = GetChatPhoto(chat.Photo);
        tChat.ParticipantsCount = chat.ChatMembers.Count;
        tChat.DefaultBannedRights ??=
            _objectMapper.Map<ChatBannedRights, TChatBannedRights>(new ChatBannedRights());

        return tChat;
    }

    public IChatFull ToChatFull(IChatReadModel chat,
        IReadOnlyCollection<IUserReadModel> userList,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        long selfUserId)
    {
        var tChat = ToChat(chat, selfUserId);
        var fullChat = new Schema.TChatFull
        {
            About = chat.About ?? string.Empty,
            CanSetUsername = true,
            Id = chat.ChatId,
            ChatPhoto = chat.Photo.ToTObject<Schema.IPhoto>() ?? new TPhotoEmpty(),
            Participants = ToChatParticipants(chat.ChatId,
                chat.ChatMembers,
                chat.Date,
                chat.CreatorUid,
                (int)(chat.Version ?? 0)),
            NotifySettings = _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(
                peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings)
        };
        var tUserList = ToUserList(userList, selfUserId);
        var chatFull = new TChatFull
        {
            Chats = new TVector<IChat>(tChat),
            Users = new TVector<IUser>(tUserList),
            FullChat = fullChat
        };

        return chatFull;
    }

    public IMessages ToMessages(GetMessageOutput output)
    {
        var messageList = _messageConverter.ToMessages(output.MessageList, output.SelfUserId);
        var userList = ToUserList(output.UserList, output.SelfUserId);
        var chatList = ToChatList(output.ChatList, output.SelfUserId);
        var channelList = ToChannelList(output.ChannelList,
            output.JoinedChannelIdList,
            output.ChannelMemberList,
            output.SelfUserId);
        chatList.AddRange(channelList);

        if (output.MessageList.All(p => p.ToPeerType == PeerType.Channel) && !output.IsSearchGlobal)
        {
            var offsetId = messageList.Any() ? messageList.Max(p => p.Id) : 0;
            //var messageIdList=messageList.Select()
            //var offsetId = output.HasMoreData && messageList.Any() ? messageList.Min(p => p.Id) : 0;
            //if(messageList.Count==output.l)
            // Console.WriteLine($"offsetId={offsetId}");
            var channelPts = output.ChannelList.FirstOrDefault()?.Pts ?? output.Pts;
            //var channelPts = output.MessageList.Any() ? output.MessageList.Min(p => p.Pts) : output.Pts;

            return new TChannelMessages
            {
                Chats = new TVector<IChat>(chatList),
                Messages = new TVector<IMessage>(messageList),
                Users = new TVector<IUser>(userList),
                Pts = channelPts,
                Count = messageList.Count,
                OffsetIdOffset = offsetId
            };
        }

        return new TMessages
        {
            Chats = new TVector<IChat>(chatList),
            Messages = new TVector<IMessage>(messageList),
            Users = new TVector<IUser>(userList)
        };
    }

    public TChannelFull ToChannelFull(IChannelReadModel channelReadModel,
        IChannelFullReadModel channelFullReadModel,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        long selfUserId
    )
    {
        //var channel = ToChannel(selfUserId, channelReadModel);
        var channelFull = _objectMapper.Map<IChannelFullReadModel, TChannelFull>(channelFullReadModel);
        channelFull.About ??= string.Empty;
        channelFull.ChatPhoto = channelReadModel.Photo.ToTObject<Schema.IPhoto>() ?? new TPhotoEmpty();
        channelFull.NotifySettings =
            _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(
                peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings);

        channelFull.BotInfo = new TVector<IBotInfo>();
        channelFull.Id = channelFullReadModel.ChannelId;
        channelFull.Pts = channelReadModel.Pts;
        channelFull.ParticipantsCount = channelReadModel.ParticipantsCount;

        // Only creator and channel admin can view participants list for broadcast
        if (channelReadModel.Broadcast)
        {
            if (channelReadModel.CreatorId == selfUserId ||
                channelReadModel.AdminList.FirstOrDefault(p => p.UserId == selfUserId) != null)
            {
                channelFull.CanViewParticipants = true;
            }
            else
            {
                channelFull.CanViewParticipants = false;
            }
        }

        if (selfUserId == MyTelegramServerDomainConsts.LeftChannelUid)
        {
            channelFull.CanViewParticipants = false;
            channelFull.CanSetUsername = false;
        }

        if (channelReadModel.CreatorId == selfUserId)
        {
            channelFull.CanSetUsername = true;
        }

        if (channelFull.SlowmodeSeconds > 0)
        {
            if (selfUserId != channelReadModel.CreatorId && selfUserId == channelReadModel.LastSenderPeerId)
            {
                var nextSendDate = channelReadModel.LastSendDate + channelFull.SlowmodeSeconds;
                channelFull.SlowmodeNextSendDate = nextSendDate;
            }
        }

        return channelFull;
    }

    public List<IChat> ToChatList(IReadOnlyCollection<IChatReadModel> chatList,
        long userId)
    {
        var tChatList = new List<IChat>();
        foreach (var chatReadModel in chatList)
        {
            tChatList.Add(ToChat(chatReadModel, userId));
        }

        return tChatList;
    }

    private static IChatPhoto GetChatPhoto(byte[]? photo)
    {
        if (photo?.Length > 0)
        {
            var tlObject = photo.ToTObject<IObject>();
            if (tlObject is Schema.TPhoto tPhoto)
            {
                return new TChatPhoto
                {
                    DcId = tPhoto.DcId,
                    HasVideo = tPhoto.VideoSizes?.Count > 0,
                    PhotoId = tPhoto.Id
                    //PhotoSmall = new TFileLocationToBeDeprecated
                    //{
                    //    VolumeId = tPhoto.Id,
                    //    LocalId = 1
                    //},
                    //PhotoBig = new TFileLocationToBeDeprecated
                    //{
                    //    VolumeId = tPhoto.Id,
                    //    LocalId = 1
                    //}
                };
            }

            var chatPhoto = photo.ToTObject<IChatPhoto>();
            return chatPhoto;
            //var hasVideo = false;
            //var id = 0L;
            //switch (chatPhoto)
            //{
            //    case TChatPhoto chatPhoto1:
            //        return chatPhoto1;
            //    case Schema.TChatPhoto chatPhoto2:
            //        hasVideo = chatPhoto2.HasVideo;
            //        id = chatPhoto2.PhotoId;
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(chatPhoto));
            //}

            //return new TChatPhoto()
            //{
            //    DcId = MyTelegramServerDomainConsts.MediaDcId,
            //    PhotoSmall = new TFileLocationToBeDeprecated
            //    {
            //        VolumeId = id,
            //        LocalId = 1,
            //    },
            //    PhotoBig = new TFileLocationToBeDeprecated
            //    {
            //        VolumeId = id,
            //        LocalId = 1,
            //    },
            //    HasVideo = hasVideo

            //    //PhotoId = chatPhoto.Id,
            //    //StrippedThumb = s1?.Bytes
            //};
        }

        return new TChatPhotoEmpty();
    }

    private IUserProfilePhoto GetProfilePhoto(byte[]? profilePhoto)
    {
        if (profilePhoto?.Length > 0)
        {
            var photo = profilePhoto.ToTObject<Schema.IPhoto>();
            var hasVideo = false;
            if (photo is Schema.TPhoto tPhoto)
            {
                hasVideo = tPhoto.VideoSizes?.Count > 0;
            }

            return new TUserProfilePhoto
            {
                DcId = _dataCenterHelper.GetMediaDcId(),
                PhotoId = photo.Id,

                //PhotoBig = new TFileLocationToBeDeprecated
                //{
                //    VolumeId = photo.Id,
                //    LocalId = 1,
                //},
                //PhotoSmall = new TFileLocationToBeDeprecated
                //{
                //    VolumeId = photo.Id,
                //    LocalId = 1
                //},
                HasVideo = hasVideo
            };
        }

        return new TUserProfilePhotoEmpty();
    }

    private Schema.IAuthorization ToAuthorization(IDeviceReadModel deviceReadModel,
        long selfPermAuthKeyId)
    {
        var auth = _objectMapper.Map<IDeviceReadModel, Schema.TAuthorization>(deviceReadModel);
        auth.AppName = deviceReadModel.LangPack;
        auth.Country = "TestCountry";
        auth.Region = string.Empty;

        auth.Current = selfPermAuthKeyId == deviceReadModel.PermAuthKeyId;
        // TODO:Use ip2region to query country by ip
        return auth;
    }

    private IChat ToChannel(IChannelReadModel channelReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        long selfUserId,
        bool channelMemberIsLeft)
    {
        if (channelMemberReadModel is { Kicked: true })
        {
            return new TChannelForbidden
            {
                Broadcast = channelReadModel.Broadcast,
                AccessHash = channelReadModel.AccessHash,
                Id = channelReadModel.ChannelId,
                Title = channelReadModel.Title,
                Megagroup = channelReadModel.MegaGroup,
                UntilDate = channelMemberReadModel.UntilDate
            };
        }

        var channel = _objectMapper.Map<IChannelReadModel, TChannel>(channelReadModel);

        channel.Creator = channelReadModel.CreatorId == selfUserId;
        channel.Photo = GetChatPhoto(channelReadModel.Photo);

        if (channelMemberIsLeft)
        {
            channel.Left = true;
        }
        else
        {
            if (channelMemberReadModel != null && channelMemberReadModel.BannedRights != 0)
            {
                var bannedRights = _objectMapper.Map<ChatBannedRights, TChatBannedRights>(
                    ChatBannedRights.FromValue(channelMemberReadModel.BannedRights,
                        channelMemberReadModel.UntilDate));
                channel.BannedRights = bannedRights;
            }
        }

        if (channel.Creator)
        {
            channel.AdminRights =
                _objectMapper.Map<ChatAdminRights, TChatAdminRights>(ChatAdminRights.GetCreatorRights());
        }
        else
        {
            var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == selfUserId);
            if (admin != null)
            {
                channel.AdminRights = _objectMapper.Map<ChatAdminRights, TChatAdminRights>(admin.AdminRights);
            }
        }

        return channel;
    }

    private Schema.IChannelParticipant ToChannelParticipantCore(IChannelReadModel channelReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        long selfUserId)
    {
        var bannedRights = _objectMapper.Map<ChatBannedRights, TChatBannedRights>(
            ChatBannedRights.FromValue(channelMemberReadModel.BannedRights,
                channelMemberReadModel.UntilDate));
        if (channelMemberReadModel.Kicked ||
            (channelMemberReadModel.BannedRights != ChatBannedRights.Default.ToIntValue() &&
             !channelMemberReadModel.Left))
        {
            return new TChannelParticipantBanned
            {
                BannedRights = bannedRights,
                Date = channelMemberReadModel.Date,
                Peer = new TPeerUser { UserId = channelMemberReadModel.UserId },
                KickedBy = channelMemberReadModel.KickedBy,
                Left = false
            };
        }

        if (channelMemberReadModel.Left)
        {
            return new TChannelParticipantLeft { Peer = new TPeerUser { UserId = channelMemberReadModel.UserId } };
        }

        if (channelMemberReadModel.UserId == channelReadModel.CreatorId)
        {
            return new TChannelParticipantCreator
            {
                UserId = channelMemberReadModel.UserId,
                AdminRights = new TChatAdminRights()
            };
        }

        var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == channelMemberReadModel.UserId);
        if (admin != null)
        {
            return new TChannelParticipantAdmin
            {
                AdminRights = _objectMapper.Map<ChatAdminRights, TChatAdminRights>(admin.AdminRights),
                Date = channelMemberReadModel.Date,
                InviterId = channelMemberReadModel.InviterId,
                Rank = admin.Rank,
                UserId = admin.UserId,
                Self = channelMemberReadModel.UserId == selfUserId,
                CanEdit = admin.CanEdit,
                PromotedBy = admin.PromotedBy
            };
        }

        if (channelMemberReadModel.UserId == selfUserId)
        {
            return new TChannelParticipantSelf
            {
                Date = channelMemberReadModel.Date,
                InviterId = channelMemberReadModel.InviterId,
                UserId = channelMemberReadModel.UserId
            };
        }

        return new Schema.TChannelParticipant
        {
            UserId = channelMemberReadModel.UserId,
            Date = channelMemberReadModel.Date
        };
    }

    private IReadOnlyList<Schema.IChannelParticipant> ToChannelParticipantsCore(
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        IChannelReadModel channelReadModel)
    {
        var participants = new List<Schema.IChannelParticipant>();
        foreach (var channelMemberReadModel in channelMemberReadModels)
        {
            participants.Add(ToChannelParticipantCore(channelReadModel, channelMemberReadModel, 0));
        }

        return participants;
    }

    private static TChatParticipants ToChatParticipants(long chatId,
        IReadOnlyList<ChatMember> chatMemberList,
        int date,
        long creatorUid,
        int chatVersion)
    {
        var participants = chatMemberList.Select(p =>
        {
            if (p.UserId == creatorUid)
            {
                return (IChatParticipant)new TChatParticipantCreator { UserId = p.UserId };
            }

            return new TChatParticipant { Date = date, InviterId = creatorUid, UserId = p.UserId };
        }).ToList();

        return new TChatParticipants
        {
            ChatId = chatId,
            Participants = new TVector<IChatParticipant>(participants),
            Version = chatVersion
        };
    }


    private IUser ToUser(UserItem userItem)
    {
        var tUser = _objectMapper.Map<UserItem, TUser>(userItem);
        tUser.Id = userItem.UserId;
        tUser.Self = true;
        tUser.Status = _userStatusAppService.GetUserStatus(userItem.UserId);
        tUser.Photo = GetProfilePhoto(userItem.ProfilePhoto);

        return tUser;
    }

    private IUser ToUser(UserCreatedEvent aggregateEvent)
    {
        var tUser = _objectMapper.Map<UserCreatedEvent, TUser>(source: aggregateEvent);
        tUser.Self = true;
        tUser.Photo = new TUserProfilePhotoEmpty();

        return tUser;
    }
}
