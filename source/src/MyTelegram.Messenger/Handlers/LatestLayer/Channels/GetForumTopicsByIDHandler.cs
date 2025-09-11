namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Get forum topics by their ID
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_FORUM_MISSING This supergroup is not a forum.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 TOPICS_EMPTY &nbsp;
/// See <a href="https://corefork.telegram.org/method/channels.getForumTopicsByID" />
///</summary>
internal sealed class GetForumTopicsByIDHandler(IAccessHashHelper accessHashHelper,IPeerHelper peerHelper, IPtsHelper ptsHelper) : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetForumTopicsByID, MyTelegram.Schema.Messages.IForumTopics>
{
    protected override async Task<MyTelegram.Schema.Messages.IForumTopics> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetForumTopicsByID obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Channel);
        var peer = peerHelper.GetChannel(obj.Channel);
        var pts = ptsHelper.GetCachedPts(peer.PeerId);

        var forumTopics = new TForumTopics
        {
            Pts=pts,
            Chats = new(),
            Messages = new(),
            Topics = new(),
            Users = new(),
        };

        return forumTopics;
    }
}
