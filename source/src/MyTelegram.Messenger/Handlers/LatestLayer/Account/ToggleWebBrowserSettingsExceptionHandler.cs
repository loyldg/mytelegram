
namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ToggleWebBrowserSettingsExceptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestToggleWebBrowserSettingsException, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestToggleWebBrowserSettingsException obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Chats = [],
            Users = [],
            Updates = [],
            Date = CurrentDate
        });
    }
}

