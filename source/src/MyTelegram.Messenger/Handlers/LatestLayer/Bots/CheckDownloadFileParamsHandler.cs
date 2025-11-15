namespace MyTelegram.Messenger.Handlers.LatestLayer.Bots;
/// <summary>
/// Check if a <a href="https://corefork.telegram.org/api/bots/webapps">mini app</a> can request the download of a specific file: called when handling <a href="https://corefork.telegram.org/api/web-events#web-app-request-file-download">web_app_request_file_download events »</a>
/// Possible errors
/// Code Type Description
/// 400 BOT_INVALID This is not a valid bot.
/// <para><c>See <a href="https://corefork.telegram.org/method/bots.checkDownloadFileParams"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CheckDownloadFileParamsHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestCheckDownloadFileParams, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Bots.RequestCheckDownloadFileParams obj)
    {
        throw new NotImplementedException();
    }
}