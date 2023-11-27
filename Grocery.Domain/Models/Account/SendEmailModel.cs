using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Domain.Models.Account
{
    public class SendEmailModel
    {
        [Required]
        [RegularExpression(pattern: "^(\\d|\\w)+(\\d|\\w|\\.|_|-)*@\\w+\\.\\w+$", ErrorMessage = "Email is invalid")]
        public required string Email { get; set; }
    }
}
