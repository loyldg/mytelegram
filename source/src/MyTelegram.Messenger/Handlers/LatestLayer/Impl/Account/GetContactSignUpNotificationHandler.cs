// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Whether the user will receive notifications when contacts sign up
/// See <a href="https://corefork.telegram.org/method/account.getContactSignUpNotification" />
///</summary>
internal sealed class GetContactSignUpNotificationHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetContactSignUpNotification, IBool>,
    Account.IGetContactSignUpNotificationHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetContactSignUpNotificationHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetContactSignUpNotification obj)
    {
        var user = await _queryProcessor
                .ProcessAsync(new GetUserByIdQuery(input.UserId), CancellationToken.None)
            ;

        return user!.ShowContactSignUpNotification ? new TBoolTrue() : new TBoolFalse();
    }
}