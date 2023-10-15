//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging.Abstractions;
//using MyTelegram.Messenger.QueryServer.EventHandlers;
//using MyTelegram.MessengerServer;
//using RabbitMQ.Client.Events;
//using RabbitMQ.Client;
//using Volo.Abp;
//using Volo.Abp.Autofac;
//using Volo.Abp.EventBus.RabbitMq;
//using Volo.Abp.ExceptionHandling;
//using Volo.Abp.Modularity;
//using Volo.Abp.RabbitMQ;
//using Volo.Abp.Threading;
//using JetBrains.Annotations;
//using Volo.Abp.Collections;
//using Volo.Abp.EventBus.Local;
//using Volo.Abp.EventBus;
//using Volo.Abp.Guids;
//using Volo.Abp.MultiTenancy;
//using Volo.Abp.Tracing;
//using Volo.Abp.Uow;
//using IEventHandlerInvoker = Volo.Abp.EventBus.IEventHandlerInvoker;
//using IRabbitMqSerializer = Volo.Abp.RabbitMQ.IRabbitMqSerializer;
//using IClock = Volo.Abp.Timing.IClock;
//using IEventHandler=Volo.Abp.EventBus.IEventHandler;


//namespace MyTelegram.Messenger.QueryServer.Abp;
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
//            //options.Handlers.Add<PtsEventHandler>();
//            options.Handlers.Add<UserIsOnlineEventHandler>();

//        });

//        Configure<AbpLocalEventBusOptions>(options =>
//        {
//            options.Handlers.Add<DistributedDomainEventHandler>();
//            options.Handlers.Add<DuplicateOperationExceptionHandler>();
//            options.Handlers.Add<MessengerEventHandler>();
//            //options.Handlers.Add<PtsEventHandler>();
//            options.Handlers.Add<UserIsOnlineEventHandler>();

//        });

//        //context.Services.AddSingleton<IRabbitMqMessageConsumer, RabbitMqMessageConsumer>();
//    }

//    //public override void OnApplicationInitialization(ApplicationInitializationContext context)
//    //{
//    //    context.ServiceProvider.GetRequiredService<MyRabbitMqDistributedEventBus>().Initialize();
//    //}
//}

////[Volo.Abp.DependencyInjection.Dependency(ReplaceServices = true)]
////[ExposeServices(typeof(IDistributedEventBus), typeof(MyRabbitMqDistributedEventBus))]
////public class MyRabbitMqDistributedEventBus : DistributedEventBusBase, ISingletonDependency
////{
////    protected AbpRabbitMqEventBusOptions AbpRabbitMqEventBusOptions { get; }
////    protected IConnectionPool ConnectionPool { get; }
////    protected IRabbitMqSerializer Serializer { get; }

////    //TODO: Accessing to the List<IEventHandlerFactory> may not be thread-safe!
////    protected ConcurrentDictionary<Type, List<IEventHandlerFactory>> HandlerFactories { get; }
////    protected ConcurrentDictionary<string, Type> EventTypes { get; }
////    protected IRabbitMqMessageConsumerFactory MessageConsumerFactory { get; }
////    protected IRabbitMqMessageConsumer Consumer { get; private set; }

////    private bool _exchangeCreated;

////    public MyRabbitMqDistributedEventBus(
////        IOptions<AbpRabbitMqEventBusOptions> options,
////        IConnectionPool connectionPool,
////        IRabbitMqSerializer serializer,
////        IServiceScopeFactory serviceScopeFactory,
////        IOptions<AbpDistributedEventBusOptions> distributedEventBusOptions,
////        IRabbitMqMessageConsumerFactory messageConsumerFactory,
////        ICurrentTenant currentTenant,
////        IUnitOfWorkManager unitOfWorkManager,
////        IGuidGenerator guidGenerator,
////        Volo.Abp.Timing.IClock clock,
////        IEventHandlerInvoker eventHandlerInvoker,
////        ILocalEventBus localEventBus)
////        : base(
////            serviceScopeFactory,
////            currentTenant,
////            unitOfWorkManager,
////            distributedEventBusOptions,
////            guidGenerator,
////            clock,
////            eventHandlerInvoker,
////            localEventBus)
////    {

////        ConnectionPool = connectionPool;
////        Serializer = serializer;
////        MessageConsumerFactory = messageConsumerFactory;
////        AbpRabbitMqEventBusOptions = options.Value;

////        HandlerFactories = new ConcurrentDictionary<Type, List<IEventHandlerFactory>>();
////        EventTypes = new ConcurrentDictionary<string, Type>();
////    }

////    public void Initialize()
////    {
////        Consumer = MessageConsumerFactory.Create(
////            new ExchangeDeclareConfiguration(
////                AbpRabbitMqEventBusOptions.ExchangeName,
////                type: AbpRabbitMqEventBusOptions.GetExchangeTypeOrDefault(),
////                durable: true
////            ),
////            new QueueDeclareConfiguration(
////                AbpRabbitMqEventBusOptions.ClientName,
////                durable: true,
////                exclusive: false,
////                autoDelete: false,
////                prefetchCount: AbpRabbitMqEventBusOptions.PrefetchCount
////            ),
////            AbpRabbitMqEventBusOptions.ConnectionName
////        );

