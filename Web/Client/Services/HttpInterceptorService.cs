using System.Net.Http.Headers;
using Toolbelt.Blazor;
using Web.Client.Interfaces;

namespace Web.Client.Services
{
	public sealed class HttpInterceptorService
	{
		private readonly IAuthService _authService;
		private readonly HttpClientInterceptor _interceptor;

		public HttpInterceptorService(IAuthService authService, HttpClientInterceptor interceptor)
		{
			_authService = authService;
			_interceptor = interceptor;
		}

		public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

		public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
		{
			var absPath = e.Request.RequestUri!.AbsolutePath;

			if (!absPath.Contains("account"))
			{
				var token = await _authService.TryRefreshToken();

				if (!string.IsNullOrWhiteSpace(token))
					e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
			}
		}

		public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
	}
}