using EventsWeb.Client.Authentication;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace EventsWeb.Client.Pages.Authentication
{
    public partial class Login
    {
        private EventsSignInRequest SigninRequest = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowLoginErrors { get; set; }
        public string Errors { get; set; }
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navManager { get; set; }

        private async Task LoginUser()
        {
            ShowLoginErrors = false;
            IsProcessing = true;
            var result = await _authService.Login(SigninRequest);
            if (result.IsAuthSuccessful)
            {
                //registration is successful
                _navManager.NavigateTo("/category", forceLoad: true);
            }
            else
            {
                Errors = result.ErrorMessage;
                ShowLoginErrors = true;
            }
            IsProcessing = false;
        }
    }
}
