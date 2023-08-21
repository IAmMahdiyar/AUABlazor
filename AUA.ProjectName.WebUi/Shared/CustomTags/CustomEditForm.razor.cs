using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Shared.CustomTags
{
    public partial class CustomEditForm
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }

        [Parameter] public Func<Task> SubmitMethod { get; set; }

        [Parameter] public RenderFragment Body { get; set; }
        private async Task SubmitAsync()
        {
            await JsRuntime.StartLoading();

            await SubmitMethod();

            await JsRuntime.StopLoading();
        }
    }

}
