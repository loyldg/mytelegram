namespace MyTelegram.Core;

public partial record LayeredData<TData>(Dictionary<int, TData>? DataWithLayer);