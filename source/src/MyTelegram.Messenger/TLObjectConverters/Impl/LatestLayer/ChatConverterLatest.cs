using IChannelParticipant = MyTelegram.Schema.Channels.IChannelParticipant;
using IChatFull = MyTelegram.Schema.IChatFull;
using IExportedChatInvite = MyTelegram.Schema.IExportedChatInvite;
using TChannelParticipant = MyTelegram.Schema.Channels.TChannelParticipant;
using TChatFull = MyTelegram.Schema.Messages.TChatFull;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class ChatConverterLatest : ChatConverterBase, IChatConverterLatest
{
    private readonly ILayeredService<IPeerNotifySettingsConverter> _layeredPeerNotifySettingsService;
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    private IPhotoConverter? _photoConverter;

    public ChatConverterLatest(IObjectMapper objectMapper,
        IOptions<MyTelegramMessengerServerOptions> options,
        ILayeredService<IPhotoConverter> layeredPhotoService,
        ILayeredService<IPeerNotifySettingsConverter> layeredPeerNotifySettingsService)
    {
        ObjectMapper = objectMapper;
        _options = options;
        _layeredPhotoService = layeredPhotoService;
        _layeredPeerNotifySettingsService = layeredPeerNotifySettingsService;
    }

    public override int Layer => Layers.LayerLatest;
    protected IObjectMapper ObjectMapper { get; }
    public IChat ToChannel(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        //IChatPhoto chatPhoto,
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

        var channel = ToChannel(channelReadModel); // _objectMapper.Map<IChannelReadModel, TChannel>(channelReadModel);
        channel.DefaultBannedRights = ToChatBannedRights(channelReadModel.DefaultBannedRights);
        channel.Creator = channelReadModel.CreatorId == selfUserId;
        //channel.Photo = chatPhoto;
        channel.Photo = GetPhotoConverter().ToChatPhoto(photoReadModel);

        if (channelMemberIsLeft)
        {
            channel.Left = true;
        }
        else
        {
            if (channelMemberReadModel != null && channelMemberReadModel.BannedRights != 0)
            {
                var bannedRights = ToChatBannedRights(
                    ChatBannedRights.FromValue(channelMemberReadModel.BannedRights,
                        channelMemberReadModel.UntilDate));
                channel.BannedRights = bannedRights;
            }
        }

        if (channel.Creator)
        {
            channel.AdminRights =
                ToChatAdminRights(ChatAdminRights.GetCreatorRights());
            channel.Left = false;
        }
        else
        {
            var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == selfUserId);
            if (admin != null)
            {
                channel.AdminRights = ToChatAdminRights(admin.AdminRights);
            }
        }

        return channel;
    }

    public override ILayeredChannel ToChannel(ChannelCreatedEvent channelCreatedEvent)
    {
        return ObjectMapper.Map<ChannelCreatedEvent, TChannel>(channelCreatedEvent);
    }

    //protected IUserConverter GetUserConverter()
    //{
    //    return _userConverter ??= _layeredUserService.GetConverter(Layer);
    //}
    public override ILayeredChannel ToChannel(IChannelReadModel channelReadModel)
    {
        return ObjectMapper.Map<IChannelReadModel, TChannel>(channelReadModel);
    }

    public IChatFull ToChannelFull(
            long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelFullReadModel channelFullReadModel,
        //IPhoto chatFullPhoto,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel,
        IChatInviteReadModel? chatInviteReadModel = null
    )
    {
        //var channel = ToChannel(selfUserId, channelReadModel);
        //var channelFull = _objectMapper.Map<IChannelFullReadModel, TChannelFull>(channelFullReadModel);
        var channelFull = ToChatFull(channelFullReadModel);
        //channelFull.ChatPhoto = chatFullPhoto; // channelReadModel.Photo.ToTObject<IPhoto>() ?? new TPhotoEmpty();
        //channelFull.ChatPhoto = channelReadModel.Photo.ToTObject<IPhoto>() ?? new TPhotoEmpty();
        channelFull.ChatPhoto = GetPhotoConverter().ToPhoto(photoReadModel);
        //channelFull.NotifySettings =
        //    ObjectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(
        //        peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings);
        channelFull.NotifySettings = GetPeerNotifySettings(peerNotifySettingsReadModel?.NotifySettings);

        channelFull.BotInfo = new TVector<IBotInfo>();
        channelFull.Pts = channelReadModel.Pts;
        channelFull.ParticipantsCount = channelReadModel.ParticipantsCount;
        if (channelFullReadModel.RecentRequesters?.Count > 0 &&
            channelReadModel.AdminList.Any(p => p.UserId == selfUserId))
        {
            channelFull.RequestsPending = channelFullReadModel.RequestsPending;
            channelFull.RecentRequesters = new TVector<long>(channelFullReadModel.RecentRequesters);
        }


        //if (channelFullReadModel.ChannelId == 800000000002)
        //{
        //    channelFull.Call = new TInputGroupCall
        //    {
        //        AccessHash = 1,
        //        Id = 1
        //    };
        //}

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

        if (chatInviteReadModel != null && channelReadModel.AdminList.Any(p => p.UserId == selfUserId))
        {
            channelFull.ExportedInvite =
                ObjectMapper.Map<IChatInviteReadModel, TChatInviteExported>(chatInviteReadModel);
        }

        return channelFull;
    }

    public IList<IChat> ToChannelList(
        long selfUserId,
        IReadOnlyCollection<IChannelReadModel> channelReadModels,
        IReadOnlyCollection<IPhotoReadModel>? photoReadModels,
        IReadOnlyCollection<long> joinedChannelIdList,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        //IPhotoConverter photoConverter,
        //IEnumerable<IChat> channels,
        //IEnumerable<IChannelMemberReadModel> channelMemberReadModels,
        bool resetLeftToFalse = false)
    {
        //foreach (var channel in channels)
        //{
        //    if (channel is ILayeredChannel chat)
        //    {
        //        if (!joinedChannelIdList.Contains(chat.Id))
        //        {
        //            chat.Left = true;
        //        }

        //        if (resetLeftToFalse)
        //        {
        //            chat.Left = false;
        //        }

        //        yield return chat;
        //    }
        //    else
        //    {
        //        yield return channel;
        //    }
        //}

        var channelList = new List<IChat>();
        var memberDict = channelMemberReadModels.ToDictionary(k => k.ChannelId, v => v);
        var photoDict = photoReadModels?.ToDictionary(k => k.PhotoId, v => v);
        foreach (var channelReadModel in channelReadModels)
        {
            IPhotoReadModel? photoReadModel = null;
            photoDict?.TryGetValue(channelReadModel.PhotoId ?? 0, out photoReadModel);
            memberDict.TryGetValue(channelReadModel.ChannelId, out var memberReadModel);

            //var chatPhoto = GetPhotoConverter().GetChatPhoto(channelReadModel.Photo);
            var channel = ToChannel(selfUserId, channelReadModel, photoReadModel, memberReadModel);
            if (channel is ILayeredChannel chat)
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

    public IChannelParticipant ToChannelParticipant(
            long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        //IChatPhoto chatPhoto,
        //IUserReadModel userReadModel,
        IUser user //,
        //IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    )
    {
        var participant = ToChannelParticipantCore(selfUserId, channelReadModel, channelMemberReadModel);
        //var user = GetUserConverter().ToUser(userReadModel, selfUserId, privacies: privacies);
        var channel = ToChannel(selfUserId, channelReadModel, photoReadModel, channelMemberReadModel);
        return new TChannelParticipant
        {
            Chats = new TVector<IChat>(channel),
            Participant = participant,
            Users = new TVector<IUser>(user)
        };
    }

    public IChannelParticipant ToChannelParticipantLayerN(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel channelMemberReadModel,
        //IUserReadModel userReadModel,
        //IChatPhoto chatPhoto,
        IUser user
        //,
        //IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    )
    {
        var participant = ToChannelParticipantCore(selfUserId, channelReadModel, channelMemberReadModel);
        //var user = GetUserConverter().ToUser(userReadModel, selfUserId, privacies: privacies);
        //var channel = ToChannel(channelReadModel, selfUserId);
        return new Schema.Channels.LayerN.TChannelParticipant
            { Participant = participant, Users = new TVector<IUser>(user) };
    }

    public IChannelParticipants ToChannelParticipants(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IReadOnlyCollection<IChatAdminReadModel>? chatAdminReadModels,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels,
        //IChatPhoto chatPhoto,
        //IReadOnlyCollection<IUserReadModel> userReadModels,
        IEnumerable<IUser> users,
        DeviceType deviceType,
        bool forceNotLeft //,
        //IReadOnlyCollection<IContactReadModel>? contactReadModels = null,
        //IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    )
    {
        var participants =
            ToChannelParticipantsCore(selfUserId, channelReadModel, chatAdminReadModels, channelMemberReadModels);
        //var users = GetUserConverter().ToUserList(userReadModels, selfUserId, contactReadModels, privacies);
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

        var channel = ToChannel(
            selfUserId,
            channelReadModel,
            photoReadModel,
            channelMemberReadModel,
            //chatPhoto,
            channelMemberIsLeft);

        return new TChannelParticipants
        {
            Chats = new TVector<IChat>(channel),
            Count = participants.Count,
            Participants = new TVector<Schema.IChannelParticipant>(participants),
            Users = new TVector<IUser>(users)
        };
    }

    public override IChat ToChat(ChatCreatedEvent chatCreatedEvent)
    {
        return ObjectMapper.Map<ChatCreatedEvent, TChat>(chatCreatedEvent);
    }

    public IChat ToChat(
        long selfUserId,
        IChatReadModel chat,
        IPhotoReadModel? photoReadModel
        //IChatPhoto chatPhoto,
    )
    {
        if (chat.ChatMembers.All(p => p.UserId != selfUserId))
        {
            return new TChatForbidden { Id = chat.ChatId, Title = chat.Title };
        }

        var tChat = ToChat(chat);
        tChat.Id = chat.ChatId;
        tChat.Creator = chat.CreatorUid == selfUserId;
        tChat.Photo = GetPhotoConverter().ToChatPhoto(photoReadModel);
        //tChat.Photo = GetPhotoConverter().GetChatPhoto(chat.Photo);
        //tChat.Photo = chatPhoto;
        tChat.ParticipantsCount = chat.ChatMembers.Count;
        tChat.DefaultBannedRights = ToChatBannedRights(chat.DefaultBannedRights);

        if (chat.IsDeleted)
        {
            tChat.ParticipantsCount = 0;
            tChat.Deactivated = true;
        }

        return tChat;
    }

    public override ILayeredChat ToChat(IChatReadModel chatReadModel)
    {
        return ObjectMapper.Map<IChatReadModel, TChat>(chatReadModel);
    }

    public Schema.Messages.IChatFull ToChatFull(
            long selfUserId,
        IChatReadModel chat,
        IPhotoReadModel? photoReadModel,
        //IChatPhoto chatPhoto,
        //IReadOnlyCollection<IUserReadModel> userList,
        IEnumerable<IUser> users,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        IChannelReadModel? migratedToChannelReadModel = null,
        IChatInviteReadModel? chatInviteReadModel = null
        //,
        //IReadOnlyCollection<IContactReadModel>? contactReadModels = null,
        //IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    )
    {
        var tChat = ToChat(selfUserId, chat, photoReadModel);
        var fullChat = ToChatFull(chat);

        fullChat.ChatPhoto = GetPhotoConverter().ToPhoto(photoReadModel);
        //fullChat.NotifySettings = ObjectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(
        //    peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings);
        fullChat.NotifySettings = GetPeerNotifySettings(peerNotifySettingsReadModel?.NotifySettings);
        fullChat.Participants = chat.IsDeleted
            ? new TChatParticipants
            {
                ChatId = chat.ChatId,
                Participants = new TVector<IChatParticipant>()
            }
            : ToChatParticipants(chat.ChatId,
                chat.ChatMembers,
                chat.Date,
                chat.CreatorUid,
                (int)(chat.Version ?? 0));

        //var tUserList = GetUserConverter().ToUserList(userList, selfUserId, contactReadModels, privacies);
        var chatFull = new TChatFull
        {
            Chats = new TVector<IChat>(tChat),
            Users = new TVector<IUser>(users),
            FullChat = fullChat
        };
        if (migratedToChannelReadModel != null)
        {
            var channel = ToChannel(selfUserId, migratedToChannelReadModel, photoReadModel, null, false);
            chatFull.Chats.Add(channel);
        }

        if (chatInviteReadModel != null)
        {
            fullChat.ExportedInvite = ObjectMapper.Map<IChatInviteReadModel, TChatInviteExported>(chatInviteReadModel);
        }

        return chatFull;
    }

    public Schema.Messages.IChatFull ToChatFull(
        long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelFullReadModel channelFullReadModel,
        IChannelMemberReadModel? channelMemberReadModel,
        //IChatPhoto chatPhoto,
        //IPhoto chatFullPhoto,
        IPeerNotifySettingsReadModel peerNotifySettingsReadModel,
        IChatReadModel? migratedFromChatReadModel,
        IChatInviteReadModel? chatInviteReadModel = null
    )
    {
        var channel = ToChannel(selfUserId, channelReadModel, photoReadModel, channelMemberReadModel);
        var chat = migratedFromChatReadModel == null
            ? null
            : ToChat(selfUserId, migratedFromChatReadModel, photoReadModel);
        var fullChat = ToChannelFull(
            selfUserId,
            channelReadModel,
            photoReadModel,
            channelFullReadModel,
            //chatFullPhoto,
            peerNotifySettingsReadModel,
            chatInviteReadModel
        );
        var chatFull = new TChatFull
        {
            Chats = new TVector<IChat>(channel),
            FullChat = fullChat,
            Users = new TVector<IUser>()
        };
        if (chat != null)
        {
            chatFull.Chats.Add(chat);
        }

        return chatFull;
    }

    public override ILayeredChannelFull ToChatFull(IChannelFullReadModel channelFullReadModel)
    {
        return ObjectMapper.Map<IChannelFullReadModel, TChannelFull>(channelFullReadModel);
    }

    public override ILayeredChatFull ToChatFull(IChatReadModel chatReadModel)
    {
        return ObjectMapper.Map<IChatReadModel, Schema.TChatFull>(chatReadModel);
    }

    public IList<IChat> ToChatList(
                long selfUserId,
        IReadOnlyCollection<IChatReadModel> chats,
        IReadOnlyCollection<IPhotoReadModel>? photoReadModels
        //IPhotoConverter photoConverter,
    )
    {
        var chatList = new List<IChat>();
        var photoDict = photoReadModels?.ToDictionary(k => k.PhotoId, v => v);
        foreach (var chatReadModel in chats)
        {
            IPhotoReadModel? photoReadModel = null;
            photoDict?.TryGetValue(chatReadModel.PhotoId ?? 0, out photoReadModel);
            //var photo = GetPhotoConverter().GetChatPhoto(chatReadModel.Photo);
            chatList.Add(ToChat(selfUserId, chatReadModel, photoReadModel));
        }

        return chatList;
    }

    public IExportedChatInvite ToExportedChatInvite(ChannelInviteExportedEvent eventData)
    {
        var item = ObjectMapper.Map<ChannelInviteExportedEvent, TChatInviteExported>(eventData);
        //item.Link = $"{_appSettingManager.GetSetting(MyTelegramServerConsts.JoinChatDomain)}/{item.Link}";
        item.Link = $"{_options.Value.JoinChatDomain}/+{item.Link}";

        return item;
    }

    public IUpdates ToInviteToChannelUpdates(
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        //IChatPhoto chatPhoto,
        IUserReadModel senderUserReadModel,
        int date)
    {
        var update = new TUpdateChannel { ChannelId = channelReadModel.ChannelId };
        var channel = ToChannel(
            0,
            channelReadModel,
            photoReadModel,
            null,
            //chatPhoto,
            false);
        //var user = ToUser(senderUserReadModel, 0);
        return new TUpdates
        {
            Chats = new TVector<IChat>(channel),
            Users = new TVector<IUser>(),
            Date = date,
            Updates = new TVector<IUpdate>(update)
        };
    }
    protected virtual IPeerNotifySettings GetPeerNotifySettings(PeerNotifySettings? peerNotifySettings)
    {
        return _layeredPeerNotifySettingsService.GetConverter(GetLayer()).ToPeerNotifySettings(peerNotifySettings);
    }

    protected IPhotoConverter GetPhotoConverter()
    {
        return _photoConverter ??= _layeredPhotoService.GetConverter(Layer);
    }

    protected virtual IChatAdminRights ToChatAdminRights(ChatAdminRights? rights)
    {
        if (rights == null)
        {
            return new TChatAdminRights();
        }

        return ObjectMapper.Map<ChatAdminRights, TChatAdminRights>(rights);
    }

    protected virtual IChatBannedRights ToChatBannedRights(ChatBannedRights? rights)
    {
        return ObjectMapper.Map<ChatBannedRights, TChatBannedRights>(rights ?? ChatBannedRights.Default);
    }

    protected virtual Schema.IChannelParticipant ToChatParticipantAdmin(long selfUserId,
        IChatAdminReadModel chatAdminReadModel)
    {
        if (chatAdminReadModel.IsCreator)
        {
            return new TChannelParticipantCreator
            {
                AdminRights = ToChatAdminRights(ChatAdminRights.GetCreatorRights()),
                Rank = chatAdminReadModel.Rank,
                UserId = chatAdminReadModel.UserId
            };
        }

        return new TChannelParticipantAdmin
        {
            Date = chatAdminReadModel.Date,
            InviterId = chatAdminReadModel.PromotedBy,
            UserId = chatAdminReadModel.UserId,
            CanEdit = chatAdminReadModel.CanEdit,
            AdminRights = ToChatAdminRights(chatAdminReadModel.AdminRights),
            PromotedBy = chatAdminReadModel.PromotedBy,
            Self = selfUserId == chatAdminReadModel.UserId,
            Rank = chatAdminReadModel.Rank
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

    private IChat ToChannel(
                                long selfUserId,
        IChannelReadModel channelReadModel,
        IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel? channelMemberReadModel
        //IChatPhoto chatPhoto,
    )
    {
        return ToChannel(
            selfUserId,
            channelReadModel,
            photoReadModel,
            channelMemberReadModel,
            //chatPhoto,
            channelMemberReadModel == null || channelMemberReadModel.Left);
    }
    private Schema.IChannelParticipant ToChannelParticipantCore(
        long selfUserId,
        IChannelReadModel channelReadModel,
        //IPhotoReadModel? photoReadModel,
        IChannelMemberReadModel channelMemberReadModel
    )
    {
        var bannedRights = ToChatBannedRights(
            ChatBannedRights.FromValue(channelMemberReadModel.BannedRights,
                channelMemberReadModel.UntilDate));
        if (channelMemberReadModel.Kicked ||
            (channelMemberReadModel.BannedRights != 0 &&
             channelMemberReadModel.BannedRights != ChatBannedRights.Default.ToIntValue() &&
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
                AdminRights = ToChatAdminRights(ChatAdminRights.GetCreatorRights())
            };
        }

        var admin = channelReadModel.AdminList.FirstOrDefault(p => p.UserId == channelMemberReadModel.UserId);
        if (admin != null)
        {
            return new TChannelParticipantAdmin
            {
                AdminRights = ToChatAdminRights(admin.AdminRights),
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
        long selfUserId,
        IChannelReadModel channelReadModel,
        //IPhotoReadModel? photoReadModel,
        IReadOnlyCollection<IChatAdminReadModel>? chatAdminReadModels,
        IReadOnlyCollection<IChannelMemberReadModel> channelMemberReadModels
    )
    {
        var participants = new List<Schema.IChannelParticipant>();

        foreach (var chatAdminReadModel in chatAdminReadModels ?? Array.Empty<IChatAdminReadModel>())
        {
            participants.Add(ToChatParticipantAdmin(selfUserId, chatAdminReadModel));
        }

        foreach (var channelMemberReadModel in channelMemberReadModels)
        {
            participants.Add(ToChannelParticipantCore(selfUserId, channelReadModel, channelMemberReadModel));
        }

        return participants;
    }
}