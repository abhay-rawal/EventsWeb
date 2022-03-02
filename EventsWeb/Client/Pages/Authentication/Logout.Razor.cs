using EventsWeb.Client.Authentication;
using Microsoft.AspNetCore.Components;

namespace EventsWeb.Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _authService { get; set; }
        [Inject]
        public NavigationManager _navManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await _authService.Logout();
            _navManager.NavigateTo("/",forceLoad:true);
        }
    }
}
