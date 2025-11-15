namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Deletes some posted <a href="https://corefork.telegram.org/api/stories">stories</a>.
/// Possible errors
/// Code Type Description
/// 403 BOT_ACCESS_FORBIDDEN The specified method <em>can</em> be used over a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business connection</a> for some operations, but the specified query attempted an operation that is not allowed over a business connection.
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORY_ID_EMPTY You specified no story IDs.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.deleteStories"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestDeleteStories, TVector<int>>
{
    protected override Task<TVector<int>> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestDeleteStories obj)
    {
        throw new NotImplementedException();
    }
}