// ReSharper disable All

namespace MyTelegram.Handlers.Upload;

///<summary>
/// Get SHA256 hashes for verifying downloaded <a href="https://corefork.telegram.org/cdn">CDN</a> files
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 400 RSA_DECRYPT_FAILED Internal RSA decryption failed.
/// See <a href="https://corefork.telegram.org/method/upload.getCdnFileHashes" />
///</summary>
internal sealed class GetCdnFileHashesHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetCdnFileHashes, TVector<MyTelegram.Schema.IFileHash>>,
    Upload.IGetCdnFileHashesHandler
{
    protected override Task<TVector<MyTelegram.Schema.IFileHash>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Upload.RequestGetCdnFileHashes obj)
    {
        throw new NotImplementedException();
    }
}
