using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ANC25_WEBAPI_DLL
{

    public class CountryItem
    {
        public string code { get; set; }
        public string countryLabel { get; set; }
    }

    public class CountryCodes
    {

        public List<CountryItem> Codes { get; private set; } = new List<CountryItem>();

        public CountryCodes(IOptions<CelebritiesConfig> config)
        {
            string path = config.Value.ISO3166alpha2Path;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                var data = JsonSerializer.Deserialize<List<CountryItem>>(json);
                if (data != null) Codes = data;
            }
        }
    }
}