namespace MyTelegram.Messenger.Handlers.LatestLayer.Upload;
/// <summary>
/// Request a reupload of a certain file to a <a href="https://corefork.telegram.org/cdn">CDN DC</a>.
/// Possible errors
/// Code Type Description
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 500 CDN_UPLOAD_TIMEOUT A server-side timeout occurred while reuploading the file to the CDN DC.
/// 400 FILE_TOKEN_INVALID The master DC did not accept the <code>file_token</code> (e.g., the token has expired). Continue downloading the file from the master DC using upload.getFile.
/// 400 LOCATION_INVALID The provided location is invalid.
/// 400 REQUEST_TOKEN_INVALID The master DC did not accept the <code>request_token</code> from the CDN DC. Continue downloading the file from the master DC using upload.getFile.
/// 400 RSA_DECRYPT_FAILED Internal RSA decryption failed.
/// <para><c>See <a href="https://corefork.telegram.org/method/upload.reuploadCdnFile"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class ReuploadCdnFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestReuploadCdnFile, TVector<MyTelegram.Schema.IFileHash>>
{
    protected override Task<TVector<MyTelegram.Schema.IFileHash>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Upload.RequestReuploadCdnFile obj)
    {
        throw new NotImplementedException();
    }
}