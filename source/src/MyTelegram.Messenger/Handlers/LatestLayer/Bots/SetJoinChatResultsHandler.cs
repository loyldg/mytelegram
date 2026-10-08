
namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SetJoinChatResultsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSetJoinChatResults, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestSetJoinChatResults obj)
    {
        throw new NotImplementedException();
    }
}

