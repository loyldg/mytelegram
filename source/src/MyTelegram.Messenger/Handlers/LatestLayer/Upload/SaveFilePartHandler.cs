namespace MyTelegram.Messenger.Handlers.LatestLayer.Upload;
/// <summary>
/// Saves a part of file for further sending to one of the methods.
/// Possible errors
/// Code Type Description
/// 400 FILE_PART_EMPTY The provided file part is empty.
/// 400 FILE_PART_INVALID The file part number is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/upload.saveFilePart"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SaveFilePartHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestSaveFilePart, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Upload.RequestSaveFilePart obj)
    {
        throw new NotImplementedException();
    }
}