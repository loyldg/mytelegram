using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Fetch the <a href="https://corefork.telegram.org/api/stories#pinned-or-archived-stories">story archive »</a> of a peer we control.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getStoriesArchive"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStoriesArchiveHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoriesArchive, MyTelegram.Schema.Stories.IStories>
{
    protected override Task<MyTelegram.Schema.Stories.IStories> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetStoriesArchive obj)
    {
        return Task.FromResult<MyTelegram.Schema.Stories.IStories>(new TStories { Chats = new(), Stories = new(), Users = new(), });
    }
}