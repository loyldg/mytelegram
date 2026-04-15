
namespace MyTelegram.Messenger.Handlers.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class EditChatParticipantRankHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditChatParticipantRank, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestEditChatParticipantRank obj)
    {
        throw new NotImplementedException();
    }
}

