
namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetAccessSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestGetAccessSettings, MyTelegram.Schema.Bots.IAccessSettings>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Bots.IAccessSettings> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestGetAccessSettings obj)
    {
        throw new NotImplementedException();
    }
}

