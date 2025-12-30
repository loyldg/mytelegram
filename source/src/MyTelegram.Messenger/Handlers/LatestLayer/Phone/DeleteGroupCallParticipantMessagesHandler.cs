
namespace MyTelegram.Messenger.Handlers.Phone;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class DeleteGroupCallParticipantMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestDeleteGroupCallParticipantMessages, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestDeleteGroupCallParticipantMessages obj)
    {
        throw new NotImplementedException();
    }
}