////        Consumer.OnMessageReceived(ProcessEventAsync);

////        SubscribeHandlers(AbpDistributedEventBusOptions.Handlers);
////    }

////    private async Task ProcessEventAsync(IModel channel, BasicDeliverEventArgs ea)
////    {
////        var eventName = ea.RoutingKey;
////        var eventType = EventTypes.GetOrDefault(eventName);
////        if (eventType == null)
////        {
////            return;
////        }

////        var eventData = Serializer.Deserialize(ea.Body.ToArray(), eventType);

////        if (await AddToInboxAsync(ea.BasicProperties.MessageId, eventName, eventType, eventData))
////        {
////            return;
////        }

////        await TriggerHandlersDirectAsync(eventType, eventData);
////    }

////    public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
////    {
////        var handlerFactories = GetOrCreateHandlerFactories(eventType);

////        if (factory.IsInFactories(handlerFactories))
////        {
////            return NullDisposable.Instance;
////        }

////        handlerFactories.Add(factory);

////        if (handlerFactories.Count == 1) //TODO: Multi-threading!
////        {
////            Consumer.BindAsync(Volo.Abp.EventBus.EventNameAttribute.GetNameOrDefault(eventType));
////        }

////        return new EventHandlerFactoryUnregistrar(this, eventType, factory);
////    }

////    /// <inheritdoc/>
////    public override void Unsubscribe<TEvent>(Func<TEvent, Task> action)
////    {
////        Check.NotNull(action, nameof(action));

////        GetOrCreateHandlerFactories(typeof(TEvent))
////            .Locking(factories =>
////            {
////                factories.RemoveAll(
////                    factory =>
////                    {
////                        var singleInstanceFactory = factory as SingleInstanceHandlerFactory;
////                        if (singleInstanceFactory == null)
////                        {
////                            return false;
////                        }

////                        var actionHandler = singleInstanceFactory.HandlerInstance as ActionEventHandler<TEvent>;
////                        if (actionHandler == null)
////                        {
////                            return false;
////                        }

////                        return actionHandler.Action == action;
////                    });
////            });
////    }

////    /// <inheritdoc/>
////    public override void Unsubscribe(Type eventType, Volo.Abp.EventBus.IEventHandler handler)
////    {
////        GetOrCreateHandlerFactories(eventType)
////            .Locking(factories =>
////            {
////                factories.RemoveAll(
////                    factory =>
////                        factory is SingleInstanceHandlerFactory &&
////                        (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
////                );
////            });
////    }

////    /// <inheritdoc/>
////    public override void Unsubscribe(Type eventType, IEventHandlerFactory factory)
////    {
////        GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Remove(factory));
////    }

////    /// <inheritdoc/>
////    public override void UnsubscribeAll(Type eventType)
////    {
////        GetOrCreateHandlerFactories(eventType).Locking(factories => factories.Clear());
////    }

////    protected async override Task PublishToEventBusAsync(Type eventType, object eventData)
////    {
////        await PublishAsync(eventType, eventData, null);
////    }

////    protected override void AddToUnitOfWork(IUnitOfWork unitOfWork, UnitOfWorkEventRecord eventRecord)
////    {
////        unitOfWork.AddOrReplaceDistributedEvent(eventRecord);
////    }

////    public override async Task PublishFromOutboxAsync(
////        OutgoingEventInfo outgoingEvent,
////        OutboxConfig outboxConfig)
////    {
////        await TriggerDistributedEventSentAsync(new DistributedEventSent()
////        {
////            Source = DistributedEventSource.Outbox,
////            EventName = outgoingEvent.EventName,
////            EventData = outgoingEvent.EventData
////        });

////        await PublishAsync(outgoingEvent.EventName, outgoingEvent.EventData, null, eventId: outgoingEvent.Id);
////    }

////    public async override Task PublishManyFromOutboxAsync(
////        IEnumerable<OutgoingEventInfo> outgoingEvents,
////        OutboxConfig outboxConfig)
////    {
////        using (var channel = ConnectionPool.Get(AbpRabbitMqEventBusOptions.ConnectionName).CreateModel())
////        {
////            var outgoingEventArray = outgoingEvents.ToArray();
////            channel.ConfirmSelect();

////            foreach (var outgoingEvent in outgoingEventArray)
////            {
////                await TriggerDistributedEventSentAsync(new DistributedEventSent()
////                {
////                    Source = DistributedEventSource.Outbox,
////                    EventName = outgoingEvent.EventName,
////                    EventData = outgoingEvent.EventData
////                });

////                await PublishAsync(
////                    channel,
////                    outgoingEvent.EventName,
////                    outgoingEvent.EventData,
////                    properties: null,
////                    eventId: outgoingEvent.Id);
////            }

////            channel.WaitForConfirmsOrDie();
////        }
////    }

