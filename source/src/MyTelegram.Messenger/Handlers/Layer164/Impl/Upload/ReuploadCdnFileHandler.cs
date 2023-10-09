// ReSharper disable All

namespace MyTelegram.Handlers.Upload;

///<summary>
/// Request a reupload of a certain file to a <a href="https://corefork.telegram.org/cdn">CDN DC</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 500 CDN_UPLOAD_TIMEOUT A server-side timeout occurred while reuploading the file to the CDN DC.
/// 400 FILE_TOKEN_INVALID The specified file token is invalid.
/// 400 RSA_DECRYPT_FAILED Internal RSA decryption failed.
/// See <a href="https://corefork.telegram.org/method/upload.reuploadCdnFile" />
///</summary>
internal sealed class ReuploadCdnFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestReuploadCdnFile, TVector<MyTelegram.Schema.IFileHash>>,
    Upload.IReuploadCdnFileHandler
{
    protected override Task<TVector<MyTelegram.Schema.IFileHash>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Upload.RequestReuploadCdnFile obj)
    {
        throw new NotImplementedException();
    }
}
