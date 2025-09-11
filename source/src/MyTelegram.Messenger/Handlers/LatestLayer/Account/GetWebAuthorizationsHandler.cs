namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get web <a href="https://corefork.telegram.org/widgets/login">login widget</a> authorizations
/// See <a href="https://corefork.telegram.org/method/account.getWebAuthorizations" />
///</summary>
internal sealed class GetWebAuthorizationsHandler(
    IQueryProcessor queryProcessor,
    ILayeredService<IAuthorizationConverter> layeredService)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetWebAuthorizations,
            MyTelegram.Schema.Account.IWebAuthorizations>
{
    protected override async Task<MyTelegram.Schema.Account.IWebAuthorizations> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetWebAuthorizations obj)
    {
        var deviceReadModelList = await queryProcessor
            .ProcessAsync(new GetDeviceByUserIdQuery(input.UserId), CancellationToken.None);
        var r = layeredService.GetConverter(input.Layer).ToWebAuthorizations(deviceReadModelList, input.PermAuthKeyId);
        return new TWebAuthorizations { Authorizations = [.. r], Users = [] };
    }
}
