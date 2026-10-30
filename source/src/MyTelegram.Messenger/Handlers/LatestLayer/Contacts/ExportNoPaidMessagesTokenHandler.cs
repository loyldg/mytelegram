
namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ExportNoPaidMessagesTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestExportNoPaidMessagesToken, MyTelegram.Schema.IExportedNoPaidMessagesToken>
{
    protected override Task<MyTelegram.Schema.IExportedNoPaidMessagesToken> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestExportNoPaidMessagesToken obj)
    {
        throw new NotImplementedException();
    }
}

