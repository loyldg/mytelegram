namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer164;

public abstract class LayeredConverterBase : ILayeredConverter
{
    public int RequestLayer { get; set; }
    protected int GetLayer() => RequestLayer > 0 ? RequestLayer : Layer;
    public abstract int Layer { get; }
}