namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Get phone call configuration to be passed to libtgvoip's shared config
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.getCallConfig"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetCallConfigHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestGetCallConfig, MyTelegram.Schema.IDataJSON>
{
    protected override Task<MyTelegram.Schema.IDataJSON> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestGetCallConfig obj)
    {
        throw new NotImplementedException();
    }
}