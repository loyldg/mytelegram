using MyTelegram.Schema.Channels;
using IChannelParticipant = MyTelegram.Schema.IChannelParticipant;
using IChatFull = MyTelegram.Schema.Messages.IChatFull;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;
using TChannelParticipant = MyTelegram.Schema.TChannelParticipant;
using TChatFull = MyTelegram.Schema.TChatFull;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public class TlChatConverter : ITlChatConverter
{
    private readonly IAppSettingManager _appSettingManager;
    private readonly IObjectMapper _objectMapper;
    private readonly ITlPhotoConverter _photoConverter;
    private readonly ITlUserConverter _userConverter;

    public TlChatConverter(IObjectMapper objectMapper,
        ITlPhotoConverter photoConverter,
        ITlUserConverter userConverter,
        IAppSettingManager appSettingManager)
    {
        _objectMapper = objectMapper;
        _photoConverter = photoConverter;
        _userConverter = userConverter;
        _appSettingManager = appSettingManager;
    }

    public IChat ToChannel(IChannelReadModel channelReadModel,
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
        channel.Photo = _photoConverter.GetChatPhoto(channelReadModel.Photo);

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

    public TChannel ToChannel(long selfUserId,
        long channelId,
        string title,
        long creatorUid,
        bool broadcast,
        bool megaGroup,
        long accessHash,
        int date,
        int participantsCount)
    {
        var channel = new TChannel
        {
            Id = channelId,
            AccessHash = accessHash,
            Broadcast = broadcast,
            Creator = selfUserId == creatorUid,
            Date = date,
            Title = title,
            Megagroup = megaGroup,
            Photo = new TChatPhotoEmpty(),
            ParticipantsCount = participantsCount,
            DefaultBannedRights = _objectMapper.Map<ChatBannedRights, TChatBannedRights>(new ChatBannedRights())
        };

        return channel;
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

    public TChannelFull ToChannelFull(IChannelReadModel channelReadModel,
        IChannelFullReadModel channelFullReadModel,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        long selfUserId)
    {
        //var channel = ToChannel(selfUserId, channelReadModel);
        var channelFull = _objectMapper.Map<IChannelFullReadModel, TChannelFull>(channelFullReadModel);
        channelFull.About ??= string.Empty;
        channelFull.ChatPhoto = channelReadModel.Photo.ToTObject<IPhoto>() ?? new TPhotoEmpty();
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
            channelFull.CanDeleteChannel = true;
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

    public List<IChat> ToChannelList(IReadOnlyCollection<IChannelReadModel> channelReadModels,
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

    public IChannelParticipant ToChannelParticipant(IChannelReadModel channelReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        IUserReadModel userReadModel,
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

        return new TChannelParticipant
        {
            UserId = channelMemberReadModel.UserId,
            Date = channelMemberReadModel.Date
        };
    }

    public Schema.LayerN.IChannelParticipant ToChannelParticipantLayerN(IChannelReadModel channelReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        IUserReadModel userReadModel,
        long selfUserId)
    {
        var participant = ToChannelParticipantCore(channelReadModel, channelMemberReadModel, selfUserId);
        var user = _userConverter.ToUser(userReadModel, selfUserId);
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
        var users = _userConverter.ToUserList(userReadModels, selfUserId);
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
            Participants = new TVector<IChannelParticipant>(participants),
            Users = new TVector<IUser>(users)
        };
    }

    public IChat ToChat(long chatId,
                                            string title,
        int date,
        int memberCount)
    {
        var tChat = new TChat
        {
            DefaultBannedRights = _objectMapper.Map<ChatBannedRights, TChatBannedRights>(new ChatBannedRights()),
            CallActive = true,
            CallNotEmpty = true,
            Creator = true,
            Date = date,
            Id = chatId,
            AdminRights = new TChatAdminRights(),
            Deactivated = false,
            //Kicked = false,
            Title = title,
            ParticipantsCount = memberCount,
            Left = false,
            Photo = new TChatPhotoEmpty()
        };
        return tChat;
    }
    public IChat ToChat(IChatReadModel chat,
        long selfUserId)
    {
        if (chat.ChatMembers.All(p => p.UserId != selfUserId))
        {
            return new TChatForbidden { Id = chat.ChatId, Title = chat.Title };
        }

        var tChat = _objectMapper.Map<IChatReadModel, TChat>(chat);
        tChat.Id = chat.ChatId;
        tChat.Creator = chat.CreatorUid == selfUserId;
        tChat.Photo = _photoConverter.GetChatPhoto(chat.Photo);
        tChat.ParticipantsCount = chat.ChatMembers.Count;
        tChat.DefaultBannedRights ??=
            _objectMapper.Map<ChatBannedRights, TChatBannedRights>(new ChatBannedRights());

        return tChat;
    }

    public IChatFull ToChatFull(IChatReadModel chat,
        IReadOnlyCollection<IUserReadModel> userList,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        long selfUserId)
    {
        var tChat = ToChat(chat, selfUserId);
        var fullChat = new TChatFull
        {
            About = chat.About ?? string.Empty,
            CanSetUsername = true,
            Id = chat.ChatId,
            ChatPhoto = chat.Photo.ToTObject<IPhoto>() ?? new TPhotoEmpty(),
            Participants = ToChatParticipants(chat.ChatId,
                chat.ChatMembers,
                chat.Date,
                chat.CreatorUid,
                (int)(chat.Version ?? 0)),
            NotifySettings = _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(
                peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings)
        };
        var tUserList = _userConverter.ToUserList(userList, selfUserId);
        var chatFull = new Schema.Messages.TChatFull
        {
            Chats = new TVector<IChat>(tChat),
            Users = new TVector<IUser>(tUserList),
            FullChat = fullChat
        };

        return chatFull;
    }

    public IChatFull ToChatFull(IChannelReadModel channelReadModel,
        IChannelFullReadModel channelFullReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        long selfUserId)
    {
        var channel = ToChannel(channelReadModel, channelMemberReadModel, selfUserId);
        return new Schema.Messages.TChatFull
        {
            Chats = new TVector<IChat>(channel),
            FullChat = ToChannelFull(channelReadModel, channelFullReadModel, peerNotifySettingsReadModel, selfUserId),
            Users = new TVector<IUser>()
        };
    }
    public IExportedChatInvite ToExportedChatInvite(ExportChatInviteEvent eventData)
    {
        var item = _objectMapper.Map<ExportChatInviteEvent, TChatInviteExported>(eventData);
        item.Link = $"{_appSettingManager.GetSetting(MyTelegramServerConsts.JoinChatDomain)}/{item.Link}";
        return item;
    }

    public IUpdates ToInviteToChannelUpdates(IChannelReadModel channelReadModel,
            IUserReadModel senderUserReadModel,
        int date)
    {
        var update = new TUpdateChannel { ChannelId = channelReadModel.ChannelId };
        var channel = ToChannel(channelReadModel, null, 0, false);
        return new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Users = new TVector<IUser>(),
            Date = date,
            Updates = new TVector<IUpdate>(update)
        };
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

    private IChannelParticipant ToChannelParticipantCore(IChannelReadModel channelReadModel,
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

        return new TChannelParticipant
        {
            UserId = channelMemberReadModel.UserId,
            Date = channelMemberReadModel.Date
        };
    }
    private IReadOnlyList<IChannelParticipant> ToChannelParticipantsCore(
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        IChannelReadModel channelReadModel)
    {
        var participants = new List<IChannelParticipant>();
        foreach (var channelMemberReadModel in channelMemberReadModels)
        {
            participants.Add(ToChannelParticipantCore(channelReadModel, channelMemberReadModel, 0));
        }

        return participants;
    }
}
