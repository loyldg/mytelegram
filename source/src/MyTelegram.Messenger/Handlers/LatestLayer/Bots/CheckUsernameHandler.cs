
namespace MyTelegram.Messenger.Handlers.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class CheckUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestCheckUsername, IBool>, IObjectHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestCheckUsername obj)
    {
        throw new NotImplementedException();
    }
}

