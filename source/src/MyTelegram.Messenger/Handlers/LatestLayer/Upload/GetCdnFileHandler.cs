namespace MyTelegram.Messenger.Handlers.LatestLayer.Upload;
/// <summary>
/// Download a <a href="https://corefork.telegram.org/cdn">CDN</a> file.
/// Possible errors
/// Code Type Description
/// 400 FILE_TOKEN_INVALID The master DC did not accept the <code>file_token</code> (e.g., the token has expired). Continue downloading the file from the master DC using upload.getFile.
/// 404 METHOD_INVALID The specified method is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/upload.getCdnFile"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetCdnFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetCdnFile, MyTelegram.Schema.Upload.ICdnFile>
{
    protected override Task<MyTelegram.Schema.Upload.ICdnFile> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Upload.RequestGetCdnFile obj)
    {
        throw new NotImplementedException();
    }
}