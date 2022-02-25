using EventsWeb.Shared.Model;

namespace EventsWeb.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<EventsSignUpResponse> RegisterUser(EventsSignUpRequest signUpRequest);
        Task<EventsSignInResponse> Login(EventsSignInRequest signOutRequest);
        Task Logout();
    }
}
