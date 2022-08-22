using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;

namespace MyTelegram.SmsSender;

public class AbpInitializationHostedService : IHostedService
{
    private IAbpApplicationWithInternalServiceProvider _abpApplication = null!;

    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostEnvironment;

    public AbpInitializationHostedService(IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _abpApplication = await AbpApplicationFactory.CreateAsync<MyTelegramSmsSenderModule>(options =>
        {
            options.Services.ReplaceConfiguration(_configuration);
            options.Services.AddSingleton(_hostEnvironment);
            options.Services.Configure<TwilioSmsOptions>(_configuration.GetRequiredSection("TwilioSms"));
            options.UseAutofac();
            options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
        });

        await _abpApplication.InitializeAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _abpApplication.ShutdownAsync();
    }
}
