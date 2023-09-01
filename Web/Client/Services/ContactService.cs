using System.Text.Json;
using System.Text;
using Web.Client.Entities.Contact;
using Web.Client.Interfaces;

namespace Web.Client.Services
{
    public sealed class ContactService : IContactService
	{
		private readonly HttpClient _httpClient;

		public ContactService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<(bool, string)> Send(ContactModel model)
		{
			var json = JsonSerializer.Serialize(model);
			var result = (await _httpClient.PostAsync($"api/v1/contact/send", new StringContent(json, Encoding.UTF8, "application/json")))!;

			if (!result.IsSuccessStatusCode)
			{
				var message = await result
					.Content
					.ReadAsStringAsync();

				return (false, message);
			}

			return (true, "Dados enviados com sucesso! Iremos analisar e responder em breve.");
		}
	}
}