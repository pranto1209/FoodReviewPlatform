namespace FoodReviewPlatform.Utilities.ResourceManagers
{
    public interface IResourceManager
    {
        Task<T> GetResourceFromUrlAsync<T>(string url, string? token);
        Task<bool> PostResourceToUrlAsync<T>(string url, T body, string? token);
        Task<bool> PostResourceToUrlAsync<T>(string url, T body, string? token, string? origin);
        Task<RT> PostResourceToUrlAsync<T, RT>(string url, T body, string? token);
        Task<bool> PutResourceToUrlAsync<T>(string url, T body, string? token);
        Task<bool> DeleteResourceFromUrlAsync(string url, string? token);
    }
}
