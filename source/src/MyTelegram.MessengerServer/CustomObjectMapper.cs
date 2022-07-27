using MyTelegram.Schema.Updates;

namespace MyTelegram.MessengerServer;

public class CustomObjectMapper :
    IObjectMapper<IUserReadModel, TUser>,
    IObjectMapper<UserCreatedEvent, TUser>,
    IObjectMapper<SignInSuccessEvent, TUser>,
    IObjectMapper<UserItem, TUser>,
    IObjectMapper<IPtsReadModel, TState>,
    IObjectMapper<PeerSettings, TPeerSettings>,
    IObjectMapper<SearchGlobalInput, GetMessagesQuery>,
    IObjectMapper<SearchInput, GetMessagesQuery>,
    IObjectMapper<GetHistoryInput, GetMessagesQuery>,
    IObjectMapper<GetMessagesInput, GetMessagesQuery>,
    IObjectMapper<IDialogReadModel, TDialog>,
    IObjectMapper<BotCommand, TBotCommand>,
    IObjectMapper<IReadOnlyList<BotCommand>, List<TBotCommand>>,
    IObjectMapper<IChatReadModel, TChat>,
    IObjectMapper<ExportChatInviteEvent, TChatInviteExported>,
    IObjectMapper<IChannelReadModel, TChannel>,
    IObjectMapper<IChannelFullReadModel, TChannelFull>,
    IObjectMapper<ChatBannedRights, TChatBannedRights>,
    //IObjectMapper<ChatBannedRights, TChatBannedRights>,
    //IObjectMapper<ChatAdminRights, TChatAdminRights>,
    IObjectMapper<ChatAdminRights, TChatAdminRights>,
    IObjectMapper<WebRtcConnection, TPhoneConnectionWebrtc>,
    IObjectMapper<List<WebRtcConnection>, List<TPhoneConnectionWebrtc>>,
    IObjectMapper<PeerNotifySettings, TPeerNotifySettings>,
    IObjectMapper<IDeviceReadModel, TAuthorization>,
    IObjectMapper<IChatInviteReadModel, TChatInviteExported>,
    IObjectMapper<IFileReadModel, FileItem>,
    IObjectMapper<DcOption, TDcOption>,
    IObjectMapper<List<DcOption>, List<TDcOption>>,
    IObjectMapper<TInputMediaVenue, TMessageMediaVenue>,
    IObjectMapper<MessageFwdHeader, TMessageFwdHeader>,
    IObjectMapper<GetRepliesInput, GetMessagesQuery>

