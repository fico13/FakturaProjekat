using Application.DTOs;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;

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

        public async Task<IEnumerable<KomitentDTO>> GetKomitents()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Komitent");
                if (response.IsSuccessStatusCode)
                {
                    var komitents = await response.Content.ReadFromJsonAsync<IEnumerable<KomitentDTO>>();
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

        public async Task<bool> AddKomitent(KomitentDTO komitent)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Komitent", komitent);
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        internal async Task<IReadOnlyList<KomitentDTO>> FindKomitents(string sifraKomitenta)
        {
            var requestUri = $"api/Komitent/search?sifraKomitenta={Uri.EscapeDataString(sifraKomitenta)}";
            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var komitents = await response.Content.ReadFromJsonAsync<IReadOnlyList<KomitentDTO>>();
                return komitents!;

            }
            return new List<KomitentDTO>();
        }

        internal async Task<bool> UpdateKomitent(KomitentDTO komitentDTO)
        {
            var requestUri = $"api/Komitent/";
            var response = await _httpClient.PutAsJsonAsync(requestUri, komitentDTO);
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        internal async Task<bool> DeleteKomitent(string sifraKomitenta)
        {
            var response = await _httpClient.DeleteAsync($"api/Komitent/{sifraKomitenta}");
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
