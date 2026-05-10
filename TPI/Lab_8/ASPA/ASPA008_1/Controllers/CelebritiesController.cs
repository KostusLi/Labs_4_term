using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ANC25_WEBAPI_DLL;
using AspNetCoreGeneratedDocument;
using ASPA008_1.Filters;

namespace ASPA008_1.Controllers
{
    public class CelebritiesController : Controller
    {
        private readonly IRepository _repo;
        private readonly CelebritiesConfig _config;

        public CelebritiesController(IRepository repo, IOptions<CelebritiesConfig> config)
        {
            _repo = repo;
            _config = config.Value;
        }

        public class IndexModel
        {
            public List<Celebrity> celebrities { get; set; }
            public string photosrequestpath { get; set; }
        }

        public IActionResult Index()
        {
            var model = new IndexModel
            {
                celebrities = _repo.GetAllCelebrities(),
                photosrequestpath = _config.PhotoRequestPath
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult NewHumanForm()
        {
            ViewBag.IsConfirmation = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewHumanForm(string fullname, string Nationality, IFormFile upload)
        {
            if (upload != null)
            {
                string tempname = "tmp" + Guid.NewGuid().ToString().Substring(0, 4) + ".tmp";
                string fullPath = Path.Combine(_config.PhotosFolder, tempname);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }

                ViewBag.IsConfirmation = true;
                ViewBag.FullName = fullname;
                ViewBag.FileName = tempname;
                ViewBag.OriginalName = upload.FileName;
            }

            return View();
        }

        [HttpPost]
        public IActionResult ConfirmHuman(string fullname, string Nationality, string filename)
        {
            if (string.IsNullOrEmpty(filename)) return RedirectToAction("Index");

            string oldPath = Path.Combine(_config.PhotosFolder, filename);
            string newFileName = fullname.Replace(" ", "") + ".jpg";
            string newPath = Path.Combine(_config.PhotosFolder, newFileName);

            if (System.IO.File.Exists(oldPath)) System.IO.File.Move(oldPath, newPath);

            Celebrity newCel = new Celebrity
            {
                FullName = fullname,
                Nationality = Nationality,
                ReqPhotoPath = newFileName
            };
            _repo.AddCelebrity(newCel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CancelHuman(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                string path = Path.Combine(_config.PhotosFolder, filename);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
            return RedirectToAction("Index");
        }

        [HttpGet][ASPA008_1.Filters.InfoAsyncActionFilter("WIKI")]
        public IActionResult Human(int id)
        {
            var celebrity = _repo.GetCelebrityById(id);
            if(celebrity==null) return NotFound();

            var lifeevent = _repo.GetLifeeventsByCelebrityId(id);

            ViewBag.LifeEvents = lifeevent;

            ViewBag.PhotoPath = _config.PhotoRequestPath;
            
            return View(celebrity);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var celebrity = _repo.GetCelebrityById(id);
            if (celebrity == null) return NotFound();

            ViewBag.PhotoPath = _config.PhotoRequestPath;
            return View(celebrity);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, string FullName, string Nationality, IFormFile upload, string oldFileName)
        {
            var celebrity = _repo.GetCelebrityById(id);
            if (celebrity == null) return NotFound();

            string newFileName = oldFileName;

            if (upload != null)
            {
                string oldFilePath = Path.Combine(_config.PhotosFolder, oldFileName);
                if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);

                newFileName = FullName.Replace(" ", "") + ".jpg";
                string newFilePath = Path.Combine(_config.PhotosFolder, newFileName);
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }
            }

            celebrity.FullName = FullName;
            celebrity.Nationality = Nationality;
            celebrity.ReqPhotoPath = newFileName;
            _repo.UpdCelebrity(id, celebrity);

            return RedirectToAction("Human", new { id = id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var celebrity = _repo.GetCelebrityById(id);
            if (celebrity == null) return NotFound();

            ViewBag.PhotoPath = _config.PhotoRequestPath;
            return View(celebrity);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id, string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string path = Path.Combine(_config.PhotosFolder, fileName);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }

            _repo.DelCelebrity(id);

            return RedirectToAction("Index");
        }


    }
}