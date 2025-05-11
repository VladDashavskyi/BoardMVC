using BoardMVC.DTO;
using BoardMVC.Models;

namespace BoardMVC.Services
{
    public class AnnouncementService
    {
        private readonly HttpClient _httpClient;

        public AnnouncementService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("BoardAPI");
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Announcement>>("announcement");
        }

        public async Task<Announcement> GetByIdAsync(int id)
        {

            return await _httpClient.GetFromJsonAsync<Announcement>($"announcement/{id}");
        }

        public async Task<bool> CreateAsync(AnnouncementDto announcement)
        {
            var response = await _httpClient.PostAsJsonAsync("announcement", announcement);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Announcement announcement)
        {
            var response = await _httpClient.PutAsJsonAsync($"announcement", announcement);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"announcement/{id}");
            return response.IsSuccessStatusCode;
        }  
        
        public async Task<List<CategoryDto>> GetAllCategories()
        {
           return await _httpClient.GetFromJsonAsync<List<CategoryDto>>("dictionary/categories");
        }

        public async Task<CategoryDto> GetCategorнByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryDto>($"dictionary/categories/{id}");
        }

        public async Task<List<SubCategoryDto>> GetAllSubCategories()
        {
           return await _httpClient.GetFromJsonAsync<List<SubCategoryDto>>("dictionary/subcategories");
        }

        public async Task<SubCategoryDto> GetSubCategoryByIdAsync(int id)
        {

            return await _httpClient.GetFromJsonAsync<SubCategoryDto>($"dictionary/subcategories/{id}");
        }
    }
}
