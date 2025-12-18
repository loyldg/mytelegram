namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Get web <a href="https://corefork.telegram.org/widgets/login">login widget</a> authorizations
/// <para><c>See <a href="https://corefork.telegram.org/method/account.getWebAuthorizations"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetWebAuthorizationsHandler(IQueryProcessor queryProcessor, ILayeredService<IAuthorizationConverter> layeredService) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetWebAuthorizations, MyTelegram.Schema.Account.IWebAuthorizations>
{
    protected override async Task<MyTelegram.Schema.Account.IWebAuthorizations> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestGetWebAuthorizations obj)
    {
        var deviceReadModelList = await queryProcessor.ProcessAsync(new GetDeviceByUserIdQuery(input.UserId), CancellationToken.None);
        var r = layeredService.GetConverter(input.Layer).ToWebAuthorizations(deviceReadModelList, input.PermAuthKeyId);
        return new TWebAuthorizations
        {
            Authorizations = [..r],
            Users = []
        };
    }
}