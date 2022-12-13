using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        // this is the client we'll use to consume external services(API), it should be created from d IHttpClientFactory from ASP.NET Core
        // we provide this information in Program.cs when registering the application
        private readonly HttpClient client;

        public CatalogService(HttpClient client)
        {
            this.client = client;
        }

        public Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            throw new NotImplementedException();
        }

        public Task<CatalogModel> GetCatalog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            throw new NotImplementedException();
        }
    }
}
