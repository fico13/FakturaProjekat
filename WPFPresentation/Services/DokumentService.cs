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
    }
}
