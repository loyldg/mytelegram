namespace MyTelegram.Services.Services;

public interface IExceptionProcessor
{
    Task HandleExceptionAsync(Exception ex, IRequestInput input, IObject? requestData, string? handlerName);
}