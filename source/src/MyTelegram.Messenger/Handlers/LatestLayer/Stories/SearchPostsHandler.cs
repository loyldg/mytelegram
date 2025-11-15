using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Globally search for <a href="https://corefork.telegram.org/api/stories">stories</a> using a hashtag or a <a href="https://corefork.telegram.org/api/stories#location-tags">location media area</a>, see <a href="https://corefork.telegram.org/api/stories#searching-stories">here »</a> for more info on the full flow.Either <code>hashtag</code> <strong>or</strong> <code>area</code> <strong>must</strong> be set when invoking the method.
/// Possible errors
/// Code Type Description
/// 400 HASHTAG_INVALID The specified hashtag is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.searchPosts"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SearchPostsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestSearchPosts, MyTelegram.Schema.Stories.IFoundStories>
{
    protected override Task<MyTelegram.Schema.Stories.IFoundStories> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestSearchPosts obj)
    {
        return Task.FromResult<IFoundStories>(new TFoundStories { Chats = [], Stories = [], Users = [] });
    }
}