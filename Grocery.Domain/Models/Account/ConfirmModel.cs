using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Domain.Models.Account
{
    public class ConfirmModel
    {
        [Required]
        [StringLength(6,ErrorMessage ="Code length is 6.")]
        public required string Code{ get; set; }
        [Required]
        [RegularExpression(pattern: "^09\\d{8}$", ErrorMessage = "Phone is invalid. You must to add 10 digit started with 09.")]
        public required string Phone { get; set; }
    }
}
