using MyTelegram.Domain.Commands.AppCode;
using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class SendCodeHandler : RpcResultObjectHandler<RequestSendCode, ISentCode>,
    ISendCodeHandler, IProcessedHandler
{
    //private readonly IDistributedCache<UserCacheItem> _distributedCache;
    private readonly ICacheManager<UserCacheItem> _cacheManager;
    private readonly ICommandBus _commandBus;
    private readonly IHashHelper _hashHelper;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;

    public SendCodeHandler(
        ICommandBus commandBus,
        IRandomHelper randomHelper,
        IHashHelper hashHelper,
        IPeerHelper peerHelper,
        ICacheManager<UserCacheItem> cacheManager,
        IOptions<MyTelegramMessengerServerOptions> options)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _hashHelper = hashHelper;
        _peerHelper = peerHelper;
        _cacheManager = cacheManager;
        _options = options;
    }

    protected override async Task<ISentCode> HandleCoreAsync(IRequestInput input,
        RequestSendCode obj)
    {
        var cachedUserItem = await _cacheManager.GetAsync(UserCacheItem.GetCacheKey(obj.PhoneNumber))
            .ConfigureAwait(false);

        if (cachedUserItem != null)
        {
            if (_peerHelper.IsBotUser(cachedUserItem.UserId) ||
                cachedUserItem.UserId == MyTelegramServerDomainConsts.OfficialUserId)
            {
                ThrowHelper.ThrowUserFriendlyException("PHONE_NUMBER_INVALID");
            }
        }

        // ReSharper disable once RedundantAssignment
        //        var code = _randomHelper.NextInt(10000, 99999).ToString();
        //#if DEBUG
        //        code = "2";
        //#endif
        var code = _options.Value.FixedVerifyCode == 0
            ? _randomHelper.NextInt(10000, 99999).ToString()
            : _options.Value.FixedVerifyCode.ToString();

        var codeHash = _randomHelper.NextLong().ToString();
        var phoneCodeHash = BitConverter.ToString(_hashHelper.Sha1(Encoding.UTF8.GetBytes(codeHash)))
            .Replace("-", string.Empty).ToLower();
        var timeout = 300; //300s
        var expire = DateTime.UtcNow.AddSeconds(timeout).ToTimestamp();
        var appCodeId = AppCodeId.Create(obj.PhoneNumber.ToPhoneNumber(), phoneCodeHash);
        var sendAppCodeCommand =
            new SendAppCodeCommand(appCodeId,
                input.ToRequestInfo(),
                cachedUserItem?.UserId ?? 0,
                obj.PhoneNumber.ToPhoneNumber(),
                code,
                phoneCodeHash,
                expire,
                DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        await _commandBus.PublishAsync(sendAppCodeCommand, CancellationToken.None).ConfigureAwait(false);

        return new TSentCode
        {
            Type = new TSentCodeTypeSms { Length = code.Length },
            PhoneCodeHash = phoneCodeHash,
            Timeout = timeout
        };
    }
}
