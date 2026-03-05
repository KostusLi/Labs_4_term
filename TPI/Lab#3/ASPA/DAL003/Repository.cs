using System.Text.Json;

namespace DAL003
{
    public class Repository : IRepository
    {
        public static string JSONFileName { get; set; } = "Celebrities.json";

        private Celebrity[] celebrities;

        public string BasePath { get; }

        public Repository(string basePath)
        {
            BasePath = basePath;

            string currentDir = AppDomain.CurrentDomain.BaseDirectory;

            string jsonPath = Path.Combine(currentDir, BasePath, JSONFileName);

            string json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            celebrities = JsonSerializer.Deserialize<Celebrity[]>(json, options)!;
        }

        public Celebrity[] getAllCelebrities()
        {
            return celebrities;
        }

        public static IRepository Create(string path)
        {
            return new Repository(path);
        }

        public Celebrity? getCelebrityById(int id)
        {
            return celebrities.FirstOrDefault(c => c.Id == id);
        }

        public Celebrity[] getCelebritiesBySurname(string surname)
        {
            return celebrities
                .Where(c => c.Surname.ToLower() == surname.ToLower())
                .ToArray();
        }

        public string? getPhotoPathById(int id)
        {
            var celeb = celebrities.FirstOrDefault(c => c.Id == id);

            if (celeb == null)
                return null;

            return celeb.PhotoPath;
        }

        public void Dispose()
        {
        }
    }
}