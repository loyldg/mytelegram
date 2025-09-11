namespace MyTelegram.Messenger.Handlers.LatestLayer.Stats;

///<summary>
/// Obtain forwards of a <a href="https://corefork.telegram.org/api/stories">story</a> as a message to public chats and reposts by public channels.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/stats.getStoryPublicForwards" />
///</summary>
internal sealed class GetStoryPublicForwardsHandler : RpcResultObjectHandler<MyTelegram.Schema.Stats.RequestGetStoryPublicForwards, MyTelegram.Schema.Stats.IPublicForwards>
{
    protected override Task<MyTelegram.Schema.Stats.IPublicForwards> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stats.RequestGetStoryPublicForwards obj)
    {
        throw new NotImplementedException();
    }
}
