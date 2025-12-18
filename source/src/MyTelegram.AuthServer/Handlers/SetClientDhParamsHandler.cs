namespace MyTelegram.AuthServer.Handlers;

public class SetClientDhParamsHandler(
    IStep3Helper step3ServerHelper,
    ILogger<SetClientDhParamsHandler> logger,
    ICacheManager<AuthKeyCacheItem> cacheManager,
    IEventBus eventBus
) : BaseObjectHandler<RequestSetClientDHParams, ISetClientDHParamsAnswer>, ISetClientDhParamsHandler
{
    protected override async Task<ISetClientDHParamsAnswer> HandleCoreAsync(
        IRequestInput input,
        RequestSetClientDHParams obj
    )
    {
        var dto = await step3ServerHelper.SetClientDhParamsAnswerAsync(obj);
        logger.HandshakeStep3(dto.IsPermanent ? "Perm" : "Temp", input.ConnectionId, input.AuthKeyId, input.ReqMsgId, input.ConnectionType == ConnectionType.Media);

        // Cached authentication data expires in 120 seconds
        var cacheKey = AuthKeyCacheItem.GetCacheKey(dto.AuthKeyId);
        await cacheManager.SetAsync(
            cacheKey,
            new AuthKeyCacheItem(dto.AuthKey, dto.ServerSalt, dto.IsPermanent),
            120
        );
        await eventBus.PublishAsync(
            new AuthKeyCreatedIntegrationEvent(
                input.ConnectionId,
                input.ConnectionType,
                input.ReqMsgId,
                dto.AuthKey,
                dto.ServerSalt,
                dto.IsPermanent,
                dto.SetClientDhParamsAnswer.ToBytes(),
                dto.DcId
            )
        );

        // The session server will send SetClientDhParamsAnswer to client if the perm auth key created on session server
        if (!dto.IsPermanent)
        {
            return dto.SetClientDhParamsAnswer;
        }

        return null!;
    }
}