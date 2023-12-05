using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MyTelegram.SmsSender;

public class TwilioSmsSender : ISmsSender
{
    private readonly IOptionsSnapshot<TwilioSmsOptions> _optionsSnapshot;
    private bool _isTwilioClientInited;
    private readonly SemaphoreSlim _semaphore = new(1);
    private readonly ILogger<TwilioSmsSender> _logger;
    public TwilioSmsSender(IOptionsSnapshot<TwilioSmsOptions> optionsSnapshot,
        ILogger<TwilioSmsSender> logger)
    {
        _optionsSnapshot = optionsSnapshot;
        _logger = logger;
        InitTwilioClientIfNeed();
    }

    public async Task SendAsync(SmsMessage smsMessage)
    {
        if (!_optionsSnapshot.Value.Enabled)
        {
            _logger.LogWarning("Twilio sms sender disabled,the code will not be sent.PhoneNumber:{To} Text:{Text}", smsMessage.PhoneNumber, smsMessage.Text);
            return;
        }
        // InitTwilioClientIfNeed();
        if (!_isTwilioClientInited)
        {
            return;
        }

        var resource = await MessageResource.CreateAsync(new PhoneNumber(smsMessage.PhoneNumber),
            from: new PhoneNumber(_optionsSnapshot.Value.FromNumber),
            body: smsMessage.Text);
        _logger.LogDebug("Send SMS result:{@Resource}", resource);
        _logger.LogInformation("Send SMS completed,To={To},Status={Status} DateSent={DateSent} ErrorCode={ErrorCode} ErrorMessage={ErrorMessage} ",
            resource.To,
            resource.Status,
            resource.DateSent,
            resource.ErrorCode,
            resource.ErrorMessage);
    }

    //private async Task InitTwilioClientIfNeedAsync()
    private void InitTwilioClientIfNeed()
    {
        if (!_optionsSnapshot.Value.Enabled)
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

            if (string.IsNullOrEmpty(_optionsSnapshot.Value.AccountSId))
            {
                // _logger.LogWarning("AccountSId option was not configured!");
                throw new ArgumentException("AccountSId can not be null", nameof(_optionsSnapshot.Value.AccountSId));
            }

            if (string.IsNullOrEmpty(_optionsSnapshot.Value.AuthToken))
            {
                // _logger.LogWarning("AuthToken option was not configured!");
                throw new ArgumentException("AuthToken can not be null", nameof(_optionsSnapshot.Value.AuthToken));
            }

            if (string.IsNullOrEmpty(_optionsSnapshot.Value.FromNumber))
            {
                // _logger.LogWarning("FromNumber option was not configured!");
                throw new ArgumentException("FromNumber can not be null", nameof(_optionsSnapshot.Value.FromNumber));
            }

            TwilioClient.Init(_optionsSnapshot.Value.AccountSId, _optionsSnapshot.Value.AuthToken);
            _isTwilioClientInited = true;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}