////    public async override Task ProcessFromInboxAsync(
////        IncomingEventInfo incomingEvent,
////        InboxConfig inboxConfig)
////    {
////        var eventType = EventTypes.GetOrDefault(incomingEvent.EventName);
////        if (eventType == null)
////        {
////            return;
////        }

////        var eventData = Serializer.Deserialize(incomingEvent.EventData, eventType);
////        var exceptions = new List<Exception>();
////        await TriggerHandlersFromInboxAsync(eventType, eventData, exceptions, inboxConfig);
////        if (exceptions.Any())
////        {
////            ThrowOriginalExceptions(eventType, exceptions);
////        }
////    }

////    protected override byte[] Serialize(object eventData)
////    {
////        return Serializer.Serialize(eventData);
////    }

////    public Task PublishAsync(
////        Type eventType,
////        object eventData,
////        IBasicProperties properties,
////        Dictionary<string, object> headersArguments = null)
////    {
////        var eventName = Volo.Abp.EventBus.EventNameAttribute.GetNameOrDefault(eventType);
////        var body = Serializer.Serialize(eventData);

////        return PublishAsync(eventName, body, properties, headersArguments);
////    }

////    protected virtual Task PublishAsync(
////        string eventName,
////        byte[] body,
////        IBasicProperties properties,
////        Dictionary<string, object> headersArguments = null,
////        Guid? eventId = null)
////    {
////        using (var channel = ConnectionPool.Get(AbpRabbitMqEventBusOptions.ConnectionName).CreateModel())
////        {
////            return PublishAsync(channel, eventName, body, properties, headersArguments, eventId);
////        }
////    }

////    protected virtual Task PublishAsync(
////        IModel channel,
////        string eventName,
////        byte[] body,
////        IBasicProperties properties,
////        Dictionary<string, object> headersArguments = null,
////        Guid? eventId = null)
////    {
////        EnsureExchangeExists(channel);

////        if (properties == null)
////        {
////            properties = channel.CreateBasicProperties();
////            properties.DeliveryMode = RabbitMqConsts.DeliveryModes.Persistent;
////        }

////        if (string.IsNullOrEmpty(properties.MessageId))
////        {
////            properties.MessageId = (eventId ?? GuidGenerator.Create()).ToString("N");
////        }

////        SetEventMessageHeaders(properties, headersArguments);

////        channel.BasicPublish(
////            exchange: AbpRabbitMqEventBusOptions.ExchangeName,
////            routingKey: eventName,
////            mandatory: true,
////            basicProperties: properties,
////            body: body
////        );

////        return Task.CompletedTask;
////    }

////    private void EnsureExchangeExists(IModel channel)
////    {
////        if (_exchangeCreated)
////        {
////            return;
////        }

////        try
////        {
////            using (var temporaryChannel = ConnectionPool.Get(AbpRabbitMqEventBusOptions.ConnectionName).CreateModel())
////            {
////                temporaryChannel.ExchangeDeclarePassive(AbpRabbitMqEventBusOptions.ExchangeName);
////            }
////        }
////        catch (Exception)
////        {
////            channel.ExchangeDeclare(
////                AbpRabbitMqEventBusOptions.ExchangeName,
////                AbpRabbitMqEventBusOptions.GetExchangeTypeOrDefault(),
////                durable: true
////            );
////        }
////        _exchangeCreated = true;
////    }

////    private void SetEventMessageHeaders(IBasicProperties properties, Dictionary<string, object> headersArguments)
////    {
////        if (headersArguments == null)
////        {
////            return;
////        }

////        properties.Headers ??= new Dictionary<string, object>();

////        foreach (var header in headersArguments)
////        {
////            properties.Headers[header.Key] = header.Value;
////        }
////    }

////    protected override Task OnAddToOutboxAsync(string eventName, Type eventType, object eventData)
////    {
////        EventTypes.GetOrAdd(eventName, eventType);
////        return base.OnAddToOutboxAsync(eventName, eventType, eventData);
////    }

////    private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
////    {
////        return HandlerFactories.GetOrAdd(
////            eventType,
////            type =>
////            {
////                var eventName = Volo.Abp.EventBus.EventNameAttribute.GetNameOrDefault(type);
////                EventTypes.GetOrAdd(eventName, eventType);
////                return new List<IEventHandlerFactory>();
////            }
////        );
////    }

////    protected override IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType)
////    {
////        var handlerFactoryList = new List<EventTypeWithEventHandlerFactories>();

////        foreach (var handlerFactory in
////                 HandlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
////        {
////            handlerFactoryList.Add(
////                new EventTypeWithEventHandlerFactories(handlerFactory.Key, handlerFactory.Value));
////        }

////        return handlerFactoryList.ToArray();
////    }

////    private static bool ShouldTriggerEventForHandler(Type targetEventType, Type handlerEventType)
////    {
////        //Should trigger same type
////        if (handlerEventType == targetEventType)
////        {
////            return true;
////        }

////        //TODO: Support inheritance? But it does not support on subscription to RabbitMq!
////        //Should trigger for inherited types
////        if (handlerEventType.IsAssignableFrom(targetEventType))
////        {
////            return true;
////        }

