
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetUserAddressesHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestGetUserAddresses, MyTelegram.Schema.Wallet.IUserAddresses>
{
    protected override Task<MyTelegram.Schema.Wallet.IUserAddresses> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestGetUserAddresses obj)
    {
        throw new NotImplementedException();
    }
}

