// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Get a list of peers that can be used to join a group call, presenting yourself as a specific user/channel.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/phone.getGroupCallJoinAs" />
///</summary>
internal sealed class GetGroupCallJoinAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallJoinAs, MyTelegram.Schema.Phone.IJoinAsPeers>,
    Phone.IGetGroupCallJoinAsHandler
{
    protected override Task<MyTelegram.Schema.Phone.IJoinAsPeers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestGetGroupCallJoinAs obj)
    {
        throw new NotImplementedException();
    }
}
