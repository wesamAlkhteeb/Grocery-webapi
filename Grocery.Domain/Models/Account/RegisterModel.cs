using System.ComponentModel.DataAnnotations;

namespace Grocery.Domain.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression(pattern: "^(\\d|\\w)+(\\d|\\w|\\.|_|-)*@\\w+\\.\\w+$",ErrorMessage ="Email is invalid")]
        public required string Email { get; set; }
        [Required]
        [RegularExpression(pattern: "^(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_])(?=.*\\d)[a-zA-Z\\d\\W_]{8,}$",ErrorMessage = "Password is invalid. You must to input at lest 8 character consists of capital and small character, digit and symbols.")]
        public required string Password { get; set; }
        [Required]
        [RegularExpression(pattern: "^09\\d{8}$",ErrorMessage = "Phone is invalid. You must to add 10 digit started with 09.")]
        public required string Phone { get; set; }
        [Required]
        [MinLength(4,ErrorMessage = "Name length must be at least 4 character.")]
        public required string Name { get; set;}
    }
}
