namespace MyTelegram.MessengerServer.Services;

public class GetRepliesInput : GetPagedListInput
{
    public int ReplyToMsgId { get; set; }
    public long OwnerPeerId { get; set; }
    public long SelfUserId { get; set; }
}
