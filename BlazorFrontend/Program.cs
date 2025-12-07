using Blazored.LocalStorage;
using BlazorFrontend;
using BlazorFrontend.Pages.Accounts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddScoped(sp =>
//    new HttpClient { BaseAddress = new Uri("https://localhost:7204/") });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddTransient<AuthMessageHandler>();

builder.Services.AddHttpClient("AuthorizedClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7204/");
})
.AddHttpMessageHandler<AuthMessageHandler>();
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"));


await builder.Build().RunAsync();
