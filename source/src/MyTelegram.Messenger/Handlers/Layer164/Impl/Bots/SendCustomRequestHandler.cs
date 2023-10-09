// ReSharper disable All

namespace MyTelegram.Handlers.Bots;

///<summary>
/// Sends a custom request; for bots only
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// 400 METHOD_INVALID The specified method is invalid.
/// 403 USER_BOT_INVALID User accounts must provide the <code>bot</code> method parameter when calling this method. If there is no such method parameter, this method can only be invoked by bot accounts.
/// See <a href="https://corefork.telegram.org/method/bots.sendCustomRequest" />
///</summary>
internal sealed class SendCustomRequestHandler : RpcResultObjectHandler<MyTelegram.Schema.Bots.RequestSendCustomRequest, MyTelegram.Schema.IDataJSON>,
    Bots.ISendCustomRequestHandler
{
    protected override Task<MyTelegram.Schema.IDataJSON> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Bots.RequestSendCustomRequest obj)
    {
        throw new NotImplementedException();
    }
}
