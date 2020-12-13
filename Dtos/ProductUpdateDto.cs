using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productshop.Dtos
{
    //we can use this instead of patch if we want 
    //partialy update some properties
    public class ProductUpdateDto
    {
        [Required(ErrorMessage = "Name is mandatory field!")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[async-zA-Z]+.*", ErrorMessage = "Only numerics are not allowed")]
        public string Name { get; set; }
    }
}
