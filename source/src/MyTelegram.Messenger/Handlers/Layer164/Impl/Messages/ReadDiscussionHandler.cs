// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Mark a <a href="https://corefork.telegram.org/api/threads">thread</a> as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readDiscussion" />
///</summary>
internal sealed class ReadDiscussionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadDiscussion, IBool>,
    Messages.IReadDiscussionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadDiscussion obj)
    {
        throw new NotImplementedException();
    }
}
