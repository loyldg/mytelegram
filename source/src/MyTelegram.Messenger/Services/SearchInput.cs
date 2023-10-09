namespace MyTelegram.Messenger.Services;

public class SearchInput : GetPagedListInput
{
    public MessageType MessageType { get; set; }
    public long OwnerPeerId { get; set; }
    public Peer Peer { get; set; } = default!;
    public string Q { get; set; } = default!;
    public long SelfUserId { get; set; }
}