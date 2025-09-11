using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MyTelegram.SmsSender;

public class TwilioSmsSender : ISmsSender, ITransientDependency
{
    private readonly ILogger<TwilioSmsSender> _logger;
    private readonly IOptionsMonitor<TwilioSmsOptions> _options;
    private readonly SemaphoreSlim _semaphore = new(1);
    private bool _isTwilioClientInited;

    public TwilioSmsSender(IOptionsMonitor<TwilioSmsOptions> optionsSnapshot,
        ILogger<TwilioSmsSender> logger)
    {
        _options = optionsSnapshot;
        _logger = logger;
        InitTwilioClientIfNeeded();
    }

    public bool Enabled => _options.CurrentValue.Enabled;

    public async Task SendAsync(SmsMessage smsMessage)
    {
        if (!_options.CurrentValue.Enabled)
        {
            _logger.LogWarning("Twilio sms sender disabled, the code will not be sent. PhoneNumber: {To} Text: {Text}",
                smsMessage.PhoneNumber, smsMessage.Text);
            return;
        }

        // InitTwilioClientIfNeed();
        if (!_isTwilioClientInited)
        {
            return;
        }

        var phoneNumber = smsMessage.PhoneNumber;
        if (!phoneNumber.StartsWith("+"))
        {
            phoneNumber = $"+{phoneNumber}";
        }

        var resource =
                string.IsNullOrEmpty(_options.CurrentValue.MessagingServiceSId)
                    ? await MessageResource.CreateAsync(new PhoneNumber(phoneNumber),
                        from: new PhoneNumber(_options.CurrentValue.FromNumber),
                        body: smsMessage.Text)
                    : await MessageResource.CreateAsync(new PhoneNumber(phoneNumber),
                        messagingServiceSid: _options.CurrentValue.MessagingServiceSId,
                        body: smsMessage.Text)
            ;

        _logger.LogDebug("Send SMS result: {@Resource}", resource);
        _logger.LogInformation(
            "Send SMS completed, To={To}, Status={Status} DateSent={DateSent} ErrorCode={ErrorCode} ErrorMessage={ErrorMessage} ",
            resource.To,
            resource.Status,
            resource.DateSent,
            resource.ErrorCode,
            resource.ErrorMessage);
    }

    private void InitTwilioClientIfNeeded()
    {
        if (!_options.CurrentValue.Enabled)
        {
            return;
        }

        if (_isTwilioClientInited)
        {
            return;
        }

        //await _semaphore.WaitAsync();
        _semaphore.Wait();

        try
        {
            if (_isTwilioClientInited)
            {
                return;
            }

            if (string.IsNullOrEmpty(_options.CurrentValue.AccountSId))
            {
                // _logger.LogWarning("AccountSId option was not configured!");
                throw new ArgumentException("AccountSId can not be null", nameof(_options.CurrentValue.AccountSId));
            }

            if (string.IsNullOrEmpty(_options.CurrentValue.AuthToken))
            {
                // _logger.LogWarning("AuthToken option was not configured!");
                throw new ArgumentException("AuthToken can not be null", nameof(_options.CurrentValue.AuthToken));
            }

            if (string.IsNullOrEmpty(_options.CurrentValue.FromNumber))
            {
                // _logger.LogWarning("FromNumber option was not configured!");
                throw new ArgumentException("FromNumber can not be null", nameof(_options.CurrentValue.FromNumber));
            }

            TwilioClient.Init(_options.CurrentValue.AccountSId, _options.CurrentValue.AuthToken);
            _isTwilioClientInited = true;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}