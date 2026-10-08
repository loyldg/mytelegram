using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MyTelegram.Services.Services;

public class HandlerHelper(IServiceProvider serviceProvider, ILogger<HandlerHelper> logger) : IHandlerHelper, ISingletonDependency
{
    private FrozenDictionary<uint, IObjectHandler> _handlers = new Dictionary<uint, IObjectHandler>().ToFrozenDictionary();
    private FrozenDictionary<uint, string> _handlerNames = new Dictionary<uint, string>().ToFrozenDictionary();
    public void InitAllHandlers()
    {
        var sw = Stopwatch.StartNew();
        var handlers = serviceProvider.GetRequiredService<IEnumerable<IObjectHandler>>();

        var allHandlers = new Dictionary<uint, IObjectHandler>();
        var allHandlerNames = new Dictionary<uint, string>();
        foreach (var handler in handlers)
        {
            var handlerType = handler.GetType();
            var genericArgument = handlerType.BaseType?.GetGenericArguments();
            if (genericArgument?.Length > 0)
            {
                var attr = genericArgument[0].GetCustomAttribute<TlObjectAttribute>();
                if (attr != null)
                {
                    allHandlers.TryAdd(attr.ConstructorId, handler);
                    var handlerName = !string.IsNullOrEmpty(handlerType.Namespace) ?
                        $"{handlerType.Namespace[handlerType.Namespace.LastIndexOf(".", StringComparison.OrdinalIgnoreCase)..]}.{handlerType.Name}"
                        : handlerType.Name;
                    allHandlerNames.TryAdd(attr.ConstructorId, handlerName);
                }
            }
        }

        _handlers = allHandlers.ToFrozenDictionary();
        _handlerNames = allHandlerNames.ToFrozenDictionary();
        sw.Stop();

        logger.LogInformation("All handlers created, count: {Count}, time: {TimeSpan}", _handlers.Count, sw.Elapsed);
    }

    public bool TryGetHandler(uint objectId, [NotNullWhen(true)] out IObjectHandler? handler)
    {
        if (_handlers.TryGetValue(objectId, out handler))
        {
            return true;
        }

        logger.LogWarning("****************** Unsupported request, objectId: {ObjectId:x2}", objectId);

        return false;
        //throw new NotImplementedException();
    }

    public bool TryGetHandlerName(uint objectId, [NotNullWhen(true)] out string? handlerName)
    {
        return _handlerNames.TryGetValue(objectId, out handlerName);
    }

    public bool TryGetHandlerShortName(uint objectId, [NotNullWhen(true)] out string? handlerShortName)
    {
        return _handlerNames.TryGetValue(objectId, out handlerShortName);
    }

    public string GetHandlerFullName(IObject requestData)
    {
        if (requestData is IHasSubQuery subQuery)
        {
            if (subQuery.Query is IHasSubQuery subQuery2)
            {
                if (subQuery2.Query is IHasSubQuery subQuery3)
                {
                    return
                        $"{GetName(requestData)}->{GetName(subQuery.Query)}->{GetName(subQuery2.Query)}->{GetName(subQuery3.Query)}";
                }
                return
                    $"{GetName(requestData)}->{GetName(subQuery.Query)}->{GetName(subQuery2.Query)}";
            }
            return
                $"{GetName(requestData)}->{GetName(subQuery.Query)}";
        }
        return requestData.GetType().Name;
    }

    private string GetName(IObject requestData)
    {
        const int count = 7;// "Request".Length
        return RemovePrefix(requestData.GetType().Name, count);
    }

    private static string RemovePrefix(string text, int removeCount)
    {
        if (text.Length > removeCount)
        {
            return text[removeCount..];
        }
        return text;
    }
}