namespace GateEntryExit_MVC.Services
{
    public interface IHttpClientService
    {
        Task<T> GetAllAsync<T>(T model, object postData, string endpoint);
    }
}
