using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ASPA008_1.Filters
{
    public class InfoAsyncActionFilter : Attribute, IAsyncActionFilter
    {
        public static readonly string Wikipedia = "WIKI";
        public static readonly string Facebook = "FACE";
        string infotype;
        public InfoAsyncActionFilter(string infotype = "")
        {
            this.infotype = infotype.ToUpper();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IRepository? repo = context.HttpContext.RequestServices.GetService<IRepository?>();

            int id = (int)(context.ActionArguments["id"] ?? -1);

            Celebrity? celebrity = repo?.GetCelebrityById(id);
            if (infotype.Contains(Wikipedia, StringComparison.OrdinalIgnoreCase) && celebrity != null)
            {
                context.HttpContext.Items.Add(Wikipedia, await WikiInfoCelebrity.GetReferences(celebrity.FullName));
            }

            if (infotype.Contains(Facebook) && celebrity != null)
            {
                context.HttpContext.Items.Add(Facebook, getFromFace(celebrity.FullName));
            }
            await next();
        }

        string getFromFace(string fullname)
        {
            return "Info from Face";
        }
    }

    public class WikiInfoCelebrity
    {
        HttpClient client;
        Dictionary<string, string> wikiReferens { get; set; }
        string wikiURI;

        private WikiInfoCelebrity(string fullName)
        {
            this.client = new HttpClient();

            this.client.DefaultRequestHeaders.Add("User-Agent", "ASPA008");

            this.wikiReferens = new Dictionary<string, string>();

            string safeName = Uri.EscapeDataString(fullName);
            this.wikiURI = $"https://en.wikipedia.org/w/api.php?action=opensearch&search={safeName}&prop=info&format=json";
        }

        public static async Task<Dictionary<string, string>> GetReferences(string fullname)
        {
            WikiInfoCelebrity info = new WikiInfoCelebrity(fullname);
            HttpResponseMessage message = await info.client.GetAsync(info.wikiURI);

            if (message.IsSuccessStatusCode)
            {
                string jsonResponse = await message.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    JsonElement root = doc.RootElement;

                    if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() == 4)
                    {
                        var titles = root[1].EnumerateArray().ToList();
                        var links = root[3].EnumerateArray().ToList();

                        for (int i = 0; i < titles.Count; i++)
                        {
                            string title = titles[i].GetString() ?? "";
                            string link = links[i].GetString() ?? "";

                            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(link))
                            {
                                info.wikiReferens.Add(title, link);
                            }
                        }
                    }
                }
            }

            return info.wikiReferens;
        }
    }
}
