using Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class AsyncExceptionHandler : IAsyncExceptionHandler
{
    public async Task<T> CatchAsync<T>(Func<Task<T>> action, string errorMessage)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{errorMessage}: {ex.Message}");
            throw;
        }
    }

    public async Task HandleHttpResponseAsync(HttpResponseMessage response, string errorMessage)
    {
        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var completeMessage = $"{errorMessage}. Response: {response.StatusCode} - {responseContent}";
            Console.WriteLine(completeMessage);
            throw new HttpRequestException(completeMessage);
        }
    }
}