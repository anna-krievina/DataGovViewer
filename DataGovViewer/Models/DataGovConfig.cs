namespace DataGovViewer.Models
{
    public class DataGovConfig
    {
        public Dictionary<string, DataGovType> DataGovTypes { get; set; } = new();
    }

    public partial class DataGovType
    {
        public string? ResourceId { get; set; }
        public string? Description { get; set; }
    }
}
