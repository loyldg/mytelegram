namespace MyTelegram.Messenger.Handlers.LatestLayer.Upload;
/// <summary>
/// Get SHA256 hashes for verifying downloaded files
/// Possible errors
/// Code Type Description
/// 400 LOCATION_INVALID The provided location is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/upload.getFileHashes"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetFileHashesHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetFileHashes, TVector<MyTelegram.Schema.IFileHash>>
{
    protected override Task<TVector<MyTelegram.Schema.IFileHash>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Upload.RequestGetFileHashes obj)
    {
        throw new NotImplementedException();
    }
}