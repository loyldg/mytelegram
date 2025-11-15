namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// <a href="https://corefork.telegram.org/api/business#location">Businesses »</a> may advertise their location using this method, see <a href="https://corefork.telegram.org/api/business#location">here »</a> for more info.To remove business location information invoke the method without setting any of the parameters.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateBusinessLocation"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateBusinessLocationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessLocation, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUpdateBusinessLocation obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}