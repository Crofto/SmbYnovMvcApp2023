namespace SmbYnovMvcApp2023.Models
{
    public class DataToDisplay
    {
        public List<DateTimeOffSet> dateWater { get; set; } = new List<DateTimeOffSet>();
        public List<DateTimeOffSet> dateCond { get; set; } = new List<DateTimeOffSet>();
        public List<DateTimeOffSet> dateTempSoil { get; set; } = new List<DateTimeOffSet>();

        public List<Float> valueWater { get; set; } = new List<Float>();
        public List<Float> valueCond { get; set; } = new List<Float>();
        public List<Float> valueTempSoil { get; set; } = new List<Float>();
    }
}
