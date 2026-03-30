using System;
using System.Text.Json;

namespace DAL004
{
    public class Repository : IRepository
    {
        public static string JSONFileName { get; set; } = "Celebrities.json";

        private Celebrity[] celebrities;

        public string jsonPath;

        public string BasePath { get; }

        public Repository(string basePath)
        {
            BasePath = basePath;

            string currentDir = AppDomain.CurrentDomain.BaseDirectory;

            jsonPath = Path.Combine(currentDir, BasePath, JSONFileName);

            string json = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            celebrities = JsonSerializer.Deserialize<Celebrity[]>(json, options)!;
        }

        public static IRepository Create(string path)
        {
            return new Repository(path);
        }

        public Celebrity[] getAllCelebrities()
        {
            return celebrities;
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

            return celeb?.PhotoPath;
        }

        // ДОБАВИТЬ ЗНАМЕНИТОСТЬ
        public int? addCelebrity(Celebrity celebrity)
        {
            if (celebrities.Any(c => c.Id == celebrity.Id))
                return null;

            celebrities = celebrities.Append(celebrity).ToArray();

            return celebrity.Id;
        }

        // УДАЛИТЬ ПО ID
        public bool delCelebrityById(int id)
        {
            var celeb = celebrities.FirstOrDefault(c => c.Id == id);

            if (celeb == null)
                return false;

            celebrities = celebrities.Where(c => c.Id != id).ToArray();

            SaveChanges();

            return true;
        }

        // ОБНОВИТЬ ПО ID
        public int? updCelebrityById(int id, Celebrity celebrity)
        {
            int index = Array.FindIndex(celebrities, c => c.Id == id);

            if (index == -1)
                return null;

            Celebrity updated = celebrity with { Id = id };

            celebrities[index] = updated;

            SaveChanges();

            return id;
        }

        // СОХРАНИТЬ В JSON
        public int SaveChanges()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(celebrities, options);

            File.WriteAllText(jsonPath, json);

            return celebrities.Length;
        }

        public void Dispose()
        {
        }
    }
}