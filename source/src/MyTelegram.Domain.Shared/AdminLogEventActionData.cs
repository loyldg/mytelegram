namespace MyTelegram;

public record AdminLogEventActionData<TData>(TData PreviewValue, TData NewValue);