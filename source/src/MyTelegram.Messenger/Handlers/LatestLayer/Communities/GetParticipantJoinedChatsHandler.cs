
namespace MyTelegram.Messenger.Handlers.LatestLayer.Communities;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetParticipantJoinedChatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Communities.RequestGetParticipantJoinedChats, MyTelegram.Schema.Communities.IParticipantJoinedChats>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Communities.IParticipantJoinedChats> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Communities.RequestGetParticipantJoinedChats obj)
    {
        throw new NotImplementedException();
    }
}

