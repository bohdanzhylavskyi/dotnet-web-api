using ConsoleApp.DTOs;
using System.Net.Http.Json;
using WebApi.Entities;

namespace ConsoleApp
{
    public class ApiClient
    {
        private HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDTO dto)
        {
            var response = await this.httpClient.PostAsJsonAsync($"categories", dto);
            var createdCategory = await response.Content.ReadFromJsonAsync<Category>();

            return createdCategory;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await this.httpClient.GetFromJsonAsync<Category>($"categories/{id}");
        }

        public async Task<List<Category>> GetCategoriesListAsync()
        {
            return await this.httpClient.GetFromJsonAsync<List<Category>>($"categories");
        }

        public async Task UpdateCategoryAsync(int categoryId, UpdateCategoryDTO dto)
        {
            await this.httpClient.PutAsJsonAsync($"categories/{categoryId}", dto);
        }

        public async Task DeleteCategoryByIdAsync(int id)
        {
            await this.httpClient.DeleteAsync($"categories/{id}");
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await this.httpClient.GetFromJsonAsync<Product>($"products/{id}");
        }

        public async Task<List<Product>> GetProductsListAsync(int? categoryId, int? pageNumber, int? pageSize)
        {
            var query = new Dictionary<string, string>();

            if (categoryId != null)
            {
                query.Add("categoryId", categoryId.ToString());
            }

            if (pageNumber != null)
            {
                query.Add("pageNumber", pageNumber.ToString());
            }

            if (pageSize != null)
            {
                query.Add("pageSize", pageSize.ToString());
            }

            string queryStr = UrlHelper.QueryParamsToString(query);

            return await this.httpClient.GetFromJsonAsync<List<Product>>($"products?{queryStr}");
        }
    }
}
