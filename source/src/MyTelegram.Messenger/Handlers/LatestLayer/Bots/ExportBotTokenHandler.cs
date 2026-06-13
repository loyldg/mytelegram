
namespace MyTelegram.Messenger.Handlers.Bots;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class ExportBotTokenHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestExportBotToken, MyTelegram.Schema.Bots.IExportedBotToken>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Bots.IExportedBotToken> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestExportBotToken obj)
    {
        throw new NotImplementedException();
    }
}

