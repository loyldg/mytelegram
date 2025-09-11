//namespace MyTelegram.Converters.Responses;

//public class BroadcastRevenueBalancesResponseService(
//    ILayeredService<IBroadcastRevenueBalancesResponseConverter> broadcastRevenueBalancesLayeredService)
//    : IBroadcastRevenueBalancesResponseService, ITransientDependency
//{
//    public IBroadcastRevenueBalances ToLayeredData(IBroadcastRevenueBalances latestLayerData, int layer)
//    {
//        switch (latestLayerData)
//        {
//            case TBroadcastRevenueBalances broadcastRevenueBalances:
//                return broadcastRevenueBalancesLayeredService.GetConverter(layer)
//                    .ToLayeredData(broadcastRevenueBalances);
//        }

//        return latestLayerData;
//    }
//}