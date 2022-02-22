using Microsoft.JSInterop;

namespace EventsWeb.Client.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime _jsRuntime, string message)
        {
            await _jsRuntime.InvokeVoidAsync("showToastr","success",message);
        }
        public static async ValueTask ToastrError(this IJSRuntime _jSRuntime,string message)
        {
            await _jSRuntime.InvokeVoidAsync("showToastr","error",message);
        }
        public static async ValueTask<bool> ConfirmDelete(this IJSRuntime _jSRuntime)
        {
            return await _jSRuntime.InvokeAsync<bool>("showSweetAlert");
        }
    }
}
 