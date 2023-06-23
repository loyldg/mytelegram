namespace MyTelegram.MessengerServer.Services;

public class SearchInput : GetPagedListInput
{
    public SearchInput(MessageType messageType,
        long ownerPeerId,
        Peer peer,
        string q,
        long selfUserId)
    {
        MessageType = messageType;
        OwnerPeerId = ownerPeerId;
        Peer = peer;
        Q = q;
        SelfUserId = selfUserId;
    }

    public MessageType MessageType { get; }
    public long OwnerPeerId { get; }
    public Peer Peer { get; }
    public string Q { get; }
    public long SelfUserId { get; }
}