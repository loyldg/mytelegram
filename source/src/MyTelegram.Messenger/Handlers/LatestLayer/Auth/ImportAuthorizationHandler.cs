using System.Security.Cryptography;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;
/// <summary>
/// Logs in a user using a key transmitted from his native data-center.
/// Possible errors
/// Code Type Description
/// 400 AUTH_BYTES_INVALID The provided authorization is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/auth.importAuthorization"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✔]
/// </remarks>
internal sealed class ImportAuthorizationHandler(IHashHelper hashHelper, ICacheManager<AuthorizationCacheItem> cacheManager, IEventBus eventBus, IUserAppService userAppService, ILayeredService<IAuthorizationConverter> layeredService, IUserConverterService userConverterService, IPhotoAppService photoAppService, ILogger<ImportAuthorizationHandler> logger) : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestImportAuthorization, MyTelegram.Schema.Auth.IAuthorization>
{
    protected override async Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input, RequestImportAuthorization obj)
    {
        var keyBytes = SHA1.HashData(obj.Bytes.Span);
        var key = BitConverter.ToString(keyBytes).Replace("-", string.Empty);
        var cacheKey = AuthorizationCacheItem.GetCacheKey(key);
        //var userIdText = await cacheManager.GetAsync(cacheKey);
        var cachedAuthItem = await cacheManager.GetAsync(cacheKey);
        if (cachedAuthItem == null)
        {
            RpcErrors.RpcErrors400.AuthBytesInvalid.ThrowRpcError();
        }

        var userId = cachedAuthItem!.UserId;
        {
            if (userId != obj.Id)
            {
                RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
            }

            var userReadModel = await userAppService.GetAsync(userId);
            if (userReadModel == null)
            {
                RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
            }

            await eventBus.PublishAsync(new BindUserIdToSessionEvent(userReadModel!.UserId, input.AuthKeyId, input.PermAuthKeyId, cachedAuthItem.AccessHashKeyId));
            await cacheManager.RemoveAsync(key);
            var photos = await photoAppService.GetPhotosAsync(userReadModel);
            ILayeredUser user = userConverterService.ToUser(input, userReadModel, photos, layer: input.Layer);
            user.Self = true;
            //logger.LogInformation("ImportAuthorizationHandler[{UserId}][{@Input}]:{@data}", userId,input, obj);
            return layeredService.GetConverter(input.Layer).CreateAuthorization(user);
        }

        RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
        return null !;
    }
}