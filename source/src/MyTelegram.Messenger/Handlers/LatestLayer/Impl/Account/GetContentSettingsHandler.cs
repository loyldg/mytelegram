// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get sensitive content settings
/// See <a href="https://corefork.telegram.org/method/account.getContentSettings" />
///</summary>
internal sealed class GetContentSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetContentSettings, MyTelegram.Schema.Account.IContentSettings>,
    Account.IGetContentSettingsHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetContentSettingsHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<MyTelegram.Schema.Account.IContentSettings> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetContentSettings obj)
    {
        var user = await _queryProcessor
                .ProcessAsync(new GetUserByIdQuery(input.UserId), CancellationToken.None)
            ;

        return new TContentSettings
        {
            SensitiveCanChange = user!.SensitiveCanChange,
            SensitiveEnabled = user.SensitiveEnabled
        };
    }
}
