using AUA.ProjectName.Common.Consts;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Utility
{
    public static class JsRuntimeExtensionMethods
    {
        public static async ValueTask SuccessMessage(this IJSRuntime jsRuntime, string Message)
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.SuccessMessage, Message);
        }

        public static async ValueTask ErrorMessage(this IJSRuntime jsRuntime, string Message)
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.ErrorMessage, Message);
        }

        public static async ValueTask StartLoading(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.StartLoading);
        }

        public static async ValueTask StopLoading(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.StopLoading);
        }

        public static async ValueTask DataTableInit(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.DataTableInit);
        }
        
        public static async ValueTask ShowDetails(this IJSRuntime jsRuntime, Dictionary<string, object> details)
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.ShowDetails, details);
        }

        public static async ValueTask ConfirmDelete(this IJSRuntime jsRuntime, object dotNetHelper)
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.ConfirmDelete, dotNetHelper);
        }

        public static async ValueTask ConfirmUpdate(this IJSRuntime jsRuntime, object dotNetHelper)
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.ConfirmUpdate, dotNetHelper);
        }

    }
}
