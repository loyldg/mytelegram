// ReSharper disable All

namespace MyTelegram.Handlers;

///<summary>
/// Invokes a query after successful completion of one of the previous queries.
/// See <a href="https://corefork.telegram.org/method/invokeAfterMsg" />
///</summary>
internal sealed class InvokeAfterMsgHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeAfterMsg, IObject>,
    IInvokeAfterMsgHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInvokeAfterMsg obj)
    {
        throw new NotImplementedException();
    }
}
