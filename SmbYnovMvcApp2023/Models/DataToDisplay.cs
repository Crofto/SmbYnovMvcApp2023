namespace SmbYnovMvcApp2023.Models
{
    public class DataToDisplay
    {
        public List<DateTimeOffset> dateWater { get; set; } = new List<DateTimeOffset>();
        public List<DateTimeOffset> dateCond { get; set; } = new List<DateTimeOffset>();
        public List<DateTimeOffset> dateTempSoil { get; set; } = new List<DateTimeOffset>();

        public List<float> valueWater { get; set; } = new List<float>();
        public List<float> valueCond { get; set; } = new List<float>();
        public List<float> valueTempSoil { get; set; } = new List<float>();
    }
}
