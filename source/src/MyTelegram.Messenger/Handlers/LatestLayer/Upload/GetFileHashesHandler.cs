namespace MyTelegram.Messenger.Handlers.LatestLayer.Upload;

///<summary>
/// Get SHA256 hashes for verifying downloaded files
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LOCATION_INVALID The provided location is invalid.
/// See <a href="https://corefork.telegram.org/method/upload.getFileHashes" />
///</summary>
internal sealed class GetFileHashesHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetFileHashes, TVector<MyTelegram.Schema.IFileHash>>
{
    protected override Task<TVector<MyTelegram.Schema.IFileHash>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Upload.RequestGetFileHashes obj)
    {
        throw new NotImplementedException();
    }
}
