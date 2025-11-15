namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Obtain the latest read story ID for all peers when first logging in, returned as a list of <a href="https://corefork.telegram.org/constructor/updateReadStories">updateReadStories</a> updates, see <a href="https://corefork.telegram.org/api/stories#watching-stories">here »</a> for more info.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getAllReadPeerStories"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAllReadPeerStoriesHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetAllReadPeerStories, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetAllReadPeerStories obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Updates = new(), Chats = new(), Users = new(), Date = CurrentDate, });
    }
}