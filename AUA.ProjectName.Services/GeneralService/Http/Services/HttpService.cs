using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Services.GeneralService.Http.Contracts;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;

namespace AUA.ProjectName.Services.GeneralService.Http.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var response = await _httpClient.PostAsync(url, GetStringContent(data));

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);
        }

        private StringContent GetStringContent(object data)
        {
            var dataJson = JsonSerializer.Serialize(data);

            return new StringContent(dataJson, Encoding.UTF8, AppConsts.MediaType);
        }

        public async Task<T> PutAsync<T>(string url, object data)
        {
            var response = await _httpClient.PutAsync(url, GetStringContent(data));

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);
        }

        public async Task<T> PatchAsync<T>(string url, object data)
        {
            var response = await _httpClient.PatchAsync(url, GetStringContent(data));

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            var response = await _httpClient.SendAsync(request);

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);
        }
        
        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions);
        }

        public async Task<bool> IsValidAsync<T>(IJSRuntime jsRuntime, ResultModel<T> resultModel)
        {
            if (resultModel.IsSuccess == false)
            {
                var error = resultModel.Errors.First().ErrorMessage;


                if (error == EResultStatus.AccessTokenExpired.ToDescription()
                    || error == EResultStatus.InvalidToken.ToDescription())
                    
                    await jsRuntime.InvokeVoidAsync(JsFunctions.SignOut);

                else
                    await jsRuntime.InvokeVoidAsync(JsFunctions.ErrorMessage, resultModel.Errors.First().ErrorMessage);

            }

            return resultModel.IsSuccess;
        }

        public void SetAuthorizationToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(AppConsts.AuthorizationToken, token);
        }
    }
}
