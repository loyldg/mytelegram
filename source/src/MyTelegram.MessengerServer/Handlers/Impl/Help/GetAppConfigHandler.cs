using MyTelegram.Handlers.Help;
using MyTelegram.Schema.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetAppConfigHandler : RpcResultObjectHandler<RequestGetAppConfig, IJSONValue>,
    IGetAppConfigHandler, IProcessedHandler
{
    protected override Task<IJSONValue> HandleCoreAsync(IRequestInput input,
        RequestGetAppConfig obj)
    {
        IJSONValue r = new TJsonObject
        {
            Value = new TVector<IJSONObjectValue>(
                new TJsonObjectValue { Key = "test", Value = new TJsonNumber { Value = 1 } },
                new TJsonObjectValue { Key = "emojies_animated_zoom", Value = new TJsonNumber { Value = 0.625 } },
                new TJsonObjectValue { Key = "keep_alive_service", Value = new TJsonBool { Value = true } },
                //new TJsonObjectValue
                //{
                //    Key = "background_connection",
                //    Value = new TJsonBool
                //    {
                //        Value = true,
                //    }
                //},
                //new TJsonObjectValue
                //{
                //    Key = "emojies_send_dice",
                //    Value = new TJsonArray
                //    {
                //        Value = new TVector<IJSONValue>(
                //            new TJsonString
                //            {
                //                Value = "🎲"
                //            },
                //            new TJsonString
                //            {
                //                Value = "🎯"
                //            },
                //            new TJsonString
                //            {
                //                Value = "🏀"
                //            }
                //            )
                //    }
                //},
                new TJsonObjectValue { Key = "youtube_pip", Value = new TJsonString { Value = "disabled" } },
                new TJsonObjectValue { Key = "qr_login_camera", Value = new TJsonBool { Value = false } },
                //new TJsonObjectValue { Key = "qr_login_code", Value = new TJsonString { Value = "secondary" } },
                new TJsonObjectValue { Key = "qr_login_code", Value = new TJsonString { Value = "disabled" } },

                //new TJsonObjectValue { Key = "dialog_filters_enabled", Value = new TJsonBool { Value = true } },
                new TJsonObjectValue { Key = "dialog_filters_tooltip", Value = new TJsonBool { Value = false } },
                new TJsonObjectValue { Key = "chat_read_mark_size_threshold", Value = new TJsonNumber { Value = 50 } },
                new TJsonObjectValue
                {
                    Key = "chat_read_mark_expire_period", Value = new TJsonNumber { Value = 604800 }
                }
            )
        };

        return Task.FromResult(r);
    }
}
