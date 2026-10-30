
namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class AddUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestAddUsername, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestAddUsername obj)
    {
        throw new NotImplementedException();
    }
}

