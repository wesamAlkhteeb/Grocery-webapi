
using Grocery.Infrastructure.DatabaseContext;
using MongoDB.Driver;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Grocery.Domain.Helper;
using Grocery.Infrastructure.Repository;
using Grocery.Application.Services;
using Grocery.Domain.Models.Account;
using Microsoft.AspNetCore.Http;
using Grocery.Domain.Models.Response;
namespace Grocery.test.Services
{
    public class AccountTest
    {
        private AccountService GetAccountRepository()
        {
            
            IMongoDatabase dbFake = (new MongoClient("mongodb://localhost:6066")).GetDatabase(Guid.NewGuid().ToString());
            DbContext dbContext = new DbContext(dbFake);
            IOptions<JwtHelper> jwtFake = A.Fake<IOptions<JwtHelper>>();
            AccountRepository accountRepository =new AccountRepository(dbContext, jwtFake );
            AccountService accountService = new AccountService( accountRepository);
            return accountService;
        }

        [Fact]
        public async Task Register_WhenDataIsValidation_ReturnSuccessResult() {
            //arrange
            AccountService accountController= GetAccountRepository();
            //act
            var result = await accountController.Register(new RegisterModel
            {
                Phone = "0999999999",
                Password = "Wesam@204",
                Email="Wesam.alkhteeb98@gmail.com",
                Name="Wesam"
            });
            
            //assert
            Assert.IsType<ResponseSuccessModel>(result);
        }

        [Fact]
        public async Task Register_WhenDataIsValidationAndUseNumberExists_ThrowExceptionForScondRegister()
        {
            //arrange
            AccountService accountController = GetAccountRepository();
            //act
            var result1 = await accountController.Register(new RegisterModel
            {
                Phone = "0999999999",
                Password = "Wesam@204",
                Email = "Wesam.alkhteeb98@gmail.com",
                Name = "Wesam"
            });

            //assert
            Assert.IsType<ResponseSuccessModel>(result1);
            await Assert.ThrowsAsync<BadHttpRequestException>(async() => await accountController.Register(new RegisterModel
            {
                Phone = "0999999999",
                Password = "Wesam@204",
                Email = "Wesam.alkhteeb98@gmail.com",
                Name = "Wesam"
            }));
        }

        [Fact]
        public async Task Login_CheckIfCanLogin_ExceptionBecauseNotConfirm()
        {
            //arrange
            AccountService accountController = GetAccountRepository();
            //act
            var result1 = await accountController.Register(new RegisterModel
            {
                Phone = "0999999999",
                Password = "Wesam@204",
                Email = "Wesam.alkhteeb98@gmail.com",
                Name = "Wesam"
            });

            //assert
            await Assert.ThrowsAsync<BadHttpRequestException>(async () => await accountController.Login(new LoginModel
            {
                Phone = "0999999999",
                Password = "Wesam@204"
            }));
        }
    }
}
