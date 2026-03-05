using Lab_3_1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_1
{
    public class Computer
    {
        [Required]
        public ComputerType Type { get; set; }

        [Required]
        public Processor Processor { get; set; }

        [Required]
        public VideoCard VideoCard { get; set; }

        [Range(1, 512)]
        public int RamGB { get; set; }

        [Range(128, 10000)]
        public int DiskGB { get; set; }

        [NotFutureDate]
        public DateTime PurchaseDate { get; set; }

        public decimal Price =>
            Processor.Price +
            VideoCard.Price +
            (RamGB * 10) +
            (DiskGB * 0.1m);

        public Computer(ComputerType type, Processor processor, VideoCard videoCard, int ramGB, int diskGB, DateTime purchaseDate)
        {
            Type = type;
            Processor = processor;
            VideoCard = videoCard;
            RamGB = ramGB;
            DiskGB = diskGB;
            PurchaseDate = purchaseDate;
        }

        public Computer() { }

        public override string ToString() => $"{Type} - \nПроцессор: {Processor}\n Видеокарта: {VideoCard} \n ОЗУ{RamGB} GB, {DiskGB} GB\n{PurchaseDate}";
    }
}
