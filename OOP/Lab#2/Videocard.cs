using System;

namespace Lab_2
{
    // Перечисления для Видеокарты

    public enum GpuManufacturer
    {
        Nvidia = 1,
        AMD = 2,
        Intel = 3
    }

    public enum GpuSeries
    {
        Office = 1,      // Офисная (затычка)
        GamingEntry = 2, // Бюджетный гейминг
        GamingMid = 3,   // Средний сегмент
        GamingHigh = 4,  // Топовый сегмент
        Professional = 5 // Для рабочих станций
    }

    public enum GpuFrequency
    {
        Stock = 1,       // Базовая частота
        Overclocked = 2, // Заводской разгон
        Extreme = 3      // Экстремальный разгон
    }

    public enum VramAmount
    {
        Gb4 = 1,
        Gb8 = 2,
        Gb12 = 3,
        Gb16 = 4,
        Gb20 = 5,
        Gb24 = 6
    }

    public class VideoCard
    {
        public GpuManufacturer Manufacturer { get; set; }
        public GpuSeries Series { get; set; }

        public string Model { get; set; }

        public GpuFrequency Frequency { get; set; }
        public VramAmount Vram { get; set; }

        public VideoCard() { }

        public VideoCard(
            GpuManufacturer manufacturer,
            GpuSeries series,
            string model,
            GpuFrequency frequency,
            VramAmount vram)
        {
            Manufacturer = manufacturer;
            Series = series;
            Model = model;
            Frequency = frequency;
            Vram = vram;
        }

        public static decimal CalculatePrice(VideoCard gpu)
        {
            if (gpu == null) return 0;

            decimal price = 0;

            price += 3000 * (int)gpu.Manufacturer;
            price += 15000 * (int)gpu.Series;
            price += 2000 * (int)gpu.Frequency;
            price += 4000 * (int)gpu.Vram;

            if (!string.IsNullOrEmpty(gpu.Model))
            {
                price += 1000;
            }

            return price;
        }

        public override string ToString()
        {
            return $"{Manufacturer} {Model} ({Vram} VRAM)";
        }
    }
}