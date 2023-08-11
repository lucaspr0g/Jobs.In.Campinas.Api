using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Web.Client.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Web.Client.Entities.Account;
using Web.Client.Helpers;

namespace Web.Client.Services
{
	public class AuthService : IAuthService
	{
		private const int TwoMinutes = 2;

		private readonly HttpClient _httpClient;
		private readonly AuthenticationStateProvider _authenticationStateProvider;
		private readonly ILocalStorageService _localStorage;

		public AuthService(
			HttpClient httpClient,
			AuthenticationStateProvider authenticationStateProvider,
			ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_authenticationStateProvider = authenticationStateProvider;
			_localStorage = localStorage;
		}

		public async Task<(bool, string)> Register(RegisterModel registerModel)
		{
			var result = await _httpClient.PostAsJsonAsync("api/v1/accouunt/create", registerModel);

			if (!result.IsSuccessStatusCode)
			{
				var message = await result.Content.ReadAsStringAsync();
				return (false, message);
			}

			return (true, "Usuário registrado com sucesso.");
		}

		public async Task<bool> Login(LoginModel loginModel)
		{
			var json = JsonSerializer.Serialize(loginModel);
			var response = await _httpClient.PostAsync("api/v1/account/login", new StringContent(json, Encoding.UTF8, "application/json"));

			if (!response.IsSuccessStatusCode)
				return false;

			var loginResponse = await response
				.Content
				.ReadFromJsonAsync<TokenModel>();

			await _localStorage.SetItemAsync("authToken", loginResponse!.AccessToken);
			await _localStorage.SetItemAsync("refreshToken", loginResponse!.RefreshToken);

			((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponse.AccessToken);

			return true;
		}

		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("authToken");
			await _localStorage.RemoveItemAsync("refreshToken");
			((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}

		public async Task<string> RefreshToken()
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");
			var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

			var json = JsonSerializer.Serialize(new TokenModel { AccessToken = token, RefreshToken = refreshToken });
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var result = await _httpClient.PostAsync("api/v1/account/refresh", content);

			if (!result.IsSuccessStatusCode)
				throw new ApplicationException("Something went wrong during the refresh token action");

			var tokenResponse = await result
				.Content
				.ReadFromJsonAsync<TokenModel>();

			await _localStorage.SetItemAsync("authToken", tokenResponse!.AccessToken);
			await _localStorage.SetItemAsync("refreshToken", tokenResponse.RefreshToken);

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenResponse.AccessToken);

			return tokenResponse.AccessToken!;
		}

		public async Task<string?> TryRefreshToken()
		{
			var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;

			if (user is null)
				return default;

			var exp = user.FindFirst(s => 
				s.Type.Equals("exp", StringComparison.Ordinal))!
				.Value;

			var expiryTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));

			var diff = expiryTime - DateTime.UtcNow;

			if (diff.TotalMinutes <= TwoMinutes)
				return await RefreshToken();

			return default;
		}
	}
}