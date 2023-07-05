
namespace SmbYnovMvcApp2023.Models
{
    public class TimeSeries: EntityBase
    {
        public UplinkMessage UplinkMessage { get; set; }

        public ulong UplinkMessageId { get; set; }

        public ulong DeviceId { get; set; }

        public string TimeSeriesName { get; set; }

        public DateTimeOffset DateTimeOffset { get; set; }

        public float Value { get; set; }
    }
}
