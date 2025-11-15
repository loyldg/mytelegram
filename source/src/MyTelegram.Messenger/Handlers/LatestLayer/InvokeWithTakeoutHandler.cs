namespace MyTelegram.Messenger.Handlers;
/// <summary>
/// Invoke a method within a <a href="https://corefork.telegram.org/api/takeout">takeout session, see here » for more info</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/invokeWithTakeout"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class InvokeWithTakeoutHandler : BaseObjectHandler<MyTelegram.Schema.RequestInvokeWithTakeout, IObject>
{
    protected override Task<IObject> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.RequestInvokeWithTakeout obj)
    {
        throw new NotImplementedException();
    }
}