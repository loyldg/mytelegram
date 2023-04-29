namespace MyTelegram.MessengerServer.Services;

public class GetPeerSettingsListInput
{
    public GetPeerSettingsListInput(long userId,
        List<long> targetUserIdList)
    {
        UserId = userId;
        TargetUserIdList = targetUserIdList;
    }

    public List<long> TargetUserIdList { get; }

    public long UserId { get; }
}
