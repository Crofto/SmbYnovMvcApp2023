using System;

namespace SmbYnovMvcApp2023.Models
{
    public class UplinkMessage : EntityBase
    {
        public Device Device { get; set; }
        public string DeviceId { get; set; }
        public DateTimeOffset RecieveIdAt { get; set; }
        public string RawPayLoad { get; set; }
        public int TypeFrequency { get; set; }
        public int ConnectionConfig { get; set; }
        public int ConnectionFrequency { get; set; }
    }
}
