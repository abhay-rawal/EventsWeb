using Blazored.LocalStorage;
using EventsWeb.Client;
using EventsWeb.Client.Authentication;
using EventsWeb.Client.Category;
using EventsWeb.Client.FileUpload;
using EventsWeb.Client.Product;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Add Services to the Container
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

//Local storage for Token 
builder.Services.AddBlazoredLocalStorage();

//Telerik
builder.Services.AddTelerikBlazor();

//Add Authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


await builder.Build().RunAsync();
