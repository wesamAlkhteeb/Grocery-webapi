using Grocery.Application.Repository;
using Grocery.Application.Services.interfaces;
using Grocery.Domain.Models.Account;
using Grocery.Domain.Models.Response;

namespace Grocery.Application.Services
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<ResponseSuccessModel> Login(LoginModel loginModel)
        {
            return await accountRepository.Login(loginModel);
        }

        public async Task<ResponseSuccessModel> Register(RegisterModel registerModel)
        {
            await accountRepository.IsNumberUsed(registerModel.Phone);
            return await accountRepository.Register(registerModel);
            
        }

        public async Task<ResponseSuccessModel> SendEmail(SendEmailModel emailModel)
        {
            return await accountRepository.SendEmail(emailModel.Email);
        }
        public async Task<ResponseSuccessModel> Confirm(ConfirmModel confirmModel)
        {
            return await accountRepository.Confirm(confirmModel);
        }
    }
}
