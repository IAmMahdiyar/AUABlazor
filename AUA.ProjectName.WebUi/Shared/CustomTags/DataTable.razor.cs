using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Shared.CustomTags
{
    public partial class DataTable
    {
        [Parameter] public RenderFragment Head { get; set; }
        [Parameter] public RenderFragment Body { get; set; }
        [Inject] public IJSRuntime JsRuntime { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JsRuntime.DataTableInit();
        }
    }
}
