namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Invoke with the given message range
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeWithMessagesRange"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeWithMessagesRangeHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeWithMessagesRange, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.RequestInvokeWithMessagesRange obj)
    {
        throw new NotImplementedException();
    }
}