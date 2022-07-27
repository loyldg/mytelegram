namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IMessageAppService
{
    Task<GetMessageOutput> GetChannelDifferenceAsync(GetDifferenceInput input);
    Task<GetMessageOutput> GetDifferenceAsync(GetDifferenceInput input);
    Task<GetMessageOutput> GetHistoryAsync(GetHistoryInput input);
    Task<GetMessageOutput> GetMessagesAsync(GetMessagesInput input);
    Task<GetMessageOutput> SearchAsync(SearchInput input);
    Task<GetMessageOutput> SearchGlobalAsync(SearchGlobalInput input);
    Task SendMessageAsync(SendMessageInput input);
    Task<GetMessageOutput> GetRepliesAsync(GetRepliesInput input);
}