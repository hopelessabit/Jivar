using Jivar.BO.Models;
using Jivar.Service.Constant;
using Jivar.Service.Implements;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Account.Request;
using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.PayLoads;
using Jivar.Service.Util;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Jivar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountSerivce;
        private readonly EmailService _emailService;

        public AccountController(IAuthService authService, IAccountService accountSerivce, EmailService emailService)
        {
            _authService = authService;
            _accountSerivce = accountSerivce;
            _emailService = emailService;
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount()
        {
            // Simulate user creation logic here
            var verificationToken = UserUtil.GenerateRandomSixDigitNumber();
            var verificationLink = $"{Request.Scheme}://{Request.Host}/api/account/verify?token={verificationToken}"; ;

            // Send verification email
            await _emailService.SendVerificationEmailAsync("micalminh1@gmail.com", verificationLink);

            return Ok(new { message = "Account created successfully. Please verify your email." });
        }


        [HttpPost(APIEndPointConstant.Authentication.Login)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
        public async Task<ActionResult<string>> Login([FromForm] LoginRequest request)
        {
            var (account, actorId) = await _authService.Authenticate(request.Email, request.Password);
            if (account.Verify != null)
                return Unauthorized(new Jivar.Service.PayLoads.ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = "You must verify your email to continute.",
                    TimeStamp = DateTime.Now
                });
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
            var verificationToken = UserUtil.GenerateRandomSixDigitNumber().ToString();
            var verificationLink = AccountRequest.FeUrl + verificationToken;

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
                Name = AccountRequest.Name,
                Username = AccountRequest.Username,
                Password = PasswordUtil.HashPassword(AccountRequest.Password),
                Role = Service.Enums.RoleEnums.US.GetDescriptionFromEnum(),
                Gender = AccountRequest.Gender,
                Phone = AccountRequest.Phone,
                Birthday = AccountRequest.Birthday,
                Verify = verificationToken,
                CreateTime = DateTime.Now,
            };

            // Send verification email
            await _emailService.SendVerificationEmailAsync("micalminh1@gmail.com", verificationToken);

            var check = await _accountSerivce.AddAccount(account);

            var AccountResponse = new CreateNewAccountResponse
            {
                Email = account.Email,
                Name = account.Name,
                Role = account.Role,
                Birthday = account.Birthday,
                Phone = account.Phone
            };

            return Ok(new { message = "Account created successfully. Please verify your email." });
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyAccount([FromQuery] string token)
        {
            Account? account = await _accountSerivce.FindByToken(token);
            if (account == null)
            {
                return NotFound(new Jivar.Service.PayLoads.ErrorResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Error = "Account not found.",
                    TimeStamp = DateTime.Now
                });
            }
            account.Verify = null;
            await _accountSerivce.UpdateAccount(account);
            return Ok(new { message = "Account verified successfully." });
        }

        [HttpPut(APIEndPointConstant.Account.UpdateInfo)]
        public async Task<ActionResult> SeftUpdateAccount([FromBody] UpdateAccountRequest request)
        {
            try
            {
                var result = await _accountSerivce.UpdateAccountInfo(UserUtil.GetAccountId(HttpContext), request);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut(APIEndPointConstant.Account.UpdateInfo + "/{id}")]
        public async Task<ActionResult> SeftUpdateAccount(int id,[FromBody] UpdateAccountRequest request)
        {
            try
            {
                var result = await _accountSerivce.UpdateAccountInfo(UserUtil.GetAccountId(HttpContext), request);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet(APIEndPointConstant.Account.GetInfo)]
        public async Task<ActionResult> GetSeftInfo()
        {
            var result = await _accountSerivce.GetInfoById(UserUtil.GetAccountId(HttpContext));
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet(APIEndPointConstant.Account.GetInfo + "/{id}")]
        public async Task<ActionResult> GetInfo(int id)
        {
            var result = await _accountSerivce.GetInfoById(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
