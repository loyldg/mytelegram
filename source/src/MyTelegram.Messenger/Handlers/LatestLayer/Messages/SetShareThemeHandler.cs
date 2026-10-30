
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class SetShareThemeHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetShareTheme, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetShareTheme obj)
    {
        throw new NotImplementedException();
    }
}

