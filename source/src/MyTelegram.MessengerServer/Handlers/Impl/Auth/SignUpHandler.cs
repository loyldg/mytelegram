using MyTelegram.Domain.Commands.AppCode;
using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class SignUpHandler : RpcResultObjectHandler<RequestSignUp, IAuthorization>,
    ISignUpHandler, IProcessedHandler //, IShouldCacheRequest
{
    //private readonly IDistributedCache<UserCacheItem> _distributedCache;
    private readonly ICacheManager<UserCacheItem> _cacheManager;
    private readonly ICommandBus _commandBus;
    private readonly IRandomHelper _randomHelper;

    public SignUpHandler(
        ICommandBus commandBus,
        IRandomHelper randomHelper,
        ICacheManager<UserCacheItem> cacheManager)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _cacheManager = cacheManager;
    }

    protected override async Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestSignUp obj)
    {
        // 用户登录成功后，会将用户Id和电话号码的对应关系进行缓存，登录的时候需要先判断该用户电话是否已经注册，如果已注册就直接使用之前的Uid
        var cachedUserItem = await _cacheManager.GetAsync(
            UserCacheItem.GetCacheKey(obj.PhoneNumber.ToPhoneNumber())).ConfigureAwait(false);

        var command = new CheckSignUpCodeCommand(AppCodeId.Create(obj.PhoneNumber.ToPhoneNumber(), obj.PhoneCodeHash),
            input.ToRequestInfo(),
            obj.PhoneCodeHash,
            cachedUserItem?.UserId ?? 0,
            _randomHelper.NextLong(),
            obj.PhoneNumber,
            obj.FirstName,
            obj.LastName,
            Guid.NewGuid());

        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

        return null!;
    }
}
