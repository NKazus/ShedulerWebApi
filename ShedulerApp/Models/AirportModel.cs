using System.Text;

namespace ShedulerApp.Models
{
    public class AirportModel
    {
        public class Result
        {
            public int id { get; set; }
            public string icao { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public int elev { get; set; }
            public string continent { get; set; }
            public string country { get; set; }
            public string region { get; set; }
            public string municipality { get; set; }
            public bool hasScheduledService { get; set; }
            public string gps { get; set; }
            public string iata { get; set; }
            public object localCode { get; set; }
            public string homepage { get; set; }
            public string wikipedia { get; set; }
            public string keywords { get; set; }
        }

        public class Root
        {
            public int count { get; set; }
            public List<Result> results { get; set; }
            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                foreach (var result in results)
                {
                    builder.Append('\"');
                    builder.Append(result.id);
                    builder.Append("\",\"");
                    builder.Append(result.icao);
                    builder.Append("\",\"");
                    builder.Append(result.type);
                    builder.Append("\",\"");
                    builder.Append(result.name);
                    builder.Append("\",\"");
                    builder.Append(result.lat);
                    builder.Append("\",\"");
                    builder.Append(result.lon);
                    builder.Append("\",\"");
                    builder.Append(result.elev);
                    builder.Append("\",\"");
                    builder.Append(result.continent);
                    builder.Append("\",\"");
                    builder.Append(result.country);
                    builder.Append("\",\"");
                    builder.Append(result.region);
                    builder.Append("\",\"");
                    builder.Append(result.municipality);
                    builder.Append("\",\"");
                    builder.Append(result.hasScheduledService);
                    builder.Append("\",\"");
                    builder.Append(result.gps);
                    builder.Append("\",\"");
                    builder.Append(result.iata);
                    builder.Append("\",\"");
                    builder.Append(result.localCode);
                    builder.Append("\",\"");
                    builder.Append(result.homepage);
                    builder.Append("\",\"");
                    builder.Append(result.wikipedia);
                    builder.Append("\",\"");
                    builder.Append(result.keywords);
                    builder.Append("\"\n");
                }
                return builder.ToString();
            }
        }
    }
}
