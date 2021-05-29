using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UsersController> _logger;
        public UsersController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IEmailSender emailSender,
            ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _logger = logger;
        }
        [HttpGet]
        [Route("All")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _userManager.Users;
        }

        // USER LOGIN
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return BadRequest(new ServiceResponse<ApplicationUser>
                {
                    Success = false,
                    Message = "Invalid User Credentials"
                });
            if (!user.EmailConfirmed)
                return Unauthorized(new ServiceResponse<ApplicationUser>
                {
                    Success = false,
                    Message = "User email not confirmed"
                });

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                //foreach (var userRole in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                //}

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(60),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                user.LastLogin = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                //LOGIN NOTIFICATION
                bool? emailSent = null;
                StringBuilder message = new StringBuilder();
                message.Append($"<p>Dear {user.Email}</p>");
                message.Append($"<p>Please be informed that your CEMCS profile was accessed on {DateTime.UtcNow}(UTC)</p>");

                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "CEMCS Login Notification", message.ToString());
                }
                catch (Exception)
                {
                    emailSent = false;
                }

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userId = token.Id,
                    returnUrl = model.ReturnUrl ?? _configuration["ClientSideInfo:DefaultRedirect"],
                    EmailSent = emailSent ?? true,
                    UserType = user.UserType
                });

            }

            return BadRequest(new ServiceResponse<ApplicationUser>
            {
                Success = false,
                Message = "Invalid Login Details",
            });
        }

        // USER REGISTERATION
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                UserType = model.UserType,
                UserTypeCategory = model.UserTypeCategory
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            bool? emailSent = null;
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            else
            {
                var codeGen = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                codeGen = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(codeGen));
                var callbackUrl = Url.Link(
                    "EmailConfirmation",
                    values: new { userId = user.Id, code = codeGen, returnUrl = model.ReturnUrl }
                    );
                //Send email
                string message = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                try
                {
                    await _emailSender.SendEmailAsync(model.Email, "CEMCS Registration: Confirm your email", message);
                }
                catch (Exception)
                {
                    emailSent = false;
                }
            }
            //Login User
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new
            {
                Status = "Success",
                Message = "User created successfully, Email confirmation sent!",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                EmailSent = emailSent ?? true
            });
        }

        // EMAIL CONFIRMATION
        [HttpGet]
        [Route("ConfirmEmail", Name = "EmailConfirmation")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string code, string returnUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                //CONFIRMATION NOTIFICATION
                string message = $"<p>Dear {user.Email}</p>" +
                    $"<p>Your CEMCS account has been confirmed!</p>";
                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "CEMCS Email Confirmation", message);
                }
                catch (Exception)
                {
                    return Ok(new
                    {
                        UserResuatration = true,
                        EmailSent = false,
                        Message = "Registration Email Not Sent"
                    });
                };
                return Redirect(returnUrl);
            }
            else
                return BadRequest("User email not confirmed");
        }

        // EXTERNAL LOGIN REDIRECT TO CLIENT SIDE
        [HttpPost]
        [Route("ExternalLoginRedirect")]
        public async Task<IActionResult> ExternalLoginRedirect([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    _logger.LogWarning($"Invalid Email: {model.Username}");

                    return BadRequest(new ServiceResponse<ApplicationUser>
                    {
                        Success = false,
                        Message = "Invalid Username"
                    });
                }

                if (!user.EmailConfirmed)
                {
                    _logger.LogWarning($"User email not confirmed: {user}");

                    return Unauthorized(new ServiceResponse<ApplicationUser>
                    {
                        Success = false,
                        Message = "User email not confirmed"
                    });
                }


                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    string details = $"{model.Username}:PWD:{model.Password}";
                    var codeGen = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(details));
                    var redirectUrl = $"{_configuration["AppSettings:SiteInfo:ApiUrl"]}/ExternalLogin?ext={codeGen}";
                    _logger.LogInformation($"Success: Redirecting user: {model.Username} to {redirectUrl}");
                    return Ok(new
                    {
                        RedirectUrl = redirectUrl
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}: {ex.InnerException}");
            }

            _logger.LogWarning($"Invalid password for user: {model.Username}");
            return BadRequest(new ServiceResponse<ApplicationUser>
            {
                Success = false,
                Message = "Invalid Password"
            });
        }

        // USER EXTERNAL LOGIN WITH CODE
        [HttpPost]
        [Route("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginModel model)
        {
            string[] UserDetails = (Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.code))).Split(":PWD:");
            var user = await _userManager.FindByNameAsync(UserDetails[0]);
            if (user == null)
                return BadRequest(new ServiceResponse<ApplicationUser>
                {
                    Success = false,
                    Message = "Invalid User Credentials"
                });
            if (!user.EmailConfirmed)
                return Unauthorized(new ServiceResponse<ApplicationUser>
                {
                    Success = false,
                    Message = "User email not confirmed"
                });

            if (user != null && await _userManager.CheckPasswordAsync(user, UserDetails[1]))
            {
                // var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                //foreach (var userRole in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                //}

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(60),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                user.LastLogin = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);

                //LOGIN NOTIFICATION
                bool? emailSent = null;
                StringBuilder message = new StringBuilder();
                message.Append($"<p>Dear {user.Email}</p>");
                message.Append($"<p>Please be informed that your CEMCS profile was accessed on {DateTime.UtcNow}(UTC)</p>");

                try
                {
                    await _emailSender.SendEmailAsync(user.Email, "CEMCS Login Notification", message.ToString());
                }
                catch (Exception)
                {
                    emailSent = false;
                }

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userId = token.Id,
                    returnUrl = model.ReturnUrl ?? _configuration["ClientSideInfo:DefaultRedirect"],
                    EmailSent = emailSent ?? true,
                    UserType = user.UserType
                });

            }

            return BadRequest(new ServiceResponse<ApplicationUser>
            {
                Success = false,
                Message = "Invalid Login Details",
            });
        }

        [HttpPost]
        [Route("logout")]
        public Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            //return Ok("User Logged out");
            throw new NotImplementedException();
        }

    }
}
