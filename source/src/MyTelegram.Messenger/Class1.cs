//namespace MyTelegram.Messenger;

//public interface IHasRequestLayer
//{
//    /// <summary>
//    /// The layer of the request
//    /// </summary>
//    int RequestLayer { get; set; }
//}

//public interface ILayeredConverter : IHasRequestLayer
//{
//    /// <summary>
//    /// The layer of the converter
//    /// </summary>
//    int Layer { get; }

//    ///// <summary>
//    ///// The layer of the request
//    ///// </summary>
//    //int RequestLayer { get; set; }
//}

//public interface ILayeredService<out TLayeredConverter>
//    where TLayeredConverter : ILayeredConverter
//{
//    /// <summary>
//    /// base converter
//    /// </summary>
//    TLayeredConverter Converter { get; }
//    int MinLayer { get; }
//    TLayeredConverter GetConverter(int layer);
//    //LayeredData GetLayeredData(Func<TLayeredConverter, IObject> func);
//    LayeredData<TData> GetLayeredData<TData>(Func<TLayeredConverter, TData> func);
//}

//public class LayeredService<TLayeredConverter> : ILayeredService<TLayeredConverter> where TLayeredConverter : ILayeredConverter
//{
//    private readonly IEnumerable<TLayeredConverter> _converters;
//    public LayeredService(TLayeredConverter converter,
//        IEnumerable<TLayeredConverter> converters)
//    {
//        Converter = converter;
//        //_converters = converters;
//        _converters = converters.OrderByDescending(p => p.Layer);
//    }

//    public TLayeredConverter Converter { get; }
//    public int MinLayer => 158;

//    public TLayeredConverter GetConverter(int layer)
//    {
//        var converter = Converter;

//        if (layer == 0)
//        {
//            //Converter.RequestLayer = layer;
//            converter.RequestLayer = layer;
//            return converter;
//        }

//        // latest layer converter
//        foreach (var layeredConverter in _converters)
//        {
//            // if we can not find converter.Layer==layer,return the latest layer where converter.Layer<layer
//            if (layeredConverter.Layer == layer)
//            {
//                converter = layeredConverter;
//                break;
//            }

//            if (layeredConverter.Layer < layer)
//            {
//                converter = layeredConverter;
//                break;
//            }
//            //else if (layeredConverter.Layer > layer)
//            //{
//            //    break;
//            //}
//        }
//        converter.RequestLayer = layer;
//        return converter;
//    }

//    //public LayeredData GetLayeredData(Func<TLayeredConverter, IObject> func)
//    //{
//    //    return new LayeredData(GetLayeredData<IObject>(func).DataWithLayer);
//    //}

//    public LayeredData<TData> GetLayeredData<TData>(Func<TLayeredConverter, TData> func)
//    {
//        Dictionary<int, TData>? data = null;
//        foreach (var layeredConverter in _converters)
//        {
//            if (layeredConverter.Layer < MinLayer)
//            {
//                continue;
//            }

//            //if (layeredConverter.Layer != Converter.Layer)
//            {
//                data ??= new();
//                data.TryAdd(layeredConverter.Layer, func(layeredConverter));
//            }
//        }

//        return new LayeredData<TData>(data);
//    }
//}

//public class Layers
//{
//    public const int Layer143 = 143;
//    public const int Layer144 = 144;
//    public const int Layer145 = 145;
//    public const int Layer146 = 146;
//    public const int Layer147 = 147;
//    public const int Layer148 = 148;
//    public const int Layer149 = 149;
//    public const int Layer150 = 150;
//    public const int Layer151 = 151;
//    public const int Layer152 = 152;
//    public const int Layer153 = 153;
//    public const int Layer154 = 154;
//    public const int Layer155 = 155;
//    public const int Layer156 = 156;
//    public const int Layer158 = 158;
//    public const int Layer160 = 160;
//    public const int Layer164 = 164;
//}