using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using EventsWeb.Client.Helper;
using EventsWeb.Shared.Model;

namespace EventsWeb.Client.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navManager;

        public CategoryService(HttpClient http, IJSRuntime jsRuntime, NavigationManager navManager)
        {
            _http = http;
            _jsRuntime = jsRuntime;
            _navManager = navManager;
        }

        public async Task<EventsCategory> Get(int id)
        {
            var response = await _http.GetAsync($"api/Category/Get/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var category = JsonConvert.DeserializeObject<EventsCategory>(content);
                return category;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
                _navManager.NavigateTo("CategoryList");

            }
            return new EventsCategory();
        }

        public async Task<IEnumerable<EventsCategory>> GetAll()
        {

            var response = await _http.GetAsync("api/Category/GetAll");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<EventsCategory>>(content);
                foreach (var category in categories)
                {
                    category.ImageUrl = _http.BaseAddress + category.ImageUrl;
                }
                return categories;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
            }
            return new List<EventsCategory>();
        }

        public async Task Create(EventsCategory categoryDTO)
        {
            var response = await _http.PostAsJsonAsync($"api/Category", categoryDTO);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<EventsCategory>>(content);
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
                _navManager.NavigateTo("CategoryList");
            }
        }
        public async Task Update(EventsCategory category, int id)
        {
            var response = await _http.PutAsJsonAsync($"api/Category/{id}", category);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<EventsCategory>>(content);
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
                _navManager.NavigateTo("CategoryList");
            }
        }
        public async Task<int> Delete(int id)
        {
            var response = await _http.DeleteAsync($"api/Category/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<int>(content);
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
                return 0;
            }
        }
        public async Task DeleteImage(string filePath)
        {
            string[] fileNames = filePath.Split('/');
            string fileName = fileNames[fileNames.Length - 1];
            var response = await _http.DeleteAsync($"api/Category/DeleteImage/{fileName}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorMessage>(content);
                await _jsRuntime.ToastrError(errorModel.Message);
            }
        }
    }
}
