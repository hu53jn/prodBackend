using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productshop.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Name is mandatory field!")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[async-zA-Z]+.*", ErrorMessage = "Only numerics are not allowed")]
        public string Name { get; set; }

        [Range(1, 1000, ErrorMessage = "Price must be between 1BAM and 1000BAM")]
        public int Price { get; set; }
    }
}
