namespace BlazorFrontend.Pages.Accounts;

using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

public class AuthMessageHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthMessageHandler(
        ILocalStorageService localStorage,
        AuthenticationStateProvider authStateProvider)
    {
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await _localStorage.RemoveItemAsync("authToken");

            if (_authStateProvider is CustomAuthStateProvider custom)
            {
                custom.NotifyUserLogout();
            }
        }

        return response;
    }
}

