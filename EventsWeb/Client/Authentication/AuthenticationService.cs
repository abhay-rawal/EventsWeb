using System.Text;
using Blazored.LocalStorage;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace EventsWeb.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        public AuthenticationService(HttpClient http, 
                                     ILocalStorageService localStorage, 
                                     AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<EventsSignInResponse> Login(EventsSignInRequest signInRequest)
        {
            //Convert to Json
            //var content = JsonConvert.SerializeObject(signInRequest);
            //Convert to string with encodingUTF8 type application/Json
            //var bodyContent = new StringContent(content, Encoding.UTF8,"application/json");
            //
            //var response = await _http.PostAsync("api/account/signin",bodyContent);
            return new EventsSignInResponse();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<EventsSignUpResponse> RegisterUser(EventsSignUpRequest signUpRequest)
        {
            throw new NotImplementedException();
        }
    }
}
