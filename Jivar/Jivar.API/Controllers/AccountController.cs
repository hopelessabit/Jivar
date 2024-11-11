using Jivar.BO.Models;
using Jivar.Service.Constant;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Account.Request;
using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.PayLoads;
using Jivar.Service.Util;
using Microsoft.AspNetCore.Mvc;

namespace Jivar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountSerivce;

        public AccountController(IAuthService authService, IAccountService accountSerivce)
        {
            _authService = authService;
            _accountSerivce = accountSerivce;
        }


        [HttpPost(APIEndPointConstant.Authentication.Login)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]

        public async Task<ActionResult<string>> Login([FromForm] LoginRequest request)
        {
            var (account, actorId) = await _authService.Authenticate(request.Email, request.Password);
            if (account == null)
            {
                return Unauthorized(new Jivar.Service.PayLoads.ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = MessageConstant.LoginMessage.InvalidUsernameOrPassword,
                    TimeStamp = DateTime.Now
                });
            }

            var token = await _authService.GenerateJwtToken(account, actorId);
            var loginResponse = new LoginResponse
            {
                Token = token,
                actorId = actorId ?? 0,
                Email = account.Email,
                RoleName = account.Role
            };
            return Ok(loginResponse);
        }



        [HttpPost(APIEndPointConstant.Authentication.CreateAccount)]
        [ProducesResponseType(typeof(CreateNewAccountResponse), StatusCodes.Status201Created)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<ActionResult> CreateNewAccount([FromForm] CreateNewAccountRequest AccountRequest)
        {
            var roleName = UserUtil.GetRoleName(HttpContext);
            var isEmailExist = await _accountSerivce.IsEmailExist(AccountRequest.Email);
            if (isEmailExist)
            {
                return Problem(MessageConstant.AccountMessage.EmailExist);
            }
            //var url = await _firebaseService.UploadFile(AccountRequest.AvatarImage);

            var account = new Account()
            {
                Email = AccountRequest.Email,
                Password = PasswordUtil.HashPassword(AccountRequest.Password),
                Role = Service.Enums.RoleEnums.US.GetDescriptionFromEnum(),
                Gender = AccountRequest.Gender,
                Phone = AccountRequest.Phone,
                Birthday = AccountRequest.Birthday,
                CreateTime = DateTime.Now,
            };
            var check = await _accountSerivce.AddAccount(account);

            var AccountResponse = new CreateNewAccountResponse
            {
                Email = account.Email,
                Name = account.Name,
                Role = account.Role,
                Birthday = account.Birthday,
                Phone = account.Phone
            };

            return StatusCode(StatusCodes.Status201Created, AccountResponse);
        }
    }
}
