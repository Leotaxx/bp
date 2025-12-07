using Microsoft.Extensions.Logging;

namespace BPCalculator.Telemetry
{
    public interface IBPTelemetry
    {
        void TrackSubmission(BloodPressure bp);
    }

    public class BPTelemetry : IBPTelemetry
    {
        private readonly ILogger<BPTelemetry> _logger;

        public BPTelemetry(ILogger<BPTelemetry> logger)
        {
            _logger = logger;
        }

        public void TrackSubmission(BloodPressure bp)
        {
            _logger.LogInformation("BP submission: {Systolic}/{Diastolic} | Category={Category} | Pulse={Pulse} ({PulseCategory})", bp.Systolic, bp.Diastolic, bp.Category, bp.PulsePressure, bp.PulseCategory);
        }
    }
}
