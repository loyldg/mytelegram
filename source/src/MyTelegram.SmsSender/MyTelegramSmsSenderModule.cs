using Volo.Abp.Autofac;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace MyTelegram.SmsSender;

[DependsOn(typeof(AbpEventBusRabbitMqModule),
    typeof(AbpAutofacModule))
]
public class MyTelegramSmsSenderModule : AbpModule
{
}
