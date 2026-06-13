namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// React to a story.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 REACTION_INVALID The specified reaction is invalid.
/// 400 STORIES_NEVER_CREATED This peer hasn't ever posted any stories.
/// 400 STORY_ID_EMPTY You specified no story IDs.
/// 400 STORY_ID_INVALID The specified story ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.sendReaction"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendReactionHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestSendReaction, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestSendReaction obj)
    {
        throw new NotImplementedException();
    }
}