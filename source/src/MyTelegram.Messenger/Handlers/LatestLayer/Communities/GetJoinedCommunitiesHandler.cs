
namespace MyTelegram.Messenger.Handlers.LatestLayer.Communities;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetJoinedCommunitiesHandler : RpcResultObjectHandler<MyTelegram.Schema.Communities.RequestGetJoinedCommunities, MyTelegram.Schema.Messages.IChats>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Communities.RequestGetJoinedCommunities obj)
    {
        throw new NotImplementedException();
    }
}

