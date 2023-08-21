using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Shared.CustomTags
{
    public partial class UpdateButton
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Parameter] public Func<Task> UpdateMethod { get; set; }
        [Parameter] public string ButtonText { get; set; }
        [Parameter] public string Class { get; set; }

        [JSInvokable]
        public async Task CheckConfirmAsync(bool isConfirmed)
        {
            if (isConfirmed)
                await UpdateMethod();
        }
    }
}
