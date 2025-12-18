namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Get info about an unsupported deep link, see <a href="https://corefork.telegram.org/api/links#unsupported-links">here for more info »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.getDeepLinkInfo"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✔]
/// </remarks>
internal sealed class GetDeepLinkInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetDeepLinkInfo, MyTelegram.Schema.Help.IDeepLinkInfo>
{
    protected override Task<MyTelegram.Schema.Help.IDeepLinkInfo> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Help.RequestGetDeepLinkInfo obj)
    {
        throw new NotImplementedException();
    }
}