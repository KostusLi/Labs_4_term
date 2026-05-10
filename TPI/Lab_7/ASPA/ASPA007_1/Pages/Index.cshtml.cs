using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPA007_1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Celebrity> Celebrities { get; set; } = new List<Celebrity>();

        static string costring = @"Server=(localdb)\mssqllocaldb;Database=LES01;Trusted_Connection=True;TrustServerCertificate=True;";
        Repository repository = new Repository(costring);

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Celebrities = repository.GetAllCelebrities();
        }
    }
}
