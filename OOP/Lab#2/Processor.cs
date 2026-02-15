using System;

namespace Lab_2
{
    public enum Manufacturer
    {
        AMD = 1,
        Intel = 2
    }

    public enum CpuSeries
    {
        Entry = 1,
        Middle = 2,
        High = 3,
        Enthusiast = 4,
        Server = 5
    }

    public enum CountOfCores
    {
        Cores4 = 1,
        Cores6 = 2,
        Cores8 = 3,
        Cores12 = 4,
        Cores16 = 5
    }

    public enum CpuFrequency
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Extreme = 4
    }

    public enum MaxFrequency
    {
        UpTo4 = 1,
        UpTo5 = 2,
        Over5 = 3
    }

    public enum ArchitectureBitness
    {
        x86 = 1,
        x64 = 2
    }

    public enum CacheL1
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public enum CacheL2
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public enum CacheL3
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public class Processor
    {
        public Manufacturer Manufacturer { get; set; }
        public CpuSeries Series { get; set; }
        public string Model { get; set; }

        public CountOfCores Cores { get; set; }
        public CpuFrequency Frequency { get; set; }
        public MaxFrequency MaxFrequency { get; set; }
        public ArchitectureBitness Bitness { get; set; }
        public CacheL1 CacheL1 { get; set; }
        public CacheL2 CacheL2 { get; set; }
        public CacheL3 CacheL3 { get; set; }

        public Processor() { }

        public Processor(
            Manufacturer manufacturer,
            CpuSeries series,
            string model,
            CountOfCores cores,
            CpuFrequency frequency,
            MaxFrequency maxFrequency,
            ArchitectureBitness bitness,
            CacheL1 cacheL1,
            CacheL2 cacheL2,
            CacheL3 cacheL3)
        {
            Manufacturer = manufacturer;
            Series = series;
            Model = model;
            Cores = cores;
            Frequency = frequency;
            MaxFrequency = maxFrequency;
            Bitness = bitness;
            CacheL1 = cacheL1;
            CacheL2 = cacheL2;
            CacheL3 = cacheL3;
        }

        public static decimal CalculatePrice(Processor cpu)
        {
            if (cpu == null)
                throw new ArgumentNullException(nameof(cpu));

            decimal price = 0;

            price += 5000 * (int)cpu.Manufacturer;
            price += 7000 * (int)cpu.Series;
            price += 6000 * (int)cpu.Cores;
            price += 4000 * (int)cpu.Frequency;
            price += 3000 * (int)cpu.MaxFrequency;
            price += 2000 * (int)cpu.Bitness;
            price += 1000 * (int)cpu.CacheL1;
            price += 2000 * (int)cpu.CacheL2;
            price += 3000 * (int)cpu.CacheL3;

            if (!string.IsNullOrEmpty(cpu.Model)) price += 500;

            return price;
        }

        public override string ToString()
        {
            return $"{Manufacturer} {Series} {Model} ({Cores} cores)";
        }
    }
}