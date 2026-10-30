
namespace MyTelegram.Messenger.Handlers.LatestLayer.Wallet;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ExportSecretPhraseHandler : RpcResultObjectHandler<MyTelegram.Schema.Wallet.RequestExportSecretPhrase, MyTelegram.Schema.Wallet.ISecretPhraseParts>
{
    protected override Task<MyTelegram.Schema.Wallet.ISecretPhraseParts> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Wallet.RequestExportSecretPhrase obj)
    {
        throw new NotImplementedException();
    }
}

