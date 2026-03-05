using Lab_3_1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_1
{
    public class Processor
    {
        [Required(ErrorMessage = "Производитель обязателен")]
        public Manufacturer Manufacturer { get; set; }

        [Required]
        public string Series { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(1, 128, ErrorMessage = "Ядра 1-128")]
        public int Cores { get; set; }

        [Range(1.0, 10.0)]
        public double Frequency { get; set; }

        public decimal Price =>
            100 + (Cores * 20) + ((decimal)Frequency * 50);

        public override string ToString()
        {
            return $"{Manufacturer} {Series}-{Model}, {Cores} ядер, {Frequency} GHz";
        }

        public Processor(Manufacturer manufacturer, string series, string model, int cores, double frequency)
        {
            Manufacturer = manufacturer;
            Series = series;
            Model = model;
            Cores = cores;
            Frequency = frequency;
        }

        public Processor() { }
    }
}
