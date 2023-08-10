using Domain.Commands.Account.Create;
using Domain.Commands.Account.Login;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Utils;
using Infrastructure.Repository.Collections;
using Infrastructure.Services.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services.Handlers
{
    public sealed class AccountService : IAccountService
    {
        private const string Separator = ". ";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            TokenConfigurations tokenConfigurations,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenConfigurations = tokenConfigurations;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> AuthenticateAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) ?? 
                throw new ArgumentException("Unauthorized");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                throw new ArgumentException("Unauthorized");

            return new UserDto(user.Id.ToString(), user.Name, user.Email!);
        }

        public string GenerateToken(UserDto user)
        {
            var claims = new List<Claim>(3)
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _tokenConfigurations.Issuer,
                audience: _tokenConfigurations.Audience,
                signingCredentials: new SigningCredentials(_tokenConfigurations.SecurityKey, SecurityAlgorithms.HmacSha256),
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_tokenConfigurations.Seconds)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task CreateAsync(AccountCreateRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
                throw new Exception("Email já está cadastrado.");

            user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                Name = request.Name,
                ConcurrencyStamp = ApplicationHelper.GenerateGuid()
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new ApplicationException(string.Join(Separator, RetrieveMessage(result)));
        }

        public string GetAuthenticatedUserId()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        private static string RetrieveMessage(IdentityResult? result)
        {
            if (result is null || result.Errors is null)
                return string.Empty;

            return string.Join(Separator, result.Errors.Select(s => s.Description));
        }
    }
}