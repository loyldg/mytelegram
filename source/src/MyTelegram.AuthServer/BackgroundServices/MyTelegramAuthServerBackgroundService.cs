namespace MyTelegram.AuthServer.BackgroundServices;

public class MyTelegramAuthServerBackgroundService(
    ILogger<MyTelegramAuthServerBackgroundService> logger,
    IHandlerHelper handlerHelper,
    IFingerprintHelper fingerprintHelper,
    IOptions<MyTelegramAuthServerOptions> options
) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        handlerHelper.InitAllHandlers();
        logger.LogInformation("MyTelegram auth server started");
        const long defaultFingerprint = -3591632762792723036;
        var fingerprint = fingerprintHelper.GetFingerprint();
        if (fingerprint == defaultFingerprint)
        {
            logger.DefaultPrivateKeyDetected();
            if (options.Value.RejectDefaultPrivateKey)
            {
                throw new InvalidOperationException(
                    "The default (public) RSA private key is configured and App:RejectDefaultPrivateKey is true. Configure your own private key.");
            }
        }

        return Task.CompletedTask;
    }
}