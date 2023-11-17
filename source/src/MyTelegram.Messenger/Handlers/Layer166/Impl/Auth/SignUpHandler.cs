// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Registers a validated phone number in the system.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FIRSTNAME_INVALID The first name is invalid.
/// 400 LASTNAME_INVALID The last name is invalid.
/// 400 PHONE_CODE_EMPTY phone_code is missing.
/// 400 PHONE_CODE_EXPIRED The phone code you provided has expired.
/// 400 PHONE_CODE_INVALID The provided phone code is invalid.
/// 400 PHONE_NUMBER_FLOOD You asked for the code too many times.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 400 PHONE_NUMBER_OCCUPIED The phone number is already in use.
/// See <a href="https://corefork.telegram.org/method/auth.signUp" />
///</summary>
internal sealed class SignUpHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestSignUp, MyTelegram.Schema.Auth.IAuthorization>,
    Auth.ISignUpHandler
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

    protected override async Task<MyTelegram.Schema.Auth.IAuthorization> HandleCoreAsync(IRequestInput input,
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
            obj.LastName);

        await _commandBus.PublishAsync(command, CancellationToken.None);

        return null!;
    }
}
