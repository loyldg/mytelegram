namespace MyTelegram.Messenger.Services.Interfaces;

public interface IDialogAppService
{
    Task<GetDialogOutput> GetDialogsAsync(GetDialogInput input);
    Task ReorderPinnedDialogsAsync(ReorderPinnedDialogsInput input);
}

public interface IChannelAppService
{
    Task InviteToChannelAsync(RequestInfo requestInfo, long channelId, List<long> channelMembers, int date);
}

//public class ChannelAppService : IChannelAppService
//{
//    private readonly IPeerHelper _peerHelper;
//    private readonly IPrivacyAppService _privacyAppService;
//    public Task InviteToChannelAsync(RequestInfo requestInfo, long channelId, List<long> channelMembers, int date)
//    {
//        throw new NotImplementedException();
//    }
//}