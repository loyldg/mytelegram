namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Clear all peer-specific autosave settings.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.deleteAutoSaveExceptions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class DeleteAutoSaveExceptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeleteAutoSaveExceptions, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestDeleteAutoSaveExceptions obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}