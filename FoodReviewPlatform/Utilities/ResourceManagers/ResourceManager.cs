using RestSharp;
using System.Text.Json;

namespace FoodReviewPlatform.Utilities.ResourceManagers
{
    public class ResourceManager : IResourceManager
    {
        public async Task<T> GetResourceFromUrlAsync<T>(string url, string? token)
        {
            var client = new RestClient();

            var request = new RestRequest(url, Method.Get);

            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("authorization", token);
            }

            RestResponse<T> queryResult = await client.ExecuteGetAsync<T>(request);

            return JsonSerializer.Deserialize<T>(queryResult.Content);
        }

        public async Task<bool> PostResourceToUrlAsync<T>(string url, T body, string? token)
        {
            var client = new RestClient();

            var request = new RestRequest(url, Method.Post);

            request.AddBody(body);

            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("authorization", token);
            }

            RestResponse<T> queryResult = await client.ExecutePostAsync<T>(request);

            Console.WriteLine($"External Response: {JsonSerializer.Serialize(queryResult)}");

            return queryResult.IsSuccessStatusCode;
        }

        public async Task<bool> PostResourceToUrlAsync<T>(string url, T body, string? token, string? origin)
        {
            var client = new RestClient();

            var request = new RestRequest(url, Method.Post);

            request.AddBody(body);

            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("authorization", token);
            }

            if (!string.IsNullOrWhiteSpace(origin) && !string.IsNullOrWhiteSpace(origin))
            {
                request.AddHeader("origin", origin);
            }

            RestResponse<T> queryResult = await client.ExecutePostAsync<T>(request);

            Console.WriteLine($"External Response: {JsonSerializer.Serialize(queryResult)}");

            return queryResult.IsSuccessStatusCode;
        }

        public async Task<RT> PostResourceToUrlAsync<T, RT>(string url, T body, string? token)
        {
            var client = new RestClient();

            var request = new RestRequest(url, Method.Post);

            request.AddBody(body);

            Console.WriteLine($"External Request Payload: {JsonSerializer.Serialize(body)}");

            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("authorization", token);
            }

            RestResponse<RT> queryResult = await client.ExecutePostAsync<RT>(request);

            Console.WriteLine($"External Request Response: {JsonSerializer.Serialize(queryResult)}");

            return JsonSerializer.Deserialize<RT>(queryResult.Content);
        }

        public async Task<bool> PutResourceToUrlAsync<T>(string url, T body, string? token)
        {
            var client = new RestClient();

            var request = new RestRequest(url, Method.Put);

            request.AddBody(body);

            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("authorization", token);
            }

            Console.WriteLine($"External Request Payload: {JsonSerializer.Serialize(body)}");

            RestResponse queryResult = await client.ExecutePutAsync(request);

            Console.WriteLine($"External Response: {JsonSerializer.Serialize(queryResult)}");

            return queryResult.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteResourceFromUrlAsync(string url, string? token)
        {
            var client = new RestClient();

            var request = new RestRequest(url, Method.Delete);

            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("authorization", token);
            }

            RestResponse queryResult = await client.ExecuteAsync(request);

            return queryResult.IsSuccessStatusCode;
        }
    }
}