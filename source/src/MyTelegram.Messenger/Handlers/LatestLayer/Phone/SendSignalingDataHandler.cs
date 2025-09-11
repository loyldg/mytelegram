namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;

///<summary>
/// Send VoIP signaling data
/// See <a href="https://corefork.telegram.org/method/phone.sendSignalingData" />
///</summary>
internal sealed class SendSignalingDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSendSignalingData, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestSendSignalingData obj)
    {
        throw new NotImplementedException();
    }
}
