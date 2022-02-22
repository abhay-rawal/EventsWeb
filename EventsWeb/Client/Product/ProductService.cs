using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using EventsWeb.Client.Helper;
using EventsWeb.Shared.Model;

namespace EventsWeb.Client.Product
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navManager;
        public ProductService(HttpClient http, IJSRuntime jsRuntime, NavigationManager navManager)
        {
            _http = http;
            _jsRuntime = jsRuntime;
            _navManager = navManager;
        }

        public async Task<EventsProduct> Get(int id)
        {
            var response = await _http.GetAsync($"api/Product/Get/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<EventsProduct>(content);
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
                _navManager.NavigateTo("ProductList");

            }
            return new EventsProduct();
        }

        public async Task<IEnumerable<EventsProduct>> GetAll()
        {

            var response = await _http.GetAsync("api/Product/GetAll");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var products = JsonConvert.DeserializeObject<IEnumerable<EventsProduct>>(content);
                return products;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
            }
            return new List<EventsProduct>();
        }

        public async Task Create(EventsProduct productDTo)
        {
            var response = await _http.PostAsJsonAsync($"api/Product", productDTo);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var products = JsonConvert.DeserializeObject<IEnumerable<EventsProduct>>(content);
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
             
            }
        }
        public async Task Update(EventsProduct objEvents, int id)
        {
            var response = await _http.PutAsJsonAsync($"api/Product/{id}", objEvents);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<IEnumerable<EventsProduct>>(content);
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
                _navManager.NavigateTo("ProductList");
            }
        }
        public async Task Delete(int id)
        {
            var response = await _http.DeleteAsync($"api/Product/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
            }
        }

      
    }
}
