using Application.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace WPFPresentation.Services
{
    public class RobaService
    {
        private readonly HttpClient _httpClient;

        public RobaService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5055");
        }

        internal async Task<bool> DodajRobu(RobaDTO? roba)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Roba", roba);
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        internal async Task<IEnumerable<RobaDTO>> FindRoba(string sifraRobe)
        {
            var requestUri = $"api/Roba/search?sifraRobe={Uri.EscapeDataString(sifraRobe)}";
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var roba = await response.Content.ReadFromJsonAsync<IReadOnlyList<RobaDTO>>();
                return roba!;
            }
            return new List<RobaDTO>();
        }

        internal async Task<IEnumerable<RobaDTO>> GetRoba()
        {
            var response = await _httpClient.GetAsync("api/Roba");
            if (response.IsSuccessStatusCode)
            {
                var roba = await response.Content.ReadFromJsonAsync<IReadOnlyList<RobaDTO>>();
                return roba!;
            }
            return new List<RobaDTO>();
        }

        internal async Task<bool> UpdateRoba(RobaDTO robaDTO)
        {
            var requestUri = $"api/Roba/";
            var response = await _httpClient.PutAsJsonAsync(requestUri, robaDTO);
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        internal async Task<bool> DeleteRoba(string sifraRobe)
        {
            var response = await _httpClient.DeleteAsync($"api/Roba/{sifraRobe}");
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
