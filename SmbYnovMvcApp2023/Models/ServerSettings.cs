namespace SmbYnovMvcApp2023.Models
{
    public class ServerSettings : EntityBase
    {
        public string Url { get; set; }
        public DateTimeOffset? LastConnected { get; set; }
    }
}
