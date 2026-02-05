using DataGovViewer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using DataGovViewer.Services;
using Microsoft.Extensions.Options;
using System.Text;

namespace DataGovViewer.Controllers
{
    public class DataGovController : Controller
    {
        private readonly ILogger<DataGovController> _logger;
        private readonly IDataGovAPIService _dataGovAPI;
        private readonly DataGovConfig _dataGovConfig;

        public DataGovController(ILogger<DataGovController> logger, IDataGovAPIService dataGovAPI, IOptions<DataGovConfig> dataGovConfig)
        {
            _logger = logger;
            _dataGovAPI = dataGovAPI;
            _dataGovConfig = dataGovConfig.Value;
        }

        public IActionResult Index(string type)
        {
            DataGovModel model = CallApiAndFillData(type);

            return View(model);
        }

        public IActionResult ExportCSV(string type)
        {
            DataGovModel model = CallApiAndFillData(type);
            if (model.ErrorMessage != null)
            {
                return null;
            }
            var sb = new StringBuilder();

            // headers (from first dictionary)
            var headers = model.DataList.First().Keys;
            sb.AppendLine(string.Join(",", headers));

            // rows
            foreach (var dict in model.DataList)
            {
                sb.AppendLine(string.Join(",", headers.Select(h => dict[h])));
            }
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(
                bytes,
                "text/csv",
                "export.csv"
            );
        }

        private DataGovModel CallApiAndFillData(string type)
        {
            DataGovModel model = new DataGovModel();
            model.Type = type;
            try
            {
                Task<string> returnJsonTask = null;
                model.Description = _dataGovConfig.DataGovTypes[type].Description;
                returnJsonTask = _dataGovAPI.CallDataGovAPI(type);
                var json = JObject.Parse(returnJsonTask?.Result);

                if (json != null && (bool?)json["success"] == true)
                {
                    model.Columns = json["result"]?["fields"]?.Select(f => f["id"]?.ToString()).ToList();
                    model.DataList = json["result"]?["records"]?.ToObject<List<Dictionary<string, string?>>>();
                }
                else
                {
                    model.ErrorMessage = "Notikusi kļūda iegūst datus!";
                    _logger.LogError("Error while fetching data from data.gov.lv API: " + json?["error"]?["info"]?["orig"]?[0]);
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = "Notikusi kļūda iegūst datus no API!";
                _logger.LogError(ex, "Error while fetching data from data.gov.lv API");
            }

            return model;
        }
    }
}
