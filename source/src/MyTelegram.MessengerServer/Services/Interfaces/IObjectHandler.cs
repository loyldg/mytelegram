namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IObjectHandler
{
    Task<IObject> HandleAsync(IRequestInput request,
        IObject obj);
}