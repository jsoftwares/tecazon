using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        // this is the client we'll use to consume external services(API), it should be created from d IHttpClientFactory from ASP.NET Core
        // we provide this information in Program.cs when registering the application
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            //ReadContentAs() as we defined in HttpClinetExtension helps to convent d JSON response from our API into object type ie List CatalogModelof 
            var response = await _client.GetAsync("/api/v1/Catalog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }
    }
}
