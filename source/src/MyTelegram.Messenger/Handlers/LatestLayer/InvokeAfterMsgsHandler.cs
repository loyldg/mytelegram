namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Invokes a query after a successful completion of previous queries
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeAfterMsgs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeAfterMsgsHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeAfterMsgs, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.RequestInvokeAfterMsgs obj)
    {
        throw new NotImplementedException();
    }
}