////        return false;
////    }
////}

////public abstract class DistributedEventBusBase : EventBusBase, IDistributedEventBus, ISupportsEventBoxes
////{
////    protected IGuidGenerator GuidGenerator { get; }
////    protected Volo.Abp.Timing.IClock Clock { get; }
////    protected AbpDistributedEventBusOptions AbpDistributedEventBusOptions { get; }
////    protected ILocalEventBus LocalEventBus { get; }

////    protected DistributedEventBusBase(
////        IServiceScopeFactory serviceScopeFactory,
////        ICurrentTenant currentTenant,
////        IUnitOfWorkManager unitOfWorkManager,
////        IOptions<AbpDistributedEventBusOptions> abpDistributedEventBusOptions,
////        IGuidGenerator guidGenerator,
////        Volo.Abp.Timing.IClock clock,
////        IEventHandlerInvoker eventHandlerInvoker,
////        ILocalEventBus localEventBus) : base(
////        serviceScopeFactory,
////        currentTenant,
////        unitOfWorkManager,
////        eventHandlerInvoker)
////    {
////        GuidGenerator = guidGenerator;
////        Clock = clock;
////        AbpDistributedEventBusOptions = abpDistributedEventBusOptions.Value;
////        LocalEventBus = localEventBus;
////    }

////    public IDisposable Subscribe<TEvent>(IDistributedEventHandler<TEvent> handler) where TEvent : class
////    {
////        return Subscribe(typeof(TEvent), handler);
////    }

////    public override Task PublishAsync(Type eventType, object eventData, bool onUnitOfWorkComplete = true)
////    {
////        return PublishAsync(eventType, eventData, onUnitOfWorkComplete, useOutbox: true);
////    }

////    public Task PublishAsync<TEvent>(
////        TEvent eventData,
////        bool onUnitOfWorkComplete = true,
////        bool useOutbox = true)
////        where TEvent : class
////    {
////        return PublishAsync(typeof(TEvent), eventData, onUnitOfWorkComplete, useOutbox);
////    }

////    public async Task PublishAsync(
////        Type eventType,
////        object eventData,
////        bool onUnitOfWorkComplete = true,
////        bool useOutbox = true)
////    {
////        if (onUnitOfWorkComplete && UnitOfWorkManager.Current != null)
////        {
////            AddToUnitOfWork(
////                UnitOfWorkManager.Current,
////                new UnitOfWorkEventRecord(eventType, eventData, EventOrderGenerator.GetNext(), useOutbox)
////            );
////            return;
////        }

////        if (useOutbox)
////        {
////            if (await AddToOutboxAsync(eventType, eventData))
////            {
////                return;
////            }
////        }

////        await TriggerDistributedEventSentAsync(new DistributedEventSent()
////        {
////            Source = DistributedEventSource.Direct,
////            EventName = Volo.Abp.EventBus.EventNameAttribute.GetNameOrDefault(eventType),
////            EventData = eventData
////        });

////        await PublishToEventBusAsync(eventType, eventData);
////    }

////    public abstract Task PublishFromOutboxAsync(
////        OutgoingEventInfo outgoingEvent,
////        OutboxConfig outboxConfig
////    );

////    public abstract Task PublishManyFromOutboxAsync(
////        IEnumerable<OutgoingEventInfo> outgoingEvents,
////        OutboxConfig outboxConfig
////    );

////    public abstract Task ProcessFromInboxAsync(
////        IncomingEventInfo incomingEvent,
////        InboxConfig inboxConfig);

////    protected virtual async Task<bool> AddToOutboxAsync(Type eventType, object eventData)
////    {
////        var unitOfWork = UnitOfWorkManager.Current;
////        if (unitOfWork == null)
////        {
////            return false;
////        }

////        foreach (var outboxConfig in AbpDistributedEventBusOptions.Outboxes.Values.OrderBy(x => x.Selector is null))
////        {
////            if (outboxConfig.Selector == null || outboxConfig.Selector(eventType))
////            {
////                var eventOutbox = (IEventOutbox)unitOfWork.ServiceProvider.GetRequiredService(outboxConfig.ImplementationType);
////                var eventName = Volo.Abp.EventBus.EventNameAttribute.GetNameOrDefault(eventType);

////                await OnAddToOutboxAsync(eventName, eventType, eventData);

////                await TriggerDistributedEventSentAsync(new DistributedEventSent()
////                {
////                    Source = DistributedEventSource.Direct,
////                    EventName = eventName,
////                    EventData = eventData
////                });

////                await eventOutbox.EnqueueAsync(
////                    new OutgoingEventInfo(
////                        GuidGenerator.Create(),
////                        eventName,
////                        Serialize(eventData),
////                        Clock.Now
////                    )
////                );
////                return true;
////            }
////        }

////        return false;
////    }

////    protected virtual Task OnAddToOutboxAsync(string eventName, Type eventType, object eventData)
////    {
////        return Task.CompletedTask;
////    }

