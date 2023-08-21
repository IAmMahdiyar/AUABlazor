using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using Microsoft.JSInterop;

namespace AUA.ProjectName.Services.GeneralService.Http.Contracts
{
    public interface IHttpService
    {
        Task<T> PostAsync<T>(string url, object data);
        Task<T> GetAsync<T>(string url);
        Task<T> PutAsync<T>(string url, object data);
        Task<T> PatchAsync<T>(string url, object data);
        Task<T> DeleteAsync<T>(string url);
        Task<bool> IsValidAsync<T>(IJSRuntime jsRuntime, ResultModel<T> resultModel);
        void SetAuthorizationToken(string token);
    }
}
