// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get poll results
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getPollResults" />
///</summary>
internal sealed class GetPollResultsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPollResults, MyTelegram.Schema.IUpdates>,
    Messages.IGetPollResultsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetPollResults obj)
    {
        throw new NotImplementedException();
    }
}
