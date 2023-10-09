namespace MyTelegram.Messenger.Services;

public class GetDialogInput : GetPagedListInput
{
    public int? FolderId { get; set; }

    //public int OffsetPeerId { get; set; }
    public Peer? OffsetPeer { get; set; }

    public long OwnerId { get; set; }

    //public bool ExcludePinned { get; set; }
    public List<long>? PeerIdList { get; set; }
    public bool? Pinned { get; set; }
}