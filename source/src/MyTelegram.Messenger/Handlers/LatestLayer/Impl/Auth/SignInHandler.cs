// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Signs in a user with a validated phone number.
/// <para>Possible errors</para>
/// Code Type Description
/// 500 AUTH_RESTART Restart the authorization process.
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 400 PHONE_CODE_INVALID The provided phone code is invalid.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 400 PHONE_NUMBER_UNOCCUPIED The phone number is not yet being used.
/// 500 SIGN_IN_FAILED Failure while signing in.
/// See <a href="https://corefork.telegram.org/method/auth.signIn" />
///</summary>
internal sealed class SignInHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestSignIn, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.ISignInHandler
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

    protected override async Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
        RequestSignIn obj)
    {
        _logger.LogTrace("User {PhoneNumber} start sign in", obj.PhoneNumber);

        var userId = 0L;
        var userReadModel = await _queryProcessor
                .ProcessAsync(new GetUserByPhoneNumberQuery(obj.PhoneNumber.ToPhoneNumber()), default)
            ;
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

        var command = new CheckSignInCodeCommand(AppCodeId.Create(obj.PhoneNumber.ToPhoneNumber(), obj.PhoneCodeHash),
            input.ToRequestInfo(),
            obj.PhoneCode,
            userId
        );

        await _commandBus.PublishAsync(command, CancellationToken.None);
        return null!;
    }
}
