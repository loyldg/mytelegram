namespace MyTelegram.ReadModel;

public interface IChatInviteImporterReadModel : IReadModel
{
    string Id { get; }
    long PeerId { get; }
    long InviteId { get; }
    long UserId { get; }
    //bool RequestNeeded { get; }
    ChatInviteRequestState ChatInviteRequestState { get; }
    bool Approved { get; }
    long? ApprovedBy { get; }
    int Date { get; }
    string? About { get; }
    bool ViaChatList { get; }
}