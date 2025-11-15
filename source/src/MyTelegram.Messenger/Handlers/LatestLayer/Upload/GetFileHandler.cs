namespace MyTelegram.Messenger.Handlers.LatestLayer.Upload;
/// <summary>
/// Returns content of a whole file or its part.
/// Possible errors
/// Code Type Description
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 406 FILEREF_UPGRADE_NEEDED The client has to be updated in order to support <a href="https://corefork.telegram.org/api/file-references">file references</a>.
/// 400 FILE_ID_INVALID The provided file id is invalid.
/// 400 FILE_REFERENCE_EMPTY An empty <a href="https://corefork.telegram.org/api/file-references">file reference</a> was specified.
/// 400 FILE_REFERENCE_EXPIRED File reference expired, it must be refetched as described in <a href="https://corefork.telegram.org/api/file-references">the documentation</a>.
/// 400 FILE_REFERENCE_INVALID The specified <a href="https://corefork.telegram.org/api/file-references">file reference</a> is invalid.
/// 420 FLOOD_PREMIUM_WAIT_%d Please wait %d seconds before repeating the action, or purchase a <a href="https://corefork.telegram.org/api/premium">Telegram Premium subscription</a> to remove this rate limit.
/// 400 LIMIT_INVALID The provided limit is invalid.
/// 400 LOCATION_INVALID The provided location is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 OFFSET_INVALID The provided offset is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/upload.getFile"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetFileHandler : RpcResultObjectHandler<MyTelegram.Schema.Upload.RequestGetFile, MyTelegram.Schema.Upload.IFile>
{
    protected override Task<MyTelegram.Schema.Upload.IFile> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Upload.RequestGetFile obj)
    {
        throw new NotImplementedException();
    }
}