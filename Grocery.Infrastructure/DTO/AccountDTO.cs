using Grocery.Domain.Entity;
using Grocery.Domain.Models.Account;
using Grocery.Domain.Helper;

namespace Grocery.Infrastructure.DTO
{
    public static class AccountDTO
    {
        public static AccountEntity ConvertRegisterModel(this RegisterModel registerModel)
        {
            return new AccountEntity
            {
                Email = registerModel.Email,
                Name = registerModel.Name,
                Password = registerModel.Password,
                Phone = registerModel.Phone,
                Code = GenerateHelper.generateCode(),
                IsConfirm = false,
                Role= Role.user.ToString()
            };
        }
    }
}
