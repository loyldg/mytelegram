// ReSharper disable All

namespace MyTelegram.Handlers;

///<summary>
/// Invoke a method within a takeout session
/// See <a href="https://corefork.telegram.org/method/invokeWithTakeout" />
///</summary>
internal sealed class InvokeWithTakeoutHandler : RpcResultObjectHandler<MyTelegram.Schema.RequestInvokeWithTakeout, IObject>,
    IInvokeWithTakeoutHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInvokeWithTakeout obj)
    {
        throw new NotImplementedException();
    }
}
