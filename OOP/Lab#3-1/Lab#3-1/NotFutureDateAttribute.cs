using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_1
{
    public class NotFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date > DateTime.Now)
                    return new ValidationResult("Дата не может быть в будущем");
            }

            return ValidationResult.Success;
        }
    }
}
