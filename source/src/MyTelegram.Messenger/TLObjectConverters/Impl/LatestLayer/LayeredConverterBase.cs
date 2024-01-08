namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public abstract class LayeredConverterBase : ILayeredConverter
{
    public abstract int Layer { get; }
    public int RequestLayer { get; set; }
    protected int GetLayer()
    {
        return RequestLayer > 0 ? RequestLayer : Layer;
    }
}