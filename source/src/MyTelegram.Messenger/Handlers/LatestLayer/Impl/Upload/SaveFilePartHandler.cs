// ReSharper disable All

namespace MyTelegram.Handlers.Upload;

///<summary>
/// Saves a part of file for further sending to one of the methods.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FILE_PART_EMPTY The provided file part is empty.
/// 400 FILE_PART_INVALID The file part number is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/upload.saveFilePart" />
///</summary>
internal sealed class SaveFilePartHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestSaveFilePart, IBool>,
    Upload.ISaveFilePartHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Upload.RequestSaveFilePart obj)
    {
        throw new NotImplementedException();
    }
}
