namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

/// <summary>
///     Get logged-in sessions
///     See <a href="https://corefork.telegram.org/method/account.getAuthorizations" />
/// </summary>
internal sealed class GetAuthorizationsHandler(
    IQueryProcessor queryProcessor,
    ILayeredService<IAuthorizationConverter> layeredService)
    :
        RpcResultObjectHandler<Schema.Account.RequestGetAuthorizations, Schema.Account.IAuthorizations>
{
    protected override async Task<Schema.Account.IAuthorizations> HandleCoreAsync(IRequestInput input,
        Schema.Account.RequestGetAuthorizations obj)
    {
        var deviceReadModelList = await queryProcessor
            .ProcessAsync(new GetDeviceByUserIdQuery(input.UserId));
        var r = layeredService.GetConverter(input.Layer).ToAuthorizations(deviceReadModelList, input.PermAuthKeyId);
        return new TAuthorizations { Authorizations = [.. r] };
    }
}