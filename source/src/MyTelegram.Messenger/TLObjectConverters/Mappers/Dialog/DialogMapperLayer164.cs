using TDialog = MyTelegram.Schema.TDialog;

namespace MyTelegram.Messenger.TLObjectConverters.Mappers.Dialog;

public class DialogMapperLayer164 : ILayeredMapper, IObjectMapper<IDialogReadModel, TDialog>,
    IObjectMapper<DialogFilter, TDialogFilter>
{
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
        if (source.Draft?.Message?.Length > 0)
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

        //destination.NotifySettings = new TPeerNotifySettings
        //{
        //    ShowPreviews = true,
        //    Silent = false,
        //    //Sound = "default",
        //    MuteUntil = 0
        //};
        if (source.NotifySettings != null)
        {
            destination.NotifySettings = new TPeerNotifySettings
            {
                AndroidSound = new TNotificationSoundDefault(),
                IosSound = new TNotificationSoundDefault(),
                MuteUntil = source.NotifySettings.MuteUntil,
                OtherSound = new TNotificationSoundDefault(),
                ShowPreviews = source.NotifySettings.ShowPreviews,
                Silent = source.NotifySettings.Silent
            };
        }
        else
        {
            destination.NotifySettings = new TPeerNotifySettings();
        }

        destination.TtlPeriod = source.TtlPeriod;
        destination.UnreadMentionsCount = source.UnreadMentionsCount;
        destination.UnreadReactionsCount = source.UnreadReactionsCount;

        return destination;
    }

    TDialog IObjectMapper<IDialogReadModel, TDialog>.Map(IDialogReadModel source)
    {
        return Map(source, new TDialog());
    }

    public TDialogFilter Map(DialogFilter source)
    {
        return Map(source, new TDialogFilter());
    }

    public TDialogFilter Map(DialogFilter source,
        TDialogFilter destination)
    {
        destination.Id = source.Id;
        destination.Contacts = source.Contacts;
        destination.NonContacts = source.NonContacts;
        destination.Groups = source.Groups;
        destination.Bots = source.Bots;
        destination.ExcludeMuted = source.ExcludeMuted;
        destination.ExcludeRead = source.ExcludeRead;
        destination.ExcludeArchived = source.ExcludeArchived;
        destination.Title = source.Title;
        destination.Emoticon = source.Emoticon;
        destination.Broadcasts = source.Broadcasts;
        destination.PinnedPeers = new TVector<IInputPeer>();
        destination.ExcludePeers = new TVector<IInputPeer>();
        destination.IncludePeers = new TVector<IInputPeer>();

        foreach (var peer in source.PinnedPeers)
        {
            destination.PinnedPeers.Add(Map(peer));
        }

        foreach (var peer in source.ExcludePeers)
        {
            destination.ExcludePeers.Add(Map(peer));
        }

        foreach (var peer in source.IncludePeers)
        {
            destination.IncludePeers.Add(Map(peer));
        }

        return destination;
    }

    public IInputPeer Map(InputPeer source)
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
}