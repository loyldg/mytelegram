using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using EventFlow;
using EventFlow.Core;
using Microsoft.Extensions.DependencyInjection;
using MyTelegram.Schema;
using MyTelegram.Services.NativeAot;

namespace MyTelegram.Services.Extensions;

public static class EventFlowOptionsSystemTextJsonExtensions
{
    public static IEventFlowOptions AddSystemTextJson(
        this IEventFlowOptions eventFlowOptions,
        Action<JsonSerializerOptions>? configure = default)
    {
        eventFlowOptions.ServiceCollection.AddSystemTextJson(configure);

        return eventFlowOptions;
    }

    public static IServiceCollection AddSystemTextJson(this IServiceCollection services,
        Action<JsonSerializerOptions>? configure = default)
    {
        var serializer = new SystemTextJsonSerializer((jsonOptions) =>
        {
            //jsonOptions.TypeInfoResolver = new PolymorphicTypeResolver();
            jsonOptions.Converters.Add(new BitArrayConverter());
            configure?.Invoke(jsonOptions);
        });
        services.AddSingleton<IJsonSerializer>(serializer);

        return services;
    }

    //public static IEventFlowOptions UseSystemTextJson(
    //    this IEventFlowOptions eventFlowOptions,
    //    JsonSerializerOptions? options = default)
    //{
    //    eventFlowOptions.ServiceCollection.AddSingleton(options ?? new JsonSerializerOptions());
    //    eventFlowOptions.ServiceCollection.AddSingleton<ISystemTextJsonSerializer, SystemTextJsonSerializer>();
    //    eventFlowOptions.ServiceCollection.AddSingleton<IJsonSerializer>(new SystemTextJsonSerializer());

    //    //eventFlowOptions.ServiceCollection.AddSingleton<IEventJsonSerializer, MyEventJsonSerializer>();
    //    //eventFlowOptions.ServiceCollection.AddSingleton<ISnapshotSerializer, MySnapshotSerializer>();

    //    return eventFlowOptions;
    //}
}

public class BitArrayConverter : JsonConverter<BitArray>
{
    public override BitArray? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetInt32(out var value))
        {
            return new BitArray(BitConverter.GetBytes(value));
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, BitArray value, JsonSerializerOptions options)
    {
        var bytes = new byte[4];
        value.CopyTo(bytes, 0);
        writer.WriteNumberValue(BitConverter.ToInt32(bytes));
    }
}

//public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
//{
//    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
//    {
//        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

//        var baseType = typeof(IObject);
//        if (jsonTypeInfo.Type.IsAssignableTo(baseType))
//        {
//            if (jsonTypeInfo.Type.IsInterface)
//            {
//                var subTypes = baseType.Assembly.GetTypes()
//                        .Where(p => p.IsAssignableTo(jsonTypeInfo.Type) && !p.IsAbstract)
//                    ;

//                jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
//                {
//                    TypeDiscriminatorPropertyName = jsonTypeInfo.Type.Name,

//                };


//                foreach (var subType in subTypes)
//                {
//                    jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(subType));
//                }
//            }
//        }
//        return jsonTypeInfo;
//    }
//}

//public interface IJsonContextProvider
//{
//    JsonSerializerContext GetSerializerContext();
//}

//public class MyInstantJobScheduler : IJobScheduler
//{
//    private readonly IJobDefinitionService _jobDefinitionService;
//    private readonly IJobRunner _jobRunner;
//    private readonly ILogger<MyInstantJobScheduler> _logger;
//    private readonly ISystemTextJsonSerializer _jsonSerializer;

//    public MyInstantJobScheduler(
//        ILogger<MyInstantJobScheduler> logger,
//        ISystemTextJsonSerializer jsonSerializer,
//        IJobRunner jobRunner,
//        IJobDefinitionService jobDefinitionService)
//    {
//        _logger = logger;
//        _jsonSerializer = jsonSerializer;
//        _jobRunner = jobRunner;
//        _jobDefinitionService = jobDefinitionService;
//    }

//    public async Task<IJobId> ScheduleNowAsync(IJob job, CancellationToken cancellationToken)
//    {
//        if (job == null) throw new ArgumentNullException(nameof(job));

//        var jobDefinition = _jobDefinitionService.GetDefinition(job.GetType());

