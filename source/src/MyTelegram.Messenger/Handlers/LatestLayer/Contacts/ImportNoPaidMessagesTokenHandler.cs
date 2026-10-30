
namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ImportNoPaidMessagesTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestImportNoPaidMessagesToken, MyTelegram.Schema.IUser>
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestImportNoPaidMessagesToken obj)
    {
        throw new NotImplementedException();
    }
}

