
namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class CreateOnrampSessionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestCreateOnrampSession, MyTelegram.Schema.IOnrampSession>
{
    protected override Task<MyTelegram.Schema.IOnrampSession> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestCreateOnrampSession obj)
    {
        throw new NotImplementedException();
    }
}

