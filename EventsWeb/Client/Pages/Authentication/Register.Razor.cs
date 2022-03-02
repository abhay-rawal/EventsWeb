using EventsWeb.Client.Authentication;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace EventsWeb.Client.Pages.Authentication
{
    public partial class Register
    {

        private EventsSignUpRequest SignupRequest = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Erros { get; set; }
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navManager { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await _authService.RegisterUser(SignupRequest);
            if (result.IsRegisterationSuccessful)
            {
                //registration is successful
                _navManager.NavigateTo("/login", forceLoad: true);
            }
            else
            {
                Erros = result.Errors;
                ShowRegistrationErrors = true;
            }
            IsProcessing = false;
        }
    }
}
