using dot8code.Tests.FakeHttpMessageHandler;
using Microsoft.Extensions.Configuration;
using Moq;
using Services.Interfaces;
using Services.Models;
using Services.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests
{
    public class AuthRequestTests
    {
        private readonly Mock<IHttpClientFactory> _mockFactory;
        private readonly Mock<IAsyncExceptionHandler> _mockExceptionHandler;
        private readonly AuthRequest _authRequest;
        private readonly string _baseAddress;

        public AuthRequestTests()
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\"));
            var configFilePath = Path.Combine(projectRoot, "Francisco_Iturburu_Daux_Challenge", "bin", "Debug", "net8.0", "appsettings.json");

            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException($"Configuration file not found at: {configFilePath}", configFilePath);
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
                .Build();

            _baseAddress = config["ApiBaseUrl"];
            if (string.IsNullOrEmpty(_baseAddress))
            {
                throw new InvalidOperationException("ApiBaseUrl is not set in the appsettings.json");
            }

            _mockFactory = new Mock<IHttpClientFactory>();
            _mockExceptionHandler = new Mock<IAsyncExceptionHandler>();
            _authRequest = new AuthRequest(_mockFactory.Object, _mockExceptionHandler.Object);
        }

        [Fact]
        public async Task Auth_ShouldReturnResponse_WhenApiCallIsSuccessful()
        {
            var request = new Request();
            var expectedResponse = new Response
            {
                result = "OK"
            };

            var mockedMessage = new FakeHttpMessageHandler<Response>(expectedResponse, HttpStatusCode.OK);
            var client = new HttpClient(mockedMessage)
            {
                BaseAddress = new Uri(_baseAddress)
            };

            _mockFactory.Setup(f => f.CreateClient("auth")).Returns(client);

            _mockExceptionHandler
                .Setup(e => e.CatchAsync(It.IsAny<Func<Task<Response>>>(), It.IsAny<string>()))
                .Returns<Func<Task<Response>>, string>((func, msg) => func());

            var result = await _authRequest.Auth(request);

            Assert.NotNull(result);
            Assert.Equal("OK", result.result);
        }

        [Fact]
        public async Task Auth_ShouldReturnErrorResponse_WhenHeaderAndBase64DoNotMatch()
        {
            var request = new Request();
            var expectedResponse = new Response
            {
                result = "HEADER AND BASE64 DO NOT MATCH"
            };

            var mockedMessage = new FakeHttpMessageHandler<Response>(expectedResponse, HttpStatusCode.BadRequest);
            var client = new HttpClient(mockedMessage)
            {
                BaseAddress = new Uri(_baseAddress)
            };

            _mockFactory.Setup(f => f.CreateClient("auth")).Returns(client);

            _mockExceptionHandler
                .Setup(e => e.CatchAsync(It.IsAny<Func<Task<Response>>>(), It.IsAny<string>()))
                .Returns<Func<Task<Response>>, string>((func, msg) => func());

            var result = await _authRequest.Auth(request);

            Assert.NotNull(result);
            Assert.Equal("HEADER AND BASE64 DO NOT MATCH", result.result);
        }

        [Fact]
        public async Task Auth_ShouldHandleException_WhenApiCallFails()
        {
            var request = new Request();

            _mockExceptionHandler
                .Setup(e => e.CatchAsync(It.IsAny<Func<Task<Response>>>(), It.IsAny<string>()))
                .ThrowsAsync(new HttpRequestException());

            Func<Task<Response>> func = async () => await _authRequest.Auth(request);

            await Assert.ThrowsAsync<HttpRequestException>(async () => await _mockExceptionHandler.Object.CatchAsync(func, "Test Error"));
        }
        [Fact]
        public async Task HandleHttpResponseAsync_ShouldCallExceptionHandler_WhenResponseIsNotSuccessful()
        {
            var endpoint = $"{_baseAddress}/test-encrypt";
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            _mockExceptionHandler
                .Setup(e => e.HandleHttpResponseAsync(It.IsAny<HttpResponseMessage>(), It.IsAny<string>()))
                .Verifiable();

            await _authRequest.HandleHttpResponseAsync(response, endpoint);

            _mockExceptionHandler.Verify(e => e.HandleHttpResponseAsync(response, $"Error in API call to {endpoint}"), Times.Once);
        }
    }
}
