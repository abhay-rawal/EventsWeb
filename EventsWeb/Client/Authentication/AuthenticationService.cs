using System.Text;
using Blazored.LocalStorage;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EventsWeb.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        public AuthenticationService(HttpClient http,
                                     ILocalStorageService localStorage,
                                     AuthenticationStateProvider authStateProvider)
        {
            _client = http;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<EventsSignInResponse> Login(EventsSignInRequest signInRequest)
        {
            //Convert to json
            var content = JsonConvert.SerializeObject(signInRequest);

            //Convert to string with encodingUTF8 type application/Json
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            //Call signin api Endpoint with signinRequest
            var response = await _client.PostAsync("api/account/signin", bodyContent);

            //Deserialize The EventsSignInResoponse that we get from SigninApi Endpoint
            var contentTemp = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<EventsSignInResponse>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(StaticData.Local_Token, result.Token);
                await _localStorage.SetItemAsync(StaticData.Local_UserDetail, result.UserDTO);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new EventsSignInResponse()
                {
                    IsAuthSuccessful = true
                };
            }
            else
            {
                return result;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(StaticData.Local_Token);
            await _localStorage.RemoveItemAsync(StaticData.Local_UserDetail);
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<EventsSignUpResponse> RegisterUser(EventsSignUpRequest signUpRequest)
        {
            //Convert to json
            var content = JsonConvert.SerializeObject(signUpRequest);

            //Convert to string with encodingUTF8 type application/Json
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            //Call signin api Endpoint with signinRequest
            var response = await _client.PostAsync("api/account/signup", bodyContent);

            //Deserialize The EventsSignInResoponse that we get from SigninApi Endpoint
            var contentTemp = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<EventsSignUpResponse>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new EventsSignUpResponse()
                {
                    IsRegisterationSuccessful = true
                };
            }
            else
            {
                return new EventsSignUpResponse()
                {
                    IsRegisterationSuccessful = false, Errors = result.Errors
                };
            }
        }
    }
}
