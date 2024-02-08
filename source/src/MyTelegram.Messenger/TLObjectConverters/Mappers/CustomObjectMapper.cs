using MyTelegram.Schema.Updates;
using TPeerSettings = MyTelegram.Schema.TPeerSettings;

namespace MyTelegram.Messenger.TLObjectConverters.Mappers;

public class CustomObjectMapper : ILayeredMapper, 
    IObjectMapper<IPtsReadModel, TState>,
    IObjectMapper<PeerSettings, TPeerSettings>,
    IObjectMapper<SearchGlobalInput, GetMessagesQuery>,
    IObjectMapper<SearchInput, GetMessagesQuery>,
    IObjectMapper<GetHistoryInput, GetMessagesQuery>,
    IObjectMapper<GetMessagesInput, GetMessagesQuery>,
    IObjectMapper<BotCommand, TBotCommand>,
    IObjectMapper<IReadOnlyList<BotCommand>, List<TBotCommand>>,
    //IObjectMapper<ChannelInviteExportedEvent, TChatInviteExported>,
    IObjectMapper<ChatInviteCreatedEvent, TChatInviteExported>,
    IObjectMapper<WebRtcConnection, TPhoneConnectionWebrtc>,
    IObjectMapper<List<WebRtcConnection>, List<TPhoneConnectionWebrtc>>,
    IObjectMapper<PeerNotifySettings, Schema.Messages.TPeerSettings>,
    IObjectMapper<IDeviceReadModel, TWebAuthorization>,
    IObjectMapper<IChatInviteReadModel, TChatInviteExported>,
    IObjectMapper<IFileReadModel, FileItem>,
    IObjectMapper<DcOption, TDcOption>,
    IObjectMapper<List<DcOption>, List<TDcOption>>,
    IObjectMapper<TInputMediaVenue, TMessageMediaVenue>,
    IObjectMapper<MessageFwdHeader, TMessageFwdHeader>,
    IObjectMapper<GetRepliesInput, GetMessagesQuery>,
    IObjectMapper<InputPeer, IInputPeer>
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

    public TChatInviteExported Map(IChatInviteReadModel source)
    {
        return Map(source, new TChatInviteExported());
    }

    public TChatInviteExported Map(IChatInviteReadModel source,
        TChatInviteExported destination)
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



    public TWebAuthorization? Map(IDeviceReadModel source, TWebAuthorization destination)
    {
        destination.Hash = source.Hash;
        destination.Platform = source.Platform;
        destination.Ip = source.Ip;
        destination.DateActive = source.DateActive;
        destination.DateCreated = source.DateCreated;
        destination.Browser = source.AppName;

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

    public IInputPeer? Map(InputPeer source)
    {
        switch (source.Peer.PeerType)
        {
            case PeerType.Empty:
            case PeerType.Self:
            case PeerType.User:
                return new TInputPeerUser { AccessHash = source.AccessHash, UserId = source.Peer.PeerId };
            case PeerType.Chat:
                return new TInputPeerChat { ChatId = source.Peer.PeerId };
            case PeerType.Channel:
                return new TInputPeerChannel { AccessHash = source.AccessHash, ChannelId = source.Peer.PeerId };
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public IInputPeer? Map(InputPeer source,
        IInputPeer destination)
    {
        throw new NotImplementedException();
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
        destination.Imported = source.Imported;
        destination.SavedOut=source.SavedOut;
        destination.FromId = source.FromId.ToPeer();
        destination.FromName = source.FromName;
        destination.ChannelPost = source.ChannelPost;
        destination.PostAuthor = source.PostAuthor;
        destination.Date = source.Date;
        destination.SavedFromPeer = source.SavedFromPeer!.ToPeer();
        destination.SavedFromMsgId = source.SavedFromMsgId;
        destination.SavedFromId = source.SavedFromId.ToPeer();
        destination.SavedFromName = source.SavedFromName;
        destination.SavedDate = source.SavedDate;
        destination.PsaType = source.PsaType;

        return destination;
    }



    public Schema.Messages.TPeerSettings? Map(PeerNotifySettings source,
        Schema.Messages.TPeerSettings destination)
    {
        throw new NotImplementedException();
    }

    Schema.Messages.TPeerSettings? IObjectMapper<PeerNotifySettings, Schema.Messages.TPeerSettings>.Map(
        PeerNotifySettings source)
    {
        return Map(source, new Schema.Messages.TPeerSettings());
    }

    public TPeerSettings Map(PeerSettings source)
    {
        return Map(source, new TPeerSettings());
    }

    public TPeerSettings Map(PeerSettings source,
        TPeerSettings destination)
    {
        destination.AddContact = source.AddContact;
        //destination.Autoarchived = source.NeedContactsException;
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
                0
            )
        { IsSearchGlobal = source.IsSearchGlobal };
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

    TWebAuthorization? IObjectMapper<IDeviceReadModel, TWebAuthorization>.Map(IDeviceReadModel source)
    {
        return Map(source, new TWebAuthorization());
    }

    public TChatInviteExported? Map(ChatInviteCreatedEvent source)
    {
        return Map(source, new TChatInviteExported());
    }

    public TChatInviteExported? Map(ChatInviteCreatedEvent source, TChatInviteExported destination)
    {
        destination.Date = source.Date;
        destination.AdminId = source.AdminId;
        destination.ExpireDate = source.ExpireDate;
        destination.Link = source.Hash;
        destination.Permanent = source.Permanent;
        destination.RequestNeeded = source.RequestNeeded;
        destination.Revoked = false;
        destination.StartDate = source.StartDate;
        //destination.Requested = 0;
        destination.Title = source.Title;
        destination.Usage = 0;
        destination.UsageLimit = source.UsageLimit;

        return destination;
    }
}
