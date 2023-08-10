using Domain.Commands.Job.Create;
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
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public JobService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<IEnumerable<JobResponse>> GetJobs()
        {
            var result = (await _httpClient.GetAsync("api/v1/jobs"))!;

            var jobs = await result
                .Content
                .ReadFromJsonAsync<IEnumerable<JobResponse>>();

            return jobs!;
        }

        public async Task<JobResponse> GetJobById(string id)
        {
            var result = (await _httpClient.GetAsync($"api/v1/jobs/{id}"))!;

            var job = await result
                .Content
                .ReadFromJsonAsync<JobResponse>();

            return job!;
        }

        public async Task<IEnumerable<JobResponse>> GetUserJobs()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

            var userId = authState.User
                .Claims
                .First(s => s.Type == ClaimTypes.NameIdentifier)
                .Value;

            var result = (await _httpClient.GetAsync($"api/v1/jobs/getUserJobs/{userId}"))!;

            var jobs = await result
                .Content
                .ReadFromJsonAsync<IEnumerable<JobResponse>>();

            return jobs!;
        }

        public async Task<(bool, string?)> CreateJob(CreateJobRequest request)
        {
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

			return (true, default);
        }

        public async Task<(bool, string?)> UpdateJob(UpdateJobRequest request)
        {
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

			return (true, default);
        }
    }
}
