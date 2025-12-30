
namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetPasskeysHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetPasskeys, MyTelegram.Schema.Account.IPasskeys>
{
    protected override async Task<MyTelegram.Schema.Account.IPasskeys> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetPasskeys obj)
    {
        return new TPasskeys
        {
            Passkeys = []
        };
    }
}

