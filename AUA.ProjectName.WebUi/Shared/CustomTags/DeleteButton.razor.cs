using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AUA.ProjectName.WebUi.Shared.CustomTags
{
    public partial class DeleteButton
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }
        [Parameter] public Func<Task> DeleteMethod { get; set; }

        [JSInvokable]
        public async Task CheckConfirmAsync(bool isConfirmed)
        {
            if (isConfirmed)
                await DeleteMethod();
        }
    }
}
