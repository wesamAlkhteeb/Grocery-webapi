using Grocery.API.CustomActionFilters;
using Grocery.Application.Services.interfaces;
using Grocery.Domain.Models.Account;
using Grocery.Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Grocery.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : Controller
    {
        public readonly IAccountService accountService;
        public AccountController(IAccountService accountService) { 
            this.accountService = accountService;  
        }

        [LogRequest]
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(
                new List<string>
                {
                    "Hi",
                    "Hellow",
                    "How're yoe?"
                }
                );
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
        {
            return Ok( await accountService.Register(registerModel:registerModel));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            return Ok(await accountService.Login(loginModel:loginModel));
        }
        
        [HttpPost("Confirm")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmModel confirmModel)
        {
            return Ok(await accountService.Confirm(confirmModel:confirmModel));
            
        }

        [HttpPost("Resend-code")]
        public async Task<IActionResult> SendEmail([FromBody]SendEmailModel sendEmailModel)
        {
            return Ok(await accountService.SendEmail(emailModel: sendEmailModel));
        }

    }
}
