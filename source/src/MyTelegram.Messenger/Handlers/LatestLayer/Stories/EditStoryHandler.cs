namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Edit an uploaded <a href="https://corefork.telegram.org/api/stories">story</a>May also be used in a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business connection</a>, <em>not</em> by wrapping the query in <a href="https://corefork.telegram.org/method/invokeWithBusinessConnection">invokeWithBusinessConnection »</a>, but rather by specifying the ID of a controlled business user in <code>peer</code>: in this context, the method can only be used to edit stories posted by the same business bot on behalf of the user with <a href="https://corefork.telegram.org/method/stories.sendStory">stories.sendStory</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 STORY_NOT_MODIFIED The new story information you passed is equal to the previous story information, thus it wasn't modified.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.editStory"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class EditStoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestEditStory, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestEditStory obj)
    {
        throw new NotImplementedException();
    }
}