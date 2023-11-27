using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grocery.Domain.Models.Account;
using Grocery.Domain.Models.Response;

namespace Grocery.Application.Services.interfaces;

public interface IAccountService
{
    Task<ResponseSuccessModel> Register(RegisterModel registerModel);
    Task<ResponseSuccessModel> Login(LoginModel loginModel);
    Task<ResponseSuccessModel> Confirm(ConfirmModel confirmModel);
    Task<ResponseSuccessModel> SendEmail(SendEmailModel emailModel);
}

