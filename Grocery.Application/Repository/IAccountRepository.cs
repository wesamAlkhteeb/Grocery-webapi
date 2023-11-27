using Grocery.Domain.Models.Account;
using Grocery.Domain.Models.Response;

namespace Grocery.Application.Repository
{
    public interface IAccountRepository
    {
        Task<ResponseSuccessModel> Login(LoginModel loginModel);
        Task IsNumberUsed(string number);
        Task<ResponseSuccessModel> Register(RegisterModel registerModel);
        Task<ResponseSuccessModel> SendEmail(string Email);
        Task<ResponseSuccessModel> Confirm(ConfirmModel confirmModel);
    }
}
