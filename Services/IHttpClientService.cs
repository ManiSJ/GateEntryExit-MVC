namespace GateEntryExit_MVC.Services
{
    public interface IHttpClientService
    {
        Task<T> GetAllAsync<T>(T model, object postData, string endpoint);

        Task<T> GetAsync<T>(T model, string endpoint);

        Task<T> CreateAsync<T>(T model, object postData, string endpoint);

        Task<T> EditAsync<T>(T model, object postData, string endpoint);

        Task DeleteAsync(string endpoint);

        Task PostAsync(object postData, string endpoint);
    }
}
