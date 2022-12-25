using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SubwayStation.Application.Contracts;
using SubwayStation.Domain;
using SubwayStation.Domain.DTOs;
using SubwayStation.Domain.Models;
using SubwayStation.Domain.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SubwayStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppSetting _appSetting;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 IOptions<AppSetting> options,
                                 IMapper mapper,
                                 ICacheService cacheService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _appSetting = options.Value;
            _cacheService = cacheService;
        }

        [HttpGet("refreshtoken/{refreshToken}")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            string token = string.Empty;

            //search token in cache (Redis)
            var hasValue = _cacheService.GetCacheValue(refreshToken, ref token);

            if (!hasValue) return BadRequest(new ResponseModel { Message = "Invalid refresh token" });

            var securityParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSetting.JwtSecret)),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            //Validated if token is alive yet
            var handler = new JwtSecurityTokenHandler();
            var oldTokenData = handler.ValidateToken(token, securityParams, out SecurityToken securityToken);

            //figure out if token still valid and the algorith is the same
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest(new ResponseModel { Message = "Invalid refresh token" });
            }

            //delete the old token to add new one.
            _cacheService.DelCacheValue(refreshToken);

            //Generate Token
            var tokenResult = await GenerateToken(oldTokenData.Claims.ToList());

            return Ok(new ResponseModel<TokenDTO> { Result = tokenResult, Message = "New token generate" });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            var user = login.UserName.Contains("@")
                ? await _userManager.FindByEmailAsync(login.UserName)
                : await _userManager.FindByNameAsync(login.UserName);

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var lsClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                };

                var claim = new ClaimsIdentity(new GenericIdentity(user.UserName, "TokenAuth"), lsClaims);

                //Generate Token
                var tokenResult = await GenerateToken(claim.Claims.ToList());

                return Ok(new ResponseModel<TokenDTO> { Result = tokenResult, Message = "token generate successfully" });
            }

            if (result.IsLockedOut)
            {
                return BadRequest(new ResponseModel { Message = "User blocked, please contact administrator" });
            }
            else if (result.IsNotAllowed)
            {
                return BadRequest(new ResponseModel { Message = "This user is not able to login, please contact administrator" });
            }
            else
            {
                return BadRequest(new ResponseModel { Message = "User or password incorrect" });
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] SignUpViewModel model)
        {
            var user = _mapper.Map<IdentityUser>(model);
            var result = await _userManager.CreateAsync(user, model.PasswordHash);

            if (result.Succeeded)
            {
                return Ok(new ResponseModel { Message = "User register successfully" });
            }

            var ErrorDescription = result.Errors.FirstOrDefault();
            return BadRequest(new ResponseModel { Message = ErrorDescription.Description });
        }

        private async Task<TokenDTO> GenerateToken(List<Claim> userClaims)
        {
            DateTime expiration = DateTime.Now.AddMinutes(_appSetting.JwtExpiration);

            //Sign and generate claims
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.JwtSecret));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtClaims = new JwtSecurityToken(claims: userClaims, expires: expiration, signingCredentials: sign);

            //Generate AccessToken and RefreshToken
            var accesstoken = new JwtSecurityTokenHandler().WriteToken(jwtClaims);
            string refreshToken = Guid.NewGuid().ToString("N");

            //Store token in cache to refresh token after expired (Redis)
            await _cacheService.SetCacheValueAsync(refreshToken, accesstoken, TimeSpan.FromMinutes(_appSetting.JwtRefreshExpiration));

            return new TokenDTO { AccessToken = accesstoken, ExpirationDate = expiration, RefreshToken = refreshToken };
        }
    }
}
