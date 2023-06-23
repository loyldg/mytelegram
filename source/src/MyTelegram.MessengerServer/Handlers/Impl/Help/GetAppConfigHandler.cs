using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class GetAppConfigHandler : RpcResultObjectHandler<RequestGetAppConfig, IAppConfig>,
    IGetAppConfigHandler, IProcessedHandler
{
    protected override Task<IAppConfig> HandleCoreAsync(IRequestInput input,
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
                new TJsonObjectValue { Key = "qr_login_camera", Value = new TJsonBool { Value = true } },
                new TJsonObjectValue { Key = "qr_login_code", Value = new TJsonString { Value = "secondary" } },
                new TJsonObjectValue { Key = "dialog_filters_enabled", Value = new TJsonBool { Value = true } },
                new TJsonObjectValue { Key = "dialog_filters_tooltip", Value = new TJsonBool { Value = false } },
                new TJsonObjectValue { Key = "chat_read_mark_size_threshold", Value = new TJsonNumber { Value = 50 } },
                new TJsonObjectValue
                {
                    Key = "chat_read_mark_expire_period",
                    Value = new TJsonNumber { Value = 604800 }
                },
                new TJsonObjectValue
                {
                    Key = "chat_read_mark_size_threshold",
                    Value = new TJsonNumber { Value = 100 }
                },
                new TJsonObjectValue
                {
                    Key = "reactions_default",
                    Value = new TJsonString { Value = "👍" }
                },
                new TJsonObjectValue
                {
                    Key = "reactions_uniq_max",
                    Value = new TJsonNumber { Value = 11 }
                },
                new TJsonObjectValue
                {
                    Key = "premium_purchase_blocked",
                    Value = new TJsonBool { Value = false }
                },
                new TJsonObjectValue
                {
                    Key = "channels_limit_default",
                    Value = new TJsonNumber { Value = 500 }
                },
                new TJsonObjectValue
                {
                    Key = "channels_limit_premium",
                    Value = new TJsonNumber { Value = 1000 }
                },
                new TJsonObjectValue
                {
                    Key = "saved_gifs_limit_default",
                    Value = new TJsonNumber { Value = 200 }
                },
                new TJsonObjectValue
                {
                    Key = "saved_gifs_limit_premium",
                    Value = new TJsonNumber { Value = 400 }
                },
                new TJsonObjectValue
                {
                    Key = "stickers_faved_limit_default",
                    Value = new TJsonNumber { Value = 5 }
                },
                new TJsonObjectValue
                {
                    Key = "stickers_faved_limit_premium",
                    Value = new TJsonNumber { Value = 10 }
                },
                new TJsonObjectValue
                {
                    Key = "dialog_filters_limit_default",
                    Value = new TJsonNumber { Value = 10 }
                },
                new TJsonObjectValue
                {
                    Key = "dialog_filters_limit_premium",
                    Value = new TJsonNumber { Value = 20 }
                },
                new TJsonObjectValue
                {
                    Key = "dialog_filters_chats_limit_default",
                    Value = new TJsonNumber { Value = 100 }
                },
                new TJsonObjectValue
                {
                    Key = "dialog_filters_chats_limit_premium",
                    Value = new TJsonNumber { Value = 200 }
                },
                new TJsonObjectValue
                {
                    Key = "dialogs_pinned_limit_default",
                    Value = new TJsonNumber { Value = 5 }
                },
                new TJsonObjectValue
                {
                    Key = "dialogs_pinned_limit_premium",
                    Value = new TJsonNumber { Value = 10 }
                },
                new TJsonObjectValue
                {
                    Key = "dialogs_folder_pinned_limit_default",
                    Value = new TJsonNumber { Value = 100 }
                },
                new TJsonObjectValue
                {
                    Key = "dialogs_folder_pinned_limit_premium",
                    Value = new TJsonNumber { Value = 200 }
                },
                new TJsonObjectValue
                {
                    Key = "channels_public_limit_default",
                    Value = new TJsonNumber { Value = 100 }
                },
                new TJsonObjectValue
                {
                    Key = "channels_public_limit_premium",
                    Value = new TJsonNumber { Value = 200 }
                },

                // 
                new TJsonObjectValue
                {
                    Key = "caption_length_limit_default",
                    Value = new TJsonNumber { Value = 1024 }
                },
                new TJsonObjectValue
                {
                    Key = "caption_length_limit_premium",
                    Value = new TJsonNumber { Value = 2048 }
                },
                new TJsonObjectValue
                {
                    Key = "upload_max_fileparts_default",
                    Value = new TJsonNumber { Value = 4000 }
                },
                new TJsonObjectValue
                {
                    Key = "upload_max_fileparts_premium",
                    Value = new TJsonNumber { Value = 8000 }
                },
                new TJsonObjectValue
                {
                    Key = "about_length_limit_default",
                    Value = new TJsonNumber { Value = 70 }
                },
                new TJsonObjectValue
                {
                    Key = "about_length_limit_premium",
                    Value = new TJsonNumber { Value = 140 }
                },
                new TJsonObjectValue
                {
                    Key = "stickers_premium_by_emoji_num",
                    Value = new TJsonNumber { Value = 0 }
                },
                new TJsonObjectValue
                {
                    Key = "stickers_normal_by_emoji_per_premium_num",
                    Value = new TJsonNumber { Value = 3 }
                },
                new TJsonObjectValue
                {
                    Key = "message_animated_emoji_max",
                    Value = new TJsonNumber { Value = 3 }
                },
                new TJsonObjectValue
                {
                    Key = "premium_promo_order",
                    Value = new TJsonArray
                    {
                        Value = new TVector<IJSONValue>(
                            new TJsonString
                            {
                                Value = "double_limits"
                            },
                            new TJsonString
                            {
                                Value = "more_upload"
                            },
                            new TJsonString
                            {
                                Value = "faster_download"
                            },
                            new TJsonString
                            {
                                Value = "voice_to_text"
                            },
                            new TJsonString
                            {
                                Value = "no_ads"
                            },
                            new TJsonString
                            {
                                Value = "unique_reactions"
                            },
                            new TJsonString
                            {
                                Value = "premium_stickers"
                            },
                            new TJsonString
                            {
                                Value = "animated_emoji"
                            },
                            new TJsonString
                            {
                                Value = "advanced_chat_management"
                            },
                            new TJsonString
                            {
                                Value = "profile_badge"
                            },
                            new TJsonString
                            {
                                Value = "animated_userpics"
                            },
                            new TJsonString
                            {
                                Value = "app_icons"
                            }
                        )
                    }
                },
                new TJsonObjectValue
                {
                    Key = "getfile_experimental_params",
                    Value = new TJsonBool
                    {
                        Value = false
                    }
                },
                new TJsonObjectValue
                {
                    Key = "fragment_prefixes",
                    Value = new TJsonArray
                    {
                        Value = new TVector<IJSONValue>(new TJsonString
                        {
                            Value = "888"
                        })
                    }
                }
            )
        };

        return Task.FromResult<IAppConfig>(new TAppConfig
        {
            Config = r
        });
    }
}