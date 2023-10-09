// ReSharper disable All

namespace MyTelegram.Handlers;

///<summary>
/// Invoke the specified query using the specified API <a href="https://corefork.telegram.org/api/invoking#layers">layer</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 AUTH_BYTES_INVALID The provided authorization is invalid.
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 CONNECTION_API_ID_INVALID The provided API id is invalid.
/// 406 INVITE_HASH_EXPIRED The invite link has expired.
/// See <a href="https://corefork.telegram.org/method/invokeWithLayer" />
///</summary>
internal sealed class InvokeWithLayerHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithLayer, IObject>,
    IInvokeWithLayerHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInvokeWithLayer obj)
    {
        throw new NotImplementedException();
    }
}
