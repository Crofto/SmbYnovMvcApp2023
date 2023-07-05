using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace SmbYnovMvcApp2023.Models
{
    public class Device : EntityBase
    {
        [MaxLength(100)]
        public string FonctionalId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset? LastConnected { get; set; }

        public int DeviceType { get; set; }

        public int ConnectivityType { get; set; }
    }
}
