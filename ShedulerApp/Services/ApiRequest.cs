using Newtonsoft.Json;
using ShedulerApp.Models;
using System.Text;

namespace ShedulerApp.Services
{
    public class ApiRequest : IApiRequest
    {
        private int _type;
        private string _parameter;

        private string _host;
        private string _key = "7b46088685mshd38e1b072a26c68p1a4f39jsn515215f4c63c";
        private string _requestUri;

        private string Response;

        public async Task ExecuteRequest(int type, string parameter)
        {
            _type = type;
            _parameter = parameter;
            InitializeRequestParameters();
            Console.WriteLine(_requestUri);
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_requestUri),
                Headers =
                {
                    { "X-RapidAPI-Key", _key },
                    { "X-RapidAPI-Host", _host },
                },
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    switch (_type)
                    {
                        case 2: var currencyList = JsonConvert.DeserializeObject<CurrencyModel.Root>(body);
                                Response = currencyList.ToString();
                                break;
                        case 3: var airportList = JsonConvert.DeserializeObject<AirportModel.Root>(body);
                                Response = airportList.ToString();
                                break;
                        default: Response = body; break;
                    }
                    
                }
            }
            catch { }
            
        }

        public string GetResponse()
        {
            return Response;
        }

        private void InitializeRequestParameters()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("https://");
            switch (_type)
            {
                //weather
                case 1: _host = "visual-crossing-weather.p.rapidapi.com";                    
                    builder.Append("visual-crossing-weather.p.rapidapi.com/forecast?aggregateHours=24&location=");
                    builder.Append(_parameter);
                    builder.Append("&contentType=csv&unitGroup=us&shortColumnNames=0");                    
                    break;
                //currency
                case 2: _host = "apidojo-booking-v1.p.rapidapi.com";                    
                    builder.Append("apidojo-booking-v1.p.rapidapi.com/currency/get-exchange-rates?base_currency=");
                    builder.Append(_parameter);
                    builder.Append("&languagecode=en-us");
                    break;
                //airports
                case 3: _host = "ourairport-data-search.p.rapidapi.com";
                    builder.Append("ourairport-data-search.p.rapidapi.com/api/airport/iata/");
                    builder.Append(_parameter);        
                    break;
            }
            _requestUri = builder.ToString();
        }
    }
}
