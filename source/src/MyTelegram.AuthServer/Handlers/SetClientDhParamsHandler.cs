namespace MyTelegram.AuthServer.Handlers;

public class SetClientDhParamsHandler(
    IStep3Helper step3ServerHelper,
    ILogger<SetClientDhParamsHandler> logger,
    IEventBus eventBus
) : BaseObjectHandler<RequestSetClientDHParams, ISetClientDHParamsAnswer>
{
    protected override async Task<ISetClientDHParamsAnswer> HandleCoreAsync(
        IRequestInput input,
        RequestSetClientDHParams obj
    )
    {
        var dto = await step3ServerHelper.SetClientDhParamsAnswerAsync(obj);
        logger.HandshakeStep3(dto.IsPermanent ? "Perm" : "Temp", input.ConnectionId, input.AuthKeyId, input.ReqMsgId, input.ConnectionType == ConnectionType.Media);

        // The auth key is no longer written to the distributed cache: the session server receives it with
        // AuthKeyCreatedIntegrationEvent, and keeping a copy of the secret in Redis only increases the exposure.
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

        // The session server sends SetClientDhParamsAnswer (dh_gen_ok) to the client after it has registered the
        // auth key, for both permanent and temporary keys. Otherwise the client may use the new key before the
        // session server knows it and receive -404 (AUTH_KEY not found), which makes it drop the key.
        return null!;
    }
}