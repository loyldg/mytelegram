
namespace MyTelegram.Messenger.Handlers.Stories;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class StartLiveHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestStartLive, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestStartLive obj)
    {
        throw new NotImplementedException();
    }
}

