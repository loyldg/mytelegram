namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Fetch one or more <a href="https://corefork.telegram.org/api/factcheck">factchecks, see here »</a> for the full flow.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getFactCheck" />
///</summary>
internal sealed class GetFactCheckHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFactCheck, TVector<MyTelegram.Schema.IFactCheck>>
{
    protected override Task<TVector<MyTelegram.Schema.IFactCheck>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetFactCheck obj)
    {
        throw new NotImplementedException();
    }
}
