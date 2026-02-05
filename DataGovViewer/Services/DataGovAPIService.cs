using System.Text.Json;
using System.Text;
using DataGovViewer.Models;
using Microsoft.Extensions.Options;

namespace DataGovViewer.Services
{
    public class DataGovAPIService : IDataGovAPIService
    {
        private readonly DataGovConfig _dataGovConfig;

        private const string url = "https://data.gov.lv/dati/lv/api/action/datastore_search";
        private const int maxLimit = 32001; // so it returns all results. otherwise returns 100. number taken from API sql call;

        private readonly HttpClient _http;

        public DataGovAPIService(HttpClient http, IOptions<DataGovConfig> dataGovConfig)
        {
            _http = http;
            _dataGovConfig = dataGovConfig.Value;
        }

        public async Task<string> CallDataGovAPI(string type)
        {
            string resourceId = _dataGovConfig.DataGovTypes[type].ResourceId;

            var payload = new
            {
                resource_id = resourceId,
                limit = maxLimit
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            return responseJson;
        }
    }
}
