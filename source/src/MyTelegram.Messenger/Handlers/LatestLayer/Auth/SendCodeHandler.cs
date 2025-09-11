using System.Security.Cryptography;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Auth;

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
/// 406 UPDATE_APP_TO_LOGIN Please update your client to login.
/// See <a href="https://corefork.telegram.org/method/auth.sendCode" />
///</summary>
internal sealed class SendCodeHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    IQueryProcessor queryProcessor,
    ICacheManager<FutureAuthTokenCacheItem> cacheManager,
    IHashHelper hashHelper,
    ICountryHelper countryHelper,
    ICacheHelper<long, long> cacheHelper,
    IScheduleAppService scheduleAppService,
    ILayeredService<IAuthorizationConverter> authorizationLayeredService,
    IUserConverterService userConverterService,
    IVerificationCodeGenerator verificationCodeGenerator,
    IEventBus eventBus)
    : RpcResultObjectHandler<Schema.Auth.RequestSendCode, Schema.Auth.ISentCode>
{
    private readonly int _maxFutureAuthTokens = 20;
    protected override async Task<ISentCode> HandleCoreAsync(IRequestInput input,
        RequestSendCode obj)
    {
        // Android:12222222222 Tdesktop:+1 222 222 2222
        var phoneNumber = obj.PhoneNumber.ToPhoneNumber();
        CheckPhoneNumber(phoneNumber);

        var userReadModel =
            await queryProcessor.ProcessAsync(new GetUserByPhoneNumberQuery(phoneNumber));
        if (userReadModel != null)
        {
            if (peerHelper.IsBotUser(userReadModel.UserId) ||
                userReadModel.UserId == MyTelegramConsts.OfficialUserId)
            {
                RpcErrors.RpcErrors400.PhoneNumberInvalid.ThrowRpcError();
            }

            (var sentCode, bool loginSuccess) = await SignInWithFutureAuthTokenAsync(input, obj, userReadModel);
            if (loginSuccess)
            {
                return sentCode!;
            }
        }

        var code = verificationCodeGenerator.Generate();

        var phoneCodeHash = Guid.NewGuid().ToString("N");
        var timeout = options.CurrentValue.VerificationCodeExpirationSeconds;

        return await SendSmsCodeAsync(userReadModel, input, obj.PhoneNumber, phoneCodeHash, timeout);
    }

    private void CheckPhoneNumber(string phoneNumber)
    {
        if (!long.TryParse(phoneNumber, out _))
        {
            RpcErrors.RpcErrors400.PhoneNumberInvalid.ThrowRpcError();
        }

        if (options.CurrentValue.CheckPhoneNumberFormat)
        {
            if (phoneNumber.Length < 5)
            {
                RpcErrors.RpcErrors400.PhoneNumberInvalid.ThrowRpcError();
            }

            var phoneNumberWithoutCountryCode = phoneNumber;
            CountryCodeItem? countryCodeItem = null;

            var maxCountryCodeLength = 4;
            for (int i = 1; i <= maxCountryCodeLength; i++)
            {
                if (countryHelper.TryGetCountryCodeItem(phoneNumber[..i], out countryCodeItem))
                {
                    phoneNumberWithoutCountryCode = phoneNumber[i..];
                    break;
                }
            }

            var phoneNumberLength = phoneNumberWithoutCountryCode.Length;
            if (countryCodeItem!.PhoneNumberLengths?.Count > 0)
            {
                var isValidPhoneNumber = countryCodeItem.PhoneNumberLengths.Any(p => p == phoneNumberLength);
                if (!isValidPhoneNumber)
                {
                    RpcErrors.RpcErrors400.PhoneNumberInvalid.ThrowRpcError();
                }
            }
        }
    }

    private async Task<ISentCode> SendSmsCodeAsync(IUserReadModel? userReadModel, IRequestInput input,
        string phoneNumber, string phoneCodeHash, int timeout)
    {
        var code = verificationCodeGenerator.Generate();

        var appCodeId = AppCodeId.Create(phoneNumber.ToPhoneNumber(), phoneCodeHash);
        var sendAppCodeCommand =
            new SendAppCodeCommand(appCodeId,
                input.ToRequestInfo() with { UserId = userReadModel?.UserId ?? 0 },
                userReadModel?.UserId ?? 0,
                phoneNumber.ToPhoneNumber(),
                code,
                phoneCodeHash,
                DateTime.UtcNow.ToTimestamp());
        await commandBus.PublishAsync(sendAppCodeCommand);

        return new TSentCode
        {
            Type = new TSentCodeTypeSms { Length = code.Length },
            PhoneCodeHash = phoneCodeHash,
            Timeout = timeout
        };
    }

    private async Task<(ISentCode? sentCode, bool loginSuccess)> SignInWithFutureAuthTokenAsync(IRequestInput input,
        RequestSendCode obj, IUserReadModel userReadModel)
    {
        if (obj.Settings.LogoutTokens?.Count > 0)
        {
            if (obj.Settings.LogoutTokens?.Count > 0)
            {
                var cacheKeys = obj.Settings.LogoutTokens.Take(_maxFutureAuthTokens).Select(p =>
                    FutureAuthTokenCacheItem.GetCacheKey(BitConverter.ToString(SHA1.HashData(p.Span))
                        .Replace("-", string.Empty))).ToList();
                var cachedFutureTokens = await cacheManager.GetManyAsync(cacheKeys);

                if (cachedFutureTokens.Any(p => p.Value.UserId == userReadModel.UserId))
                {
                    if (userReadModel.HasPassword)
                    {
                        await eventBus.PublishAsync(new UserSignInSuccessEvent(
                            input.ReqMsgId,
                            input.AuthKeyId,
                            input.PermAuthKeyId,
                            userReadModel.UserId,
                            PasswordState.WaitingForVerify,
                            true
                        ));

                        return (null, true);
                    }
                    else
                    {
                        var user = userConverterService.ToUser(input, userReadModel, null, layer: input.Layer);

                        await eventBus.PublishAsync(new UserSignInSuccessEvent(
                            input.ReqMsgId,
                            input.AuthKeyId, input.PermAuthKeyId,
                            user.Id, PasswordState.None));

                        return (new TSentCodeSuccess
                        {
                            Authorization = authorizationLayeredService.GetConverter(input.Layer)
                                .CreateAuthorization(user)
                        }, true);
                    }
                }
            }
        }

        return (null, false);
    }
}