using Lab_3_1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_1
{
    public class VideoCard
    {
        [Required]
        public Manufacturer Manufacturer { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(1, 48)]
        public int MemoryGB { get; set; }

        public decimal Price =>
            150 + (MemoryGB * 30);

        public override string ToString()
        {
            return $"{Manufacturer} {Model}, {MemoryGB} GB";
        }

        public VideoCard(Manufacturer manufacturer, string model, int memoryGB)
        {
            Manufacturer = manufacturer;
            Model = model;
            MemoryGB = memoryGB;
        }
        public VideoCard() { }

    }
}
