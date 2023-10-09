// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Get phone call configuration to be passed to libtgvoip's shared config
/// See <a href="https://corefork.telegram.org/method/phone.getCallConfig" />
///</summary>
internal sealed class GetCallConfigHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetCallConfig, MyTelegram.Schema.IDataJSON>,
    Phone.IGetCallConfigHandler
{
    protected override Task<MyTelegram.Schema.IDataJSON> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestGetCallConfig obj)
    {
        throw new NotImplementedException();
    }
}
