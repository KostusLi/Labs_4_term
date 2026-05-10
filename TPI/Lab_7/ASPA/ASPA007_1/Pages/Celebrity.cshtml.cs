using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPA007_1.Pages
{
    public class CelebrityModel : PageModel
    {
        public Celebrity? currentCelebrity { get; set; } = new Celebrity();
        static string costring = @"Server=(localdb)\mssqllocaldb;Database=LES01;Trusted_Connection=True;TrustServerCertificate=True;";
        Repository repository = new Repository(costring);
        public void OnGet(int id)
        {
            currentCelebrity = repository.GetCelebrityById(id);
        }
    }
}
