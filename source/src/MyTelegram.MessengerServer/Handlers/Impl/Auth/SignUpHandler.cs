using MyTelegram.Domain.Commands.AppCode;
using MyTelegram.Handlers.Auth;
using MyTelegram.Schema.Auth;
using IAuthorization = MyTelegram.Schema.Auth.IAuthorization;

namespace MyTelegram.MessengerServer.Handlers.Impl.Auth;

public class SignUpHandler : RpcResultObjectHandler<RequestSignUp, IAuthorization>,
    ISignUpHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRandomHelper _randomHelper;

    public SignUpHandler(
        ICommandBus commandBus,
        IRandomHelper randomHelper,
        IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestSignUp obj)
    {
        var phoneNumber = obj.PhoneNumber.ToPhoneNumber();
        var userReadModel = await _queryProcessor
                .ProcessAsync(new GetUserByPhoneNumberQuery(phoneNumber), default)
            ;
        var userId = userReadModel?.UserId ?? 0;

        var command = new CheckSignUpCodeCommand(AppCodeId.Create(phoneNumber, obj.PhoneCodeHash),
            input.ToRequestInfo(),
            obj.PhoneCodeHash,
            userId,
            _randomHelper.NextLong(),
            phoneNumber,
            obj.FirstName,
            obj.LastName,
            Guid.NewGuid());

        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
