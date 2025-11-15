namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Clear saved payment information
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.clearSavedInfo"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ClearSavedInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestClearSavedInfo, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestClearSavedInfo obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}