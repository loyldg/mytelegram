namespace MyTelegram.Services.TLObjectConverters;

public interface ILayeredConverter : IHasRequestLayer
{
    /// <summary>
    /// The layer of the converter
    /// </summary>
    int Layer { get; }

    ///// <summary>
    ///// The layer of the request
    ///// </summary>
    //int RequestLayer { get; set; }
}