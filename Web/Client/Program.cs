using BlazorBootstrap;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Web.Client;
using Web.Client.Helpers;
using Web.Client.Interfaces;
using Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IContactService, ContactService>();

//todo change to httpclient factory
builder.Services.AddScoped(sp => new HttpClient 
{ 
	BaseAddress = new Uri("https://empregosemcampinasapi.azurewebsites.net/") 
}
.EnableIntercept(sp));

builder.Services.AddHttpClientInterceptor();

await builder.Build().RunAsync();
