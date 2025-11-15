using MyTelegram.Schema.Stories;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Get the <a href="https://corefork.telegram.org/api/reactions">reaction</a> and interaction list of a <a href="https://corefork.telegram.org/api/stories">story</a> posted to a channel, along with the sender of each reaction.Can only be used by channel admins.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getStoryReactionsList"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetStoryReactionsListHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetStoryReactionsList, MyTelegram.Schema.Stories.IStoryReactionsList>
{
    protected override Task<MyTelegram.Schema.Stories.IStoryReactionsList> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetStoryReactionsList obj)
    {
        return Task.FromResult<IStoryReactionsList>(new TStoryReactionsList { Chats = [], Users = [], Reactions = [] });
    }
}