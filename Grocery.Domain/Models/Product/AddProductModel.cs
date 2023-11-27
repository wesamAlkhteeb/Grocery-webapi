using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Domain.Models.Product
{
    public class AddProductModel
    {
        [Required(ErrorMessage ="The name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "The description is required.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "The price is required.")]
        [Range(1,int.MaxValue,ErrorMessage ="The price must be more than 0")]
        public required decimal price { get; set; }
    }
}
