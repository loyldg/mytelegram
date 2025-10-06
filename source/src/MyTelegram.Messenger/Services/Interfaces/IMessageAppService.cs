namespace MyTelegram.Messenger.Services.Interfaces;

public interface IMessageAppService
{
    void CheckBotPermission(long requestUserId, Peer toPeer);

    Task CheckSendAsAsync(long requestUserId, Peer toPeer, Peer? sendAs);

    Task<GetMessageOutput> GetChannelDifferenceAsync(GetDifferenceInput input);
    Task<GetMessageOutput> GetDifferenceAsync(GetDifferenceInput input);
    Task<GetMessageOutput> GetHistoryAsync(GetHistoryInput input);
    Task<GetMessageOutput> GetMessagesAsync(GetMessagesInput input);
    Task<GetMessageOutput> GetRepliesAsync(GetRepliesInput input);

    Task<GetMessageOutput> SearchAsync(SearchInput input);
    Task<GetMessageOutput> SearchGlobalAsync(SearchGlobalInput input);
    Task SendMessageAsync(List<SendMessageInput> inputs);
    Task<SearchPostsResult> SearchPostsAsync(long selfUserId, SearchPostsQuery searchPostsQuery);
    (HashSet<long> userIds, HashSet<long> channelIds) GetExtraPeerIds(
        IReadOnlyCollection<IMessageReadModel> messageReadModels);

    Task<bool> CanSendAsPeerAsync(long channelId, long userId);
    Task<List<long>> ProcessMessageEntitiesAsync(string? message, IList<IMessageEntity>? entities, Peer toPeer);
    List<string> GetHashtags(string? message);
    Task<bool> IsValidSendAsPeerAsync(long requestUserId, Peer toPeer, Peer? sendAsPeer);
    Task<TMessageMediaWebPage?> CreateInvitePreviewIfAnyAsync(
        string text,
        string joinChatDomain);
}