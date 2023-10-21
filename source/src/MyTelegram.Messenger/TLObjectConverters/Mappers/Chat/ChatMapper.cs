using TChatFull = MyTelegram.Schema.TChatFull;

namespace MyTelegram.Messenger.TLObjectConverters.Mappers.Chat;

public class ChatMapper :
    IObjectMapper<ChannelCreatedEvent, TChannel>,
    IObjectMapper<IChannelReadModel, TChannel>,
    IObjectMapper<IChannelFullReadModel, TChannelFull>,
    IObjectMapper<ChatCreatedEvent, TChat>,
    IObjectMapper<IChatReadModel, TChat>,
    IObjectMapper<IChatReadModel, TChatFull>,
    IObjectMapper<ChatBannedRights, TChatBannedRights>,
    IObjectMapper<ChatAdminRights, TChatAdminRights>,
    IObjectMapper<TChatBannedRights, ChatBannedRights>,
    IObjectMapper<TChatAdminRights, ChatAdminRights>,
    ILayeredMapper
{
    public TChannel Map(ChannelCreatedEvent source)
    {
        return Map(source, new TChannel());
    }

    public TChannel Map(ChannelCreatedEvent source,
        TChannel destination)
    {
        destination.AccessHash = source.AccessHash;
        destination.Id = source.ChannelId;
        destination.Broadcast = source.Broadcast;
        destination.Date = source.Date;
        destination.Title = source.Title;
        destination.Megagroup = source.MegaGroup;
        destination.ParticipantsCount = 1;
        destination.DefaultBannedRights = Map(ChatBannedRights.Default);
        destination.Photo = new TChatPhotoEmpty();
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


    public TChat Map(ChatCreatedEvent source)
    {
        return Map(source, new TChat());
    }

    public TChat Map(ChatCreatedEvent source,
        TChat destination)
    {
        destination.CallActive = true;
        destination.CallNotEmpty = true;
        destination.Creator = true;
        destination.Date = source.Date;
        destination.Id = source.ChatId;
        destination.Deactivated = false;
        destination.Title = source.Title;
        destination.ParticipantsCount = source.MemberUidList.Count;
        destination.Left = false;
        destination.Photo = new TChatPhotoEmpty();

        return destination;
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
        if (source.AvailableReactions?.Count > 0)
        {
        }


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
        destination.DefaultBannedRights = Map(source.DefaultBannedRights ?? ChatBannedRights.Default);

        destination.HasLink = source.LinkedChatId.HasValue;
        destination.Noforwards = source.NoForwards;

        return destination;
    }

    TChat IObjectMapper<IChatReadModel, TChat>.Map(IChatReadModel source)
    {
        return Map(source, new TChat());
    }


    public TChat Map(IChatReadModel source,
        TChat destination)
    {
        destination.Id = source.ChatId;
        destination.Title = source.Title;
        destination.Date = source.Date;
        destination.DefaultBannedRights = Map(source.DefaultBannedRights ?? ChatBannedRights.Default);
        destination.ParticipantsCount = source.ChatMembers.Count;
        destination.Deactivated = source.Deactivated;
        if (source is { MigrateToChannelId: not null, MigrateToChannelAccessHash: not null })
        {
            destination.MigratedTo = new TInputChannel
            {
                ChannelId = source.MigrateToChannelId.Value,
                AccessHash = source.MigrateToChannelAccessHash.Value
            };
        }

        destination.Noforwards = source.NoForwards;

        return destination;
    }

    public TChatFull Map(IChatReadModel source,
        TChatFull destination)
    {
        destination.About = source.About ?? string.Empty;
        destination.CanSetUsername = true;
        destination.Id = source.ChatId;
        //destination.ChatPhoto = source.Photo.ToTObject<IPhoto>() ?? new TPhotoEmpty();

        if (source.AvailableReactions?.Count > 0)
        {
        }

        return destination;
    }

    TChatFull IObjectMapper<IChatReadModel, TChatFull>.Map(IChatReadModel source)
    {
        return Map(source, new TChatFull());
    }

    public ChatAdminRights Map(TChatAdminRights source)
    {
        return Map(source, new ChatAdminRights());
    }

    public ChatAdminRights Map(TChatAdminRights source, ChatAdminRights destination)
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


    public ChatBannedRights Map(TChatBannedRights source)
    {
        return Map(source, new ChatBannedRights());
    }

    public ChatBannedRights Map(TChatBannedRights source, ChatBannedRights destination)
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


    public TChatInviteExported Map(IChatInviteReadModel source)
    {
        return Map(source, new TChatInviteExported());
    }

    public TChatInviteExported Map(IChatInviteReadModel source, TChatInviteExported destination)
    {
        destination.Date = source.Date;
        destination.AdminId = source.AdminId;
        destination.ExpireDate = source.ExpireDate;
        destination.Link = source.Link;
        destination.Permanent = source.Permanent;
        destination.RequestNeeded = source.RequestNeeded;
        destination.Revoked = source.Revoked;
        destination.StartDate = source.StartDate;
        destination.Requested = source.Requested;
        destination.Title = source.Title;
        destination.Usage = source.Usage;
        destination.UsageLimit = source.UsageLimit;

        return destination;
    }
}