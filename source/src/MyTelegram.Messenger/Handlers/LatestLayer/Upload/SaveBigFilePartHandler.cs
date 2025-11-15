namespace MyTelegram.Messenger.Handlers.LatestLayer.Upload;
/// <summary>
/// Saves a part of a large file (over 10 MB in size) to be later passed to one of the methods.
/// Possible errors
/// Code Type Description
/// 400 FILE_PARTS_INVALID The number of file parts is invalid.
/// 400 FILE_PART_EMPTY The provided file part is empty.
/// 400 FILE_PART_INVALID The file part number is invalid.
/// 400 FILE_PART_SIZE_CHANGED Provided file part size has changed.
/// 400 FILE_PART_SIZE_INVALID The provided file part size is invalid.
/// 400 FILE_PART_TOO_BIG The uploaded file part is too big.
/// 400 FILE_PART_TOO_SMALL The size of the uploaded file part is too small, please see the documentation for the allowed sizes.
/// <para><c>See <a href="https://corefork.telegram.org/method/upload.saveBigFilePart"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SaveBigFilePartHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestSaveBigFilePart, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Upload.RequestSaveBigFilePart obj)
    {
        throw new NotImplementedException();
    }
}