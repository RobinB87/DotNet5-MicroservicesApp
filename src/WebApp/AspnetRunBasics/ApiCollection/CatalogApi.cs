using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.ApiCollection.Infrastructure;
using WebApp.ApiCollection.Interfaces;
using WebApp.Models;
using WebApp.Settings;

namespace WebApp.ApiCollection
{
    public class CatalogApi : BaseHttpClientWithFactory, ICatalogApi
    {
        private readonly IApiSettings _settings;

        public CatalogApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<IEnumerable<Catalog>> GetCatalog()
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();

            return await SendRequest<IEnumerable<Catalog>>(message);
        }

        public async Task<Catalog> GetCatalog(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .AddToPath(id)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();

            return await SendRequest<Catalog>(message);
        }

        public async Task<IEnumerable<Catalog>> GetCatalogByName(string name)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .AddToPath("by-name")
                .AddToPath(name)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();

            return await SendRequest<IEnumerable<Catalog>>(message);
        }

        public async Task<IEnumerable<Catalog>> GetCatalogByCategory(string category)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .AddToPath("by-category")
                .AddToPath(category)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();

            return await SendRequest<IEnumerable<Catalog>>(message);
        }

        public async Task<Catalog> CreateCatalog(Catalog catalog)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.CatalogPath)
                .HttpMethod(HttpMethod.Post)
                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(catalog);
            message.Content = new StringContent(
                json, Encoding.UTF8, "application/json");

            return await SendRequest<Catalog>(message);
        }
    }
}