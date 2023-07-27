using Domain.Commands.Account.Create;
using Domain.Commands.Account.Login;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Utils;
using Infrastructure.Repository.Collections;
using Infrastructure.Services.Entities;
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

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenConfigurations tokenConfigurations)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<UserDto> AuthenticateAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email) ?? 
                throw new ArgumentException("dados invalidos");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                throw new ArgumentException("dados invalidos");

            return new UserDto(user.Id.ToString(), user.Name, user.Email!);
        }

        public string GenerateToken(UserDto user)
        {
            var claims = new List<Claim>(3)
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, ApplicationHelper.GenerateGuid())
            };

            var handler = new JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(
                issuer: _tokenConfigurations.Issuer,
                audience: _tokenConfigurations.Audience,
                signingCredentials: new SigningCredentials(_tokenConfigurations.SecurityKey, SecurityAlgorithms.HmacSha256),
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(_tokenConfigurations.Seconds)
            );

            return handler.WriteToken(token);
        }

        public async Task<AccountCreateResponse> CreateAsync(AccountCreateRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
                return new AccountCreateResponse(false, "usuario ja existe");

            user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                Name = request.Email,
                ConcurrencyStamp = ApplicationHelper.GenerateGuid()
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new AccountCreateResponse(false, string.Join(Separator, RetrieveMessage(result)));

            return new AccountCreateResponse(true, "usuario criado com sucesso");
        }

        public async Task LogoffAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private static string RetrieveMessage(IdentityResult? result)
        {
            if (result is null || result.Errors is null)
                return string.Empty;

            return string.Join(Separator, result.Errors.Select(s => s.Description));
        }
    }
}