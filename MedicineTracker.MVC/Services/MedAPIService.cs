using MedicineTracker.MVC.Models;
using System.Net.Http;

namespace MedicineTracker.MVC.Services
{
    public class MedAPIService
    {
        private readonly HttpClient _httpClient;

        new List<Medicine> med = new List<Medicine>();
        public MedAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7127/api/");
        }
        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            var response = await _httpClient.GetAsync("medicine");
            response.EnsureSuccessStatusCode();
            var medicines = await response.Content.ReadFromJsonAsync<List<Medicine>>();
            return medicines ?? med;
        }

        public async Task AddMedicineAsync(Medicine medicine)
        {
            var response = await _httpClient.PostAsJsonAsync("medicine", medicine);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<Medicine>> SearchMedicinesAsync(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"medicine/Search?searchTerm={Uri.EscapeDataString(searchTerm)}");
            if(!response.IsSuccessStatusCode)
                return med;
            var medicines = await response.Content.ReadFromJsonAsync<List<Medicine>>();
            return medicines ?? med;
        }
    }
}
