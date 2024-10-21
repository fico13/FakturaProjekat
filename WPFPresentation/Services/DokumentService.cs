using Application.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace WPFPresentation.Services
{
    public class DokumentService
    {
        private readonly HttpClient _httpClient;

        public DokumentService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5055");
        }

        internal async Task<bool> AddDokument(DokumentDTO dokumentDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Dokument", dokumentDTO);
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        internal async Task<bool> DeleteDokument(string brojDokumenta)
        {
            var response = await _httpClient.DeleteAsync($"api/Dokument/{brojDokumenta}");
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        internal async Task<IEnumerable<DokumentDTO>> FindDokuments(string brojDokumenta)
        {
            var requestUri = $"api/Dokument/search?brojDokumenta={Uri.EscapeDataString(brojDokumenta)}";
            var response = await _httpClient.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                var dokumenti = await response.Content.ReadFromJsonAsync<IReadOnlyList<DokumentDTO>>();
                return dokumenti!;
            }
            return new List<DokumentDTO>();
        }

        internal async Task<IEnumerable<DokumentDTO>> GetDokumenti()
        {
            var response = await _httpClient.GetAsync("api/Dokument");
            if (response.IsSuccessStatusCode)
            {
                var dokumenti = await response.Content.ReadFromJsonAsync<IReadOnlyList<DokumentDTO>>();
                return dokumenti!;
            }
            return new List<DokumentDTO>();
        }

        internal async Task<bool> UpdateDokument(DokumentDTO selectedDokument)
        {
            var requestUri = $"api/Dokument/";
            var response = await _httpClient.PutAsJsonAsync(requestUri, selectedDokument);
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
