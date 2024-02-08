// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get logged-in sessions
/// See <a href="https://corefork.telegram.org/method/account.getAuthorizations" />
///</summary>
internal sealed class GetAuthorizationsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetAuthorizations, MyTelegram.Schema.Account.IAuthorizations>,
    Account.IGetAuthorizationsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IAuthorizationConverter> _layeredService;

    public GetAuthorizationsHandler(IQueryProcessor queryProcessor,
        ILayeredService<IAuthorizationConverter> layeredService)
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
    }

    protected override async Task<MyTelegram.Schema.Account.IAuthorizations> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetAuthorizations obj)
    {
        var deviceReadModelList = await _queryProcessor
            .ProcessAsync(new GetDeviceByUidQuery(input.UserId), CancellationToken.None);
        var r = _layeredService.GetConverter(input.Layer).ToAuthorizations(deviceReadModelList, input.PermAuthKeyId);
        return new TAuthorizations { Authorizations = new TVector<Schema.IAuthorization>(r) };
    }
}