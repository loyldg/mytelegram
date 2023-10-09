// ReSharper disable All

namespace MyTelegram.Handlers.Upload;

///<summary>
/// Returns content of a web file, by proxying the request through telegram, see the <a href="https://corefork.telegram.org/api/files#downloading-webfiles">webfile docs for more info</a>.<strong>Note</strong>: the query must be sent to the DC specified in the <code>webfile_dc_id</code> <a href="https://corefork.telegram.org/api/config#mtproto-configuration">MTProto configuration field</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DOCUMENT_INVALID The specified document is invalid.
/// 400 LOCATION_INVALID The provided location is invalid.
/// See <a href="https://corefork.telegram.org/method/upload.getWebFile" />
///</summary>
internal sealed class GetWebFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetWebFile, MyTelegram.Schema.Upload.IWebFile>,
    Upload.IGetWebFileHandler
{
    protected override Task<MyTelegram.Schema.Upload.IWebFile> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Upload.RequestGetWebFile obj)
    {
        throw new NotImplementedException();
    }
}
