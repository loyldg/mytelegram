using MyTelegram.Domain.Commands.AppCode;
using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class SignInHandler : RpcResultObjectHandler<RequestSignIn, IAuthorization>,
    ISignInHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly ILogger<SignInHandler> _logger;
    private readonly IQueryProcessor _queryProcessor;

    public SignInHandler(
        ICommandBus commandBus,
        ILogger<SignInHandler> logger,
        IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _logger = logger;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestSignIn obj)
    {
        _logger.LogTrace("User {PhoneNumber} start sign in", obj.PhoneNumber);

        var userId = 0L;
        var userReadModel = await _queryProcessor
            .ProcessAsync(new GetUserByPhoneNumberQuery(obj.PhoneNumber.ToPhoneNumber()), default)
            .ConfigureAwait(false);
        if (userReadModel == null)
        {
            _logger.LogInformation(
                "The phone number={PhoneNumber} not exists,user sign up required",
                obj.PhoneNumber.ToPhoneNumber());
        }
        else
        {
            userId = userReadModel.UserId;
        }

        if (string.IsNullOrEmpty(obj.PhoneCode))
        {
            ThrowHelper.ThrowUserFriendlyException("Phone code can not be null");
        }

        var command = new CheckSignInCodeCommand(AppCodeId.Create(obj.PhoneNumber.ToPhoneNumber(), obj.PhoneCodeHash),
            input.ToRequestInfo(),
            obj.PhoneCode!,
            userId,
            Guid.NewGuid()
        );

        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
        return null!;
    }
}