////    protected async Task<bool> AddToInboxAsync(
////        string messageId,
////        string eventName,
////        Type eventType,
////        object eventData)
////    {
////        if (AbpDistributedEventBusOptions.Inboxes.Count <= 0)
////        {
////            return false;
////        }

////        using (var scope = ServiceScopeFactory.CreateScope())
////        {
////            foreach (var inboxConfig in AbpDistributedEventBusOptions.Inboxes.Values.OrderBy(x => x.EventSelector is null))
////            {
////                if (inboxConfig.EventSelector == null || inboxConfig.EventSelector(eventType))
////                {
////                    var eventInbox =
////                        (IEventInbox)scope.ServiceProvider.GetRequiredService(inboxConfig.ImplementationType);

////                    if (!string.IsNullOrEmpty(messageId))
////                    {
////                        if (await eventInbox.ExistsByMessageIdAsync(messageId))
////                        {
////                            continue;
////                        }
////                    }

////                    await TriggerDistributedEventReceivedAsync(new DistributedEventReceived
////                    {
////                        Source = DistributedEventSource.Direct,
////                        EventName = Volo.Abp.EventBus.EventNameAttribute.GetNameOrDefault(eventType),
////                        EventData = eventData
////                    });

////                    await eventInbox.EnqueueAsync(
////                        new IncomingEventInfo(
////                            GuidGenerator.Create(),
////                            messageId,
////                            eventName,
////                            Serialize(eventData),
////                            Clock.Now
////                        )
////                    );
////                }
////            }
////        }

////        return true;
////    }

////    protected abstract byte[] Serialize(object eventData);

////    protected virtual async Task TriggerHandlersDirectAsync(Type eventType, object eventData)
////    {
////        await TriggerDistributedEventReceivedAsync(new DistributedEventReceived
////        {
////            Source = DistributedEventSource.Direct,
////            EventName = Volo.Abp.EventBus.EventNameAttribute.GetNameOrDefault(eventType),
////            EventData = eventData
////        });

////        await TriggerHandlersAsync(eventType, eventData);
////    }

////    protected virtual async Task TriggerHandlersFromInboxAsync(Type eventType, object eventData, List<Exception> exceptions, InboxConfig inboxConfig = null)
////    {
////        await TriggerDistributedEventReceivedAsync(new DistributedEventReceived
////        {
////            Source = DistributedEventSource.Inbox,
////            EventName = Volo.Abp.EventBus.EventNameAttribute.GetNameOrDefault(eventType),
////            EventData = eventData
////        });

////        await TriggerHandlersAsync(eventType, eventData, exceptions, inboxConfig);
////    }

////    public virtual async Task TriggerDistributedEventSentAsync(DistributedEventSent distributedEvent)
////    {
////        try
////        {
////            await LocalEventBus.PublishAsync(distributedEvent);
////        }
////        catch (Exception _)
////        {
////            // ignored
////        }
////    }

////    public virtual async Task TriggerDistributedEventReceivedAsync(DistributedEventReceived distributedEvent)
////    {
////        try
////        {
////            await LocalEventBus.PublishAsync(distributedEvent);
////        }
////        catch (Exception _)
////        {
////            // ignored
////        }
////    }
////}

////public abstract class EventBusBase : Volo.Abp.EventBus.IEventBus
////{
////    protected IServiceScopeFactory ServiceScopeFactory { get; }

////    protected ICurrentTenant CurrentTenant { get; }

////    protected IUnitOfWorkManager UnitOfWorkManager { get; }

////    protected IEventHandlerInvoker EventHandlerInvoker { get; }

////    protected EventBusBase(
////        IServiceScopeFactory serviceScopeFactory,
////        ICurrentTenant currentTenant,
////        IUnitOfWorkManager unitOfWorkManager,
////        IEventHandlerInvoker eventHandlerInvoker)
////    {
////        ServiceScopeFactory = serviceScopeFactory;
////        CurrentTenant = currentTenant;
////        UnitOfWorkManager = unitOfWorkManager;
////        EventHandlerInvoker = eventHandlerInvoker;
////    }

////    /// <inheritdoc/>
////    public virtual IDisposable Subscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class
////    {
////        return Subscribe(typeof(TEvent), new ActionEventHandler<TEvent>(action));
////    }

////    /// <inheritdoc/>
////    public virtual IDisposable Subscribe<TEvent, THandler>()
////        where TEvent : class
////        where THandler : IEventHandler, new()
////    {
////        return Subscribe(typeof(TEvent), new TransientEventHandlerFactory<THandler>());
////    }

////    /// <inheritdoc/>
////    public virtual IDisposable Subscribe(Type eventType, IEventHandler handler)
////    {
////        return Subscribe(eventType, new SingleInstanceHandlerFactory(handler));
////    }

////    /// <inheritdoc/>
////    public virtual IDisposable Subscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
////    {
////        return Subscribe(typeof(TEvent), factory);
////    }

////    public abstract IDisposable Subscribe(Type eventType, IEventHandlerFactory factory);

////    public abstract void Unsubscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class;

