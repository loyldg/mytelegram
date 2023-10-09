namespace MyTelegram.Services.TLObjectConverters;

public interface IHasRequestLayer
{
    /// <summary>
    /// The layer of the request
    /// </summary>
    int RequestLayer { get; set; }
}
