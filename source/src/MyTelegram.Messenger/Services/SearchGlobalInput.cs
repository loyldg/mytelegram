namespace MyTelegram.Messenger.Services;

public class SearchGlobalInput : GetPagedListInput
{
    public int? FolderId { get; set; }
    public bool IsSearchGlobal => true;
    public MessageType MessageType { get; set; }
    public long OwnerPeerId { get; set; }
    public string Q { get; set; } = default!;
    public long SelfUserId { get; set; }

    public List<long> JoinedChannelList { get; set; } = [];

    public bool BroadcastsOnly { get; set; }
    public bool GroupsOnly { get; set; }
    public bool UsersOnly { get; set; }
    public List<long>? Tokens { get; set; }
}