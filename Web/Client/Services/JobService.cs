﻿using Domain.Commands.Job.Create;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Web.Client.Entities.Job;
using Web.Client.Interfaces;

namespace Web.Client.Services
{
	public sealed class JobService : IJobService
	{
		private readonly HttpClient _httpClient;
		private readonly HttpInterceptorService _interceptor;
		private readonly AuthenticationStateProvider _authenticationStateProvider;

		public JobService(HttpClient httpClient, HttpInterceptorService interceptor, AuthenticationStateProvider authenticationStateProvider)
		{
			_httpClient = httpClient;
			_interceptor = interceptor;
			_authenticationStateProvider = authenticationStateProvider;
		}

		public async Task<JobResponse> GetJobs(int page)
		{
			var result = (await _httpClient.GetAsync($"api/v1/jobs/pagination/{page}"))!;

			var jobs = await result
				.Content
				.ReadFromJsonAsync<JobResponse>();

			return jobs!;
		}

		public async Task<JobEntity> GetJobById(string id)
		{
			var result = (await _httpClient.GetAsync($"api/v1/jobs/{id}"))!;

			var job = await result
				.Content
				.ReadFromJsonAsync<JobEntity>();

			return job!;
		}

		public async Task<IEnumerable<JobEntity>> GetUserJobs()
		{
			_interceptor.RegisterEvent();

			var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

			var userId = authState.User
				.Claims
				.First(s => s.Type == ClaimTypes.NameIdentifier)
				.Value;

			var result = (await _httpClient.GetAsync($"api/v1/jobs/getUserJobs"))!;

			var jobs = await result
				.Content
				.ReadFromJsonAsync<IEnumerable<JobEntity>>();

			_interceptor.DisposeEvent();

			return jobs!;
		}

		public async Task<(bool, string?)> CreateJob(CreateJobRequest request)
		{
			_interceptor.RegisterEvent();

			var json = JsonSerializer.Serialize(request);
			var result = (await _httpClient.PostAsync($"api/v1/jobs", new StringContent(json, Encoding.UTF8, "application/json")))!;

			if (!result.IsSuccessStatusCode)
			{
				if (result.StatusCode == HttpStatusCode.Unauthorized)
					return (false, "Recurso não autorizado.");

				var message = await result
					.Content
					.ReadAsStringAsync();

				return (false, message);
			}

			_interceptor.DisposeEvent();

			return (true, default);
		}

		public async Task<(bool, string?)> UpdateJob(UpdateJobRequest request)
		{
			_interceptor.RegisterEvent();

			var json = JsonSerializer.Serialize(request);
			var result = (await _httpClient.PatchAsync($"api/v1/jobs", new StringContent(json, Encoding.UTF8, "application/json")))!;

			if (!result.IsSuccessStatusCode)
			{
				if (result.StatusCode == HttpStatusCode.Unauthorized)
					return (false, "Recurso não autorizado.");

				var message = await result
					.Content
					.ReadAsStringAsync();

				return (false, message);
			}

			_interceptor.DisposeEvent();

			return (true, default);
		}

		public async Task<(bool, string?)> DeleteJob(string id)
		{
			_interceptor.RegisterEvent();

			var result = (await _httpClient.DeleteAsync($"api/v1/jobs/{id}"));

			if (!result.IsSuccessStatusCode)
			{
				if (result.StatusCode == HttpStatusCode.Unauthorized)
					return (false, "Recurso não autorizado.");

				var message = await result
					.Content
					.ReadAsStringAsync();

				return (false, message);
			}

			_interceptor.DisposeEvent();

			return (true, "Vaga excluída com sucesso.");
		}
	}
}