////    /// <inheritdoc/>
////    public virtual void Unsubscribe<TEvent>(ILocalEventHandler<TEvent> handler) where TEvent : class
////    {
////        Unsubscribe(typeof(TEvent), handler);
////    }

////    public abstract void Unsubscribe(Type eventType, IEventHandler handler);

////    /// <inheritdoc/>
////    public virtual void Unsubscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
////    {
////        Unsubscribe(typeof(TEvent), factory);
////    }

////    public abstract void Unsubscribe(Type eventType, IEventHandlerFactory factory);

////    /// <inheritdoc/>
////    public virtual void UnsubscribeAll<TEvent>() where TEvent : class
////    {
////        UnsubscribeAll(typeof(TEvent));
////    }

////    /// <inheritdoc/>
////    public abstract void UnsubscribeAll(Type eventType);

////    /// <inheritdoc/>
////    public Task PublishAsync<TEvent>(TEvent eventData, bool onUnitOfWorkComplete = true)
////        where TEvent : class
////    {
////        return PublishAsync(typeof(TEvent), eventData, onUnitOfWorkComplete);
////    }

////    /// <inheritdoc/>
////    public virtual async Task PublishAsync(
////        Type eventType,
////        object eventData,
////        bool onUnitOfWorkComplete = true)
////    {
////        if (onUnitOfWorkComplete && UnitOfWorkManager.Current != null)
////        {
////            AddToUnitOfWork(
////                UnitOfWorkManager.Current,
////                new UnitOfWorkEventRecord(eventType, eventData, EventOrderGenerator.GetNext())
////            );
////            return;
////        }

////        await PublishToEventBusAsync(eventType, eventData);
////    }

////    protected abstract Task PublishToEventBusAsync(Type eventType, object eventData);

////    protected abstract void AddToUnitOfWork(IUnitOfWork unitOfWork, UnitOfWorkEventRecord eventRecord);

////    public virtual async Task TriggerHandlersAsync(Type eventType, object eventData)
////    {
////        var exceptions = new List<Exception>();

////        await TriggerHandlersAsync(eventType, eventData, exceptions);

////        if (exceptions.Any())
////        {
////            ThrowOriginalExceptions(eventType, exceptions);
////        }
////    }

////    protected virtual async Task TriggerHandlersAsync(Type eventType, object eventData, List<Exception> exceptions, InboxConfig inboxConfig = null)
////    {
////        await new SynchronizationContextRemover();

////        foreach (var handlerFactories in GetHandlerFactories(eventType))
////        {
////            foreach (var handlerFactory in handlerFactories.EventHandlerFactories)
////            {
////                await TriggerHandlerAsync(handlerFactory, handlerFactories.EventType, eventData, exceptions, inboxConfig);
////            }
////        }

////        //Implements generic argument inheritance. See IEventDataWithInheritableGenericArgument
////        if (eventType.GetTypeInfo().IsGenericType &&
////            eventType.GetGenericArguments().Length == 1 &&
////            typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
////        {
////            var genericArg = eventType.GetGenericArguments()[0];
////            var baseArg = genericArg.GetTypeInfo().BaseType;
////            if (baseArg != null)
////            {
////                var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(baseArg);
////                var constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
////                var baseEventData = Activator.CreateInstance(baseEventType, constructorArgs);
////                await PublishToEventBusAsync(baseEventType, baseEventData);
////            }
////        }
////    }

////    protected void ThrowOriginalExceptions(Type eventType, List<Exception> exceptions)
////    {
////        if (exceptions.Count == 1)
////        {
////            exceptions[0].ReThrow();
////        }

////        throw new AggregateException(
////            "More than one error has occurred while triggering the event: " + eventType,
////            exceptions
////        );
////    }

////    protected virtual void SubscribeHandlers(ITypeList<IEventHandler> handlers)
////    {
////        foreach (var handler in handlers)
////        {
////            var interfaces = handler.GetInterfaces();
////            foreach (var @interface in interfaces)
////            {
////                if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
////                {
////                    continue;
////                }

////                var genericArgs = @interface.GetGenericArguments();
////                if (genericArgs.Length == 1)
////                {
////                    Subscribe(genericArgs[0], new IocEventHandlerFactory(ServiceScopeFactory, handler));
////                }
////            }
////        }
////    }

////    protected abstract IEnumerable<EventTypeWithEventHandlerFactories> GetHandlerFactories(Type eventType);

////    protected virtual async Task TriggerHandlerAsync(IEventHandlerFactory asyncHandlerFactory, Type eventType,
////        object eventData, List<Exception> exceptions, InboxConfig inboxConfig = null)
////    {
////        try
////        {
////            using var eventHandlerWrapper = asyncHandlerFactory.GetHandler();

////            var handlerType = eventHandlerWrapper.EventHandler.GetType();

////            if (inboxConfig?.HandlerSelector != null &&
////                !inboxConfig.HandlerSelector(handlerType))
////            {
////                return;
////            }

////            using (CurrentTenant.Change(GetEventDataTenantId(eventData)))
////            {
////                await InvokeEventHandlerAsync(eventHandlerWrapper.EventHandler, eventData, eventType);
////            }
////        }
////        catch (TargetInvocationException ex)
////        {
////            exceptions.Add(ex.InnerException);
////        }
////        catch (Exception ex)
////        {
////            exceptions.Add(ex);
////        }
////    }

