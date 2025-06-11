using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using FROASTERY.Models;
public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
private readonly ProtectedSessionStorage _sessionStorage;
    private const string SessionKey = "authUser";

    public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var result = await _sessionStorage.GetAsync<UserSession>(SessionKey);

            if (result.Success && result.Value is UserSession userSession)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userSession.UserName ?? "")
                }.Concat(userSession.Roles.Select(role => new Claim(ClaimTypes.Role, role))), "Custom");

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
        }
        catch { }

        // Nếu không có session -> anonymous user
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task MarkUserAsAuthenticated(string username, List<string> roles)
    {
        var session = new UserSession
        {
            UserName = username,
            Roles = roles
        };

        await _sessionStorage.SetAsync(SessionKey, session);

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username)
        }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role))), "Custom");

        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _sessionStorage.DeleteAsync(SessionKey);
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }

// private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

//     public override Task<AuthenticationState> GetAuthenticationStateAsync()
//     {
//         return Task.FromResult(new AuthenticationState(_currentUser));
//     }

//     public void MarkUserAsAuthenticated(ClaimsPrincipal claimsPrincipal)
//     {
//         _currentUser = claimsPrincipal;
//         NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
//     }

//     public void MarkUserAsLoggedOut()
//     {
//         _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
//         NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
//     }
}
