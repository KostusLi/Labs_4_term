using System;

namespace Lab_2
{
    // --- Перечисления для Компьютера ---

    public enum ComputerType
    {
        Server = 1,         // Сервер
        Workstation = 2,    // Рабочая станция
        Desktop = 3,        // Домашний ПК
        Laptop = 4          // Ноутбук
    }

    public enum RamType
    {
        DDR3 = 1,
        DDR4 = 2,
        DDR5 = 3
    }

    public enum DiskType
    {
        HDD = 1,    // Жесткий диск
        SSD = 2,    // SATA SSD
        NVMe = 3    // M.2 NVMe SSD
    }

    // --- Класс Компьютер ---
    public class Computer
    {
        public ComputerType Type { get; set; }

        public Processor Cpu { get; set; }
        public VideoCard Gpu { get; set; }

        public int RamSize { get; set; }
        public RamType RamType { get; set; }

        public int DiskSize { get; set; }
        public DiskType DiskType { get; set; }

        public DateTime PurchaseDate { get; set; }

        public Computer()
        {
            PurchaseDate = DateTime.Now;
        }

        public Computer(
            ComputerType type,
            Processor cpu,
            VideoCard gpu,
            int ramSize,
            RamType ramType,
            int diskSize,
            DiskType diskType,
            DateTime purchaseDate)
        {
            Type = type;
            Cpu = cpu;
            Gpu = gpu;
            RamSize = ramSize;
            RamType = ramType;
            DiskSize = diskSize;
            DiskType = diskType;
            PurchaseDate = purchaseDate;
        }

        public static decimal CalculateTotalPrice(Computer comp)
        {
            if (comp == null) return 0;

            decimal total = 0;

            total += (int)comp.Type * 5000;

            total += (int)comp.RamType * comp.RamSize * 100;

            total += (int)comp.DiskType * comp.DiskSize * 5;

            if (comp.Cpu != null)
                total += Processor.CalculatePrice(comp.Cpu);

            if (comp.Gpu != null)
                total += VideoCard.CalculatePrice(comp.Gpu);

            return total;
        }

        public override string ToString()
        {
            return $"{Type} | RAM: {RamSize}GB | Disk: {DiskSize}GB | Total: {CalculateTotalPrice(this):C}";
        }
    }
}