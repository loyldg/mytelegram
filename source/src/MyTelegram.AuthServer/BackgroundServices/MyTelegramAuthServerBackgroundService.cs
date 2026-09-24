namespace MyTelegram.AuthServer.BackgroundServices;

public class MyTelegramAuthServerBackgroundService(
    ILogger<MyTelegramAuthServerBackgroundService> logger,
    IHandlerHelper handlerHelper,
    IFingerprintHelper fingerprintHelper
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
        }

        return Task.CompletedTask;
    }
}