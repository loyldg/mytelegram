using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Check whether we can post stories as the specified peer.
/// Possible errors
/// Code Type Description
/// 400 BOOSTS_REQUIRED The specified channel must first be <a href="https://corefork.telegram.org/api/boost">boosted by its users</a> in order to perform this action.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// 400 STORIES_TOO_MUCH You have hit the maximum active stories limit as specified by the <a href="https://corefork.telegram.org/api/config#story-expiring-limit-default"><code>story_expiring_limit_*</code> client configuration parameters</a>: you should buy a <a href="https://corefork.telegram.org/api/premium">Premium</a> subscription, delete an active story, or wait for the oldest story to expire.
/// 400 STORY_SEND_FLOOD_MONTHLY_%d You've hit the monthly story limit as specified by the <a href="https://corefork.telegram.org/api/config#stories-sent-monthly-limit-default"><code>stories_sent_monthly_limit_*</code> client configuration parameters</a>: wait %d seconds before posting a new story.
/// 400 STORY_SEND_FLOOD_WEEKLY_%d You've hit the weekly story limit as specified by the <a href="https://corefork.telegram.org/api/config#stories-sent-weekly-limit-default"><code>stories_sent_weekly_limit_*</code> client configuration parameters</a>: wait for %d seconds before posting a new story.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.canSendStory"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CanSendStoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestCanSendStory, ICanSendStoryCount>
{
    protected override Task<ICanSendStoryCount> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestCanSendStory obj)
    {
        return Task.FromResult<ICanSendStoryCount>(new TCanSendStoryCount());
    }
}