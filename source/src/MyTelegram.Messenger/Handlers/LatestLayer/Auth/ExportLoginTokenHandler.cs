namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

///<summary>
/// Generate a login token, for <a href="https://corefork.telegram.org/api/qr-login">login via QR code</a>.<br>
/// The generated login token should be encoded using base64url, then shown as a <code>tg://login?token=base64encodedtoken</code> <a href="https://corefork.telegram.org/api/links#qr-code-login-links">deep link »</a> in the QR code.For more info, see <a href="https://corefork.telegram.org/api/qr-login">login via QR code</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 API_ID_INVALID API ID invalid.
/// 400 API_ID_PUBLISHED_FLOOD This API id was published somewhere, you can't use it now.
/// See <a href="https://corefork.telegram.org/method/auth.exportLoginToken" />
///</summary>
internal sealed class ExportLoginTokenHandler(
    ICacheHelper<long, long> cacheHelper,
    ICommandBus commandBus,
    IRandomHelper randomHelper,
    IEventBus eventBus,
    IUserAppService userAppService,
    ILayeredService<IAuthorizationConverter> layeredService,
    IUserConverterService userConverterService,
    IPhotoAppService photoAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestExportLoginToken, MyTelegram.Schema.Auth.ILoginToken>
{
    protected override async Task<ILoginToken> HandleCoreAsync(IRequestInput input,
        RequestExportLoginToken obj)
    {
        if (cacheHelper.TryRemove(input.AuthKeyId, out var userId))
        {
            await eventBus
                .PublishAsync(new BindUserIdToSessionEvent(userId, input.AuthKeyId, input.PermAuthKeyId));

            var userReadModel = await userAppService.GetAsync(userId);
            var photos = await photoAppService.GetPhotosAsync(userReadModel);
            ILayeredUser? user = userReadModel == null ? null : userConverterService.ToUser(input, userReadModel, photos);
            return new TLoginTokenSuccess
            {
                Authorization = layeredService.GetConverter(input.Layer).CreateAuthorization(user)
            };
        }

        var token = new byte[32];
        randomHelper.NextBytes(token);
        var expireDate = DateTime.UtcNow.AddSeconds(MyTelegramConsts.QrCodeExpireSeconds).ToTimestamp();
        var qrCodeId = QrCodeId.Create(BitConverter.ToString(token));
        var command = new ExportLoginTokenCommand(qrCodeId,
            input.ToRequestInfo(),
            input.AuthKeyId,
            input.PermAuthKeyId,
            token,
            expireDate,
            obj.ExceptIds.ToList());
        await commandBus.PublishAsync(command);

        return null!;
    }
}
