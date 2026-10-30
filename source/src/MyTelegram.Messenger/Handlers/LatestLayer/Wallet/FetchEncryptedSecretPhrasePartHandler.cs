
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class FetchEncryptedSecretPhrasePartHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestFetchEncryptedSecretPhrasePart, MyTelegram.Schema.Wallet.IEncryptedSecretPhrasePart>
{
    protected override Task<MyTelegram.Schema.Wallet.IEncryptedSecretPhrasePart> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestFetchEncryptedSecretPhrasePart obj)
    {
        throw new NotImplementedException();
    }
}