////    protected virtual Task InvokeEventHandlerAsync(IEventHandler eventHandler, object eventData, Type eventType)
////    {
////        return EventHandlerInvoker.InvokeAsync(eventHandler, eventData, eventType);
////    }

////    protected virtual Guid? GetEventDataTenantId(object eventData)
////    {
////        return eventData switch
////        {
////            IMultiTenant multiTenantEventData => multiTenantEventData.TenantId,
////            IEventDataMayHaveTenantId eventDataMayHaveTenantId when eventDataMayHaveTenantId.IsMultiTenant(out var tenantId) => tenantId,
////            _ => CurrentTenant.Id
////        };
////    }

////    protected class EventTypeWithEventHandlerFactories
////    {
////        public Type EventType { get; }

////        public List<IEventHandlerFactory> EventHandlerFactories { get; }

////        public EventTypeWithEventHandlerFactories(Type eventType, List<IEventHandlerFactory> eventHandlerFactories)
////        {
////            EventType = eventType;
////            EventHandlerFactories = eventHandlerFactories;
////        }
////    }

////    // Reference from
////    // https://blogs.msdn.microsoft.com/benwilli/2017/02/09/an-alternative-to-configureawaitfalse-everywhere/
////    protected struct SynchronizationContextRemover : INotifyCompletion
////    {
////        public bool IsCompleted
////        {
////            get { return SynchronizationContext.Current == null; }
////        }

////        public void OnCompleted(Action continuation)
////        {
////            var prevContext = SynchronizationContext.Current;
////            try
////            {
////                SynchronizationContext.SetSynchronizationContext(null);
////                continuation();
////            }
////            finally
////            {
////                SynchronizationContext.SetSynchronizationContext(prevContext);
////            }
////        }

////        public SynchronizationContextRemover GetAwaiter()
////        {
////            return this;
////        }

////        public void GetResult()
////        {
////        }
////    }
////}



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

//public class AbpRunner
//{
//    public Task RunAsync()
//    {
//        using var app = AbpApplicationFactory.Create<MyTelegramMessengerQueryServerModule>(options =>
//        {
//            options.UseAutofac();
//        });
//        app.Initialize();

//        return Task.CompletedTask;
//    }
//}

//public class RabbitMqMessageConsumer : IRabbitMqMessageConsumer, ITransientDependency, IDisposable
//{
//    public ILogger<RabbitMqMessageConsumer> Logger { get; set; }

//    protected IConnectionPool ConnectionPool { get; }

//    protected IExceptionNotifier ExceptionNotifier { get; }

//    protected AbpAsyncTimer Timer { get; }

//    protected ExchangeDeclareConfiguration Exchange { get; private set; } = default!;

//    protected QueueDeclareConfiguration Queue { get; private set; } = default!;

//    protected string? ConnectionName { get; private set; }

//    protected ConcurrentBag<Func<IModel, BasicDeliverEventArgs, Task>> Callbacks { get; }

//    protected IModel? Channel { get; private set; }

//    protected ConcurrentQueue<QueueBindCommand> QueueBindCommands { get; }

//    protected object ChannelSendSyncLock { get; } = new object();

//    public RabbitMqMessageConsumer(
//        IConnectionPool connectionPool,
//        AbpAsyncTimer timer,
//        IExceptionNotifier exceptionNotifier)
//    {
//        ConnectionPool = connectionPool;
//        Timer = timer;
//        ExceptionNotifier = exceptionNotifier;
//        Logger = NullLogger<RabbitMqMessageConsumer>.Instance;

//        QueueBindCommands = new ConcurrentQueue<QueueBindCommand>();
//        Callbacks = new ConcurrentBag<Func<IModel, BasicDeliverEventArgs, Task>>();

//        Timer.Period = 5000; //5 sec.
//        Timer.Elapsed = Timer_Elapsed;
//        Timer.RunOnStart = true;
//    }

//    public void Initialize(
//          ExchangeDeclareConfiguration exchange,
//          QueueDeclareConfiguration queue,
//        string? connectionName = null)
//    {
//        Exchange = Check.NotNull(exchange, nameof(exchange));
//        Queue = Check.NotNull(queue, nameof(queue));
//        ConnectionName = connectionName;
//        Timer.Start();
//    }

//    public virtual async Task BindAsync(string routingKey)
//    {
//        QueueBindCommands.Enqueue(new QueueBindCommand(QueueBindType.Bind, routingKey));
//        await TrySendQueueBindCommandsAsync();
//    }

//    public virtual async Task UnbindAsync(string routingKey)
//    {
//        QueueBindCommands.Enqueue(new QueueBindCommand(QueueBindType.Unbind, routingKey));
//        await TrySendQueueBindCommandsAsync();
//    }

//    protected virtual async Task TrySendQueueBindCommandsAsync()
//    {
//        try
//        {
//            while (!QueueBindCommands.IsEmpty)
//            {
//                if (Channel == null || Channel.IsClosed)
//                {
//                    return;
//                }

