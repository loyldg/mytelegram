// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get web <a href="https://corefork.telegram.org/widgets/login">login widget</a> authorizations
/// See <a href="https://corefork.telegram.org/method/account.getWebAuthorizations" />
///</summary>
internal sealed class GetWebAuthorizationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetWebAuthorizations, MyTelegram.Schema.Account.IWebAuthorizations>,
    Account.IGetWebAuthorizationsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IAuthorizationConverter> _layeredService;

    public GetWebAuthorizationsHandler(IQueryProcessor queryProcessor, ILayeredService<IAuthorizationConverter> layeredService)
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
    }

    protected override async Task<MyTelegram.Schema.Account.IWebAuthorizations> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetWebAuthorizations obj)
    {
        var deviceReadModelList = await _queryProcessor
            .ProcessAsync(new GetDeviceByUidQuery(input.UserId), CancellationToken.None);
        var r = _layeredService.GetConverter(input.Layer).ToWebAuthorizations(deviceReadModelList, input.PermAuthKeyId);
        return new TWebAuthorizations { Authorizations = new TVector<Schema.IWebAuthorization>(r), Users = new() };
    }
}