{
    public TBotCommand Map(BotCommand source)
    {
        return Map(source, new TBotCommand());
    }

    public TBotCommand Map(BotCommand source,
        TBotCommand destination)
    {
        destination.Command = source.Command;
        destination.Description = source.Description;

        return destination;
    }

    public TChatAdminRights Map(ChatAdminRights source)
    {
        return Map(source, new TChatAdminRights());
    }

    public TChatAdminRights Map(ChatAdminRights source,
        TChatAdminRights destination)
    {
        destination.ChangeInfo = source.ChangeInfo;
        destination.PostMessages = source.PostMessages;
        destination.EditMessages = source.EditMessages;
        destination.DeleteMessages = source.DeleteMessages;
        destination.BanUsers = source.BanUsers;
        destination.InviteUsers = source.InviteUsers;
        destination.PinMessages = source.PinMessages;
        destination.AddAdmins = source.AddAdmins;
        destination.Anonymous = source.Anonymous;
        destination.ManageCall = source.ManageCall;
        destination.Other = source.Other;

        return destination;
    }

    public TChatBannedRights Map(ChatBannedRights source)
    {
        return Map(source, new TChatBannedRights());
    }

    public TChatBannedRights Map(ChatBannedRights source,
        TChatBannedRights destination)
    {
        destination.ViewMessages = source.ViewMessages;
        destination.SendMessages = source.SendMessages;
        destination.SendMedia = source.SendMedia;
        destination.SendStickers = source.SendStickers;
        destination.SendGifs = source.SendGifs;
        destination.SendGames = source.SendGames;
        destination.SendInline = source.SendInline;
        destination.EmbedLinks = source.EmbedLinks;
        destination.SendPolls = source.SendPolls;
        destination.ChangeInfo = source.ChangeInfo;
        destination.InviteUsers = source.InviteUsers;
        destination.PinMessages = source.PinMessages;
        destination.UntilDate = source.UntilDate;

        return destination;
    }

    public TDcOption Map(DcOption source)
    {
        return Map(source, new TDcOption());
    }

    public TDcOption Map(DcOption source,
        TDcOption destination)
    {
        destination.Id = source.Id;
        destination.Ipv6 = source.Ipv6;
        destination.Port = source.Port;
        destination.Cdn = source.Cdn;
        destination.IpAddress = source.IpAddress;
        destination.MediaOnly = source.MediaOnly;
        destination.Secret = source.Secret;
        destination.Static = source.Static;

        return destination;
    }

    public TChatInviteExported Map(ExportChatInviteEvent source)
    {
        return Map(source, new TChatInviteExported());
    }

    public TChatInviteExported Map(ExportChatInviteEvent source,
        TChatInviteExported destination)
    {
        destination.ExpireDate = source.ExpireDate;
        destination.UsageLimit = source.UsageLimit;
        destination.Revoked = source.Revoke;
        destination.Link = source.Link;
        destination.Permanent = source.Permanent;
        destination.Date = source.Date;
        destination.StartDate = source.StartDate;

        return destination;
    }

    public GetMessagesQuery Map(GetHistoryInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery Map(GetHistoryInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            MessageType.Unknown,
            null,
            new List<int>(),
            source.ChannelHistoryMinId,
            source.Limit,
            null,
            source.Peer,
            source.SelfUserId,
            0);
    }

    public GetMessagesQuery Map(GetMessagesInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery Map(GetMessagesInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            MessageType.Unknown,
            null,
            source.MessageIdList,
            0,
            source.Limit,
            null,
            source.Peer,
            source.SelfUserId,
            0);
    }

    public GetMessagesQuery? Map(GetRepliesInput source)
    {
        return Map(source, null!);
    }
    public GetMessagesQuery? Map(GetRepliesInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            MessageType.Unknown,
            null,
            new List<int>(),
            0,
            source.Limit,
            null,
            null,
            source.SelfUserId,
            0,
            source.ReplyToMsgId);
    }
    public TChannelFull Map(IChannelFullReadModel source)
    {
        return Map(source, new TChannelFull());
    }

    public TChannelFull Map(IChannelFullReadModel source,
        TChannelFull destination)
    {
        destination.Id = source.ChannelId;
        destination.About = source.About ?? string.Empty;

        destination.CanViewParticipants = source.CanViewParticipants;
        destination.CanSetUsername = source.CanSetUserName;
        destination.CanSetStickers = source.CanSetStickers;
        destination.HiddenPrehistory = source.HiddenPreHistory;
        destination.CanViewStats = source.CanViewStats;
        destination.CanSetLocation = source.CanSetLocation;
        destination.AdminsCount = source.AdminsCount;
        destination.KickedCount = source.KickedCount;
        destination.BannedCount = source.BannedCount;
        destination.OnlineCount = source.OnlineCount;
        destination.ReadInboxMaxId = source.ReadInboxMaxId;
        destination.ReadOutboxMaxId = source.ReadOutboxMaxId;
        destination.UnreadCount = source.UnreadCount;
        destination.MigratedFromChatId = source.MigratedFromChatId;
        destination.MigratedFromMaxId = source.MigratedFromMaxId;
        destination.PinnedMsgId = source.PinnedMsgId;
        destination.AvailableMinId = source.AvailableMinId;
        destination.FolderId = source.FolderId;
        destination.LinkedChatId = source.LinkedChatId;
        destination.SlowmodeSeconds = source.SlowModeSeconds;
        destination.SlowmodeNextSendDate = source.SlowModeNextSendDate;

        return destination;
    }

    public TChannel Map(IChannelReadModel source)
    {
        return Map(source, new TChannel());
    }

    public TChannel Map(IChannelReadModel source,
        TChannel destination)
    {
        destination.Id = source.ChannelId;
        destination.Title = source.Title;
        destination.ParticipantsCount = source.ParticipantsCount;
        destination.Broadcast = source.Broadcast;
        destination.Megagroup = source.MegaGroup;
        destination.Verified = source.Verified;
        destination.Signatures = source.Signatures;
        destination.AccessHash = source.AccessHash;
        destination.Username = source.UserName;
        destination.Date = source.Date;
        destination.SlowmodeEnabled = source.SlowModeEnabled;
        destination.DefaultBannedRights = Map(source.DefaultBannedRights ?? new ChatBannedRights());
        //destination.HasLink = !source.UserName.IsNullOrEmpty();

        //destination.HasLink = !string.IsNullOrEmpty(source.UserName);
        destination.HasLink = source.LinkedChatId.HasValue;

        return destination;
    }

    public TChatInviteExported Map(IChatInviteReadModel source)
    {
        return Map(source, new TChatInviteExported());
    }

    public TChatInviteExported Map(IChatInviteReadModel source,
        TChatInviteExported destination)
    {
        destination.ExpireDate = source.ExpireDate;
        destination.UsageLimit = source.UsageLimit;
        destination.Revoked = source.Revoked;
        destination.Link = source.Link;
        destination.Permanent = source.Permanent;
        destination.Date = source.Date;
        destination.StartDate = source.StartDate;
        destination.AdminId = source.AdminId;
        destination.Usage = source.Usage;

        return destination;
    }

    public TChat Map(IChatReadModel source)
    {
        return Map(source, new TChat());
    }

    public TChat Map(IChatReadModel source,
        TChat destination)
    {
        destination.Id = source.ChatId;
        destination.Title = source.Title;
        destination.Date = source.Date;
        destination.DefaultBannedRights = Map(new ChatBannedRights());
        destination.ParticipantsCount = source.ChatMembers.Count;

        return destination;
    }

    public TAuthorization Map(IDeviceReadModel source)
    {
        return Map(source, new TAuthorization());
    }

    public TAuthorization Map(IDeviceReadModel source,
        TAuthorization destination)
    {
        destination.ApiId = source.AppId;
        destination.AppName = source.AppName;
        destination.AppVersion = source.AppVersion;
        destination.Hash = source.Hash;
        destination.OfficialApp = source.OfficialApp;
        destination.PasswordPending = source.PasswordPending;
        destination.DeviceModel = source.DeviceModel;
        destination.Platform = source.Platform;
        destination.SystemVersion = source.SystemVersion;
        destination.Ip = source.Ip;
        destination.DateActive = source.DateActive;
        destination.DateCreated = source.DateCreated;

        return destination;
    }

    public TDialog Map(IDialogReadModel source)
    {
        return Map(source, new TDialog());
    }

    public TDialog Map(IDialogReadModel source,
        TDialog destination)
    {
        destination.Pts = source.Pts;
        destination.TopMessage = source.TopMessage;
        destination.Pinned = source.Pinned;
        destination.UnreadCount = source.UnreadCount;
        //destination.UnreadMark = source.u;
        destination.ReadInboxMaxId = source.ReadInboxMaxId;
        destination.ReadOutboxMaxId = source.ReadOutboxMaxId;
        destination.Peer = new Peer(source.ToPeerType, source.ToPeerId).ToPeer();
        if (source.Draft?.Message.Length > 0)
        {
            destination.Draft = new TDraftMessage
            {
                Date = source.Draft.Date,
                Message = source.Draft.Message,
                NoWebpage = source.Draft.NoWebpage,
                ReplyToMsgId = source.Draft.ReplyToMsgId,
                Entities = source.Draft.Entities.ToTObject<TVector<IMessageEntity>>()
            };
        }

        destination.NotifySettings = new TPeerNotifySettings
        {
            ShowPreviews = true,
            Silent = false,
            //Sound = "default",
            //AndroidSound = new TNotificationSoundDefault(),
            //IosSound = new TNotificationSoundDefault(),
            //OtherSound = new TNotificationSoundDefault(),
            MuteUntil = 0
        };

        return destination;
    }

    public FileItem Map(IFileReadModel source)
    {
        return Map(source, new FileItem());
    }

    public FileItem Map(IFileReadModel source,
        FileItem destination)
    {
        destination.UserId = source.UserId;
        destination.FileId = source.FileId;
        destination.LocalId = source.LocalId;
        destination.AccessHash = source.AccessHash;
        destination.TotalParts = source.TotalParts;
        destination.Size = source.Size;
        destination.FileReference = source.FileReference;
        destination.MimeType = source.MimeType;
        destination.VolumeId = source.VolumeId;
        destination.Date = source.Date;
        destination.Attributes = source.Attributes;
        destination.Name = source.Name;
        destination.Md5 = source.Md5CheckSum;
        //destination.Parts = source.Parts;
        destination.Thumbs = source.Thumbs;

        return destination;
    }

    public TState Map(IPtsReadModel source)
    {
        return Map(source, new TState());
    }

    public TState Map(IPtsReadModel source,
        TState destination)
    {
        destination.Date = source.Date;
        destination.Pts = source.Pts;
        destination.Qts = source.Qts;
        destination.UnreadCount = source.UnreadCount;

        return destination;
    }

    public List<TBotCommand> Map(IReadOnlyList<BotCommand> source)
    {
        return Map(source, new List<TBotCommand>());
    }

    public List<TBotCommand> Map(IReadOnlyList<BotCommand> source,
        List<TBotCommand> destination)
    {
        foreach (var botCommand in source)
        {
            destination.Add(Map(botCommand));
        }

        return destination;
    }

    public TUser Map(IUserReadModel source)
    {
        return Map(source, new TUser());
    }

    public TUser Map(IUserReadModel source,
        TUser destination)
    {
        destination.Id = source.UserId;
        destination.Photo = new TUserProfilePhotoEmpty();
        destination.AccessHash = source.AccessHash;
        destination.Bot = source.Bot;
        destination.BotInfoVersion = source.BotInfoVersion;
        destination.Username = source.UserName;
        destination.Phone = source.PhoneNumber;
        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.Verified = source.Verified;
        destination.Support = source.Support;
        destination.Premium = source.Premium;
        return destination;
    }

    public List<TDcOption> Map(List<DcOption> source)
    {
        return Map(source, new List<TDcOption>());
    }

    public List<TDcOption> Map(List<DcOption> source,
        List<TDcOption> destination)
    {
        foreach (var dcOption in source)
        {
            destination.Add(Map(dcOption));
        }

        return destination;
    }

    public List<TPhoneConnectionWebrtc> Map(List<WebRtcConnection> source)
    {
        return Map(source, new List<TPhoneConnectionWebrtc>());
    }

    public List<TPhoneConnectionWebrtc> Map(List<WebRtcConnection> source,
        List<TPhoneConnectionWebrtc> destination)
    {
        foreach (var webRtcConnection in source)
        {
            destination.Add(Map(webRtcConnection));
        }

        return destination;
    }

    public TMessageFwdHeader Map(MessageFwdHeader source)
    {
        return Map(source, new TMessageFwdHeader());
    }

    public TMessageFwdHeader Map(MessageFwdHeader source,
        TMessageFwdHeader destination)
    {
        destination.FromId = source.FromId.ToPeer();
        destination.FromName = source.FromName;
        destination.ChannelPost = source.ChannelPost;
        destination.PostAuthor = source.PostAuthor;
        destination.Date = source.Date;
        destination.SavedFromPeer = source.SavedFromPeer!.ToPeer();
        destination.SavedFromMsgId = source.SavedFromMsgId;

        return destination;
    }

    public TPeerNotifySettings Map(PeerNotifySettings source)
    {
        return Map(source, new TPeerNotifySettings());
    }

    public TPeerNotifySettings Map(PeerNotifySettings source,
        TPeerNotifySettings destination)
    {
        destination.Silent = source.Silent;
        destination.MuteUntil = source.MuteUntil;
        //destination.Sound = source.Sound;
        //destination.IosSound=source.
        destination.ShowPreviews = source.ShowPreviews;

        return destination;
    }

    public TPeerSettings Map(PeerSettings source)
    {
        return Map(source, new TPeerSettings());
    }

    public TPeerSettings Map(PeerSettings source,
        TPeerSettings destination)
    {
        destination.AddContact = source.AddContact;
        destination.BlockContact = source.BlockContact;
        //destination.InviteMembers=source.
        destination.NeedContactsException = source.NeedContactsException;
        destination.ReportGeo = source.ReportGeo;
        destination.ReportSpam = source.ReportSpam;
        destination.ShareContact = source.ShareContact;

        return destination;
    }

    public GetMessagesQuery Map(SearchGlobalInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery Map(SearchGlobalInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            source.MessageType,
            source.Q,
            new List<int>(),
            0,
            source.Limit,
            null,
            null,
            source.SelfUserId,
            0);
    }

    public GetMessagesQuery Map(SearchInput source)
    {
        return Map(source, null!);
    }

    public GetMessagesQuery Map(SearchInput source,
        GetMessagesQuery destination)
    {
        return new GetMessagesQuery(source.OwnerPeerId,
            source.MessageType,
            source.Q,
            new List<int>(),
            0,
            source.Limit,
            null,
            source.Peer,
            source.SelfUserId,
            0);
    }

    public TUser Map(SignInSuccessEvent source)
    {
        return Map(source, new TUser());
    }

    public TUser Map(SignInSuccessEvent source,
        TUser destination)
    {
        destination.Id = source.UserId;
        destination.Photo = new TUserProfilePhotoEmpty();
        destination.Phone = source.PhoneNumber;
        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;

        return destination;
    }

    public TMessageMediaVenue Map(TInputMediaVenue source)
    {
        return Map(source, new TMessageMediaVenue());
    }

    public TMessageMediaVenue Map(TInputMediaVenue source,
        TMessageMediaVenue destination)
    {
        destination.Title = source.Title;
        destination.Address = source.Address;
        destination.Provider = source.Provider;
        destination.VenueId = source.VenueId;
        destination.VenueType = source.VenueType;
        //destination.Geo=source.GeoPoint
        return destination;
    }

    public TUser Map(UserCreatedEvent source)
    {
        return Map(source, new TUser());
    }

    public TUser Map(UserCreatedEvent source,
        TUser destination)
    {
        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.Phone = source.PhoneNumber;
        destination.Id = source.UserId;
        destination.AccessHash = source.AccessHash;
        destination.Bot = source.Bot;
        destination.BotInfoVersion = source.BotInfoVersion;

        return destination;
    }

    public TUser Map(UserItem source)
    {
        return Map(source, new TUser());
    }

    public TUser Map(UserItem source,
        TUser destination)
    {
        destination.Id = source.UserId;
        destination.Photo = new TUserProfilePhotoEmpty();
        destination.Phone = source.Phone;
        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.Username = source.UserName;

        return destination;
    }

    public TPhoneConnectionWebrtc Map(WebRtcConnection source)
    {
        return Map(source, new TPhoneConnectionWebrtc());
    }

    public TPhoneConnectionWebrtc Map(WebRtcConnection source,
        TPhoneConnectionWebrtc destination)
    {
        destination.Ip = source.Ip;
        destination.Ipv6 = source.Ipv6;
        destination.Port = source.Port;
        destination.Turn = source.Turn;
        destination.Stun = source.Stun;
        destination.Username = source.UserName;
        destination.Password = source.Password;

        return destination;
    }
}