//                lock (ChannelSendSyncLock)
//                {
//                    if (QueueBindCommands.TryPeek(out var command))
//                    {
//                        switch (command.Type)
//                        {
//                            case QueueBindType.Bind:
//                                Channel.QueueBind(
//                                    queue: Queue.QueueName,
//                                    exchange: Exchange.ExchangeName,
//                                    routingKey: command.RoutingKey
//                                );
//                                break;
//                            case QueueBindType.Unbind:
//                                Channel.QueueUnbind(
//                                    queue: Queue.QueueName,
//                                    exchange: Exchange.ExchangeName,
//                                    routingKey: command.RoutingKey
//                                );
//                                break;
//                            default:
//                                throw new AbpException($"Unknown {nameof(QueueBindType)}: {command.Type}");
//                        }

//                        QueueBindCommands.TryDequeue(out command);
//                    }
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Logger.LogException(ex, LogLevel.Warning);
//            await ExceptionNotifier.NotifyAsync(ex, logLevel: LogLevel.Warning);
//        }
//    }

//    public virtual void OnMessageReceived(Func<IModel, BasicDeliverEventArgs, Task> callback)
//    {
//        Callbacks.Add(callback);
//    }

//    protected virtual async Task Timer_Elapsed(AbpAsyncTimer timer)
//    {
//        if (Channel == null || Channel.IsOpen == false)
//        {
//            await TryCreateChannelAsync();
//            await TrySendQueueBindCommandsAsync();
//        }
//    }

//    protected virtual async Task TryCreateChannelAsync()
//    {
//        await DisposeChannelAsync();

//        try
//        {
//            Channel = ConnectionPool
//                .Get(ConnectionName)
//                .CreateModel();

//            Channel.ExchangeDeclare(
//                exchange: Exchange.ExchangeName,
//                type: Exchange.Type,
//                durable: Exchange.Durable,
//                autoDelete: Exchange.AutoDelete,
//                arguments: Exchange.Arguments
//            );

//            Channel.QueueDeclare(
//                queue: Queue.QueueName,
//                durable: Queue.Durable,
//                exclusive: Queue.Exclusive,
//                autoDelete: Queue.AutoDelete,
//                arguments: Queue.Arguments
//            );

//            if (Queue.PrefetchCount.HasValue)
//            {
//                Channel.BasicQos(0, Queue.PrefetchCount.Value, false);
//            }

//            var consumer = new AsyncEventingBasicConsumer(Channel);
//            consumer.Received += HandleIncomingMessageAsync;

//            Channel.BasicConsume(
//                queue: Queue.QueueName,
//                autoAck: false,
//                consumer: consumer
//            );
//        }
//        catch (Exception ex)
//        {
//            Logger.LogException(ex, LogLevel.Warning);
//            await ExceptionNotifier.NotifyAsync(ex, logLevel: LogLevel.Warning);
//        }
//    }

//    protected virtual async Task HandleIncomingMessageAsync(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
//    {
//        try
//        {
//            foreach (var callback in Callbacks)
//            {
//                await callback(Channel!, basicDeliverEventArgs);
//            }

//            Channel?.BasicAck(basicDeliverEventArgs.DeliveryTag, multiple: false);
//        }
//        catch (Exception ex)
//        {
//            try
//            {
//                Channel?.BasicNack(
//                    basicDeliverEventArgs.DeliveryTag,
//                    multiple: false,
//                    requeue: true
//                );
//            }
//            // ReSharper disable once EmptyGeneralCatchClause
//            catch { }

//            Logger.LogException(ex);
//            await ExceptionNotifier.NotifyAsync(ex);
//        }
//    }

//    protected virtual async Task DisposeChannelAsync()
//    {
//        if (Channel == null)
//        {
//            return;
//        }

//        try
//        {
//            Channel.Dispose();
//        }
//        catch (Exception ex)
//        {
//            Logger.LogException(ex, LogLevel.Warning);
//            await ExceptionNotifier.NotifyAsync(ex, logLevel: LogLevel.Warning);
//        }
//    }

//    protected virtual void DisposeChannel()
//    {
//        if (Channel == null)
//        {
//            return;
//        }

//        try
//        {
//            Channel.Dispose();
//        }
//        catch (Exception ex)
//        {
//            Logger.LogException(ex, LogLevel.Warning);
//            AsyncHelper.RunSync(() => ExceptionNotifier.NotifyAsync(ex, logLevel: LogLevel.Warning));
//        }
//    }

//    public virtual void Dispose()
//    {
//        Timer.Stop();
//        DisposeChannel();
//    }

//    protected class QueueBindCommand
//    {
//        public QueueBindType Type { get; }

//        public string RoutingKey { get; }

//        public QueueBindCommand(QueueBindType type, string routingKey)
//        {
//            Type = type;
//            RoutingKey = routingKey;
//        }
//    }

//    protected enum QueueBindType
//    {
//        Bind,
//        Unbind
//    }
//}