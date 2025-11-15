namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Uploads a <a href="https://corefork.telegram.org/api/stories">Telegram Story</a>.May also be used in a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business connection</a>, <em>not</em> by wrapping the query in <a href="https://corefork.telegram.org/method/invokeWithBusinessConnection">invokeWithBusinessConnection »</a>, but rather by specifying the ID of a controlled business user in <code>peer</code>.
/// Possible errors
/// Code Type Description
/// 400 BOOSTS_REQUIRED The specified channel must first be <a href="https://corefork.telegram.org/api/boost">boosted by its users</a> in order to perform this action.
/// 403 BOT_ACCESS_FORBIDDEN The specified method <em>can</em> be used over a <a href="https://corefork.telegram.org/api/bots/connected-business-bots">business connection</a> for some operations, but the specified query attempted an operation that is not allowed over a business connection.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 IMAGE_PROCESS_FAILED Failure while processing image.
/// 400 MEDIA_EMPTY The provided media object is invalid.
/// 400 MEDIA_FILE_INVALID The specified media file is invalid.
/// 400 MEDIA_TYPE_INVALID The specified media type cannot be used in stories.
/// 400 MEDIA_VIDEO_STORY_MISSING A non-story video cannot be repubblished as a story (emitted when trying to resend a non-story video as a story using inputDocument).
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// 400 STORIES_TOO_MUCH You have hit the maximum active stories limit as specified by the <a href="https://corefork.telegram.org/api/config#story-expiring-limit-default"><code>story_expiring_limit_*</code> client configuration parameters</a>: you should buy a <a href="https://corefork.telegram.org/api/premium">Premium</a> subscription, delete an active story, or wait for the oldest story to expire.
/// 400 STORY_PERIOD_INVALID The specified story period is invalid for this account.
/// 400 VENUE_ID_INVALID The specified venue ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.sendStory"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class SendStoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestSendStory, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestSendStory obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Updates = [], Chats = [], Users = [], Date = CurrentDate });
    }
}