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
            else MessageBox.Show("Greska prilikom dodavanja komitenta");
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

        internal async Task UpdateKomitent(KomitentDTO komitentDTO)
        {
            var requestUri = $"api/Komitent/{komitentDTO.Id}";
            var response = await _httpClient.PutAsJsonAsync(requestUri, komitentDTO);
            if (response.IsSuccessStatusCode) MessageBox.Show("Komitent je uspesno izmenjen");
            else MessageBox.Show("Greska prilikom izmene komitenta");
        }

        internal async Task DeleteKomitent(KomitentDTO komitentDTO)
        {
            var response = await _httpClient.DeleteAsync($"api/Komitent/{komitentDTO.Id}");
            if (response.IsSuccessStatusCode) MessageBox.Show("Komitent je uspesno obrisan");
            else MessageBox.Show("Greska prilikom brisanja komitenta");
        }
    }
}
