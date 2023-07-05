namespace SmbYnovMvcApp2023.Models
{
    public class DataToDisplay
    {
        public List<Device> devices { get; set; } = new List<Device>();
        public List<TimeSeries> waters { get; set; } = new List<TimeSeries>();
        public List<TimeSeries> conds { get; set; } = new List<TimeSeries>();
        public List<TimeSeries> soilTemps { get; set; } = new List<TimeSeries>();
    }
}
