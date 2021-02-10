using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.ApiCollection.Infrastructure;
using WebApp.ApiCollection.Interfaces;
using WebApp.Models;
using WebApp.Settings;

namespace WebApp.ApiCollection
{
    public class BasketApi : BaseHttpClientWithFactory, IBasketApi
    {
        private readonly IApiSettings _settings;

        public BasketApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<Basket> GetBasket(string userName)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.BasketPath)
                .AddQueryString("username", userName)
                .HttpMethod(HttpMethod.Get)
                .GetHttpMessage();

            return await SendRequest<Basket>(message);
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.BasketPath)
                .HttpMethod(HttpMethod.Post)
                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(basket);
            message.Content = new StringContent(
                json, Encoding.UTF8, "application/json");

            return await SendRequest<Basket>(message);
        }

        public async Task CheckoutBasket(BasketCheckout basket)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                .SetPath(_settings.BasketPath)
                .AddToPath("checkout")
                .HttpMethod(HttpMethod.Post)
                .GetHttpMessage();

            var json = JsonConvert.SerializeObject(basket);
            message.Content = new StringContent(
                json, Encoding.UTF8, "application/json");

            await SendRequest<BasketCheckout>(message);
        }
    }
}