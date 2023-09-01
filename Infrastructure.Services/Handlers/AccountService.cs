using Domain.Commands.Account.Create;
using Domain.Commands.Account.Login;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Utils;
using Infrastructure.Repository.Collections;
using Infrastructure.Services.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;

namespace Infrastructure.Services.Handlers
{
	public sealed class AccountService : IAccountService
	{
		private const string Separator = ". ";
		private const string Unauthorized = "Unauthorized";

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly TokenConfigurations _tokenConfigurations;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ISmtpService _smtpService;

		public AccountService(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			TokenConfigurations tokenConfigurations,
			IHttpContextAccessor httpContextAccessor,
			ISmtpService smtpService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenConfigurations = tokenConfigurations;
			_httpContextAccessor = httpContextAccessor;
			_smtpService = smtpService;
		}

		public async Task<(UserDto, string)> AuthenticateAsync(LoginRequestDto request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email) ??
				throw new ArgumentException(Unauthorized);

			var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

			if (!emailConfirmed)
				throw new InvalidOperationException("É necessário confirmar o email para fazer login.");

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

			if (!result.Succeeded)
				throw new ArgumentException(Unauthorized);

			user.RefreshToken = GenerateRefreshToken();
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

			await _userManager.UpdateAsync(user);

			return (new UserDto(user.Id.ToString(), user.Name, user.Email!), user.RefreshToken);
		}

		public string GenerateToken(UserDto user)
		{
			var claims = GenerateUserClaims(user.Id, user.Name, user.Email);
			var credentials = GetSigninCredentials();
			var tokenOptions = GenerateTokenOptions(credentials, claims);

			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		}

		public async Task CreateAsync(AccountCreateRequest request, CancellationToken cancellationToken)
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

			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			var message = _smtpService.BuildMessage(request.Email, token);
			_smtpService.SendEmail(message, cancellationToken);
		}

		public string GetAuthenticatedUserId()
		{
			return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = _tokenConfigurations.SecurityKey,
				ValidateLifetime = false,
				ValidIssuer = _tokenConfigurations.Issuer,
				ValidAudience = _tokenConfigurations.Audience
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var principal = tokenHandler
				.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

			var jwtSecurityToken = securityToken as JwtSecurityToken;

			if (jwtSecurityToken is null ||
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Token inválido.");

			return principal;
		}

		public async Task<(string, string)> ValidateAndUpdateRefreshToken(string email, string refreshToken)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
				throw new Exception("Refresh token inválido ou expirado.");

			var credentials = GetSigninCredentials();
			var claims = GenerateUserClaims(user.Id.ToString(), user.Name, user.Email!);
			var tokenOptions = GenerateTokenOptions(credentials, claims);

			var accessToken = new JwtSecurityTokenHandler()
				.WriteToken(tokenOptions);

			user.RefreshToken = GenerateRefreshToken();
			//user.RefreshTokenExpiryTime

			await _userManager.UpdateAsync(user);

			return (accessToken, user.RefreshToken);
		}

		private static string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];

			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);

			return Convert.ToBase64String(randomNumber);
		}

		private static string RetrieveMessage(IdentityResult? result)
		{
			if (result is null || result.Errors is null)
				return string.Empty;

			return string.Join(Separator, result.Errors.Select(s => s.Description));
		}

		private static IEnumerable<Claim> GenerateUserClaims(string id, string name, string email)
		{
			return new List<Claim>(3)
			{
				new Claim(ClaimTypes.Name, name),
				new Claim(ClaimTypes.NameIdentifier, id),
				new Claim(ClaimTypes.Email, email)
			};
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, IEnumerable<Claim> claims)
		{
			return new JwtSecurityToken(
				issuer: _tokenConfigurations.Issuer,
				audience: _tokenConfigurations.Audience,
				signingCredentials: GetSigninCredentials(),
				claims: claims,
				notBefore: DateTime.Now,
				expires: DateTime.Now.AddSeconds(_tokenConfigurations.Seconds)
			);
		}

		private SigningCredentials GetSigninCredentials()
		{
			return new SigningCredentials(_tokenConfigurations.SecurityKey, SecurityAlgorithms.HmacSha256);
		}

		public async Task ConfirmEmailAsync(string email, string token)
		{
			var user = await _userManager.FindByEmailAsync(email) ?? 
				throw new ArgumentException("Dados de confirmação inválidos.");

			var result = await _userManager.ConfirmEmailAsync(user, token);

			if (!result.Succeeded)
				throw new ArgumentException(result.Errors.FirstOrDefault()?.Description);
		}
	}
}