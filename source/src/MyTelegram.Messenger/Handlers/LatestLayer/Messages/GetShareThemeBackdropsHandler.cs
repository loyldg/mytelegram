
namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
/// <para><c>See <a href="" /> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ] [Bot ] [Anonymous ]
/// </remarks>
internal sealed class GetShareThemeBackdropsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetShareThemeBackdrops, MyTelegram.Schema.IShareThemeBackdrops>
{
    protected override Task<MyTelegram.Schema.IShareThemeBackdrops> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetShareThemeBackdrops obj)
    {
        throw new NotImplementedException();
    }
}

