using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Options;
using System.IO;

namespace ASPA007_1.Pages
{
    public class NewCelebrityModel : PageModel
    {
        private readonly CelebritiesConfig _config;
        private readonly IRepository _repo;

        public NewCelebrityModel(IOptions<CelebritiesConfig> config, IRepository repo)
        {
            _config = config.Value;
            _repo = repo;
        }

        [BindProperty(SupportsGet = true)]public string? Nationality {  get; set; }
        [BindProperty(SupportsGet = true)] public string? FileName { get; set; }
        [BindProperty(SupportsGet = true)] public string? FullName { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostUploadAsync(string FullName, IFormFile UploadedFile)
        {
            if (UploadedFile != null && !string.IsNullOrEmpty(FullName))
            {
                string folder = _config.PhotosFolder;
                string tempname = "tmp" + Guid.NewGuid().ToString().Substring(0, 4) + ".tmp";
                string fullPath = Path.Combine(folder, tempname);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await UploadedFile.CopyToAsync(stream);
                }

                return RedirectToPage(new { FileName = tempname, FullName = FullName, Nationality = Nationality });
            }
            return Page();
        }

        public IActionResult OnPostConfirm()
        {
            if(string.IsNullOrEmpty(FileName)) return RedirectToPage("/Index");

            string folder = _config.PhotosFolder;
            string oldPath = Path.Combine(folder, FileName);

            string newFileName = FullName?.Replace(" ", "") + ".jpg";
            string newPath = Path.Combine(folder, newFileName);

            if(System.IO.File.Exists(oldPath))
            {
                System.IO.File.Move(oldPath, newPath);
            }

            Celebrity newCelebrity = new Celebrity
            {
                FullName = FullName ?? "Anonymus",
                Nationality = Nationality ?? "Moon",
                ReqPhotoPath = newFileName
            };

            _repo.AddCelebrity(newCelebrity);

            return RedirectToPage("/Index");
        }

        public IActionResult OnPostCancel()
        {
            if(!string.IsNullOrEmpty(FileName))
            {
                string path = Path.Combine(_config.PhotosFolder, FileName);
                if(System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
            return RedirectToPage("/Index");
        }
    }
}
