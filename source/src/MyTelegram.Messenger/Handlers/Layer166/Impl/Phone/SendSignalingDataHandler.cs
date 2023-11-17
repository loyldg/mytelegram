// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Send VoIP signaling data
/// See <a href="https://corefork.telegram.org/method/phone.sendSignalingData" />
///</summary>
internal sealed class SendSignalingDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSendSignalingData, IBool>,
    Phone.ISendSignalingDataHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestSendSignalingData obj)
    {
        throw new NotImplementedException();
    }
}
