// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Mark <a href="https://corefork.telegram.org/api/reactions">message reactions »</a> as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readReactions" />
///</summary>
internal sealed class ReadReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadReactions, MyTelegram.Schema.Messages.IAffectedHistory>,
    Messages.IReadReactionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadReactions obj)
    {
        throw new NotImplementedException();
    }
}
