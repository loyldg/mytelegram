// ReSharper disable All

namespace MyTelegram.Handlers;

///<summary>
/// Invokes a query after a successful completion of previous queries
/// See <a href="https://corefork.telegram.org/method/invokeAfterMsgs" />
///</summary>
internal sealed class InvokeAfterMsgsHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeAfterMsgs, IObject>,
    IInvokeAfterMsgsHandler
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.RequestInvokeAfterMsgs obj)
    {
        throw new NotImplementedException();
    }
}
