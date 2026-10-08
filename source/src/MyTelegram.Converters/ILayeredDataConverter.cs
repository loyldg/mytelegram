namespace MyTelegram.Converters;

public interface ILayeredDataConverter
{
    TDestinationLayerData ToLayeredData<TSource, TDestinationLayerData>(TSource source)
        where TDestinationLayerData : IObject, new()
        where TSource : IObject, new();
}