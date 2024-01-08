// ReSharper disable All

namespace MyTelegram.Handlers.Account.LayerN;

///<summary>
/// Register device to receive <a href="https://corefork.telegram.org/api/push-updates">PUSH notifications</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 TOKEN_EMPTY The specified token is empty.
/// 400 TOKEN_INVALID The provided token is invalid.
/// 400 TOKEN_TYPE_INVALID The specified token type is invalid.
/// 400 WEBPUSH_AUTH_INVALID The specified web push authentication secret is invalid.
/// 400 WEBPUSH_KEY_INVALID The specified web push elliptic curve Diffie-Hellman public key is invalid.
/// 400 WEBPUSH_TOKEN_INVALID The specified web push token is invalid.
/// See <a href="https://corefork.telegram.org/method/account.registerDevice" />
///</summary>
internal sealed class RegisterDeviceHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.LayerN.RequestRegisterDevice, IBool>,
    Account.LayerN.IRegisterDeviceHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IEventBus _eventBus;
    public RegisterDeviceHandler(ICommandBus commandBus, IEventBus eventBus)
    {
        _commandBus = commandBus;
        _eventBus = eventBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.LayerN.RequestRegisterDevice obj)
    {
        var command = new RegisterDeviceCommand(PushDeviceId.Create(obj.Token),
            input.ToRequestInfo(),
            input.UserId,
            input.AuthKeyId,
            obj.TokenType,
            obj.Token,
            false,
            false,
            null,
            null);
        await _commandBus.PublishAsync(command, CancellationToken.None);

        // tokenType:7 - MTProto separate session
        if (obj.TokenType == 7)
        {
            if (long.TryParse(obj.Token, out var sessionId))
            {
                await _eventBus.PublishAsync(new DeviceRegisteredEvent(input.AuthKeyId, input.PermAuthKeyId,
                    sessionId));
            }
        }

        return new TBoolTrue();
    }
}
