using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ShopApp.Model
{
    public class FileService
    {
        private const string PATH = "products.json";

        public void Save(IEnumerable<Product> products)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };

                string json = JsonSerializer.Serialize(products, options);

                File.WriteAllText(PATH, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }

        public List<Product> Load()
        {
            try
            {
                if (!File.Exists(PATH))
                {
                    return new List<Product>();
                }

                string json = File.ReadAllText(PATH);

                var products = JsonSerializer.Deserialize<List<Product>>(json);

                return products ?? new List<Product>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
                return new List<Product>();
            }
        }

    }
}
