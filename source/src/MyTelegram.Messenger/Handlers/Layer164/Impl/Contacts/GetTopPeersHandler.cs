// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Get most used peers
/// <para>Possible errors</para>
/// Code Type Description
/// 400 TYPES_EMPTY No top peer type was provided.
/// See <a href="https://corefork.telegram.org/method/contacts.getTopPeers" />
///</summary>
internal sealed class GetTopPeersHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetTopPeers, MyTelegram.Schema.Contacts.ITopPeers>,
    Contacts.IGetTopPeersHandler
{
    protected override Task<MyTelegram.Schema.Contacts.ITopPeers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetTopPeers obj)
    {
        throw new NotImplementedException();
    }
}
