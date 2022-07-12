using MyTelegram.Domain.Commands.AppCode;
using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class SignInHandler : RpcResultObjectHandler<RequestSignIn, IAuthorization>,
    ISignInHandler, IProcessedHandler
{
    private readonly ICacheManager<UserCacheItem> _cacheManager;
    private readonly ICommandBus _commandBus;
    private readonly ILogger<SignInHandler> _logger;
    private readonly IQueryProcessor _queryProcessor;

    public SignInHandler(
        ICommandBus commandBus,
        ILogger<SignInHandler> logger,
        ICacheManager<UserCacheItem> cacheManager,
        IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _logger = logger;
        _cacheManager = cacheManager;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestSignIn obj)
    {
        _logger.LogTrace("User {PhoneNumber} start sign in", obj.PhoneNumber);

        var userId = 0L;
        var cachedUserItem = await _cacheManager.GetAsync(
            UserCacheItem.GetCacheKey(obj.PhoneNumber.ToPhoneNumber())).ConfigureAwait(false);
        if (cachedUserItem == null)
        {
            var userReadModel = await _queryProcessor
                .ProcessAsync(new GetUserByPhoneNumberQuery(obj.PhoneNumber.ToPhoneNumber()), default)
                .ConfigureAwait(false);
            if (userReadModel == null)
            {
                _logger.LogInformation(
                    "Get cached user info failed.phone number={PhoneNumber},user sign up required",
                    obj.PhoneNumber.ToPhoneNumber());
            }
            else
            {
                userId = userReadModel.UserId;
            }
        }
        else
        {
            userId = cachedUserItem.UserId;
        }

        var command = new CheckSignInCodeCommand(AppCodeId.Create(obj.PhoneNumber.ToPhoneNumber(), obj.PhoneCodeHash),
            input.ToRequestInfo(),
            obj.PhoneCode,
            userId,
            Guid.NewGuid()
        );

        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
        return null!;
    }
}

//public interface IValidationAppService
//{
//    Task CheckCodeAsync(string phoneNumber,
//        string phoneCodeHash,
//        string code);
//}

//public class ValidationAppService : IValidationAppService //, ITransientDependency
//{
//    private readonly IQueryProcessor _queryProcessor;

//    public ValidationAppService(IQueryProcessor queryProcessor)
//    {
//        _queryProcessor = queryProcessor;
//    }

//    public async Task CheckCodeAsync(string phoneNumber,
//        string phoneCodeHash,
//        string code)
//    {
//        if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(code))
//        {
//            throw new BadRequestException("PHONE_CODE_EMPTY");
//        }

//        var appCode = await _queryProcessor.ProcessAsync(new GetLatestAppCodeQuery(phoneNumber, phoneCodeHash),
//            CancellationToken.None).ConfigureAwait(false);
//        if (appCode == null)
//        {
//            throw new BadRequestException("PHONE_CODE_INVALID");
//        }

//        // code==null is sign up
//        if (!string.IsNullOrEmpty(code) && string.Compare(appCode.Code, code, StringComparison.OrdinalIgnoreCase) != 0)
//        {
//            throw new BadRequestException("PHONE_CODE_INVALID");
//        }

//        var date = DateTime.UtcNow.ToTimestamp();
//        if (date > appCode.Expire)
//        {
//            throw new BadRequestException("PHONE_CODE_INVALID");
//        }
//    }
//}
