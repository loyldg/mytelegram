namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Get a list of peers that can be used to join a group call, presenting yourself as a specific user/channel.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.getGroupCallJoinAs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetGroupCallJoinAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetGroupCallJoinAs, MyTelegram.Schema.Phone.IJoinAsPeers>
{
    protected override Task<MyTelegram.Schema.Phone.IJoinAsPeers> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestGetGroupCallJoinAs obj)
    {
        throw new NotImplementedException();
    }
}