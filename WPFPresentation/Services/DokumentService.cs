using Application.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

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

        internal async Task AddDokument(DokumentDTO dokumentDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Dokument", dokumentDTO);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Dokument uspesno dodat");
            }
            else
            {
                MessageBox.Show("Greska prilikom dodavanja dokumenta");

            }
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
