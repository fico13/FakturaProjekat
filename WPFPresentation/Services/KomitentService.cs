using Application.DTOs;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace WPFPresentation.Services
{
    public class KomitentService
    {
        private readonly HttpClient _httpClient;

        public KomitentService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5055");
        }

        public async Task<IReadOnlyList<KomitentDTO>> GetKomitents()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Komitent");
                if (response.IsSuccessStatusCode)
                {
                    var komitents = await response.Content.ReadFromJsonAsync<IReadOnlyList<KomitentDTO>>();
                    return komitents!;
                }
                return new List<KomitentDTO>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task AddKomitent(KomitentDTO komitent)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Komitent", komitent);
            if (response.IsSuccessStatusCode) MessageBox.Show("Komitent je uspesno dodat");
        }

        internal async Task<IReadOnlyList<KomitentDTO>> FindKomitents(string name)
        {
            var requestUri = $"api/Komitent/search?name={Uri.EscapeDataString(name)}";
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var komitents = await response.Content.ReadFromJsonAsync<IReadOnlyList<KomitentDTO>>();
                return komitents!;
            }
            return new List<KomitentDTO>();
        }
    }
}
