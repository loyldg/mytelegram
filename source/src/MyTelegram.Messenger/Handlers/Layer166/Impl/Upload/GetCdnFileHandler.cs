// ReSharper disable All

namespace MyTelegram.Handlers.Upload;

///<summary>
/// Download a <a href="https://corefork.telegram.org/cdn">CDN</a> file.
/// See <a href="https://corefork.telegram.org/method/upload.getCdnFile" />
///</summary>
internal sealed class GetCdnFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetCdnFile, MyTelegram.Schema.Upload.ICdnFile>,
    Upload.IGetCdnFileHandler
{
    protected override Task<MyTelegram.Schema.Upload.ICdnFile> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Upload.RequestGetCdnFile obj)
    {
        throw new NotImplementedException();
    }
}
