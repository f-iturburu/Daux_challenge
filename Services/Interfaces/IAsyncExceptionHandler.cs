using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAsyncExceptionHandler
    {
        Task<T> CatchAsync<T>(Func<Task<T>> action, string errorMessage);
        Task HandleHttpResponseAsync(HttpResponseMessage response, string errorMessage);
    }
}