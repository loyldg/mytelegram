using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class GetCallConfigHandler : RpcResultObjectHandler<RequestGetCallConfig, IDataJSON>,
    IGetCallConfigHandler, IProcessedHandler
{
    protected override Task<IDataJSON> HandleCoreAsync(IRequestInput input,
        RequestGetCallConfig obj)
    {
        IDataJSON r = new TDataJSON {
            Data =
                "{\"audio_frame_size\":60,\"jitter_min_delay_60\":2,\"jitter_max_delay_60\":10,\"jitter_max_slots_60\":20,\"jitter_losses_to_reset\":20,\"jitter_resync_threshold\":0.5,\"audio_congestion_window\":1024,\"audio_max_bitrate\":20000,\"audio_max_bitrate_edge\":16000,\"audio_max_bitrate_gprs\":8000,\"audio_max_bitrate_saving\":8000,\"audio_init_bitrate\":16000,\"audio_init_bitrate_edge\":8000,\"audio_init_bitrate_gprs\":8000,\"audio_init_bitrate_saving\":8000,\"audio_bitrate_step_incr\":1000,\"audio_bitrate_step_decr\":1000,\"use_system_ns\":true,\"use_system_aec\":true,\"force_tcp\":false,\"jitter_initial_delay_60\":2,\"adsp_good_impls\":\"(Qualcomm Fluence)\",\"bad_call_rating\":true,\"use_ios_vpio_agc\":false,\"use_tcp\":false,\"audio_medium_fec_bitrate\":20000,\"audio_medium_fec_multiplier\":0.1,\"audio_strong_fec_bitrate\":7000}"
        };

        return Task.FromResult(r);
    }
}
