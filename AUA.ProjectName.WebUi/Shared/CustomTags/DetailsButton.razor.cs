using AUA.ProjectName.WebUi.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Shared.CustomTags
{
    public partial class DetailsButton
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }

        [Parameter] public Dictionary<string, object> Details { get; set; }

        [Parameter] public string Class { get; set; } = "btn btn-primary";
        private async Task ShowDetailsAsync()
        {
            await JsRuntime.ShowDetails(Details);
        }
    }
}
