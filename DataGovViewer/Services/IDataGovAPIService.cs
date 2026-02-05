namespace DataGovViewer.Services
{
    public interface IDataGovAPIService
    {
        public Task<string> CallDataGovAPI(string type);
    }
}
