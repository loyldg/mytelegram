// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Edit <a href="https://corefork.telegram.org/api/forum">forum topic</a>; requires <a href="https://corefork.telegram.org/api/rights"><code>manage_topics</code> rights</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 TOPIC_ID_INVALID The specified topic ID is invalid.
/// 400 TOPIC_NOT_MODIFIED The updated topic info is equal to the current topic info, nothing was changed.
/// See <a href="https://corefork.telegram.org/method/channels.editForumTopic" />
///</summary>
internal sealed class EditForumTopicHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestEditForumTopic, MyTelegram.Schema.IUpdates>,
    Channels.IEditForumTopicHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestEditForumTopic obj)
    {
        throw new NotImplementedException();
    }
}
