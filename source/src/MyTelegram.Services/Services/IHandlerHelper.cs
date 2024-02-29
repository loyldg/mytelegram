using MyTelegram.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MyTelegram.Services.Services;

public interface IHandlerHelper
{
    void InitAllHandlers(Assembly assembly, int totalHandlersCount = 0);

    bool TryGetHandler(uint objectId,
        [NotNullWhen(true)] out IObjectHandler? handler);

    bool TryGetHandlerName(uint objectId,
        [NotNullWhen(true)] out string? handlerName);

    bool TryGetHandlerShortName(uint objectId,
        [NotNullWhen(true)] out string? handlerShortName);

    string GetHandlerFullName(IObject requestData);
}