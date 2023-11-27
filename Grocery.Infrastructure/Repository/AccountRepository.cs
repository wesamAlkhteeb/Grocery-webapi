using Grocery.Application.Repository;
using Grocery.Domain.Entity;
using Grocery.Domain.Helper;
using Grocery.Domain.Models.Account;
using Grocery.Domain.Models.Response;
using Grocery.Infrastructure.DatabaseContext;
using Grocery.Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Grocery.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public readonly DbContext dbContext;
        public readonly JwtHelper jwtHelper;
        public AccountRepository(DbContext dbContext, IOptions<JwtHelper> jwtHelper) { 
            this.dbContext = dbContext;
            this.jwtHelper = jwtHelper.Value;
        }

        public async Task<ResponseSuccessModel> Confirm(ConfirmModel confirmModel)
        {
            FilterDefinition<AccountEntity> filter = Builders<AccountEntity>.Filter.Eq(account => account.Phone, confirmModel.Phone);
            UpdateDefinition<AccountEntity> updateAccount =
                    Builders<AccountEntity>.Update.Set(account => account.IsConfirm, true);
            await dbContext.accountCollection.UpdateOneAsync(filter, updateAccount);
            return new ResponseSuccessModel
            {
                data = "Account has Confirmed."
            };
        }

        public async Task IsNumberUsed(string number)
        {
            FilterDefinition<AccountEntity> filter = Builders<AccountEntity>.Filter.Eq(account=>account.Phone, number);
            AccountEntity account =  await this.dbContext.accountCollection.Find(filter).FirstOrDefaultAsync();
            if(account != null)
            {
                throw new BadHttpRequestException("This phone has used from other user.");
            }
        }

        private async Task<AccountEntity> GetAccoutByPhone(string phone)
        {
            FilterDefinition<AccountEntity> filter = Builders<AccountEntity>.Filter.Eq(account => account.Phone, phone);
            AccountEntity account = await this.dbContext.accountCollection.Find(filter).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new BadHttpRequestException("Account is not found.");
            }
            return account;
        }

        public async Task<ResponseSuccessModel> Login(LoginModel loginModel)
        {
            AccountEntity account = await GetAccoutByPhone(loginModel.Phone);
        
            if (!account.IsConfirm)
            {
                throw new BadHttpRequestException("Account is not confirm");
            }

            Dictionary<string, object> response = JwtSecurity.securityData.GenerateToken(account.Name , account.Id , account.Role,jwtHelper);

            return new ResponseSuccessModel
            {
                data = response
            };
        }

        public async Task<ResponseSuccessModel> Register(RegisterModel registerModel)
        {
            AccountEntity accountEntity = registerModel.ConvertRegisterModel();
            await dbContext.accountCollection.InsertOneAsync(accountEntity);
            await Task.Run(() => EmailService.emailService.sendMessage(email: registerModel.Email, accountEntity.Code));
            return new ResponseSuccessModel
            {
                data = "Register Succesfully"
            };
        }

        public async Task<ResponseSuccessModel> SendEmail(string Email)
        {
            FilterDefinition<AccountEntity> filter = Builders<AccountEntity>.Filter.Eq(account => account.Email, Email);
            AccountEntity account = await this.dbContext.accountCollection.Find(filter).FirstOrDefaultAsync();
            if (account == null)
            {
                throw new BadHttpRequestException("Account is not found.");
            }
            EmailService.emailService.sendMessage(email: account.Email, account.Code);
            return new ResponseSuccessModel
            {
                data = "Code has sended to email."
            };
        }

    }
}