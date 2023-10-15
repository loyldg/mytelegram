// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// Send the verification code for login
/// <para>Possible errors</para>
/// Code Type Description
/// 400 API_ID_INVALID API ID invalid.
/// 400 API_ID_PUBLISHED_FLOOD This API id was published somewhere, you can't use it now.
/// 500 AUTH_RESTART Restart the authorization process.
/// 400 PHONE_NUMBER_APP_SIGNUP_FORBIDDEN You can't sign up using this app.
/// 400 PHONE_NUMBER_BANNED The provided phone number is banned from telegram.
/// 400 PHONE_NUMBER_FLOOD You asked for the code too many times.
/// 406 PHONE_NUMBER_INVALID The phone number is invalid.
/// 406 PHONE_PASSWORD_FLOOD You have tried logging in too many times.
/// 400 PHONE_PASSWORD_PROTECTED This phone is password protected.
/// 400 SMS_CODE_CREATE_FAILED An error occurred while creating the SMS code.
/// 400 Sorry, too many invalid attempts to enter your password. Please try again later. &nbsp;
/// See <a href="https://corefork.telegram.org/method/auth.sendCode" />
///</summary>
internal sealed class SendCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestSendCode, MyTelegram.Schema.Auth.ISentCode>,
    Auth.ISendCodeHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    private readonly IQueryProcessor _queryProcessor;

    public SendCodeHandler(
        ICommandBus commandBus,
        IRandomHelper randomHelper,
        IPeerHelper peerHelper,
        IOptions<MyTelegramMessengerServerOptions> options,
        IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _peerHelper = peerHelper;
        _options = options;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<ISentCode> HandleCoreAsync(IRequestInput input,
        RequestSendCode obj)
    {
        var userReadModel = await _queryProcessor
            .ProcessAsync(new GetUserByPhoneNumberQuery(obj.PhoneNumber.ToPhoneNumber()), default)
     ;
        if (userReadModel != null)
        {
            if (_peerHelper.IsBotUser(userReadModel.UserId) || userReadModel.UserId == MyTelegramServerDomainConsts.OfficialUserId)
            {
                RpcErrors.RpcErrors400.PhoneNumberInvalid.ThrowRpcError();
            }
        }

        var code = _options.Value.FixedVerifyCode == 0
            ? _randomHelper.NextInt(10000, 99999).ToString()
            : _options.Value.FixedVerifyCode.ToString();

        var phoneCodeHash = Guid.NewGuid().ToString("N");
        var timeout = 300; //300s

        var expire = DateTime.UtcNow.AddSeconds(timeout).ToTimestamp();
        var appCodeId = AppCodeId.Create(obj.PhoneNumber.ToPhoneNumber(), phoneCodeHash);
        var sendAppCodeCommand =
            new SendAppCodeCommand(appCodeId,
                input.ToRequestInfo(),
                userReadModel?.UserId ?? 0,
                obj.PhoneNumber.ToPhoneNumber(),
                code,
                phoneCodeHash,
                expire,
                DateTime.UtcNow.ToTimestamp());
        await _commandBus.PublishAsync(sendAppCodeCommand, CancellationToken.None);

        return new TSentCode
        {
            Type = new TSentCodeTypeSms { Length = code.Length },
            PhoneCodeHash = phoneCodeHash,
            Timeout = timeout
        };
    }
}
