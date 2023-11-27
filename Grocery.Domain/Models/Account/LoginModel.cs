using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Domain.Models.Account
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(pattern: "^09\\d{8}$", ErrorMessage = "Phone is invalid. You must to add 10 digit started with 09.")]
        public required string Phone { get; set; }
        [Required]
        [RegularExpression(pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_])(?=.*\\d)[a-zA-Z\\d\\W_]{8,}$", ErrorMessage = "Password is invalid. You must to input at lest 8 character consists of capital and small character, digit and symbols.")]
        public required string Password { get; set; }
    }
}
