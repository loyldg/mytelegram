using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public interface IObjectHandler
{
    Task<IObject> HandleAsync(IRequestInput request,
        IObject obj);
}