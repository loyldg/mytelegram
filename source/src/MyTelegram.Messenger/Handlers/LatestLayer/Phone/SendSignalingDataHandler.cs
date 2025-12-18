namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Send VoIP signaling data
/// Possible errors
/// Code Type Description
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.sendSignalingData"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SendSignalingDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSendSignalingData, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestSendSignalingData obj)
    {
        throw new NotImplementedException();
    }
}