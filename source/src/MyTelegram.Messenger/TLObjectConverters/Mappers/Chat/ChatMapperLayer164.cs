using TChannel = MyTelegram.Schema.TChannel;
using TChannelFull = MyTelegram.Schema.TChannelFull;

namespace MyTelegram.Messenger.TLObjectConverters.Mappers.Chat;
public class ChatMapperLayer164 : ChatMapper, ILayeredMapper,
    IObjectMapper<ChannelCreatedEvent, TChannel>,
    IObjectMapper<IChannelReadModel, TChannel>,
    IObjectMapper<IChannelFullReadModel, TChannelFull>
{
    public TChannel? Map(ChannelCreatedEvent source, TChannel destination)
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

    public TChannelFull? Map(IChannelFullReadModel source, TChannelFull destination)
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
        switch (source.ReactionType)
        {
            case ReactionType.ReactionNone:
                destination.AvailableReactions = new TChatReactionsNone();
                break;
            case ReactionType.ReactionAll:
                destination.AvailableReactions = new TChatReactionsAll
                {
                    AllowCustom = source.AllowCustomReaction
                };
                break;
            case ReactionType.ReactionSome:
                if (source.AvailableReactions?.Count > 0)
                {
                    destination.AvailableReactions = new TChatReactionsSome
                    {
                        Reactions = new TVector<IReaction>(source.AvailableReactions.Select(p => new TReactionEmoji
                        {
                            Emoticon = p
                        }))
                    };
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        destination.Antispam = source.AntiSpam;
        destination.TtlPeriod = source.TtlPeriod;
        destination.TranslationsDisabled = false;


        return destination;
    }

    public TChannel? Map(IChannelReadModel source, TChannel destination)
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

    TChannel? IObjectMapper<ChannelCreatedEvent, TChannel>.Map(ChannelCreatedEvent source)
    {
        return Map(source, new TChannel());
    }

    TChannel? IObjectMapper<IChannelReadModel, TChannel>.Map(IChannelReadModel source)
    {
        return Map(source, new TChannel());
    }

    TChannelFull? IObjectMapper<IChannelFullReadModel, TChannelFull>.Map(IChannelFullReadModel source)
    {
        return Map(source, new TChannelFull());
    }
}
