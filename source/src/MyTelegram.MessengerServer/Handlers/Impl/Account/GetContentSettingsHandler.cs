using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetContentSettingsHandler : RpcResultObjectHandler<RequestGetContentSettings, IContentSettings>,
    IGetContentSettingsHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetContentSettingsHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IContentSettings> HandleCoreAsync(IRequestInput input,
        RequestGetContentSettings obj)
    {
        var user = await _queryProcessor
            .ProcessAsync(new GetUserByIdQuery(input.UserId), CancellationToken.None)
            .ConfigureAwait(false);

        return new TContentSettings {
            SensitiveCanChange = user!.SensitiveCanChange, SensitiveEnabled = user.SensitiveEnabled
        };
    }
}
