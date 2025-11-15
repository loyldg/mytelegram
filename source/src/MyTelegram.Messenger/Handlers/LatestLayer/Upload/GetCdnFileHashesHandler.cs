namespace MyTelegram.Messenger.Handlers.LatestLayer.Upload;
/// <summary>
/// Get SHA256 hashes for verifying downloaded <a href="https://corefork.telegram.org/cdn">CDN</a> files
/// Possible errors
/// Code Type Description
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 400 FILE_TOKEN_INVALID The master DC did not accept the <code>file_token</code> (e.g., the token has expired). Continue downloading the file from the master DC using upload.getFile.
/// 400 RSA_DECRYPT_FAILED Internal RSA decryption failed.
/// <para><c>See <a href="https://corefork.telegram.org/method/upload.getCdnFileHashes"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetCdnFileHashesHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetCdnFileHashes, TVector<MyTelegram.Schema.IFileHash>>
{
    protected override Task<TVector<MyTelegram.Schema.IFileHash>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Upload.RequestGetCdnFileHashes obj)
    {
        throw new NotImplementedException();
    }
}