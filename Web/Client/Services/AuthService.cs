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
				.ReadFromJsonAsync<LoginResponse>();

			await _localStorage.SetItemAsync("authToken", loginResponse!.Token);

			((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponse.Token);

			return true;
		}

		public async Task Logout()
		{
			await _localStorage.RemoveItemAsync("authToken");
			((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}