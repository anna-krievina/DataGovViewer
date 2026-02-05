namespace DataGovViewer.Models
{
    public class DataGovModel
    {
        public DataGovModel()
        {
            DataList = new();
            Columns = new();
        }

        public List<Dictionary<string, string?>> DataList { get; set; }
        public List<string> Columns { get; set; }
        public string Description { get; set; }
        public string ErrorMessage { get; set; }
        public string Type { get; set; }
    }
}
