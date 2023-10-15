//using MyTelegram.Messenger.QueryServer.EventHandlers;
//using Volo.Abp.Autofac;
//using Volo.Abp.EventBus.Local;
//using Volo.Abp.EventBus.RabbitMq;
//using Volo.Abp.Modularity;

//namespace MyTelegram.Messenger.QueryServer.Abp;

//public class AbpEventBus : MyTelegram.EventBus.IEventBus
//{
//    private readonly IDistributedEventBus _distributedEventBus;

//    public AbpEventBus(IDistributedEventBus distributedEventBus)
//    {
//        _distributedEventBus = distributedEventBus;
//    }

//    public Task PublishAsync<TEventData>(TEventData eventData) where TEventData : class
//    {
//        return _distributedEventBus.PublishAsync(eventData);
//    }

//    public Task PublishAsync(Type eventDataType, object eventData)
//    {
//        return _distributedEventBus.PublishAsync(eventDataType, eventData);
//    }

//    public void Subscribe<TEvent, TEventHandler>() where TEventHandler : IEventHandler<TEvent>
//    {
//        throw new NotImplementedException();
//    }

//    public void Unsubscribe<TEvent, TEventHandler>() where TEventHandler : IEventHandler<TEvent>
//    {
//        throw new NotImplementedException();
//    }
//}
//[DependsOn(
//    typeof(AbpEventBusRabbitMqModule),
//    typeof(AbpAutofacModule)
//    )]
//public class MyTelegramMessengerQueryServerModule : AbpModule
//{
//    public override void ConfigureServices(ServiceConfigurationContext context)
//    {
//        context.Services.AddTransient<MyTelegram.EventBus.IEventBus, AbpEventBus>();
//        //context.Services.AddTransient<DistributedDomainEventHandler>();
//        //context.Services.AddTransient<DuplicateOperationExceptionHandler>();
//        //context.Services.AddTransient<MessengerEventHandler>();
//        //context.Services.AddTransient<PtsEventHandler>();
//        //context.Services.AddTransient<UserIsOnlineEventHandler>();

//        Configure<AbpRabbitMqEventBusOptions>(options =>
//        {
//            //options.ClientName = "MyTelegramMessengerQueryServer";
//            //options.ExchangeName = "MyTelegramExchange";
//        });


//        Configure<AbpDistributedEventBusOptions>(options =>
//        {
//            options.Handlers.Add<DistributedDomainEventHandler>();
//            options.Handlers.Add<DuplicateOperationExceptionHandler>();
//            options.Handlers.Add<MessengerEventHandler>();
//            options.Handlers.Add<PtsEventHandler>();
//            options.Handlers.Add<UserIsOnlineEventHandler>();
//            //options.Handlers.Add<PtsEventHandler>();

//        });

//        Configure<AbpLocalEventBusOptions>(options =>
//        {
//            options.Handlers.Add<DistributedDomainEventHandler>();
//            options.Handlers.Add<DuplicateOperationExceptionHandler>();
//            options.Handlers.Add<MessengerEventHandler>();
//            options.Handlers.Add<PtsEventHandler>();
//            options.Handlers.Add<UserIsOnlineEventHandler>();
//            //options.Handlers.Add<PtsEventHandler>();

//        });

//        //context.Services.AddSingleton<IRabbitMqMessageConsumer, RabbitMqMessageConsumer>();
//    }

//    //public override void OnApplicationInitialization(ApplicationInitializationContext context)
//    //{
//    //    context.ServiceProvider.GetRequiredService<MyRabbitMqDistributedEventBus>().Initialize();
//    //}
//}