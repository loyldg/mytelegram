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
