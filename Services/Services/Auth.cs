using Services.Helpers;
using Services.Interfaces;
using Services.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthRequest : IAuthRequest
    {
        private readonly IHttpClientFactory _factory;
        private readonly IAsyncExceptionHandler _exceptionHandler;
        public AuthRequest(IHttpClientFactory factory, IAsyncExceptionHandler exceptionHandler)
        {
            _factory = factory;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<Response> Auth(Request request)
        {
            var encondedString = Encode.EncodeString(request);
            using var client = this.CreateHttpClient(encondedString);
            var reqData = JsonContent.Create(request);
            var endpoint = $"{client.BaseAddress}/test-encrypt" +
                $"";

            return await _exceptionHandler.CatchAsync(async () =>
            {
                var httpResponse = await client.PostAsync(endpoint, reqData);
                await HandleHttpResponseAsync(httpResponse, endpoint);

                return await ParseResponseAsync(httpResponse);
            }, $"Error in API call to {endpoint}");
        }

        private HttpClient CreateHttpClient(string encodedValue)
        {
            var client = _factory.CreateClient("auth");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", encodedValue);
            return client;
        }

        private async Task HandleHttpResponseAsync(HttpResponseMessage response, string endpoint)
        {
            await _exceptionHandler.HandleHttpResponseAsync(response, $"Error in API call to {endpoint}");
        }

        private async Task<Response> ParseResponseAsync(HttpResponseMessage response)
        {
            var res = await response.Content.ReadFromJsonAsync<Response>();
            return res ?? new Response();
        }
    }
}