//        try
//        {
//            var json = _jsonSerializer.Serialize(job, job.GetType(), MyJsonContext.Default);

//            _logger.LogDebug(
//                "Executing job {JobName} v{JobVersion}: {Json}",
//                jobDefinition.Name,
//                jobDefinition.Version,
//                json);

//            await _jobRunner.ExecuteAsync(jobDefinition.Name, jobDefinition.Version, json, cancellationToken);
//        }
//        catch (Exception e)
//        {
//            // We want the InstantJobScheduler to behave as an out-of-process scheduler, i.e., doesn't
//            // throw exceptions directly related to the job execution

//            _logger.LogError(
//                e,
//                "Execution of job {JobName} v{JobVersion} failed due to {ExceptionType}: {ExceptionMessage}",
//                jobDefinition.Name,
//                jobDefinition.Version,
//                e.GetType().PrettyPrint(),
//                e.Message);
//        }

//        return JobId.New;
//    }

//    public Task<IJobId> ScheduleAsync(
//        IJob job,
//        DateTimeOffset runAt,
//        CancellationToken cancellationToken)
//    {
//        if (job == null)
//        {
//            throw new ArgumentNullException(nameof(job));
//        }

//        _logger.LogWarning(
//            "Instant scheduling configured, executing job {JobType} NOW! Instead of at {RunAt}",
//            job.GetType().PrettyPrint(),
//            runAt);

//        // Don't schedule, just execute...
//        return ScheduleNowAsync(job, cancellationToken);
//    }

//    public Task<IJobId> ScheduleAsync(
//        IJob job,
//        TimeSpan delay,
//        CancellationToken cancellationToken)
//    {
//        if (job == null)
//        {
//            throw new ArgumentNullException(nameof(job));
//        }

//        _logger.LogWarning(
//            "Instant scheduling configured, executing job {JobType} NOW! Instead of in {Delay}",
//            job.GetType().PrettyPrint(),
//            delay);

//        // Don't schedule, just execute...
//        return ScheduleNowAsync(job, cancellationToken);
//    }
//}

//public class SystemTextJsonSingleValueObjectConverter<TSourceType, TValue> : System.Text.Json.Serialization.JsonConverter<TValue>
//    where TValue : SingleValueObject<TValue>
//{
//    private static Func<TSourceType, TValue> _createInstanceFunc;
//    //private readonly static Type UnderlyingType;

//    //static SystemTextJsonSingleValueObjectConverter()
//    //{
//    //    UnderlyingType = typeof(TValue);
//    //}

//    public override TValue? Read(ref Utf8JsonReader reader,
//        Type typeToConvert,
//        JsonSerializerOptions options)
//    {
//        var text = reader.GetString();

//        _createInstanceFunc ??= GenericConstructorHelper.CompileConstructor<TSourceType, TValue>();
//        var value = JsonSerializer.Deserialize<TSourceType>(ref reader, options);
//        return _createInstanceFunc(value);

//        //return (T)Activator.CreateInstance(typeof(T), text);
//    }

//    public override void Write(Utf8JsonWriter writer,
//        TValue value,
//        JsonSerializerOptions options)
//    {
//        writer.WriteStringValue(value.GetValue().ToString());
//    }

//    public override bool CanConvert(Type typeToConvert)
//    {
//        return typeof(TValue).GetTypeInfo().IsAssignableFrom(typeToConvert);
//    }
//}

//public class SystemTextJsonSingleValueObjectConverterFactory : JsonConverterFactory
//{
//    private readonly static Type ConverterGenericType = typeof(SystemTextJsonSingleValueObjectConverter<,>);
//    private readonly static ConcurrentDictionary<Type, JsonConverter> Converters = new();

//    public override bool CanConvert(Type typeToConvert)
//    {
//        return typeof(ISingleValueObject).GetTypeInfo().IsAssignableFrom(typeToConvert);
//    }

//    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
//    {
//        var converter = Converters.GetOrAdd(
//            typeToConvert,
//            t => {
//                var constructedType = ConverterGenericType.MakeGenericType(typeToConvert.GetGenericArguments());
//                Console.WriteLine($"Create new converter:{constructedType.Name}");
//                return Activator.CreateInstance(constructedType) as JsonConverter;
//            });

//        return converter;
//    }
//}
