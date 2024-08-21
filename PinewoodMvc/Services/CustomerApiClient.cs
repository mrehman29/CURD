using PinewoodMvc.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PinewoodMvc.Services
{
    public class CustomerApiClient
    {
        private readonly HttpClient _httpClient;

        public CustomerApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>("api/customer");
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Customer>($"api/customer/{id}");
        }

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customer", customer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customer/{customer.Id}", customer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/customer/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
