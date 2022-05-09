namespace MyTelegram.Abp;

[DependsOn(
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpEventBusRabbitMqModule))]
public class MyTelegramAbpModule : AbpModule
{
}