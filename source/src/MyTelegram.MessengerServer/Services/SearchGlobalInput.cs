namespace MyTelegram.MessengerServer.Services;

public class SearchGlobalInput : GetPagedListInput
{
    public SearchGlobalInput(MessageType messageType,
        long ownerPeerId,
        //Peer? peer,
        string q,
        long selfUserId,
        int? folderId)
    {
        MessageType = messageType;
        OwnerPeerId = ownerPeerId;
        //Peer = peer;
        Q = q;
        SelfUserId = selfUserId;
        FolderId = folderId;
    }

    public int? FolderId { get; }

    //public bool IsSearchGlobal => true;
    public MessageType MessageType { get; }

    public long OwnerPeerId { get; }

    //public Peer? Peer { get; }
    public string Q { get; }
    public long SelfUserId { get; }
}
